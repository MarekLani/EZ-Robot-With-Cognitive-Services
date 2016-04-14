namespace EZFormApplication
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Threading;
    using System.Windows.Forms;
    using EZ_B;

    public partial class Form1 : Form
    {
        private bool isClosing;
        private EZB ezb;
        private EventWaitHandle frameEventObject;
        private Camera camera;
        private long fpsCounter;

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

            //create event object to avoid sleep/poll check
            this.frameEventObject = new EventWaitHandle(false, EventResetMode.AutoReset);

            ////create a timer to display the number of frames per second 
            //this.fpsTimer = new System.Windows.Forms.Timer();
            //this.fpsTimer.Interval = 60 * 1000; // 1 second
            //this.fpsTimer.Tick += this.FpsTimer_Tick;
            //this.fpsTimer.Enabled;
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
                if (this.frameEventObject.WaitOne(10000))
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

        private void CameraOnOnNewFrame()
        {
            this.frameEventObject.Set();
            this.pictureBox1.Invoke(new EventHandler(delegate { this.pictureBox1.Image = this.camera.GetCurrentBitmap; }));
            Interlocked.Increment(ref this.fpsCounter);
        }

        private void CameraOnStop()
        {
            this.WriteDebug("Camera Stopped!");
            this.frameEventObject.Reset();
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
            var labels = this.StartCameraButton.Tag.ToString().Split(new[] { '|' });

            if (this.camera.IsActive)
            {
                this.WriteDebug("Stopping camera.");
                this.camera.StopCamera();
                this.StartCameraButton.Text = labels[0];
            }
            else
            {
                this.WriteDebug("Starting camera.");
                this.frameEventObject.Reset();
                this.camera.StartCamera(new ValuePair("EZB://" + this.IpAddressTB.Text), 160, 120);
                this.StartCameraButton.Text = labels[1];
            }
        }
    }
}
