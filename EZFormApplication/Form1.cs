namespace EZFormApplication
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Windows.Forms;
    using EZ_B;
    using Microsoft.ProjectOxford.SpeakerRecognition.Contract.Identification;
    using System.Threading.Tasks;
    using EZFormApplication.CognitiveServicesCommunicators;
    using Microsoft.CognitiveServices.SpeechRecognition;
    using Newtonsoft.Json.Linq;
    using static EZFormApplication.RobotSettings;
    using System.Drawing.Imaging;

    public partial class Form1 : Form
    {
        public event EventHandler ToggleFaceRecognitionEvent;
        private List<FaceResult> personResults = new List<FaceResult>();
        private DateTime lastFaceDetectTime = DateTime.MinValue;

        //Robot Positions
        private WavePositions wavePosition;
        private GrabPositions grabPosition;
        private SquatGrabPositions squatGrabPosition;
        private LeftPositions leftPosition;
        private RightPositions rightPosition;
        private ForwardPositions forwardPosition;
        private ReversePositions reversePosition;
        private LeftHandPositions leftHandPosition;


        private bool isClosing;
        private EZB ezb;
        private EventWaitHandle ezbConnectionStatusChangedWaitHandle;
        private Camera camera;
        private long fpsCounter;
        private bool headTrackingActive;

        private static bool isRecording = false;
        private System.Timers.Timer timer1 = new System.Timers.Timer();

        public void WriteDebug(object obj, bool clear = false)
        {
            if (this.isClosing)
            {
                return;
            }

            string text = obj.ToString();
            this.DebugTextBox.Invoke(new EventHandler(delegate
            {
                if (clear)
                {
                    this.DebugTextBox.Text = string.Empty;
                }
                this.DebugTextBox.Text = this.DebugTextBox.Text + DateTime.Now.ToString("HH:mm:ss.fff", CultureInfo.InvariantCulture) + ">" + text + "\r\n";

                this.DebugTextBox.SelectionLength = 0;
                this.DebugTextBox.SelectionStart = this.DebugTextBox.Text.Length;
                this.DebugTextBox.ScrollToCaret();
            }));
        }

        public Form1()
        {
            this.InitializeComponent();
            //this.ImageFileNameTB.Text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "capture-" + DateTime.Now.ToString("yyyyxMMxddxHHxmmxss") + ".jpg");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.ezb = new EZB();
            this.camera = new Camera(this.ezb);
            this.camera.OnStart += this.CameraOnOnStart;
            this.camera.OnNewFrame += this.CameraOnOnNewFrame;
            this.camera.OnStop += this.CameraOnStop;

            this.ezb.OnConnectionChange += this.EzbOnConnectionChange;

            //We leave out this and use Cognitive Services for speech recognition
            //ezb.SpeechSynth.OnPhraseRecognized += SpeechSynth_OnPhraseRecognized;
            //ezb.SpeechSynth.SetDictionaryOfPhrases((new string[] { "Track face", "Are you thirsty", "where is screwdriver", "what do you see" }));

            //create event object to avoid sleep/poll check
            this.ezbConnectionStatusChangedWaitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);

            //create event for ezb connection status changes
            this.ezbConnectionStatusChangedWaitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);

            //activate forms timer
            this.OneSecondTimer.Enabled = true;
            this.OneSecondTimer.Start();

            //Load Settings
            IpAddressTB.Text = Settings.Instance.IPAdress;
            EmotionApiKey.Text = Settings.Instance.EmotionApiKey;
            FaceApiKey.Text = Settings.Instance.FaceApiKey;
            SpeakerRecognitionApiKey.Text = Settings.Instance.SpeakerRecognitionApiKeyValue;
            LuisEndpointTextBox.Text = Settings.Instance.LuisEndpoint;
            SpeechApiKeyTextBox.Text = Settings.Instance.SpeechRecognitionApiKey;
            CustomVisionApiKey_Textbox.Text = Settings.Instance.VisionApiKey;
            CustomVisionPredictionKey_TextBox.Text = Settings.Instance.PredictionKey;
            CustomVisionProjectID_Textbox.Text = Settings.Instance.VisionApiProjectId;
            CustomVisionIterationId_Textbox.Text = Settings.Instance.VisionApiIterationId;
       
            //Face recognition
            ToggleFaceRecognitionEvent += new EventHandler(ToggleFaceRecognition);
        }


        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.isClosing = true;
            //clean up
            if (this.camera != null)
            {
                if (this.camera.IsActive)
                {
                    this.camera.StopCamera();
                }
                this.camera.Dispose();
            }
            if (this.ezb != null)
            {
                if (this.ezb.IsConnected)
                {
                    this.ezb.Disconnect();
                }
                this.ezb.Dispose();
            }
        }

        delegate void SetPropCallback(string text);

        private void ToggleFaceRecognition(object sender, EventArgs e)
        {
            //to avoid concurrency issues
            try
            {
                if (!camera.IsActive)
                    return;
            }
            catch (Exception ex)
            {
                this.WriteDebug("Error Exception=" + ex);
            }

            if (this.StartCameraButton.InvokeRequired)
            {
                BeginInvoke((Action)(() =>
                {
                    this.StartCameraButton.Enabled = false;
                }));
            }
            else
            {
                this.StartCameraButton.Enabled = false;
            }
            

            var button = HeadTrackingButton;
            var labels = button.Tag.ToString().Split(new[] { '|' });

            if (this.headTrackingActive)
            {
                this.headTrackingActive = false;
                this.WriteDebug("Stopping Head Tracking.");

                if (button.InvokeRequired)
                {
                    BeginInvoke((Action)(() =>
                    {
                        button.Text = labels[0];
                    }));
                }
                else
                {
                    button.Text = labels[0];
                }
            }
            else
            {
                if (!this.camera.IsActive)
                {
                    this.WriteDebug("Error: Please start the camera.");
                }
                else if (!this.ezb.IsConnected)
                {
                    this.WriteDebug("Error: Please connect the EZB.");
                }
                else
                {
                    //Note: assign servo values before start tracking
                    this.ezb.Servo.SetServoPosition(HeadServoHorizontalPort, mapPortToServoLimits[HeadServoHorizontalPort].CenterPosition);
                    this.ezb.Servo.SetServoPosition(HeadServoVerticalPort, mapPortToServoLimits[HeadServoVerticalPort].MinPosition + 15);

                    this.headTrackingActive = true;
                    this.WriteDebug("Starting Head Tracking.");

                    if (button.InvokeRequired)
                    {
                        BeginInvoke((Action)(() =>
                        {
                            button.Text = labels[1];
                        }));
                    }
                    else
                    {
                        button.Text = labels[1];
                    }
                    
                }
            }
        }


        /// <summary>
        /// Custom EZRobot speech recognition,
        /// We leave out this, due to low precission of native ezrobot speech recognition support
        /// </summary>
        /// <param name="text"></param>
        /// <param name="confidence"></param>
        //private async void SpeechSynth_OnPhraseRecognized(string text, float confidence)
        //{
        //    switch (text)
        //    {
        //        case "track face":
        //            {
        //                ToggleFaceRecognitionEvent?.Invoke(this, null);
        //                break;

        //            }

        //        case "what do you see":
        //            {
        //                await Task.Delay(1000);
        //                var currentBitmap = camera.GetCurrentBitmap;
        //                currentBitmap.Save(Guid.NewGuid().ToString() + ".jpg", ImageFormat.Jpeg);
        //                var description = await CustomVisionCommunicator.RecognizeObjectsInImage(currentBitmap);
        //                ezb.SoundV4.PlayData(ezb.SpeechSynth.SayToStream(description));
        //                break;
        //            }
        //        case "where is screwdriver":
        //            {
        //                We are trying to find screwdriver, first in front, then on the right, last on the left
        //                ezb.SoundV4.PlayData(ezb.SpeechSynth.SayToStream("I am on it"));
        //                this.ezb.Servo.SetServoPosition(HeadServoHorizontalPort, this.mapPortToServoLimits[HeadServoHorizontalPort].CenterPosition);
        //                this.ezb.Servo.SetServoPosition(HeadServoVerticalPort, this.mapPortToServoLimits[HeadServoVerticalPort].MinPosition + 55);
        //                await Task.Delay(1000);
        //                if (RecognizeObject("screwdriver"))
        //                {
        //                    Forward();
        //                    squatGrabPosition.StartAction_Grab();
        //                    Reverse();
        //                    //Grab

        //                    ezb.SoundV4.PlayData(ezb.SpeechSynth.SayToStream("Screwdriver is in front of me"));
        //                    return;
        //                }
        //                this.ezb.Servo.SetServoPosition(HeadServoHorizontalPort, this.mapPortToServoLimits[HeadServoHorizontalPort].CenterPosition + 45);
        //                await Task.Delay(1000);
        //                if (RecognizeObject("screwdriver"))
        //                {
        //                    TurnRight();
        //                    Forward();
        //                    squatGrabPosition.StartAction_Grab();
        //                    Reverse();
        //                    TurnLeft();
        //                    ezb.SoundV4.PlayData(ezb.SpeechSynth.SayToStream("Screwdriver is on the left side of me."));
        //                    return;
        //                }

        //                this.ezb.Servo.SetServoPosition(HeadServoHorizontalPort, this.mapPortToServoLimits[HeadServoHorizontalPort].CenterPosition - 45);
        //                await Task.Delay(1000);
        //                if (RecognizeObject("screwdriver"))
        //                {
        //                    TurnLeft();
        //                    Forward();
        //                    squatGrabPosition.StartAction_Grab();
        //                    Reverse();
        //                    TurnRight();
        //                    ezb.SoundV4.PlayData(ezb.SpeechSynth.SayToStream("Screwdriver is on the right side of me."));

        //                    return;
        //                }

        //                break;
        //            }
        //        case "are you thirsty":
        //            {

        //                ezb.SoundV4.PlayData(ezb.SpeechSynth.SayToStream(("Yes I am always hungry! I would have an oil!")));
        //                var currentBitmap = camera.GetCurrentBitmap;
        //                currentBitmap.Save(Guid.NewGuid().ToString() + ".jpg", ImageFormat.Jpeg);
        //                var predictions = CustomVisionCommunicator.RecognizeObject(currentBitmap);
        //                if (RecognizeObject("oil"))
        //                {
        //                    grabPosition.StartAction_Takefood();
        //                    await Task.Delay(1000);
        //                    ezb.SoundV4.PlayData(ezb.SpeechSynth.SayToStream(("Hmm oil")));
        //                }
        //                else
        //                    ezb.SpeechSynth.Say("I do not eat this");
        //                break;
        //            }
        //        default: break;

        //    }

        //}

        private bool RecognizeObject(string objectName)
        {
            var currentBitmap = camera.GetCurrentBitmap;

            //Used when creating training dataset
            //currentBitmap.Save(Guid.NewGuid().ToString() + ".jpg", ImageFormat.Jpeg);

            var cvc = new CustomVisionCommunicator();
            var predictions = cvc.RecognizeObject(currentBitmap);
            if (predictions.Where(p => p.Tag == objectName).First().Probability > 0.7)
                return true;
            else
                return false;
        }


        private void StartOrStopVideoRecordingButton_Click(object sender, EventArgs e)
        {
            if (!this.camera.IsActive)
            {
                this.WriteDebug("ERROR: Camera not started");
                return;
            }

            //Note: .Tag = "Start Video Recording|Stop Video Recording
            var labels = this.StartOrStopVideoRecordingButton.Tag.ToString().Split(new[] { '|' });

            if (!this.camera.AVIIsRecording)
            {
                var filename = "video.avi";
                this.WriteDebug("AVI Start recording to file=" + filename + " .");

                const int framesPerSecond = 3;
                this.camera.AVIStartRecording(filename, Camera.VideoCodec.MPEG4, framesPerSecond);

                this.StartOrStopVideoRecordingButton.Text = labels[1];
            }
            else
            {
                this.WriteDebug("AVI Stop recording");

                this.camera.AVIStopRecording();

                this.StartOrStopVideoRecordingButton.Text = labels[0];
            }
        }

        private void StartCameraButton_Click(object sender, EventArgs e)
        {
            //to avoid concurrency issues
            this.StartCameraButton.Enabled = false;

            try
            {
               StartOrStopCamera();
            }
            catch (Exception ex)
            {
                this.WriteDebug("Error Exception=" + ex);
            }

            this.StartCameraButton.Enabled = true;
        }


        private void CameraOnOnStart()
        {
            this.WriteDebug("Camera Started!");
        }

        private void EzbOnConnectionChange(bool isConnected)
        {
            this.ezbConnectionStatusChangedWaitHandle.Set();
            this.ChangeEzbConnectButtonText();

            if (isConnected)
            {
                wavePosition = new WavePositions(ezb);
                grabPosition = new GrabPositions(ezb);
                squatGrabPosition = new SquatGrabPositions(ezb);
                leftPosition = new LeftPositions(ezb);
                rightPosition = new RightPositions(ezb);
                forwardPosition = new ForwardPositions(ezb);
                reversePosition = new ReversePositions(ezb);
                leftHandPosition = new LeftHandPositions(ezb);

                this.WriteDebug("Connected to EZB");

                //Listening
                //Not usable in noisy environments
                //ezb.SpeechSynth.OnAudioLevelChanged += SpeechSynth_OnAudioLevelChanged;
            }
            else
            {
                this.WriteDebug("Disconnected from EZB");
                this.headTrackingActive = false;
            }
        }

        private void SpeechSynth_OnAudioLevelChanged(int level)
        {
            throw new NotImplementedException();
        }

        private void CameraOnOnNewFrame()
        {
            this.ezbConnectionStatusChangedWaitHandle.Set();
            this.pictureBox1.Invoke(new EventHandler(delegate { this.pictureBox1.Image = this.camera.GetCurrentBitmap; }));
            Interlocked.Increment(ref this.fpsCounter);          

            this.HeadTracking();
        }


        private void CameraOnStop()
        {
            this.WriteDebug("Camera Stopped!");
            this.ezbConnectionStatusChangedWaitHandle.Reset();
        }

        private void OneSecondTimer_Tick(object sender, EventArgs e)
        {
            var cnt = Interlocked.Read(ref this.fpsCounter);
            Interlocked.Exchange(ref this.fpsCounter, 0);
           
            this.FpsLabel.Text = ezb.SpeechSynth.AudioLevel.ToString();
            this.fpsLabelLabel.Visible = this.FpsLabel.Visible = cnt > 0;

            //Not using EZ's listening and speech recognition
            //if (ezb.IsConnected && !ezb.SpeechSynth.IsListening)
            //{
            //    ezb.SpeechSynth.StartListening();
            //}
        }


        private bool StartOrStopCamera()
        {
            var labels = this.StartCameraButton.Tag.ToString().Split(new[] { '|' });

            if (this.camera.IsActive)
            {
                this.WriteDebug("Stopping camera.");
                this.camera.StopCamera();
                this.StartCameraButton.Text = labels[0];
                return false;
            }
            else
            {
                this.WriteDebug("Starting camera.");
                this.ezbConnectionStatusChangedWaitHandle.Reset();
                this.camera.StartCamera(new ValuePair("EZB://" + this.IpAddressTB.Text), CameraWidth, CameraHeight);
                this.StartCameraButton.Text = labels[1];
                this.camera.SetPreviewControl = this.Video2P;
                return true;
            }
        }

        private void ChangeEzbConnectButtonText()
        {
            this.StartEZBButton.Invoke(new EventHandler(delegate
            {
                var labels = this.StartEZBButton.Tag.ToString().Split(new[] { '|' });
                this.StartEZBButton.Text = this.ezb.IsConnected ? labels[0] : labels[1];
            }));
        }

        private void StartEZBButton_Click(object sender, EventArgs e)
        {
            if (this.ezb.IsConnected)
            {
                this.ezb.Disconnect();
            }
            else
            {
                this.ezbConnectionStatusChangedWaitHandle.Reset();
                this.ezb.Connect(this.IpAddressTB.Text);
            }
        }

        private void HeadTrackingButton_Click(object sender, EventArgs e)
        {
            //this.ezb.Servo.SetServoPosition(HeadServoVerticalPort, mapPortToServoLimits[HeadServoVerticalPort].MinPosition + 55);

            //Storing images for training dataset
            var currentBitmap = camera.GetCurrentBitmap;
            currentBitmap.Save(Guid.NewGuid().ToString() + ".jpg", ImageFormat.Jpeg);

           // ToggleFaceRecognitionEvent?.Invoke(this, null);
        }

        private async void HeadTracking()
        {

            if (!this.headTrackingActive)
            {
                return;
            }

            var faceLocations = this.camera.CameraFaceDetection.GetFaceDetection(32, 1000, 1);
            if (faceLocations.Length > 0)
            {
                //DO Local Face detection, only once per second
                if (this.fpsCounter == 1)
                {
                    foreach (var objectLocation in faceLocations)
                    {
                        this.WriteDebug(string.Format("Face detected at H:{0} V:{1}", objectLocation.HorizontalLocation, objectLocation.VerticalLocation));
                    }
                }
            }

            //If no face return
            if (faceLocations.Length == 0)
            {
                return;
            }

            //Grab the first face location (ONLY ONE)
            var faceLocation = faceLocations.First();

            var servoVerticalPosition = this.ezb.Servo.GetServoPosition(HeadServoVerticalPort);
            var servoHorizontalPosition = this.ezb.Servo.GetServoPosition(HeadServoHorizontalPort);

            //Track face
            var yDiff = faceLocation.CenterY - CameraHeight / 2;
            if (Math.Abs(yDiff) > YDiffMargin)
            {
                if (yDiff < -1 * RobotSettings.sensitivity)
                {
                    if (servoVerticalPosition - ServoStepValue >= mapPortToServoLimits[HeadServoVerticalPort].MinPosition)
                    {
                        servoVerticalPosition -= ServoStepValue;
                    }
                }
                else if (yDiff > RobotSettings.sensitivity)
                {
                    if (servoVerticalPosition + ServoStepValue <= mapPortToServoLimits[HeadServoVerticalPort].MaxPosition)
                    {
                        servoVerticalPosition += ServoStepValue;
                    }
                }
            }

            var xDiff = faceLocation.CenterX - CameraWidth / 2;
            if (Math.Abs(xDiff) > XDiffMargin)
            {
                if (xDiff > RobotSettings.sensitivity)
                {
                    if (servoHorizontalPosition - ServoStepValue >= mapPortToServoLimits[HeadServoHorizontalPort].MinPosition)
                    {
                        servoHorizontalPosition -= ServoStepValue;
                    }
                }
                else if (xDiff < -1 * RobotSettings.sensitivity)
                {
                    if (servoHorizontalPosition + ServoStepValue <= mapPortToServoLimits[HeadServoHorizontalPort].MaxPosition)
                    {
                        servoHorizontalPosition += ServoStepValue;
                    }
                }
            }

            this.ezb.Servo.SetServoPosition(HeadServoVerticalPort, servoVerticalPosition);
            this.ezb.Servo.SetServoPosition(HeadServoHorizontalPort, servoHorizontalPosition);

            //FACE Detection
            //Face recognition thru api
            var currentBitmap = camera.GetCurrentBitmap;

            (var faces, var person, var emotions) = await FaceApiCommunicator.DetectAndIdentifyFace(currentBitmap);

            //If Face API identified person and if ezrobot is not speaking
            if (person != null && !ezb.SoundV4.IsPlaying)
            {
                //If identified person seems to be sad
                if (emotions[0].Scores.Sadness > 0.02)
                {
                    ezb.SoundV4.PlayData(ezb.SpeechSynth.SayToStream("You look bit sad, but I have something to cheer you up. A joke! Here it is: My dog used to chase people on a bike a lot. It got so bad, finally I had to take his bike away. Ha Ha Ha"));
                    //Wait for robot to finish speaking
                    Thread.Sleep(25000);
                }
                else
                {
                    ezb.SoundV4.PlayData(ezb.SpeechSynth.SayToStream("Hello " + person.Name));
                    Wave();
                }             
            }
            //if no identified person but detected faces
            else if (faces != null && faces.Any() && !ezb.SoundV4.IsPlaying)
            {
                if (faces[0].FaceAttributes.Gender == "male")
                    ezb.SoundV4.PlayData(ezb.SpeechSynth.SayToStream("Hello mister stranger your age is probably " + faces[0].FaceAttributes.Age));
                else
                    ezb.SoundV4.PlayData(ezb.SpeechSynth.SayToStream("Hello misses stranger your age is probably " + faces[0].FaceAttributes.Age));
                Wave();
            }
        }


        #region positions
        private async void Wave()
        {
            wavePosition.StartAction_Wave();
            await Task.Delay(5000);
            wavePosition.Stop();
            ezb.Servo.ReleaseAllServos();
        }

        private async void TurnLeft()
        {
            leftPosition.StartAction_Left();
            await Task.Delay(5000);
            leftPosition.Stop();
            ezb.Servo.ReleaseAllServos();
        }

        private async void TurnRight()
        {
            rightPosition.StartAction_Right();
            await Task.Delay(5000);
            rightPosition.Stop();
            ezb.Servo.ReleaseAllServos();
        }

        private async void Forward()
        {
            forwardPosition.StartAction_Forward();
            await Task.Delay(5000);
            forwardPosition.Stop();
            ezb.Servo.ReleaseAllServos();
        }

        private async void Reverse()
        {
            reversePosition.StartAction_Reverse();
            await Task.Delay(5000);
            reversePosition.Stop();
            ezb.Servo.ReleaseAllServos();
        }

        #endregion

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            RobotSettings.sensitivity = trackBar1.Value;
        }

        private async void ListenButton_Click(object sender, EventArgs e)
        {
            var vr = new WavRecording();

            if (!isRecording)
            {
                var r = vr.StartRecording();
                //if succesfull
                if (r == "1")
                {
                    isRecording = true;
                    ListenButton.Text = "Stop listening";
                }
                else
                    WriteDebug(r);
            }
            else
            {
                var r = vr.StopRecording();
                if (r == "1")
                    try
                    {
                        var sr = new SpeakerRecognitionCommunicator();
                        var identificationResponse = await sr.RecognizeSpeaker("result.wav");

                        WriteDebug("Identification Done.");
                        wavePosition.StartAction_Wave();

                        var name = Speakers.ListOfSpeakers.Where(s => s.ProfileId == identificationResponse.ProcessingResult.IdentifiedProfileId.ToString()).First().Name;
                        ezb.SoundV4.PlayData(ezb.SpeechSynth.SayToStream("Hello " + name));

                        await Task.Delay(5000);
                        wavePosition.Stop();
                        ezb.Servo.ReleaseAllServos();
                    }
                    catch (IdentificationException ex)
                    {
                        WriteDebug("Speaker Identification Error: " + ex.Message);
                        wavePosition.StartAction_Wave();

                        ezb.SoundV4.PlayData(ezb.SpeechSynth.SayToStream("Hello stranger"));

                        //Wait for robot to finish speaking
                        await Task.Delay(5000);
                        wavePosition.Stop();
                        ezb.Servo.ReleaseAllServos();
                    }
                    catch (Exception ex)
                    {
                        WriteDebug("Error: " + ex.Message);
                    }
                else
                    WriteDebug(r);

                isRecording = false;
                ListenButton.Text = "Recognize Voice";

            }
        }


        private void ReleaseServosButton_Click(object sender, EventArgs e)
        {
            ezb.Servo.ReleaseAllServos();
        }

        ///Introduction
        private async void button1_Click(object sender, EventArgs e)
        {
            ezb.SoundV4.PlayData(ezb.SpeechSynth.SayToStream("Hello I am EZ Robot and today together with my assistant Marek, we are going to show you my artificial intelligence."));
            await Task.Delay(3000);
            leftHandPosition.StartAction_Lefthandup();
            await Task.Delay(4000);
            leftHandPosition.StartAction_Lefthanddown();
        }


        //Objection
        private async void button2_Click(object sender, EventArgs e)
        {
            ezb.SoundV4.PlayData(ezb.SpeechSynth.SayToStream("Heey, what do you mean by that?"));
            await Task.Delay(4000);
        }


        #region CommandRecognition
        //Speech Recognition
        /// <summary>
        /// The microphone client
        /// </summary>
        private MicrophoneRecognitionClient micClient;

        private DataRecognitionClient dataClient;

        /// <summary>
        /// Gets the default locale.
        /// </summary>
        /// <value>
        /// The default locale.
        /// </value>
        private string DefaultLocale
        {
            get { return "en-US"; }
        }



        /// <summary>
        /// Called when a final response is received and its intent is parsed
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SpeechIntentEventArgs"/> instance containing the event data.</param>
        private async void OnIntentHandler(object sender, SpeechIntentEventArgs e)
        {

            WriteDebug("--- Intent received by OnIntentHandler() ---");
            WriteDebug(e.Payload);
            dynamic intenIdentificationResult = JObject.Parse(e.Payload);
            var res = intenIdentificationResult["topScoringIntent"];
            var intent = Convert.ToString(res["intent"]);

            switch (intent)
            {
                case "TrackFace":
                    {
                        ToggleFaceRecognitionEvent?.Invoke(this, null);
                        break;

                    }

                case "ComputerVision":
                    {
                        var currentBitmap = camera.GetCurrentBitmap;
                        var cvc = new ComputerVisionCommunicator();
                        var description = await cvc.RecognizeObjectsInImage(currentBitmap);
                        ezb.SoundV4.PlayData(ezb.SpeechSynth.SayToStream(description));
                        break;
                    }
                case "FindScrewdriver":
                    {
                        //We are trying to find screwdriver, first in front, then on the right, last on the left 
                        ezb.SoundV4.PlayData(ezb.SpeechSynth.SayToStream("I am on it"));
                        this.ezb.Servo.SetServoPosition(HeadServoHorizontalPort, mapPortToServoLimits[HeadServoHorizontalPort].CenterPosition);
                        this.ezb.Servo.SetServoPosition(HeadServoVerticalPort, mapPortToServoLimits[HeadServoVerticalPort].MinPosition + 55);
                        await Task.Delay(1000);
                        if (RecognizeObject("screwdriver"))
                        {
                            //Forward();
                            //squatGrabPosition.StartAction_Grab();
                            //Reverse();
                            ////Grab

                            ezb.SoundV4.PlayData(ezb.SpeechSynth.SayToStream("Screwdriver is in front of me"));
                            return;
                        }
                        this.ezb.Servo.SetServoPosition(HeadServoHorizontalPort, mapPortToServoLimits[HeadServoHorizontalPort].CenterPosition + 45);
                        await Task.Delay(1000);
                        if (RecognizeObject("screwdriver"))
                        {
                            //TurnRight();
                            //Forward();
                            //squatGrabPosition.StartAction_Grab();
                            //Reverse();
                            //TurnLeft();
                            ezb.SoundV4.PlayData(ezb.SpeechSynth.SayToStream("Screwdriver is on my left side"));
                            return;
                        }

                        this.ezb.Servo.SetServoPosition(HeadServoHorizontalPort, mapPortToServoLimits[HeadServoHorizontalPort].CenterPosition - 45);
                        await Task.Delay(1000);
                        if (RecognizeObject("screwdriver"))
                        {
                            //TurnLeft();
                            //Forward();
                            //squatGrabPosition.StartAction_Grab();
                            //Reverse();
                            //TurnRight();
                            ezb.SoundV4.PlayData(ezb.SpeechSynth.SayToStream("Screwdriver is on my right side."));

                            return;
                        }

                        break;
                    }
                case "Hungry":
                    {

                        ezb.SoundV4.PlayData(ezb.SpeechSynth.SayToStream(("Yes I am always hungry! I would have an oil!")));
                        var currentBitmap = camera.GetCurrentBitmap;

                        var cvc = new CustomVisionCommunicator();
                        var predictions = cvc.RecognizeObject(currentBitmap);
                        if (RecognizeObject("oil"))
                        {
                            grabPosition.StartAction_Takefood();
                            await Task.Delay(1000);
                            ezb.SoundV4.PlayData(ezb.SpeechSynth.SayToStream("Hmm oil"));
                        }
                        else
                            ezb.SoundV4.PlayData(ezb.SpeechSynth.SayToStream("I do not eat this"));
                        break;
                    }
                default: break;

            }
        }

        /// <summary>
        /// Called when the microphone status has changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MicrophoneEventArgs"/> instance containing the event data.</param>
        private void OnMicrophoneStatus(object sender, MicrophoneEventArgs e)
        {

            BeginInvoke((Action)(() =>
            {
                WriteDebug("--- Microphone status change received by OnMicrophoneStatus() ---");
                WriteDebug(String.Format("********* Microphone status: {0} *********", e.Recording));
                if (e.Recording)
                {
                    WriteDebug("Please start speaking.");
                }

                WriteDebug("");
            }));
        }

        private void OnDataShortPhraseResponseReceivedHandler(object sender, SpeechResponseEventArgs e)
        {
            BeginInvoke((Action)(() =>
            {
                //this.WriteLine("--- OnDataShortPhraseResponseReceivedHandler ---");

                // we got the final result, so it we can end the mic reco.  No need to do this
                // for dataReco, since we already called endAudio() on it as soon as we were done
                // sending all the data.
                this.WriteResponseResult(e);


            }));
        }

        private void WriteResponseResult(SpeechResponseEventArgs e)
        {
            if (e.PhraseResponse.Results.Length == 0)
            {
                WriteDebug("No phrase response is available.");
            }
            else
            {
                WriteDebug("********* Final n-BEST Results *********");
                for (int i = 0; i < e.PhraseResponse.Results.Length; i++)
                {
                    WriteDebug(String.Format(
                        "[{0}] Confidence={1}, Text=\"{2}\"",
                        i,
                        e.PhraseResponse.Results[i].Confidence,
                        e.PhraseResponse.Results[i].DisplayText));
                }

            }
        }


        /// <summary>
        /// Creates a data client with LUIS intent support.
        /// Speech recognition with data (for example from a file or audio source).  
        /// The data is broken up into buffers and each buffer is sent to the Speech Recognition Service.
        /// No modification is done to the buffers, so the user can apply their
        /// own Silence Detection if desired.
        /// </summary>
        private void CreateDataRecoClientWithIntent()
        {
            this.dataClient = SpeechRecognitionServiceFactory.CreateDataClientWithIntentUsingEndpointUrl(
                this.DefaultLocale,
                Settings.Instance.SpeechRecognitionApiKey,
                Settings.Instance.LuisEndpoint);
            this.dataClient.AuthenticationUri = "";

            // Event handlers for speech recognition results
            this.dataClient.OnResponseReceived += this.OnDataShortPhraseResponseReceivedHandler;
            this.dataClient.OnPartialResponseReceived += this.OnPartialResponseReceivedHandler;
            this.dataClient.OnConversationError += this.OnConversationErrorHandler;

            // Event handler for intent result
            this.dataClient.OnIntent += this.OnIntentHandler;
        }

        /// <summary>
        /// Sends the audio helper. Used in case, we would send wav file for speech recognition
        /// </summary>
        /// <param name="wavFileName">Name of the wav file.</param>
        private void SendAudioHelper(string wavFileName)
        {
            using (FileStream fileStream = new FileStream(wavFileName, FileMode.Open, FileAccess.Read))
            {
                // Note for wave files, we can just send data from the file right to the server.
                // In the case you are not an audio file in wave format, and instead you have just
                // raw data (for example audio coming over bluetooth), then before sending up any 
                // audio data, you must first send up an SpeechAudioFormat descriptor to describe 
                // the layout and format of your raw audio data via DataRecognitionClient's sendAudioFormat() method.
                int bytesRead = 0;
                byte[] buffer = new byte[1024];

                try
                {
                    do
                    {
                        // Get more Audio data to send into byte buffer.
                        bytesRead = fileStream.Read(buffer, 0, buffer.Length);

                        // Send of audio data to service. 
                        this.dataClient.SendAudio(buffer, bytesRead);
                    }
                    while (bytesRead > 0);
                }
                finally
                {
                    // We are done sending audio.  Final recognition results will arrive in OnResponseReceived event call.
                    this.dataClient.EndAudio();
                }
            }
        }


        /// <summary>
        /// Called when a partial response is received.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PartialSpeechResponseEventArgs"/> instance containing the event data.</param>
        private void OnPartialResponseReceivedHandler(object sender, PartialSpeechResponseEventArgs e)
        {
            WriteDebug("--- Partial result received by OnPartialResponseReceivedHandler() ---");
            WriteDebug(e.PartialResult);

        }

        /// <summary>
        /// Called when a final response is received;
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SpeechResponseEventArgs"/> instance containing the event data.</param>
        private void OnMicShortPhraseResponseReceivedHandler(object sender, SpeechResponseEventArgs e)
        {
            BeginInvoke((Action)(() =>
            {
                WriteDebug("--- OnMicShortPhraseResponseReceivedHandler ---");

                // we got the final result, so it we can end the mic reco.  No need to do this
                // for dataReco, since we already called endAudio() on it as soon as we were done
                // sending all the data.
                this.micClient.EndMicAndRecognition();

                WriteDebug(e.PhraseResponse);

                SayCommandButton.Enabled = true;
            }));
        }

        /// <summary>
        /// Called when an error is received.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SpeechErrorEventArgs"/> instance containing the event data.</param>
        private void OnConversationErrorHandler(object sender, SpeechErrorEventArgs e)
        {
            BeginInvoke((Action)(() =>
            {
                SayCommandButton.Enabled = true;
            }));

            WriteDebug("--- Error received by OnConversationErrorHandler() ---");
            WriteDebug(String.Format("Error code: {0}", e.SpeechErrorCode.ToString()));
            WriteDebug(String.Format("Error text: {0}", e.SpeechErrorText));
            WriteDebug("");
        }

        #endregion


        private void SayCommandButton_Click(object sender, EventArgs e)
        {
            WriteDebug("--- Start microphone dictation with Intent detection ----");

            this.micClient =
                SpeechRecognitionServiceFactory.CreateMicrophoneClientWithIntentUsingEndpointUrl(
                    this.DefaultLocale,
                    Settings.Instance.SpeechRecognitionApiKey,
                    Settings.Instance.LuisEndpoint);
            this.micClient.AuthenticationUri = "";
            this.micClient.OnIntent += this.OnIntentHandler;

            // Event handlers for speech recognition results
            this.micClient.OnMicrophoneStatus += this.OnMicrophoneStatus;
            this.micClient.OnPartialResponseReceived += this.OnPartialResponseReceivedHandler;
            this.micClient.OnResponseReceived += this.OnMicShortPhraseResponseReceivedHandler;
            this.micClient.OnConversationError += this.OnConversationErrorHandler;
            this.micClient.StartMicAndRecognition();
        }


        #region ApiKeysSettings

        private void faceApiKey_TextChanged(object sender, EventArgs e)
        {
            Settings.Instance.FaceApiKey = ((TextBox)sender).Text;
            Settings.SaveSettings();
        }

        private void speakerRecognitionApiKey_TextChanged(object sender, EventArgs e)
        {
            Settings.Instance.SpeakerRecognitionApiKeyValue = ((TextBox)sender).Text;
            Settings.SaveSettings();
        }

        private void SpeechApiKeyTextBox_TextChanged(object sender, EventArgs e)
        {
            Settings.Instance.SpeechRecognitionApiKey = ((TextBox)sender).Text;
            Settings.SaveSettings();
        }

        private void LuisEndpointTextBox_TextChanged(object sender, EventArgs e)
        {
            Settings.Instance.LuisEndpoint = ((TextBox)sender).Text;
            Settings.SaveSettings();
        }

        private void CustomVisionApiKey_Textbox_TextChanged(object sender, EventArgs e)
        {
            Settings.Instance.VisionApiKey = ((TextBox)sender).Text;
            Settings.SaveSettings();
        }

        private void CustomVisionPredictionKey_TextBox_TextChanged(object sender, EventArgs e)
        {
            Settings.Instance.PredictionKey = ((TextBox)sender).Text;
            Settings.SaveSettings();
        }

        private void CustomVisionIterationId_Textbox_TextChanged(object sender, EventArgs e)
        {
            Settings.Instance.VisionApiIterationId = ((TextBox)sender).Text;
            Settings.SaveSettings();
        }

        private void CustomVisionProjectID_Textbox_TextChanged(object sender, EventArgs e)
        {
            Settings.Instance.VisionApiProjectId = ((TextBox)sender).Text;
            Settings.SaveSettings();
        }

        private void IpAddressTB_TextChanged(object sender, EventArgs e)
        {
            Settings.Instance.IPAdress = ((TextBox)sender).Text;
            Settings.SaveSettings();
        }

        private void EmotionApiKey_TextChanged(object sender, EventArgs e)
        {
            Settings.Instance.EmotionApiKey = ((TextBox)sender).Text;
            Settings.SaveSettings();
        }
        #endregion
    }
}