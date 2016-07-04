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

    public partial class Form1 : Form
    {
        private struct ServoLimits
        {
            public readonly int MaxPosition;
            public readonly int CenterPosition;
            public readonly int MinPosition;

            public ServoLimits(int min, int max)
            {
                this.MinPosition = min;
                this.MaxPosition = max;
                this.CenterPosition = (max - min)/2 + min;
            }
        }

        private const Servo.ServoPortEnum HeadServoHorizontalPort = Servo.ServoPortEnum.D0;
        private const Servo.ServoPortEnum HeadServoVerticalPort = Servo.ServoPortEnum.D1;
        private const int CameraWidth = 320;
        private const int CameraHeight = 240;
        private const int ServoStepValue = 1;
        private const int YDiffMargin = CameraWidth/80;
        private const int XDiffMargin = CameraHeight/80;

        private readonly Dictionary<Servo.ServoPortEnum, ServoLimits> mapPortToServoLimits = new Dictionary<Servo.ServoPortEnum, ServoLimits>()
        {
            {Servo.ServoPortEnum.D0, new ServoLimits(5, 176)}, //Head Horizontal
            {Servo.ServoPortEnum.D1, new ServoLimits(70, 176)} //Head Vertical 
        };

        private bool isClosing;
        private EZB ezb;
        private EventWaitHandle cameraFrameEventWaitHandle;
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
            this.ImageFileNameTB.Text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "capture-" + DateTime.Now.ToString("yyyyxMMxddxHHxmmxss") + ".jpg");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.ezb = new EZB();
            this.camera = new Camera(this.ezb);
            this.camera.OnStart += this.CameraOnOnStart;
            this.camera.OnNewFrame += this.CameraOnOnNewFrame;
            this.camera.OnStop += this.CameraOnStop;

            this.ezb.OnConnectionChange += this.EzbOnConnectionChange;

            //create event object to avoid sleep/poll check
            this.ezbConnectionStatusChangedWaitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);

            //create event for ezb connection status changes
            this.ezbConnectionStatusChangedWaitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);

            //activate forms timer
            this.OneSecondTimer.Enabled = true;
            this.OneSecondTimer.Start();
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
            var labels = this.StartOrStopVideoRecordingButton.Tag.ToString().Split(new[] {'|'});

            if (!this.camera.AVIIsRecording)
            {
                var filename = this.ImageFileNameTB.Text + ".avi";
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

        private void TakeAPicButton_Click(object sender, EventArgs e)
        {
            //to avoid concurrency issues
            this.StartCameraButton.Enabled = false;
            this.TakeAPicButton.Enabled = false;

            try
            {
                //current camera state
                //if camera is stopped => start camera => take a pic => stop camera
                //if camera is started => take a pic
                bool lastCameraIsActive = this.camera.IsActive;

                if (!lastCameraIsActive)
                {
                    this.StartOrStopCamera();
                }

                //wait 10 seconds for the first camera frame
                if (this.ezbConnectionStatusChangedWaitHandle.WaitOne(10000))
                {
                    this.camera.SaveImageAsJPEG(this.ImageFileNameTB.Text);
                    this.WriteDebug("Picture done!");
                }
                else
                {
                    this.WriteDebug("Error: Unable to get a valid camera frame!");
                }

                if (!lastCameraIsActive)
                {
                    this.StartOrStopCamera();
                }
            }
            catch (Exception ex)
            {
                this.WriteDebug("Error Exception=" + ex);
            }

            this.StartCameraButton.Enabled = true;
            this.TakeAPicButton.Enabled = true;
        }

        private void StartCameraButton_Click(object sender, EventArgs e)
        {
            //to avoid concurrency issues
            this.StartCameraButton.Enabled = false;
            this.TakeAPicButton.Enabled = false;

            try
            {
                this.StartOrStopCamera();
            }
            catch (Exception ex)
            {
                this.WriteDebug("Error Exception=" + ex);
            }

            this.StartCameraButton.Enabled = true;
            this.TakeAPicButton.Enabled = true;
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
                this.WriteDebug("Connected to EZB");
            }
            else
            {
                this.WriteDebug("Disconnected from EZB");
                this.headTrackingActive = false;
            }
        }

        private void CameraOnOnNewFrame()
        {
            this.ezbConnectionStatusChangedWaitHandle.Set();
            this.pictureBox1.Invoke(new EventHandler(delegate { this.pictureBox1.Image = this.camera.GetCurrentBitmap; }));
            Interlocked.Increment(ref this.fpsCounter);

            var objectLocations = this.camera.CameraFaceDetection.GetFaceDetection();
            //if (objectLocation.IsObjectFound)
            if (objectLocations.Length > 0)
            {
                if (this.fpsCounter == 1)
                {
                    foreach (var objectLocation in objectLocations)
                    {
                        this.WriteDebug(string.Format("Face detected at H:{0} V:{1}", objectLocation.HorizontalLocation, objectLocation.VerticalLocation));
                    }
                }
            }

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
            this.FpsLabel.Text = cnt.ToString(CultureInfo.InvariantCulture);
            this.fpsLabelLabel.Visible = this.FpsLabel.Visible = cnt > 0;
        }


        private void StartOrStopCamera()
        {
            var labels = this.StartCameraButton.Tag.ToString().Split(new[] {'|'});

            if (this.camera.IsActive)
            {
                this.WriteDebug("Stopping camera.");
                this.camera.StopCamera();
                this.StartCameraButton.Text = labels[0];
            }
            else
            {
                this.WriteDebug("Starting camera.");
                this.ezbConnectionStatusChangedWaitHandle.Reset();
                this.camera.StartCamera(new ValuePair("EZB://" + this.IpAddressTB.Text), CameraWidth, CameraHeight);
                //this.camera.StartCamera(new ValuePair("EZB://" + this.IpAddressTB.Text), this.Video1P, this.Video2P, 320, 240);
                this.StartCameraButton.Text = labels[1];
                this.camera.SetPreviewControl = this.Video2P;
            }
        }

        private void ChangeEzbConnectButtonText()
        {
            this.StartEZBButton.Invoke(new EventHandler(delegate
            {
                var labels = this.StartEZBButton.Tag.ToString().Split(new[] {'|'});
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
            var button = (Button) sender;
            var labels = button.Tag.ToString().Split(new[] {'|'});

            if (this.headTrackingActive)
            {
                this.headTrackingActive = false;
                this.WriteDebug("Stopping Head Tracking.");
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
                    this.ezb.Servo.SetServoPosition(HeadServoVerticalPort, this.mapPortToServoLimits[HeadServoVerticalPort].CenterPosition);

                    this.headTrackingActive = true;
                    this.WriteDebug("Starting Head Tracking.");
                    button.Text = labels[1];
                }
            }
        }

        private void HeadTracking()
        {
            if (!this.headTrackingActive)
            {
                return;
            }

            var faceLocations = this.camera.CameraFaceDetection.GetFaceDetection();
            if (faceLocations.Length == 0)
            {
                return;
            }

            //Grab the first face location (ONLY ONE)
            var faceLocation = faceLocations.First();

            var servoVerticalPosition = this.ezb.Servo.GetServoPosition(HeadServoVerticalPort);
            var servoHorizontalPosition = this.ezb.Servo.GetServoPosition(HeadServoHorizontalPort);

            var yDiff = faceLocation.CenterY - CameraHeight/2;
            if (Math.Abs(yDiff) > YDiffMargin)
            {
                if (yDiff < 0)
                {
                    if (servoVerticalPosition - ServoStepValue >= this.mapPortToServoLimits[HeadServoVerticalPort].MinPosition)
                    {
                        servoVerticalPosition -= ServoStepValue;
                    }
                }
                else
                {
                    if (servoVerticalPosition + ServoStepValue <= this.mapPortToServoLimits[HeadServoVerticalPort].MaxPosition)
                    {
                        servoVerticalPosition += ServoStepValue;
                    }
                }
            }

            var xDiff = faceLocation.CenterX - CameraWidth/2;
            if (Math.Abs(xDiff) > XDiffMargin)
            {
                if (xDiff > 0)
                {
                    if (servoHorizontalPosition - ServoStepValue >= this.mapPortToServoLimits[HeadServoHorizontalPort].MinPosition)
                    {
                        servoHorizontalPosition -= ServoStepValue;
                    }
                }
                else
                {
                    if (servoHorizontalPosition + ServoStepValue <= this.mapPortToServoLimits[HeadServoHorizontalPort].MaxPosition)
                    {
                        servoHorizontalPosition += ServoStepValue;
                    }
                }
            }

            this.ezb.Servo.SetServoPosition(HeadServoVerticalPort, servoVerticalPosition);
            this.ezb.Servo.SetServoPosition(HeadServoHorizontalPort, servoHorizontalPosition);
        }
    }
}