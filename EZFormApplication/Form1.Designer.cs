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
            this.label1 = new System.Windows.Forms.Label();
            this.IpAddressTB = new System.Windows.Forms.TextBox();
            this.TakeAPicButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.DebugTextBox = new System.Windows.Forms.TextBox();
            this.ImageFileNameTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP";
            // 
            // IpAddressTB
            // 
            this.IpAddressTB.Location = new System.Drawing.Point(35, 15);
            this.IpAddressTB.Name = "IpAddressTB";
            this.IpAddressTB.Size = new System.Drawing.Size(94, 20);
            this.IpAddressTB.TabIndex = 1;
            this.IpAddressTB.Text = "192.168.18.85";
            // 
            // TakeAPicButton
            // 
            this.TakeAPicButton.Location = new System.Drawing.Point(191, 13);
            this.TakeAPicButton.Name = "TakeAPicButton";
            this.TakeAPicButton.Size = new System.Drawing.Size(75, 23);
            this.TakeAPicButton.TabIndex = 2;
            this.TakeAPicButton.Text = "Take a PIC";
            this.TakeAPicButton.UseVisualStyleBackColor = true;
            this.TakeAPicButton.Click += new System.EventHandler(this.TakeAPicButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(15, 98);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(160, 120);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // DebugTextBox
            // 
            this.DebugTextBox.Location = new System.Drawing.Point(15, 224);
            this.DebugTextBox.Multiline = true;
            this.DebugTextBox.Name = "DebugTextBox";
            this.DebugTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.DebugTextBox.Size = new System.Drawing.Size(414, 98);
            this.DebugTextBox.TabIndex = 4;
            // 
            // ImageFileNameTB
            // 
            this.ImageFileNameTB.Location = new System.Drawing.Point(73, 54);
            this.ImageFileNameTB.Name = "ImageFileNameTB";
            this.ImageFileNameTB.Size = new System.Drawing.Size(356, 20);
            this.ImageFileNameTB.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Image File";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 354);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ImageFileNameTB);
            this.Controls.Add(this.DebugTextBox);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.TakeAPicButton);
            this.Controls.Add(this.IpAddressTB);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox IpAddressTB;
        private System.Windows.Forms.Button TakeAPicButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox DebugTextBox;
        private System.Windows.Forms.TextBox ImageFileNameTB;
        private System.Windows.Forms.Label label2;
    }
}

