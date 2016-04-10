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
        private EZB ezb;
        private EventWaitHandle startEvent;
        private Camera camera;

        public void WriteDebug(object obj, bool clear = false)
        {
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

        private void TakeAPicButton_Click(object sender, EventArgs e)
        {
            this.DebugTextBox.Text = "";

            try
            {
                using (ezb = new EZB())
                {

                    WriteDebug("Connecting to EZ-B @ " + this.IpAddressTB.Text);
                    ezb.Connect(this.IpAddressTB.Text);

                    if (!ezb.IsConnected)
                    {
                        WriteDebug("Error: Unable to connect to EZ-B");
                        return;
                    }

                    using (camera = new EZ_B.Camera(ezb))
                    {
                        startEvent = new EventWaitHandle(false, EventResetMode.AutoReset);

                        camera.OnStart += CameraOnOnStart;
                        camera.OnNewFrame += CameraOnOnNewFrame;
                        camera.StartCamera(new ValuePair("EZB://" + this.IpAddressTB.Text), 160, 120);

                        //wait 10 seconds for the first camera frame
                        if (startEvent.WaitOne(10000))
                        {
                            //Save an image on the desktop
                            camera.SaveImageAsJPEG(this.ImageFileNameTB.Text);
                            this.pictureBox1.Image = camera.GetCurrentBitmap;
                            camera.StopCamera();
                        }
                        else
                        {
                            WriteDebug("Error: Unable to get the picture!");
                        }
                    }

                    WriteDebug("Done.");
                    ezb.Disconnect();
                }
            }
            catch (Exception ex)
            {
                this.WriteDebug("Error Exception=" + ex);
            }
        }

        private void CameraOnOnNewFrame()
        {
            startEvent.Set();
        }

        private void CameraOnOnStart()
        {
            WriteDebug("Camera Started!");
        }
    }
}