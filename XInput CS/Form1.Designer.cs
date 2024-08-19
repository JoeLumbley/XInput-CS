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
            groupBox2 = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)NumControllerToVib).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TrackBarSpeed).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
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
            LabelButtons.Location = new Point(620, 86);
            LabelButtons.Name = "LabelButtons";
            LabelButtons.Size = new Size(114, 25);
            LabelButtons.TabIndex = 1;
            LabelButtons.Text = "LabelButtons";
            // 
            // LabelLeftTrigger
            // 
            LabelLeftTrigger.AutoSize = true;
            LabelLeftTrigger.Location = new Point(16, 38);
            LabelLeftTrigger.Name = "LabelLeftTrigger";
            LabelLeftTrigger.Size = new Size(136, 25);
            LabelLeftTrigger.TabIndex = 2;
            LabelLeftTrigger.Text = "LabelLeftTrigger";
            // 
            // LabelRightTrigger
            // 
            LabelRightTrigger.AutoSize = true;
            LabelRightTrigger.Location = new Point(620, 38);
            LabelRightTrigger.Name = "LabelRightTrigger";
            LabelRightTrigger.Size = new Size(149, 25);
            LabelRightTrigger.TabIndex = 3;
            LabelRightTrigger.Text = "LabelRightTrigger";
            // 
            // LabelLeftThumbX
            // 
            LabelLeftThumbX.AutoSize = true;
            LabelLeftThumbX.Location = new Point(16, 86);
            LabelLeftThumbX.Name = "LabelLeftThumbX";
            LabelLeftThumbX.Size = new Size(149, 25);
            LabelLeftThumbX.TabIndex = 4;
            LabelLeftThumbX.Text = "LabelLeftThumbX";
            // 
            // LabelLeftThumbY
            // 
            LabelLeftThumbY.AutoSize = true;
            LabelLeftThumbY.Location = new Point(16, 111);
            LabelLeftThumbY.Name = "LabelLeftThumbY";
            LabelLeftThumbY.Size = new Size(148, 25);
            LabelLeftThumbY.TabIndex = 5;
            LabelLeftThumbY.Text = "LabelLeftThumbY";
            // 
            // LabelRightThumbX
            // 
            LabelRightThumbX.AutoSize = true;
            LabelRightThumbX.Location = new Point(620, 111);
            LabelRightThumbX.Name = "LabelRightThumbX";
            LabelRightThumbX.Size = new Size(162, 25);
            LabelRightThumbX.TabIndex = 6;
            LabelRightThumbX.Text = "LabelRightThumbX";
            // 
            // LabelRightThumbY
            // 
            LabelRightThumbY.AutoSize = true;
            LabelRightThumbY.Location = new Point(620, 136);
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
            groupBox1.Location = new Point(17, 218);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(566, 163);
            groupBox1.TabIndex = 14;
            groupBox1.TabStop = false;
            groupBox1.Text = "Rumble";
            // 
            // LabelDPad
            // 
            LabelDPad.AutoSize = true;
            LabelDPad.Location = new Point(16, 161);
            LabelDPad.Name = "LabelDPad";
            LabelDPad.Size = new Size(95, 25);
            LabelDPad.TabIndex = 15;
            LabelDPad.Text = "LabelDPad";
            // 
            // LabelLeftBumper
            // 
            LabelLeftBumper.AutoSize = true;
            LabelLeftBumper.Location = new Point(16, 63);
            LabelLeftBumper.Name = "LabelLeftBumper";
            LabelLeftBumper.Size = new Size(144, 25);
            LabelLeftBumper.TabIndex = 16;
            LabelLeftBumper.Text = "LabelLeftBumper";
            // 
            // LabelRightBumper
            // 
            LabelRightBumper.AutoSize = true;
            LabelRightBumper.Location = new Point(620, 63);
            LabelRightBumper.Name = "LabelRightBumper";
            LabelRightBumper.Size = new Size(157, 25);
            LabelRightBumper.TabIndex = 17;
            LabelRightBumper.Text = "LabelRightBumper";
            // 
            // LabelLeftThumbButton
            // 
            LabelLeftThumbButton.AutoSize = true;
            LabelLeftThumbButton.Location = new Point(16, 136);
            LabelLeftThumbButton.Name = "LabelLeftThumbButton";
            LabelLeftThumbButton.Size = new Size(191, 25);
            LabelLeftThumbButton.TabIndex = 18;
            LabelLeftThumbButton.Text = "LabelLeftThumbButton";
            // 
            // LabelRightThumbButton
            // 
            LabelRightThumbButton.AutoSize = true;
            LabelRightThumbButton.Location = new Point(620, 161);
            LabelRightThumbButton.Name = "LabelRightThumbButton";
            LabelRightThumbButton.Size = new Size(204, 25);
            LabelRightThumbButton.TabIndex = 19;
            LabelRightThumbButton.Text = "LabelRightThumbButton";
            // 
            // LabelBack
            // 
            LabelBack.AutoSize = true;
            LabelBack.Location = new Point(306, 86);
            LabelBack.Name = "LabelBack";
            LabelBack.Size = new Size(89, 25);
            LabelBack.TabIndex = 20;
            LabelBack.Text = "LabelBack";
            // 
            // LabelStart
            // 
            LabelStart.AutoSize = true;
            LabelStart.Location = new Point(462, 86);
            LabelStart.Name = "LabelStart";
            LabelStart.Size = new Size(89, 25);
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
            groupBox2.Location = new Point(17, 8);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(943, 203);
            groupBox2.TabIndex = 22;
            groupBox2.TabStop = false;
            groupBox2.Text = "Monitor - Press any button your controller.";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(980, 399);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)NumControllerToVib).EndInit();
            ((System.ComponentModel.ISupportInitialize)TrackBarSpeed).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
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
        private GroupBox groupBox1;
        private Label LabelDPad;
        private Label LabelLeftBumper;
        private Label LabelRightBumper;
        private Label LabelLeftThumbButton;
        private Label LabelRightThumbButton;
        private Label LabelBack;
        private Label LabelStart;
        private GroupBox groupBox2;
    }
}
