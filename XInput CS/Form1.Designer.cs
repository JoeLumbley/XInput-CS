namespace XInput_CS
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            timer1 = new System.Windows.Forms.Timer(components);
            LabelButtons = new Label();
            LabelLeftTrigger = new Label();
            LabelRightTrigger = new Label();
            LabelLeftThumbX = new Label();
            LabelLeftThumbY = new Label();
            LabelRightThumbX = new Label();
            LabelRightThumbY = new Label();
            NumControllerToVib = new NumericUpDown();
            ButtonVibrateLeft = new Button();
            ButtonVibrateRight = new Button();
            TrackBarSpeed = new TrackBar();
            label1 = new Label();
            LabelSpeed = new Label();
            groupBox1 = new GroupBox();
            LabelDPad = new Label();
            LabelLeftBumper = new Label();
            LabelRightBumper = new Label();
            LabelLeftThumbButton = new Label();
            LabelRightThumbButton = new Label();
            LabelBack = new Label();
            LabelStart = new Label();
            ((System.ComponentModel.ISupportInitialize)NumControllerToVib).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TrackBarSpeed).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Tick += timer1_Tick;
            // 
            // LabelButtons
            // 
            LabelButtons.AutoSize = true;
            LabelButtons.Location = new Point(616, 57);
            LabelButtons.Name = "LabelButtons";
            LabelButtons.Size = new Size(114, 25);
            LabelButtons.TabIndex = 1;
            LabelButtons.Text = "LabelButtons";
            // 
            // LabelLeftTrigger
            // 
            LabelLeftTrigger.AutoSize = true;
            LabelLeftTrigger.Location = new Point(12, 9);
            LabelLeftTrigger.Name = "LabelLeftTrigger";
            LabelLeftTrigger.Size = new Size(136, 25);
            LabelLeftTrigger.TabIndex = 2;
            LabelLeftTrigger.Text = "LabelLeftTrigger";
            // 
            // LabelRightTrigger
            // 
            LabelRightTrigger.AutoSize = true;
            LabelRightTrigger.Location = new Point(616, 9);
            LabelRightTrigger.Name = "LabelRightTrigger";
            LabelRightTrigger.Size = new Size(149, 25);
            LabelRightTrigger.TabIndex = 3;
            LabelRightTrigger.Text = "LabelRightTrigger";
            // 
            // LabelLeftThumbX
            // 
            LabelLeftThumbX.AutoSize = true;
            LabelLeftThumbX.Location = new Point(12, 57);
            LabelLeftThumbX.Name = "LabelLeftThumbX";
            LabelLeftThumbX.Size = new Size(149, 25);
            LabelLeftThumbX.TabIndex = 4;
            LabelLeftThumbX.Text = "LabelLeftThumbX";
            // 
            // LabelLeftThumbY
            // 
            LabelLeftThumbY.AutoSize = true;
            LabelLeftThumbY.Location = new Point(12, 82);
            LabelLeftThumbY.Name = "LabelLeftThumbY";
            LabelLeftThumbY.Size = new Size(148, 25);
            LabelLeftThumbY.TabIndex = 5;
            LabelLeftThumbY.Text = "LabelLeftThumbY";
            // 
            // LabelRightThumbX
            // 
            LabelRightThumbX.AutoSize = true;
            LabelRightThumbX.Location = new Point(616, 82);
            LabelRightThumbX.Name = "LabelRightThumbX";
            LabelRightThumbX.Size = new Size(162, 25);
            LabelRightThumbX.TabIndex = 6;
            LabelRightThumbX.Text = "LabelRightThumbX";
            // 
            // LabelRightThumbY
            // 
            LabelRightThumbY.AutoSize = true;
            LabelRightThumbY.Location = new Point(616, 107);
            LabelRightThumbY.Name = "LabelRightThumbY";
            LabelRightThumbY.Size = new Size(161, 25);
            LabelRightThumbY.TabIndex = 7;
            LabelRightThumbY.Text = "LabelRightThumbY";
            // 
            // NumControllerToVib
            // 
            NumControllerToVib.Location = new Point(132, 31);
            NumControllerToVib.Maximum = new decimal(new int[] { 3, 0, 0, 0 });
            NumControllerToVib.Name = "NumControllerToVib";
            NumControllerToVib.Size = new Size(93, 31);
            NumControllerToVib.TabIndex = 8;
            // 
            // ButtonVibrateLeft
            // 
            ButtonVibrateLeft.Location = new Point(14, 80);
            ButtonVibrateLeft.Name = "ButtonVibrateLeft";
            ButtonVibrateLeft.Size = new Size(138, 34);
            ButtonVibrateLeft.TabIndex = 9;
            ButtonVibrateLeft.Text = "Vibrate Left";
            ButtonVibrateLeft.UseVisualStyleBackColor = true;
            ButtonVibrateLeft.Click += ButtonVibrateLeft_Click;
            // 
            // ButtonVibrateRight
            // 
            ButtonVibrateRight.Location = new Point(413, 80);
            ButtonVibrateRight.Name = "ButtonVibrateRight";
            ButtonVibrateRight.Size = new Size(138, 34);
            ButtonVibrateRight.TabIndex = 10;
            ButtonVibrateRight.Text = "Vibrate Right";
            ButtonVibrateRight.UseVisualStyleBackColor = true;
            ButtonVibrateRight.Click += ButtonVibrateRight_Click;
            // 
            // TrackBarSpeed
            // 
            TrackBarSpeed.LargeChange = 16384;
            TrackBarSpeed.Location = new Point(158, 83);
            TrackBarSpeed.Maximum = 65535;
            TrackBarSpeed.Name = "TrackBarSpeed";
            TrackBarSpeed.Size = new Size(250, 69);
            TrackBarSpeed.SmallChange = 8192;
            TrackBarSpeed.TabIndex = 11;
            TrackBarSpeed.TickFrequency = 16384;
            TrackBarSpeed.Scroll += TrackBarSpeed_Scroll;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(29, 33);
            label1.Name = "label1";
            label1.Size = new Size(94, 25);
            label1.TabIndex = 12;
            label1.Text = "Controller:";
            // 
            // LabelSpeed
            // 
            LabelSpeed.AutoSize = true;
            LabelSpeed.Location = new Point(223, 121);
            LabelSpeed.Name = "LabelSpeed";
            LabelSpeed.Size = new Size(103, 25);
            LabelSpeed.TabIndex = 13;
            LabelSpeed.Text = "LabelSpeed";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(LabelSpeed);
            groupBox1.Controls.Add(NumControllerToVib);
            groupBox1.Controls.Add(ButtonVibrateLeft);
            groupBox1.Controls.Add(TrackBarSpeed);
            groupBox1.Controls.Add(ButtonVibrateRight);
            groupBox1.Location = new Point(17, 194);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(566, 163);
            groupBox1.TabIndex = 14;
            groupBox1.TabStop = false;
            groupBox1.Text = "Rumble";
            // 
            // LabelDPad
            // 
            LabelDPad.AutoSize = true;
            LabelDPad.Location = new Point(12, 132);
            LabelDPad.Name = "LabelDPad";
            LabelDPad.Size = new Size(95, 25);
            LabelDPad.TabIndex = 15;
            LabelDPad.Text = "LabelDPad";
            // 
            // LabelLeftBumper
            // 
            LabelLeftBumper.AutoSize = true;
            LabelLeftBumper.Location = new Point(12, 34);
            LabelLeftBumper.Name = "LabelLeftBumper";
            LabelLeftBumper.Size = new Size(144, 25);
            LabelLeftBumper.TabIndex = 16;
            LabelLeftBumper.Text = "LabelLeftBumper";
            // 
            // LabelRightBumper
            // 
            LabelRightBumper.AutoSize = true;
            LabelRightBumper.Location = new Point(616, 34);
            LabelRightBumper.Name = "LabelRightBumper";
            LabelRightBumper.Size = new Size(157, 25);
            LabelRightBumper.TabIndex = 17;
            LabelRightBumper.Text = "LabelRightBumper";
            // 
            // LabelLeftThumbButton
            // 
            LabelLeftThumbButton.AutoSize = true;
            LabelLeftThumbButton.Location = new Point(12, 107);
            LabelLeftThumbButton.Name = "LabelLeftThumbButton";
            LabelLeftThumbButton.Size = new Size(191, 25);
            LabelLeftThumbButton.TabIndex = 18;
            LabelLeftThumbButton.Text = "LabelLeftThumbButton";
            // 
            // LabelRightThumbButton
            // 
            LabelRightThumbButton.AutoSize = true;
            LabelRightThumbButton.Location = new Point(616, 132);
            LabelRightThumbButton.Name = "LabelRightThumbButton";
            LabelRightThumbButton.Size = new Size(204, 25);
            LabelRightThumbButton.TabIndex = 19;
            LabelRightThumbButton.Text = "LabelRightThumbButton";
            // 
            // LabelBack
            // 
            LabelBack.AutoSize = true;
            LabelBack.Location = new Point(302, 57);
            LabelBack.Name = "LabelBack";
            LabelBack.Size = new Size(89, 25);
            LabelBack.TabIndex = 20;
            LabelBack.Text = "LabelBack";
            // 
            // LabelStart
            // 
            LabelStart.AutoSize = true;
            LabelStart.Location = new Point(458, 57);
            LabelStart.Name = "LabelStart";
            LabelStart.Size = new Size(89, 25);
            LabelStart.TabIndex = 21;
            LabelStart.Text = "LabelStart";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(961, 450);
            Controls.Add(LabelStart);
            Controls.Add(LabelBack);
            Controls.Add(LabelRightThumbButton);
            Controls.Add(LabelLeftThumbButton);
            Controls.Add(LabelRightBumper);
            Controls.Add(LabelLeftBumper);
            Controls.Add(LabelDPad);
            Controls.Add(groupBox1);
            Controls.Add(LabelRightThumbY);
            Controls.Add(LabelRightThumbX);
            Controls.Add(LabelLeftThumbY);
            Controls.Add(LabelLeftThumbX);
            Controls.Add(LabelRightTrigger);
            Controls.Add(LabelLeftTrigger);
            Controls.Add(LabelButtons);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)NumControllerToVib).EndInit();
            ((System.ComponentModel.ISupportInitialize)TrackBarSpeed).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private Label LabelButtons;
        private Label LabelLeftTrigger;
        private Label LabelRightTrigger;
        private Label LabelLeftThumbX;
        private Label LabelLeftThumbY;
        private Label LabelRightThumbX;
        private Label LabelRightThumbY;
        private NumericUpDown NumControllerToVib;
        private Button ButtonVibrateLeft;
        private Button ButtonVibrateRight;
        private TrackBar TrackBarSpeed;
        private Label label1;
        private Label LabelSpeed;
        private GroupBox groupBox1;
        private Label LabelDPad;
        private Label LabelLeftBumper;
        private Label LabelRightBumper;
        private Label LabelLeftThumbButton;
        private Label LabelRightThumbButton;
        private Label LabelBack;
        private Label LabelStart;
    }
}
