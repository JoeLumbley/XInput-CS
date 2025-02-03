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
            RumbleGroupBox = new GroupBox();
            LabelTimeToVibe = new Label();
            NumericUpDownTimeToVib = new NumericUpDown();
            LabelDPad = new Label();
            LabelLeftBumper = new Label();
            LabelRightBumper = new Label();
            LabelLeftThumbButton = new Label();
            LabelRightThumbButton = new Label();
            LabelBack = new Label();
            LabelStart = new Label();
            groupBox2 = new GroupBox();
            groupBox3 = new GroupBox();
            LabelController3Status = new Label();
            LabelController2Status = new Label();
            LabelController1Status = new Label();
            LabelController0Status = new Label();
            ((System.ComponentModel.ISupportInitialize)NumControllerToVib).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TrackBarSpeed).BeginInit();
            RumbleGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NumericUpDownTimeToVib).BeginInit();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
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
            LabelButtons.Location = new Point(558, 79);
            LabelButtons.Name = "LabelButtons";
            LabelButtons.Size = new Size(109, 23);
            LabelButtons.TabIndex = 1;
            LabelButtons.Text = "LabelButtons";
            // 
            // LabelLeftTrigger
            // 
            LabelLeftTrigger.AutoSize = true;
            LabelLeftTrigger.Location = new Point(14, 35);
            LabelLeftTrigger.Name = "LabelLeftTrigger";
            LabelLeftTrigger.Size = new Size(131, 23);
            LabelLeftTrigger.TabIndex = 2;
            LabelLeftTrigger.Text = "LabelLeftTrigger";
            // 
            // LabelRightTrigger
            // 
            LabelRightTrigger.AutoSize = true;
            LabelRightTrigger.Location = new Point(558, 35);
            LabelRightTrigger.Name = "LabelRightTrigger";
            LabelRightTrigger.Size = new Size(143, 23);
            LabelRightTrigger.TabIndex = 3;
            LabelRightTrigger.Text = "LabelRightTrigger";
            // 
            // LabelLeftThumbX
            // 
            LabelLeftThumbX.AutoSize = true;
            LabelLeftThumbX.Location = new Point(14, 79);
            LabelLeftThumbX.Name = "LabelLeftThumbX";
            LabelLeftThumbX.Size = new Size(142, 23);
            LabelLeftThumbX.TabIndex = 4;
            LabelLeftThumbX.Text = "LabelLeftThumbX";
            // 
            // LabelLeftThumbY
            // 
            LabelLeftThumbY.AutoSize = true;
            LabelLeftThumbY.Location = new Point(14, 102);
            LabelLeftThumbY.Name = "LabelLeftThumbY";
            LabelLeftThumbY.Size = new Size(141, 23);
            LabelLeftThumbY.TabIndex = 5;
            LabelLeftThumbY.Text = "LabelLeftThumbY";
            // 
            // LabelRightThumbX
            // 
            LabelRightThumbX.AutoSize = true;
            LabelRightThumbX.Location = new Point(558, 102);
            LabelRightThumbX.Name = "LabelRightThumbX";
            LabelRightThumbX.Size = new Size(154, 23);
            LabelRightThumbX.TabIndex = 6;
            LabelRightThumbX.Text = "LabelRightThumbX";
            // 
            // LabelRightThumbY
            // 
            LabelRightThumbY.AutoSize = true;
            LabelRightThumbY.Location = new Point(558, 125);
            LabelRightThumbY.Name = "LabelRightThumbY";
            LabelRightThumbY.Size = new Size(153, 23);
            LabelRightThumbY.TabIndex = 7;
            LabelRightThumbY.Text = "LabelRightThumbY";
            // 
            // NumControllerToVib
            // 
            NumControllerToVib.Location = new Point(107, 32);
            NumControllerToVib.Maximum = new decimal(new int[] { 3, 0, 0, 0 });
            NumControllerToVib.Name = "NumControllerToVib";
            NumControllerToVib.Size = new Size(84, 30);
            NumControllerToVib.TabIndex = 8;
            // 
            // ButtonVibrateLeft
            // 
            ButtonVibrateLeft.Location = new Point(14, 74);
            ButtonVibrateLeft.Name = "ButtonVibrateLeft";
            ButtonVibrateLeft.Size = new Size(124, 31);
            ButtonVibrateLeft.TabIndex = 9;
            ButtonVibrateLeft.Text = "Vibrate Left";
            ButtonVibrateLeft.UseVisualStyleBackColor = true;
            ButtonVibrateLeft.Click += ButtonVibrateLeft_Click;
            // 
            // ButtonVibrateRight
            // 
            ButtonVibrateRight.Location = new Point(396, 74);
            ButtonVibrateRight.Name = "ButtonVibrateRight";
            ButtonVibrateRight.Size = new Size(124, 31);
            ButtonVibrateRight.TabIndex = 10;
            ButtonVibrateRight.Text = "Vibrate Right";
            ButtonVibrateRight.UseVisualStyleBackColor = true;
            ButtonVibrateRight.Click += ButtonVibrateRight_Click;
            // 
            // TrackBarSpeed
            // 
            TrackBarSpeed.LargeChange = 16384;
            TrackBarSpeed.Location = new Point(156, 76);
            TrackBarSpeed.Maximum = 65535;
            TrackBarSpeed.Name = "TrackBarSpeed";
            TrackBarSpeed.Size = new Size(225, 64);
            TrackBarSpeed.SmallChange = 8192;
            TrackBarSpeed.TabIndex = 11;
            TrackBarSpeed.TickFrequency = 16384;
            TrackBarSpeed.Scroll += TrackBarSpeed_Scroll;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 34);
            label1.Name = "label1";
            label1.Size = new Size(86, 23);
            label1.TabIndex = 12;
            label1.Text = "Controller";
            // 
            // LabelSpeed
            // 
            LabelSpeed.AutoSize = true;
            LabelSpeed.Location = new Point(214, 111);
            LabelSpeed.Name = "LabelSpeed";
            LabelSpeed.Size = new Size(97, 23);
            LabelSpeed.TabIndex = 13;
            LabelSpeed.Text = "LabelSpeed";
            // 
            // RumbleGroupBox
            // 
            RumbleGroupBox.Controls.Add(LabelTimeToVibe);
            RumbleGroupBox.Controls.Add(NumericUpDownTimeToVib);
            RumbleGroupBox.Controls.Add(label1);
            RumbleGroupBox.Controls.Add(LabelSpeed);
            RumbleGroupBox.Controls.Add(NumControllerToVib);
            RumbleGroupBox.Controls.Add(ButtonVibrateLeft);
            RumbleGroupBox.Controls.Add(TrackBarSpeed);
            RumbleGroupBox.Controls.Add(ButtonVibrateRight);
            RumbleGroupBox.Location = new Point(15, 201);
            RumbleGroupBox.Name = "RumbleGroupBox";
            RumbleGroupBox.Size = new Size(546, 150);
            RumbleGroupBox.TabIndex = 14;
            RumbleGroupBox.TabStop = false;
            RumbleGroupBox.Text = "Rumble";
            // 
            // LabelTimeToVibe
            // 
            LabelTimeToVibe.AutoSize = true;
            LabelTimeToVibe.Location = new Point(275, 34);
            LabelTimeToVibe.Name = "LabelTimeToVibe";
            LabelTimeToVibe.Size = new Size(74, 23);
            LabelTimeToVibe.TabIndex = 15;
            LabelTimeToVibe.Text = "Time ms";
            // 
            // NumericUpDownTimeToVib
            // 
            NumericUpDownTimeToVib.Increment = new decimal(new int[] { 25, 0, 0, 0 });
            NumericUpDownTimeToVib.Location = new Point(358, 32);
            NumericUpDownTimeToVib.Maximum = new decimal(new int[] { 5000, 0, 0, 0 });
            NumericUpDownTimeToVib.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            NumericUpDownTimeToVib.Name = "NumericUpDownTimeToVib";
            NumericUpDownTimeToVib.Size = new Size(162, 30);
            NumericUpDownTimeToVib.TabIndex = 14;
            NumericUpDownTimeToVib.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            NumericUpDownTimeToVib.ValueChanged += NumericUpDownTimeToVib_ValueChanged;
            // 
            // LabelDPad
            // 
            LabelDPad.AutoSize = true;
            LabelDPad.Location = new Point(14, 148);
            LabelDPad.Name = "LabelDPad";
            LabelDPad.Size = new Size(90, 23);
            LabelDPad.TabIndex = 15;
            LabelDPad.Text = "LabelDPad";
            // 
            // LabelLeftBumper
            // 
            LabelLeftBumper.AutoSize = true;
            LabelLeftBumper.Location = new Point(14, 58);
            LabelLeftBumper.Name = "LabelLeftBumper";
            LabelLeftBumper.Size = new Size(138, 23);
            LabelLeftBumper.TabIndex = 16;
            LabelLeftBumper.Text = "LabelLeftBumper";
            // 
            // LabelRightBumper
            // 
            LabelRightBumper.AutoSize = true;
            LabelRightBumper.Location = new Point(558, 58);
            LabelRightBumper.Name = "LabelRightBumper";
            LabelRightBumper.Size = new Size(150, 23);
            LabelRightBumper.TabIndex = 17;
            LabelRightBumper.Text = "LabelRightBumper";
            // 
            // LabelLeftThumbButton
            // 
            LabelLeftThumbButton.AutoSize = true;
            LabelLeftThumbButton.Location = new Point(14, 125);
            LabelLeftThumbButton.Name = "LabelLeftThumbButton";
            LabelLeftThumbButton.Size = new Size(184, 23);
            LabelLeftThumbButton.TabIndex = 18;
            LabelLeftThumbButton.Text = "LabelLeftThumbButton";
            // 
            // LabelRightThumbButton
            // 
            LabelRightThumbButton.AutoSize = true;
            LabelRightThumbButton.Location = new Point(558, 148);
            LabelRightThumbButton.Name = "LabelRightThumbButton";
            LabelRightThumbButton.Size = new Size(196, 23);
            LabelRightThumbButton.TabIndex = 19;
            LabelRightThumbButton.Text = "LabelRightThumbButton";
            // 
            // LabelBack
            // 
            LabelBack.AutoSize = true;
            LabelBack.Location = new Point(275, 79);
            LabelBack.Name = "LabelBack";
            LabelBack.Size = new Size(85, 23);
            LabelBack.TabIndex = 20;
            LabelBack.Text = "LabelBack";
            // 
            // LabelStart
            // 
            LabelStart.AutoSize = true;
            LabelStart.Location = new Point(416, 79);
            LabelStart.Name = "LabelStart";
            LabelStart.Size = new Size(85, 23);
            LabelStart.TabIndex = 21;
            LabelStart.Text = "LabelStart";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(LabelStart);
            groupBox2.Controls.Add(LabelButtons);
            groupBox2.Controls.Add(LabelBack);
            groupBox2.Controls.Add(LabelLeftTrigger);
            groupBox2.Controls.Add(LabelRightThumbButton);
            groupBox2.Controls.Add(LabelRightTrigger);
            groupBox2.Controls.Add(LabelLeftThumbButton);
            groupBox2.Controls.Add(LabelLeftThumbX);
            groupBox2.Controls.Add(LabelRightBumper);
            groupBox2.Controls.Add(LabelLeftThumbY);
            groupBox2.Controls.Add(LabelLeftBumper);
            groupBox2.Controls.Add(LabelRightThumbX);
            groupBox2.Controls.Add(LabelDPad);
            groupBox2.Controls.Add(LabelRightThumbY);
            groupBox2.Location = new Point(15, 7);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(849, 187);
            groupBox2.TabIndex = 22;
            groupBox2.TabStop = false;
            groupBox2.Text = "Monitor - Press any button on your controller";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(LabelController3Status);
            groupBox3.Controls.Add(LabelController2Status);
            groupBox3.Controls.Add(LabelController1Status);
            groupBox3.Controls.Add(LabelController0Status);
            groupBox3.Location = new Point(583, 201);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(281, 149);
            groupBox3.TabIndex = 23;
            groupBox3.TabStop = false;
            groupBox3.Text = "Status";
            // 
            // LabelController3Status
            // 
            LabelController3Status.AutoSize = true;
            LabelController3Status.Location = new Point(21, 104);
            LabelController3Status.Name = "LabelController3Status";
            LabelController3Status.Size = new Size(181, 23);
            LabelController3Status.TabIndex = 24;
            LabelController3Status.Text = "LabelController3Status";
            // 
            // LabelController2Status
            // 
            LabelController2Status.AutoSize = true;
            LabelController2Status.Location = new Point(21, 81);
            LabelController2Status.Name = "LabelController2Status";
            LabelController2Status.Size = new Size(181, 23);
            LabelController2Status.TabIndex = 24;
            LabelController2Status.Text = "LabelController2Status";
            // 
            // LabelController1Status
            // 
            LabelController1Status.AutoSize = true;
            LabelController1Status.Location = new Point(21, 56);
            LabelController1Status.Name = "LabelController1Status";
            LabelController1Status.Size = new Size(181, 23);
            LabelController1Status.TabIndex = 24;
            LabelController1Status.Text = "LabelController1Status";
            // 
            // LabelController0Status
            // 
            LabelController0Status.AutoSize = true;
            LabelController0Status.Location = new Point(21, 33);
            LabelController0Status.Name = "LabelController0Status";
            LabelController0Status.Size = new Size(181, 23);
            LabelController0Status.TabIndex = 24;
            LabelController0Status.Text = "LabelController0Status";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(882, 366);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(RumbleGroupBox);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)NumControllerToVib).EndInit();
            ((System.ComponentModel.ISupportInitialize)TrackBarSpeed).EndInit();
            RumbleGroupBox.ResumeLayout(false);
            RumbleGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)NumericUpDownTimeToVib).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
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
        private GroupBox RumbleGroupBox;
        private Label LabelDPad;
        private Label LabelLeftBumper;
        private Label LabelRightBumper;
        private Label LabelLeftThumbButton;
        private Label LabelRightThumbButton;
        private Label LabelBack;
        private Label LabelStart;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Label LabelController0Status;
        private Label LabelController1Status;
        private Label LabelController2Status;
        private Label LabelController3Status;
        private Label LabelTimeToVibe;
        private NumericUpDown NumericUpDownTimeToVib;
    }
}
