namespace EZFormApplication
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.IpAddressTB = new System.Windows.Forms.TextBox();
            this.StartOrStopVideoRecordingButton = new System.Windows.Forms.Button();
            this.StartCameraButton = new System.Windows.Forms.Button();
            this.Video1GB = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.FpsLabel = new System.Windows.Forms.Label();
            this.fpsLabelLabel = new System.Windows.Forms.Label();
            this.InfoGB = new System.Windows.Forms.GroupBox();
            this.DebugTextBox = new System.Windows.Forms.TextBox();
            this.OneSecondTimer = new System.Windows.Forms.Timer(this.components);
            this.Video2GB = new System.Windows.Forms.GroupBox();
            this.Video2P = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.CustomVisionProjectID_Textbox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.CustomVisionPredictionKey_TextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.CustomVisionIterationId_Textbox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.CustomVisionApiKey_Textbox = new System.Windows.Forms.TextBox();
            this.LuisEndpointTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SpeechApiKeyTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.EmotionApiKey = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SpeakerRecognitionApiKey = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.FaceApiKey = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.ReleaseServosButton = new System.Windows.Forms.Button();
            this.HeadTrackingButton = new System.Windows.Forms.Button();
            this.StartEZBButton = new System.Windows.Forms.Button();
            this.ListenButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SayCommandButton = new System.Windows.Forms.Button();
            this.Video1GB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.InfoGB.SuspendLayout();
            this.Video2GB.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP";
            // 
            // IpAddressTB
            // 
            this.IpAddressTB.Location = new System.Drawing.Point(47, 18);
            this.IpAddressTB.Margin = new System.Windows.Forms.Padding(4);
            this.IpAddressTB.Name = "IpAddressTB";
            this.IpAddressTB.Size = new System.Drawing.Size(124, 22);
            this.IpAddressTB.TabIndex = 1;
            this.IpAddressTB.Text = "192.168.137.84";
            this.IpAddressTB.TextChanged += new System.EventHandler(this.IpAddressTB_TextChanged);
            // 
            // StartOrStopVideoRecordingButton
            // 
            this.StartOrStopVideoRecordingButton.Location = new System.Drawing.Point(373, 8);
            this.StartOrStopVideoRecordingButton.Margin = new System.Windows.Forms.Padding(4);
            this.StartOrStopVideoRecordingButton.Name = "StartOrStopVideoRecordingButton";
            this.StartOrStopVideoRecordingButton.Size = new System.Drawing.Size(68, 42);
            this.StartOrStopVideoRecordingButton.TabIndex = 15;
            this.StartOrStopVideoRecordingButton.Tag = "Start Video Recording|Stop Video Recording";
            this.StartOrStopVideoRecordingButton.Text = "Start Video Recording";
            this.StartOrStopVideoRecordingButton.UseVisualStyleBackColor = true;
            this.StartOrStopVideoRecordingButton.Click += new System.EventHandler(this.StartOrStopVideoRecordingButton_Click);
            // 
            // StartCameraButton
            // 
            this.StartCameraButton.Location = new System.Drawing.Point(268, 8);
            this.StartCameraButton.Margin = new System.Windows.Forms.Padding(4);
            this.StartCameraButton.Name = "StartCameraButton";
            this.StartCameraButton.Size = new System.Drawing.Size(94, 42);
            this.StartCameraButton.TabIndex = 10;
            this.StartCameraButton.Tag = "Start Camera|Stop Camera";
            this.StartCameraButton.Text = "Start Camera";
            this.StartCameraButton.UseVisualStyleBackColor = true;
            this.StartCameraButton.Click += new System.EventHandler(this.StartCameraButton_Click);
            // 
            // Video1GB
            // 
            this.Video1GB.Controls.Add(this.pictureBox1);
            this.Video1GB.Location = new System.Drawing.Point(79, 297);
            this.Video1GB.Margin = new System.Windows.Forms.Padding(4);
            this.Video1GB.Name = "Video1GB";
            this.Video1GB.Padding = new System.Windows.Forms.Padding(4);
            this.Video1GB.Size = new System.Drawing.Size(457, 331);
            this.Video1GB.TabIndex = 11;
            this.Video1GB.TabStop = false;
            this.Video1GB.Text = "Video Frames";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(15, 23);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(427, 295);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // FpsLabel
            // 
            this.FpsLabel.AutoSize = true;
            this.FpsLabel.Location = new System.Drawing.Point(172, 167);
            this.FpsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.FpsLabel.Name = "FpsLabel";
            this.FpsLabel.Size = new System.Drawing.Size(0, 17);
            this.FpsLabel.TabIndex = 11;
            this.FpsLabel.Visible = false;
            // 
            // fpsLabelLabel
            // 
            this.fpsLabelLabel.AutoSize = true;
            this.fpsLabelLabel.Location = new System.Drawing.Point(16, 266);
            this.fpsLabelLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.fpsLabelLabel.Name = "fpsLabelLabel";
            this.fpsLabelLabel.Size = new System.Drawing.Size(130, 17);
            this.fpsLabelLabel.TabIndex = 9;
            this.fpsLabelLabel.Text = "frames per second:";
            this.fpsLabelLabel.Visible = false;
            // 
            // InfoGB
            // 
            this.InfoGB.Controls.Add(this.DebugTextBox);
            this.InfoGB.Location = new System.Drawing.Point(20, 636);
            this.InfoGB.Margin = new System.Windows.Forms.Padding(4);
            this.InfoGB.Name = "InfoGB";
            this.InfoGB.Padding = new System.Windows.Forms.Padding(4);
            this.InfoGB.Size = new System.Drawing.Size(1069, 209);
            this.InfoGB.TabIndex = 12;
            this.InfoGB.TabStop = false;
            this.InfoGB.Text = "Info";
            // 
            // DebugTextBox
            // 
            this.DebugTextBox.Location = new System.Drawing.Point(15, 23);
            this.DebugTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.DebugTextBox.Multiline = true;
            this.DebugTextBox.Name = "DebugTextBox";
            this.DebugTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.DebugTextBox.Size = new System.Drawing.Size(1041, 170);
            this.DebugTextBox.TabIndex = 5;
            // 
            // OneSecondTimer
            // 
            this.OneSecondTimer.Interval = 1000;
            this.OneSecondTimer.Tick += new System.EventHandler(this.OneSecondTimer_Tick);
            // 
            // Video2GB
            // 
            this.Video2GB.Controls.Add(this.Video2P);
            this.Video2GB.Location = new System.Drawing.Point(632, 297);
            this.Video2GB.Margin = new System.Windows.Forms.Padding(4);
            this.Video2GB.Name = "Video2GB";
            this.Video2GB.Padding = new System.Windows.Forms.Padding(4);
            this.Video2GB.Size = new System.Drawing.Size(457, 331);
            this.Video2GB.TabIndex = 13;
            this.Video2GB.TabStop = false;
            this.Video2GB.Text = "Processed Video";
            // 
            // Video2P
            // 
            this.Video2P.Location = new System.Drawing.Point(17, 23);
            this.Video2P.Margin = new System.Windows.Forms.Padding(4);
            this.Video2P.Name = "Video2P";
            this.Video2P.Size = new System.Drawing.Size(427, 295);
            this.Video2P.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.CustomVisionProjectID_Textbox);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.CustomVisionPredictionKey_TextBox);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.CustomVisionIterationId_Textbox);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.CustomVisionApiKey_Textbox);
            this.groupBox2.Controls.Add(this.LuisEndpointTextBox);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.SpeechApiKeyTextBox);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.EmotionApiKey);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.SpeakerRecognitionApiKey);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.FaceApiKey);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.trackBar1);
            this.groupBox2.Location = new System.Drawing.Point(20, 50);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1069, 233);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "More settings";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(554, 183);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(162, 17);
            this.label11.TabIndex = 34;
            this.label11.Text = "Custom Vision Project ID";
            // 
            // CustomVisionProjectID_Textbox
            // 
            this.CustomVisionProjectID_Textbox.Location = new System.Drawing.Point(557, 204);
            this.CustomVisionProjectID_Textbox.Margin = new System.Windows.Forms.Padding(4);
            this.CustomVisionProjectID_Textbox.Name = "CustomVisionProjectID_Textbox";
            this.CustomVisionProjectID_Textbox.Size = new System.Drawing.Size(246, 22);
            this.CustomVisionProjectID_Textbox.TabIndex = 33;
            this.CustomVisionProjectID_Textbox.TextChanged += new System.EventHandler(this.CustomVisionProjectID_Textbox_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(554, 117);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(192, 17);
            this.label10.TabIndex = 32;
            this.label10.Text = "Custom Vision Prediction Key";
            // 
            // CustomVisionPredictionKey_TextBox
            // 
            this.CustomVisionPredictionKey_TextBox.Location = new System.Drawing.Point(557, 138);
            this.CustomVisionPredictionKey_TextBox.Margin = new System.Windows.Forms.Padding(4);
            this.CustomVisionPredictionKey_TextBox.Name = "CustomVisionPredictionKey_TextBox";
            this.CustomVisionPredictionKey_TextBox.Size = new System.Drawing.Size(246, 22);
            this.CustomVisionPredictionKey_TextBox.TabIndex = 31;
            this.CustomVisionPredictionKey_TextBox.TextChanged += new System.EventHandler(this.CustomVisionPredictionKey_TextBox_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(253, 183);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(167, 17);
            this.label9.TabIndex = 30;
            this.label9.Text = "Custom Vision Iteration Id";
            // 
            // CustomVisionIterationId_Textbox
            // 
            this.CustomVisionIterationId_Textbox.Location = new System.Drawing.Point(256, 204);
            this.CustomVisionIterationId_Textbox.Margin = new System.Windows.Forms.Padding(4);
            this.CustomVisionIterationId_Textbox.Name = "CustomVisionIterationId_Textbox";
            this.CustomVisionIterationId_Textbox.Size = new System.Drawing.Size(246, 22);
            this.CustomVisionIterationId_Textbox.TabIndex = 29;
            this.CustomVisionIterationId_Textbox.TextChanged += new System.EventHandler(this.CustomVisionIterationId_Textbox_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(253, 117);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(150, 17);
            this.label8.TabIndex = 28;
            this.label8.Text = "Custom Vision API Key";
            // 
            // CustomVisionApiKey_Textbox
            // 
            this.CustomVisionApiKey_Textbox.Location = new System.Drawing.Point(256, 138);
            this.CustomVisionApiKey_Textbox.Margin = new System.Windows.Forms.Padding(4);
            this.CustomVisionApiKey_Textbox.Name = "CustomVisionApiKey_Textbox";
            this.CustomVisionApiKey_Textbox.Size = new System.Drawing.Size(246, 22);
            this.CustomVisionApiKey_Textbox.TabIndex = 27;
            this.CustomVisionApiKey_Textbox.TextChanged += new System.EventHandler(this.CustomVisionApiKey_Textbox_TextChanged);
            // 
            // LuisEndpointTextBox
            // 
            this.LuisEndpointTextBox.Location = new System.Drawing.Point(816, 39);
            this.LuisEndpointTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.LuisEndpointTextBox.Name = "LuisEndpointTextBox";
            this.LuisEndpointTextBox.Size = new System.Drawing.Size(246, 22);
            this.LuisEndpointTextBox.TabIndex = 26;
            this.LuisEndpointTextBox.TextChanged += new System.EventHandler(this.LuisEndpointTextBox_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(813, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 17);
            this.label7.TabIndex = 25;
            this.label7.Text = "LUIS Endpoint";
            // 
            // SpeechApiKeyTextBox
            // 
            this.SpeechApiKeyTextBox.Location = new System.Drawing.Point(557, 87);
            this.SpeechApiKeyTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.SpeechApiKeyTextBox.Name = "SpeechApiKeyTextBox";
            this.SpeechApiKeyTextBox.Size = new System.Drawing.Size(246, 22);
            this.SpeechApiKeyTextBox.TabIndex = 24;
            this.SpeechApiKeyTextBox.TextChanged += new System.EventHandler(this.SpeechApiKeyTextBox_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(554, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(109, 17);
            this.label6.TabIndex = 23;
            this.label6.Text = "Speech API Key";
            // 
            // EmotionApiKey
            // 
            this.EmotionApiKey.Location = new System.Drawing.Point(557, 39);
            this.EmotionApiKey.Margin = new System.Windows.Forms.Padding(4);
            this.EmotionApiKey.Name = "EmotionApiKey";
            this.EmotionApiKey.Size = new System.Drawing.Size(246, 22);
            this.EmotionApiKey.TabIndex = 21;
            this.EmotionApiKey.TextChanged += new System.EventHandler(this.EmotionApiKey_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(554, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 17);
            this.label5.TabIndex = 20;
            this.label5.Text = "Emotion Api Key";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(253, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(168, 17);
            this.label4.TabIndex = 19;
            this.label4.Text = "Speaker Recognition Key";
            // 
            // SpeakerRecognitionApiKey
            // 
            this.SpeakerRecognitionApiKey.Location = new System.Drawing.Point(256, 85);
            this.SpeakerRecognitionApiKey.Margin = new System.Windows.Forms.Padding(4);
            this.SpeakerRecognitionApiKey.Name = "SpeakerRecognitionApiKey";
            this.SpeakerRecognitionApiKey.Size = new System.Drawing.Size(246, 22);
            this.SpeakerRecognitionApiKey.TabIndex = 18;
            this.SpeakerRecognitionApiKey.TextChanged += new System.EventHandler(this.speakerRecognitionApiKey_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(253, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 17);
            this.label2.TabIndex = 17;
            this.label2.Text = "Face API Key";
            // 
            // FaceApiKey
            // 
            this.FaceApiKey.Location = new System.Drawing.Point(256, 39);
            this.FaceApiKey.Margin = new System.Windows.Forms.Padding(4);
            this.FaceApiKey.Name = "FaceApiKey";
            this.FaceApiKey.Size = new System.Drawing.Size(246, 22);
            this.FaceApiKey.TabIndex = 16;
            this.FaceApiKey.TextChanged += new System.EventHandler(this.faceApiKey_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 17);
            this.label3.TabIndex = 12;
            this.label3.Text = "Tracking sensitivity";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(6, 52);
            this.trackBar1.Maximum = 50;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(230, 56);
            this.trackBar1.TabIndex = 11;
            this.trackBar1.Tag = "";
            this.trackBar1.Value = 1;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // ReleaseServosButton
            // 
            this.ReleaseServosButton.Location = new System.Drawing.Point(958, 13);
            this.ReleaseServosButton.Name = "ReleaseServosButton";
            this.ReleaseServosButton.Size = new System.Drawing.Size(146, 40);
            this.ReleaseServosButton.TabIndex = 22;
            this.ReleaseServosButton.Text = "Release Servos";
            this.ReleaseServosButton.UseVisualStyleBackColor = true;
            this.ReleaseServosButton.Click += new System.EventHandler(this.ReleaseServosButton_Click);
            // 
            // HeadTrackingButton
            // 
            this.HeadTrackingButton.Location = new System.Drawing.Point(449, 7);
            this.HeadTrackingButton.Margin = new System.Windows.Forms.Padding(4);
            this.HeadTrackingButton.Name = "HeadTrackingButton";
            this.HeadTrackingButton.Size = new System.Drawing.Size(87, 42);
            this.HeadTrackingButton.TabIndex = 10;
            this.HeadTrackingButton.Tag = "Start Head Tracking|Stop Head Tracking";
            this.HeadTrackingButton.Text = "Start Head Tracking";
            this.HeadTrackingButton.UseVisualStyleBackColor = true;
            this.HeadTrackingButton.Click += new System.EventHandler(this.HeadTrackingButton_Click);
            // 
            // StartEZBButton
            // 
            this.StartEZBButton.Location = new System.Drawing.Point(179, 8);
            this.StartEZBButton.Margin = new System.Windows.Forms.Padding(4);
            this.StartEZBButton.Name = "StartEZBButton";
            this.StartEZBButton.Size = new System.Drawing.Size(81, 42);
            this.StartEZBButton.TabIndex = 15;
            this.StartEZBButton.Tag = "Connect to EZB|Disconnect from EZB";
            this.StartEZBButton.Text = "Connect to EZB";
            this.StartEZBButton.UseVisualStyleBackColor = true;
            this.StartEZBButton.Click += new System.EventHandler(this.StartEZBButton_Click);
            // 
            // ListenButton
            // 
            this.ListenButton.Location = new System.Drawing.Point(551, 8);
            this.ListenButton.Margin = new System.Windows.Forms.Padding(4);
            this.ListenButton.Name = "ListenButton";
            this.ListenButton.Size = new System.Drawing.Size(85, 42);
            this.ListenButton.TabIndex = 16;
            this.ListenButton.Tag = "Start Head Tracking|Stop Head Tracking";
            this.ListenButton.Text = "Recognize Voice";
            this.ListenButton.UseVisualStyleBackColor = true;
            this.ListenButton.Click += new System.EventHandler(this.ListenButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(649, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 42);
            this.button1.TabIndex = 17;
            this.button1.Text = "Intro";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(739, 11);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(98, 40);
            this.button2.TabIndex = 18;
            this.button2.Text = "Objection";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // SayCommandButton
            // 
            this.SayCommandButton.Location = new System.Drawing.Point(844, 11);
            this.SayCommandButton.Margin = new System.Windows.Forms.Padding(4);
            this.SayCommandButton.Name = "SayCommandButton";
            this.SayCommandButton.Size = new System.Drawing.Size(107, 42);
            this.SayCommandButton.TabIndex = 19;
            this.SayCommandButton.Tag = "Start Head Tracking|Stop Head Tracking";
            this.SayCommandButton.Text = "Say Command";
            this.SayCommandButton.UseVisualStyleBackColor = true;
            this.SayCommandButton.Click += new System.EventHandler(this.SayCommandButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1130, 845);
            this.Controls.Add(this.SayCommandButton);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ListenButton);
            this.Controls.Add(this.ReleaseServosButton);
            this.Controls.Add(this.StartOrStopVideoRecordingButton);
            this.Controls.Add(this.StartEZBButton);
            this.Controls.Add(this.HeadTrackingButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.Video2GB);
            this.Controls.Add(this.FpsLabel);
            this.Controls.Add(this.InfoGB);
            this.Controls.Add(this.fpsLabelLabel);
            this.Controls.Add(this.Video1GB);
            this.Controls.Add(this.StartCameraButton);
            this.Controls.Add(this.IpAddressTB);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Video1GB.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.InfoGB.ResumeLayout(false);
            this.InfoGB.PerformLayout();
            this.Video2GB.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox IpAddressTB;
        private System.Windows.Forms.Button StartCameraButton;
        private System.Windows.Forms.GroupBox Video1GB;
        private System.Windows.Forms.Label FpsLabel;
        private System.Windows.Forms.Label fpsLabelLabel;
        private System.Windows.Forms.GroupBox InfoGB;
        private System.Windows.Forms.TextBox DebugTextBox;
        private System.Windows.Forms.Timer OneSecondTimer;
        private System.Windows.Forms.GroupBox Video2GB;
        private System.Windows.Forms.Panel Video2P;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button StartOrStopVideoRecordingButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button HeadTrackingButton;
        private System.Windows.Forms.Button StartEZBButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox SpeakerRecognitionApiKey;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox FaceApiKey;
        private System.Windows.Forms.Button ListenButton;
        private System.Windows.Forms.TextBox EmotionApiKey;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button ReleaseServosButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button SayCommandButton;
        private System.Windows.Forms.TextBox LuisEndpointTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox SpeechApiKeyTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox CustomVisionProjectID_Textbox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox CustomVisionPredictionKey_TextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox CustomVisionIterationId_Textbox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox CustomVisionApiKey_Textbox;
    }
}

