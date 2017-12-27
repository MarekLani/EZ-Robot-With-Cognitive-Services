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
    using Microsoft.ProjectOxford.SpeakerRecognition;
    using System.Runtime.InteropServices;
    using Microsoft.ProjectOxford.SpeakerRecognition.Contract.Identification;
    using System.Threading.Tasks;
    using EZFormApplication.CognitiveServicesCommunicators;
    using System.Drawing.Imaging;
 
    using Microsoft.CognitiveServices.SpeechRecognition;
    using System.Web.Script.Serialization;
    using System.Collections;
    using Newtonsoft.Json.Linq;

    public partial class Form1 : Form
    {

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
                        await Task.Delay(1000);
                        var currentBitmap = camera.GetCurrentBitmap;
                        //currentBitmap.Save(Guid.NewGuid().ToString() + ".jpg", ImageFormat.Jpeg);
                        var cvc = new CustomVisionCommunicator(Settings.Instance.PredictionKey, Settings.Instance.VisionApiKey, Settings.Instance.VisionApiProjectId, Settings.Instance.VisionApiIterationId);
                        var description = await cvc.RecognizeObjectsInImage(currentBitmap);
                        ezb.SoundV4.PlayData(ezb.SpeechSynth.SayToStream(description));
                        break;
                    }
                case "FindScrewdriver":
                    {
                        //We are trying to find screwdriver, first in front, then on the right, last on the left 
                        ezb.SoundV4.PlayData(ezb.SpeechSynth.SayToStream("I am on it"));
                        this.ezb.Servo.SetServoPosition(HeadServoHorizontalPort, this.mapPortToServoLimits[HeadServoHorizontalPort].CenterPosition);
                        this.ezb.Servo.SetServoPosition(HeadServoVerticalPort, this.mapPortToServoLimits[HeadServoVerticalPort].MinPosition + 55);
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
                        this.ezb.Servo.SetServoPosition(HeadServoHorizontalPort, this.mapPortToServoLimits[HeadServoHorizontalPort].CenterPosition + 45);
                        await Task.Delay(1000);
                        if (RecognizeObject("screwdriver"))
                        {
                            //TurnRight();
                            //Forward();
                            //squatGrabPosition.StartAction_Grab();
                            //Reverse();
                            //TurnLeft();
                            ezb.SoundV4.PlayData(ezb.SpeechSynth.SayToStream("Screwdriver is on the left side of me."));
                            return;
                        }

                        this.ezb.Servo.SetServoPosition(HeadServoHorizontalPort, this.mapPortToServoLimits[HeadServoHorizontalPort].CenterPosition - 45);
                        await Task.Delay(1000);
                        if (RecognizeObject("screwdriver"))
                        {
                            //TurnLeft();
                            //Forward();
                            //squatGrabPosition.StartAction_Grab();
                            //Reverse();
                            //TurnRight();
                            ezb.SoundV4.PlayData(ezb.SpeechSynth.SayToStream("Screwdriver is on the right side of me."));

                            return;
                        }

                        break;
                    }
                case "Hungry":
                    {

                        ezb.SoundV4.PlayData(ezb.SpeechSynth.SayToStream(("Yes I am always hungry! I would have an oil!")));
                        var currentBitmap = camera.GetCurrentBitmap;

                        var cvc = new CustomVisionCommunicator(Settings.Instance.PredictionKey, Settings.Instance.VisionApiKey, Settings.Instance.VisionApiProjectId, Settings.Instance.VisionApiIterationId);
                        var predictions = cvc.RecognizeObject(currentBitmap);
                        if (RecognizeObject("oil"))
                        {
                            grabPosition.StartAction_Takefood();
                            await Task.Delay(1000);
                            ezb.SoundV4.PlayData(ezb.SpeechSynth.SayToStream(("Hmm oil")));
                        }
                        else
                            ezb.SpeechSynth.Say("I do not eat this");
                        break;
                    }
                default: break;

            }
        }

        private delegate void InvokeDelegate();
        /// <summary>
        /// Called when the microphone status has changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MicrophoneEventArgs"/> instance containing the event data.</param>
        private void OnMicrophoneStatus(object sender, MicrophoneEventArgs e)
        {
        
            BeginInvoke ((Action)(() =>
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
        /// Sends the audio helper.
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

        public event EventHandler ToggleFaceRecognitionEvent;
        private static bool faceRecognitionActive = false;

        //Stack overflow: Error outputting for interop
        public enum MCIErrors
        {
            NO_ERROR = 0,
            MCIERR_BASE = 256,
            MCIERR_INVALID_DEVICE_ID = 257,
            MCIERR_UNRECOGNIZED_KEYWORD = 259,
            MCIERR_UNRECOGNIZED_COMMAND = 261,
            MCIERR_HARDWARE = 262,
            MCIERR_INVALID_DEVICE_NAME = 263,
            MCIERR_OUT_OF_MEMORY = 264,
            MCIERR_DEVICE_OPEN = 265,
            MCIERR_CANNOT_LOAD_DRIVER = 266,
            MCIERR_MISSING_COMMAND_STRING = 267,
            MCIERR_PARAM_OVERFLOW = 268,
            MCIERR_MISSING_STRING_ARGUMENT = 269,
            MCIERR_BAD_INTEGER = 270,
            MCIERR_PARSER_INTERNAL = 271,
            MCIERR_DRIVER_INTERNAL = 272,
            MCIERR_MISSING_PARAMETER = 273,
            MCIERR_UNSUPPORTED_FUNCTION = 274,
            MCIERR_FILE_NOT_FOUND = 275,
            MCIERR_DEVICE_NOT_READY = 276,
            MCIERR_INTERNAL = 277,
            MCIERR_DRIVER = 278,
            MCIERR_CANNOT_USE_ALL = 279,
            MCIERR_MULTIPLE = 280,
            MCIERR_EXTENSION_NOT_FOUND = 281,
            MCIERR_OUTOFRANGE = 282,
            MCIERR_FLAGS_NOT_COMPATIBLE = 283,
            MCIERR_FILE_NOT_SAVED = 286,
            MCIERR_DEVICE_TYPE_REQUIRED = 287,
            MCIERR_DEVICE_LOCKED = 288,
            MCIERR_DUPLICATE_ALIAS = 289,
            MCIERR_BAD_CONSTANT = 290,
            MCIERR_MUST_USE_SHAREABLE = 291,
            MCIERR_MISSING_DEVICE_NAME = 292,
            MCIERR_BAD_TIME_FORMAT = 293,
            MCIERR_NO_CLOSING_QUOTE = 294,
            MCIERR_DUPLICATE_FLAGS = 295,
            MCIERR_INVALID_FILE = 296,
            MCIERR_NULL_PARAMETER_BLOCK = 297,
            MCIERR_UNNAMED_RESOURCE = 298,
            MCIERR_NEW_REQUIRES_ALIAS = 299,
            MCIERR_NOTIFY_ON_AUTO_OPEN = 300,
            MCIERR_NO_ELEMENT_ALLOWED = 301,
            MCIERR_NONAPPLICABLE_FUNCTION = 302,
            MCIERR_ILLEGAL_FOR_AUTO_OPEN = 303,
            MCIERR_FILENAME_REQUIRED = 304,
            MCIERR_EXTRA_CHARACTERS = 305,
            MCIERR_DEVICE_NOT_INSTALLED = 306,
            MCIERR_GET_CD = 307,
            MCIERR_SET_CD = 308,
            MCIERR_SET_DRIVE = 309,
            MCIERR_DEVICE_LENGTH = 310,
            MCIERR_DEVICE_ORD_LENGTH = 311,
            MCIERR_NO_INTEGER = 312,
            MCIERR_WAVE_OUTPUTSINUSE = 320,
            MCIERR_WAVE_SETOUTPUTINUSE = 321,
            MCIERR_WAVE_INPUTSINUSE = 322,
            MCIERR_WAVE_SETINPUTINUSE = 323,
            MCIERR_WAVE_OUTPUTUNSPECIFIED = 324,
            MCIERR_WAVE_INPUTUNSPECIFIED = 325,
            MCIERR_WAVE_OUTPUTSUNSUITABLE = 326,
            MCIERR_WAVE_SETOUTPUTUNSUITABLE = 327,
            MCIERR_WAVE_INPUTSUNSUITABLE = 328,
            MCIERR_WAVE_SETINPUTUNSUITABLE = 329,
            MCIERR_SEQ_DIV_INCOMPATIBLE = 336,
            MCIERR_SEQ_PORT_INUSE = 337,
            MCIERR_SEQ_PORT_NONEXISTENT = 338,
            MCIERR_SEQ_PORT_MAPNODEVICE = 339,
            MCIERR_SEQ_PORT_MISCERROR = 340,
            MCIERR_SEQ_TIMER = 341,
            MCIERR_SEQ_PORTUNSPECIFIED = 342,
            MCIERR_SEQ_NOMIDIPRESENT = 343,
            MCIERR_NO_WINDOW = 346,
            MCIERR_CREATEWINDOW = 347,
            MCIERR_FILE_READ = 348,
            MCIERR_FILE_WRITE = 349,
            MCIERR_CUSTOM_DRIVER_BASE = 512
        };


        [DllImport("winmm.dll", EntryPoint = "mciSendStringA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int Record(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);


        private struct ServoLimits
        {
            public readonly int MaxPosition;
            public readonly int CenterPosition;
            public readonly int MinPosition;

            public ServoLimits(int min, int max)
            {
                this.MinPosition = min;
                this.MaxPosition = max;
                this.CenterPosition = (max - min) / 2 + min;
            }
        }

        private const Servo.ServoPortEnum HeadServoHorizontalPort = Servo.ServoPortEnum.D0;
        private const Servo.ServoPortEnum HeadServoVerticalPort = Servo.ServoPortEnum.D1;
        private const Servo.ServoPortEnum RightShoulderServoPort = Servo.ServoPortEnum.D2;
        private const Servo.ServoPortEnum RightPalmPort = Servo.ServoPortEnum.D5;

        private const int CameraWidth = 320;
        private const int CameraHeight = 240;
        private const int ServoStepValue = 1;
        private const int ServoSpeed = 15;
        private const int YDiffMargin = CameraWidth / 80;
        private const int XDiffMargin = CameraHeight / 80;
        private int sensitivity = 0;
        private int audioLevel = 0;
       
        private List<FaceResult> personResults = new List<FaceResult>();
        private DateTime lastFaceDetectTime = DateTime.MinValue;

        private WavePositions wavePosition;
        private GrabPositions grabPosition;
        private SquatGrabPositions squatGrabPosition;
        private LeftPositions leftPosition;
        private RightPositions rightPosition;
        private ForwardPositions forwardPosition;
        private ReversePositions reversePosition;
        private LeftHandPositions leftHandPosition;

        

        private readonly Dictionary<Servo.ServoPortEnum, ServoLimits> mapPortToServoLimits = new Dictionary<Servo.ServoPortEnum, ServoLimits>()
        {
            {Servo.ServoPortEnum.D0, new ServoLimits(5, 176)}, //Head Horizontal
            {Servo.ServoPortEnum.D1, new ServoLimits(70, 176)}, //Head Vertical 
            {Servo.ServoPortEnum.D2, new ServoLimits(5, 176)}, //Shoulder
            {Servo.ServoPortEnum.D5, new ServoLimits(5, 176)} //Palm
        };

        private bool isClosing;
        private EZB ezb;
        private EventWaitHandle ezbConnectionStatusChangedWaitHandle;
        private Camera camera;
        private long fpsCounter;
        private bool headTrackingActive;

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

           // ezb.SpeechSynth.OnPhraseRecognized += SpeechSynth_OnPhraseRecognized;

            //We leave out this and use Cognitive Services for speech recognition
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

        private void ToggleFaceRecognition(object sender, EventArgs e)
        {
            //to avoid concurrency issues
            
            try
            {
                if (!camera.IsActive)
                    return;
                //this.StartOrStopCamera();
            }
            catch (Exception ex)
            {
                this.WriteDebug("Error Exception=" + ex);
            }

            this.StartCameraButton.Enabled = false;

            var button = HeadTrackingButton;
            var labels = button.Tag.ToString().Split(new[] { '|' });

            if (this.headTrackingActive)
            {
                this.headTrackingActive = false;
                this.WriteDebug("Stopping Head Tracking.");
                //this.ezb.Servo.SetServoPosition(HeadServoHorizontalPort, this.ezb.Servo.GetServoFineTune(HeadServoHorizontalPort));
                //this.ezb.Servo.SetServoPosition(HeadServoVerticalPort, this.ezb.Servo.GetServoFineTune(HeadServoVerticalPort));

                button.Text = labels[0];
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
                    this.ezb.Servo.SetServoPosition(HeadServoHorizontalPort, this.mapPortToServoLimits[HeadServoHorizontalPort].CenterPosition);
                    this.ezb.Servo.SetServoPosition(HeadServoVerticalPort, this.mapPortToServoLimits[HeadServoVerticalPort].MinPosition + 15);

                    this.headTrackingActive = true;
                    this.WriteDebug("Starting Head Tracking.");
                    button.Text = labels[1];
                }
            }
        }

        //We leave out this, due to low precission of native ezrobot speech recognition support
        //private async void SpeechSynth_OnPhraseRecognized(string text, float confidence)
        //{
        //    switch(text)
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
        //                //currentBitmap.Save(Guid.NewGuid().ToString() + ".jpg", ImageFormat.Jpeg);
        //                var description = await CustomVisionCommunicator.RecognizeObjectsInImage(currentBitmap);
        //                ezb.SoundV4.PlayData(ezb.SpeechSynth.SayToStream(description));
        //                break;
        //            }
        //        case "where is screwdriver":
        //        {
        //                //We are trying to find screwdriver, first in front, then on the right, last on the left 
        //                ezb.SoundV4.PlayData(ezb.SpeechSynth.SayToStream("I am on it"));
        //                this.ezb.Servo.SetServoPosition(HeadServoHorizontalPort, this.mapPortToServoLimits[HeadServoHorizontalPort].CenterPosition);
        //                this.ezb.Servo.SetServoPosition(HeadServoVerticalPort, this.mapPortToServoLimits[HeadServoVerticalPort].MinPosition + 55);
        //                await Task.Delay(1000);
        //                if (RecognizeObject("screwdriver"))
        //                {
        //                    //Forward();
        //                    //squatGrabPosition.StartAction_Grab();
        //                    //Reverse();
        //                    ////Grab
                           
        //                    ezb.SoundV4.PlayData(ezb.SpeechSynth.SayToStream("Screwdriver is in front of me"));
        //                    return;
        //                }
        //                this.ezb.Servo.SetServoPosition(HeadServoHorizontalPort, this.mapPortToServoLimits[HeadServoHorizontalPort].CenterPosition + 45);
        //                await Task.Delay(1000);
        //                if (RecognizeObject("screwdriver"))
        //                {
        //                    //TurnRight();
        //                    //Forward();
        //                    //squatGrabPosition.StartAction_Grab();
        //                    //Reverse();
        //                    //TurnLeft();
        //                    ezb.SoundV4.PlayData(ezb.SpeechSynth.SayToStream("Screwdriver is on the left side of me."));
        //                    return;
        //                }

        //                this.ezb.Servo.SetServoPosition(HeadServoHorizontalPort, this.mapPortToServoLimits[HeadServoHorizontalPort].CenterPosition - 45);
        //                await Task.Delay(1000);
        //                if (RecognizeObject("screwdriver"))
        //                {
        //                    //TurnLeft();
        //                    //Forward();
        //                    //squatGrabPosition.StartAction_Grab();
        //                    //Reverse();
        //                    //TurnRight();
        //                    ezb.SoundV4.PlayData(ezb.SpeechSynth.SayToStream("Screwdriver is on the right side of me."));

        //                    return;
        //                }

        //                break;
        //        }
        //        case "are you thirsty":
        //        {

        //                ezb.SoundV4.PlayData(ezb.SpeechSynth.SayToStream(("Yes I am always hungry! I would have an oil!")));
        //                var currentBitmap = camera.GetCurrentBitmap;
        //                //currentBitmap.Save(Guid.NewGuid().ToString() + ".jpg", ImageFormat.Jpeg);
        //                var predictions = CustomVisionCommunicator.RecognizeObject(currentBitmap);
        //                if( RecognizeObject("oil"))
        //                {
        //                    grabPosition.StartAction_Takefood();
        //                    await Task.Delay(1000);
        //                    ezb.SoundV4.PlayData(ezb.SpeechSynth.SayToStream(("Hmm oil")));
        //                }
        //                else
        //                    ezb.SpeechSynth.Say("I do not eat this");
        //                break;
        //        }
        //        default: break;

        //    }
            
        //}

        private bool RecognizeObject(string objectName)
        {
            var currentBitmap = camera.GetCurrentBitmap;

            //Used when creating training datase
            //currentBitmap.Save(Guid.NewGuid().ToString() + ".jpg", ImageFormat.Jpeg);

            var cvc = new CustomVisionCommunicator(Settings.Instance.PredictionKey, Settings.Instance.VisionApiKey,Settings.Instance.VisionApiProjectId, Settings.Instance.VisionApiIterationId);
            var predictions = cvc.RecognizeObject(currentBitmap);
            if (predictions.Where(p => p.Tag == objectName).First().Probability > 0.7)
                return true;
            else
                return false;
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
           
            this.FpsLabel.Text = ezb.SpeechSynth.AudioLevel.ToString(); // cnt.ToString(CultureInfo.InvariantCulture);
            this.fpsLabelLabel.Visible = this.FpsLabel.Visible = cnt > 0;

            if (ezb.IsConnected && !ezb.SpeechSynth.IsListening)
            {
                ezb.SpeechSynth.StartListening();
            }


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
                //this.camera.StartCamera(new ValuePair("EZB://" + this.IpAddressTB.Text), this.Video1P, this.Video2P, 320, 240);
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
            this.ezb.Servo.SetServoPosition(HeadServoVerticalPort, this.mapPortToServoLimits[HeadServoVerticalPort].MinPosition + 55);
            var currentBitmap = camera.GetCurrentBitmap;
            currentBitmap.Save(Guid.NewGuid().ToString() + ".jpg", ImageFormat.Jpeg);
            //ToggleFaceRecognitionEvent?.Invoke(this, null);
        }

        static bool isSaying = false;

        private async void HeadTracking()
        {
            if (isSaying)
                return;

            if (!this.headTrackingActive)
            {
                return;
            }

            var faceLocations = this.camera.CameraFaceDetection.GetFaceDetection(32, 1000, 1);
            if (faceLocations.Length > 0)
            {
                //DO Face detection
                if (this.fpsCounter == 1)
                {
                    foreach (var objectLocation in faceLocations)
                    {
                        this.WriteDebug(string.Format("Face detected at H:{0} V:{1}", objectLocation.HorizontalLocation, objectLocation.VerticalLocation));
                    }
                }
            }

            if (faceLocations.Length == 0)
            {
                return;
            }

            //Grab the first face location (ONLY ONE)
            var faceLocation = faceLocations.First();


            var servoVerticalPosition = this.ezb.Servo.GetServoPosition(HeadServoVerticalPort);
            var servoHorizontalPosition = this.ezb.Servo.GetServoPosition(HeadServoHorizontalPort);

            var yDiff = faceLocation.CenterY - CameraHeight / 2;
            if (Math.Abs(yDiff) > YDiffMargin)
            {
                if (yDiff < -1 * sensitivity)
                {
                    if (servoVerticalPosition - ServoStepValue >= this.mapPortToServoLimits[HeadServoVerticalPort].MinPosition)
                    {
                        servoVerticalPosition -= ServoStepValue;
                    }
                }
                else if (yDiff > sensitivity)
                {
                    if (servoVerticalPosition + ServoStepValue <= this.mapPortToServoLimits[HeadServoVerticalPort].MaxPosition)
                    {
                        servoVerticalPosition += ServoStepValue;
                    }
                }
            }

            var xDiff = faceLocation.CenterX - CameraWidth / 2;
            if (Math.Abs(xDiff) > XDiffMargin)
            {
                if (xDiff > sensitivity)
                {
                    if (servoHorizontalPosition - ServoStepValue >= this.mapPortToServoLimits[HeadServoHorizontalPort].MinPosition)
                    {
                        servoHorizontalPosition -= ServoStepValue;
                    }
                }
                else if (xDiff < -1 * sensitivity)
                {
                    if (servoHorizontalPosition + ServoStepValue <= this.mapPortToServoLimits[HeadServoHorizontalPort].MaxPosition)
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

            if (person != null && !ezb.SoundV4.IsPlaying)
            {
                if (emotions[0].Scores.Sadness > 0.02)
                {
                    isSaying = true;
                    ezb.SoundV4.PlayData(ezb.SpeechSynth.SayToStream("You look bit sad, but I have something to cheer you up. A joke! Here it is: My dog used to chase people on a bike a lot. It got so bad, finally I had to take his bike away."));
                    Thread.Sleep(25000);
                    isSaying = false;
                }
                else
                {
                    ezb.SoundV4.PlayData(ezb.SpeechSynth.SayToStream("Hello " + person.Name));
                    Wave();
                }

                           
            }
            else if (faces != null && faces.Any())
            {
                if (faces[0].FaceAttributes.Gender == "male")
                    ezb.SoundV4.PlayData(ezb.SpeechSynth.SayToStream("Hello mister stranger your age is probably " + faces[0].FaceAttributes.Age));
                else
                    ezb.SoundV4.PlayData(ezb.SpeechSynth.SayToStream("Hello misses stranger your age is probably " + faces[0].FaceAttributes.Age));

                Wave();
            }
        }


        /// 
        /// Positions
        /// 
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

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            sensitivity = trackBar1.Value;
        }

        private void faceApiKey_TextChanged(object sender, EventArgs e)
        {
            FaceApiCommunicator.SetFaceKey(((TextBox)sender).Text);
            Settings.Instance.FaceApiKey = ((TextBox)sender).Text;
            Settings.SaveSettings();
        }

        private void speakerRecognitionApiKey_TextChanged(object sender, EventArgs e)
        {
            Settings.Instance.SpeakerRecognitionApiKeyValue = ((TextBox)sender).Text;
            Settings.SaveSettings();
        }

        private static bool isRecording = false;
        private System.Timers.Timer timer1 = new System.Timers.Timer();

        private  bool StartRecording()
        {
            var result = (MCIErrors)Record("open new Type waveaudio Alias recsound", "", 0, 0);
            if (result != MCIErrors.NO_ERROR)
            {
                WriteDebug("Error code: " + result.ToString());
                return false;
            }
            result = (MCIErrors)Record("set recsound time format ms alignment 2 bitspersample 16 samplespersec 16000 channels 1 bytespersec 88200", "", 0, 0);
            if (result != MCIErrors.NO_ERROR)
            {
                WriteDebug("Error code: " + result.ToString());
                return false;
            }

            result = (MCIErrors)Record("record recsound", "", 0, 0);
            if (result != MCIErrors.NO_ERROR)
            {
                WriteDebug("Error code: " + result.ToString());
                return false;
            }
            return true;
        }

        private bool StopRecording()
        {
            var result = (MCIErrors)Record("save recsound result.wav", "", 0, 0);
            if (result != MCIErrors.NO_ERROR)
            {
                WriteDebug("Error code: " + result.ToString());
                return false;
            }
            result = (MCIErrors)Record("close recsound ", "", 0, 0);
            if (result != MCIErrors.NO_ERROR)
            {
                WriteDebug("Error code: " + result.ToString());
                return false;
            }

            return true;
        }

        private async void ListenButton_Click(object sender, EventArgs e)
        {
            var srsc = new SpeakerIdentificationServiceClient(Settings.Instance.SpeakerRecognitionApiKeyValue);
            if (!isRecording)
            {

                if(StartRecording())
                {
               
                    isRecording = true;
                    ListenButton.Text = "Stop listening";
                }
            }
            else
            {
                if (StopRecording())
                try
                {
                    var profiles = await srsc.GetProfilesAsync();

                    Guid[] testProfileIds = new Guid[profiles.Length];
                    for (int i = 0; i < testProfileIds.Length; i++)
                    {
                        testProfileIds[i] = profiles[i].ProfileId;
                    }

                    OperationLocation processPollingLocation;
                    using (Stream audioStream = File.OpenRead("result.wav"))
                    {
                        processPollingLocation = await srsc.IdentifyAsync(audioStream, testProfileIds,true);
                    }

                    IdentificationOperation identificationResponse = null;
                    int numOfRetries = 10;
                    TimeSpan timeBetweenRetries = TimeSpan.FromSeconds(5.0);
                    while (numOfRetries > 0)
                    {
                        await Task.Delay(timeBetweenRetries);
                        identificationResponse = await srsc.CheckIdentificationStatusAsync(processPollingLocation);

                        if (identificationResponse.Status == Microsoft.ProjectOxford.SpeakerRecognition.Contract.Identification.Status.Succeeded)
                        {
                            break;
                        }
                        else if (identificationResponse.Status == Microsoft.ProjectOxford.SpeakerRecognition.Contract.Identification.Status.Failed)
                        {
                            throw new IdentificationException(identificationResponse.Message);
                        }
                        numOfRetries--;
                    }
                    if (numOfRetries <= 0)
                    {
                        throw new IdentificationException("Identification operation timeout.");
                    }

                    WriteDebug("Identification Done.");

                    wavePosition.StartAction_Wave();

                    var name = Speakers.ListOfSpeakers.Where(s => s.ProfileId == identificationResponse.ProcessingResult.IdentifiedProfileId.ToString()).First().Name;
                    ezb.SoundV4.PlayData(ezb.SpeechSynth.SayToStream("Hello " + name));

                    await Task.Delay(5000);
                    wavePosition.Stop();
                    ezb.Servo.ReleaseAllServos();
                    
                    //_identificationResultTxtBlk.Text = identificationResponse.ProcessingResult.IdentifiedProfileId.ToString();
                    //_identificationConfidenceTxtBlk.Text = identificationResponse.ProcessingResult.Confidence.ToString();
                    //_identificationResultStckPnl.Visibility = Visibility.Visible;
                }
                catch (IdentificationException ex)
                {
                    WriteDebug("Speaker Identification Error: " + ex.Message);
                    wavePosition.StartAction_Wave();

                    ezb.SoundV4.PlayData(ezb.SpeechSynth.SayToStream("Hello stranger"));

                    await Task.Delay(5000);
                    wavePosition.Stop();
                    ezb.Servo.ReleaseAllServos();
                }
                catch (Exception ex)
                {
                    WriteDebug("Error: " + ex.Message);
                }

                isRecording = false;
                ListenButton.Text = "Recognize Voice";

            }

        }

        private void IpAddressTB_TextChanged(object sender, EventArgs e)
        {
            Settings.Instance.IPAdress = ((TextBox)sender).Text;
            Settings.SaveSettings();
        }

        private void EmotionApiKey_TextChanged(object sender, EventArgs e)
        {
            FaceApiCommunicator.SetEmotionKey(((TextBox)sender).Text);
            Settings.Instance.EmotionApiKey = ((TextBox)sender).Text;
            Settings.SaveSettings();
        }

        private void ReleaseServosButton_Click(object sender, EventArgs e)
        {
            ezb.Servo.ReleaseAllServos();
        }

        //Introduction
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
    }
}