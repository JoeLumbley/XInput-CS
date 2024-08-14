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
            ((System.ComponentModel.ISupportInitialize)NumControllerToVib).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TrackBarSpeed).BeginInit();
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
            LabelButtons.Location = new Point(365, 57);
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
            LabelRightTrigger.Location = new Point(365, 9);
            LabelRightTrigger.Name = "LabelRightTrigger";
            LabelRightTrigger.Size = new Size(149, 25);
            LabelRightTrigger.TabIndex = 3;
            LabelRightTrigger.Text = "LabelRightTrigger";
            // 
            // LabelLeftThumbX
            // 
            LabelLeftThumbX.AutoSize = true;
            LabelLeftThumbX.Location = new Point(12, 93);
            LabelLeftThumbX.Name = "LabelLeftThumbX";
            LabelLeftThumbX.Size = new Size(149, 25);
            LabelLeftThumbX.TabIndex = 4;
            LabelLeftThumbX.Text = "LabelLeftThumbX";
            // 
            // LabelLeftThumbY
            // 
            LabelLeftThumbY.AutoSize = true;
            LabelLeftThumbY.Location = new Point(12, 118);
            LabelLeftThumbY.Name = "LabelLeftThumbY";
            LabelLeftThumbY.Size = new Size(148, 25);
            LabelLeftThumbY.TabIndex = 5;
            LabelLeftThumbY.Text = "LabelLeftThumbY";
            // 
            // LabelRightThumbX
            // 
            LabelRightThumbX.AutoSize = true;
            LabelRightThumbX.Location = new Point(365, 93);
            LabelRightThumbX.Name = "LabelRightThumbX";
            LabelRightThumbX.Size = new Size(162, 25);
            LabelRightThumbX.TabIndex = 6;
            LabelRightThumbX.Text = "LabelRightThumbX";
            // 
            // LabelRightThumbY
            // 
            LabelRightThumbY.AutoSize = true;
            LabelRightThumbY.Location = new Point(365, 118);
            LabelRightThumbY.Name = "LabelRightThumbY";
            LabelRightThumbY.Size = new Size(161, 25);
            LabelRightThumbY.TabIndex = 7;
            LabelRightThumbY.Text = "LabelRightThumbY";
            // 
            // NumControllerToVib
            // 
            NumControllerToVib.Location = new Point(197, 147);
            NumControllerToVib.Maximum = new decimal(new int[] { 3, 0, 0, 0 });
            NumControllerToVib.Name = "NumControllerToVib";
            NumControllerToVib.Size = new Size(138, 31);
            NumControllerToVib.TabIndex = 8;
            // 
            // ButtonVibrateLeft
            // 
            ButtonVibrateLeft.Location = new Point(14, 194);
            ButtonVibrateLeft.Name = "ButtonVibrateLeft";
            ButtonVibrateLeft.Size = new Size(138, 34);
            ButtonVibrateLeft.TabIndex = 9;
            ButtonVibrateLeft.Text = "Vibrate Left";
            ButtonVibrateLeft.UseVisualStyleBackColor = true;
            ButtonVibrateLeft.Click += ButtonVibrateLeft_Click;
            // 
            // ButtonVibrateRight
            // 
            ButtonVibrateRight.Location = new Point(389, 194);
            ButtonVibrateRight.Name = "ButtonVibrateRight";
            ButtonVibrateRight.Size = new Size(138, 34);
            ButtonVibrateRight.TabIndex = 10;
            ButtonVibrateRight.Text = "Vibrate Right";
            ButtonVibrateRight.UseVisualStyleBackColor = true;
            ButtonVibrateRight.Click += ButtonVibrateRight_Click;
            // 
            // TrackBarSpeed
            // 
            TrackBarSpeed.Location = new Point(172, 194);
            TrackBarSpeed.Maximum = 65535;
            TrackBarSpeed.Name = "TrackBarSpeed";
            TrackBarSpeed.Size = new Size(180, 69);
            TrackBarSpeed.TabIndex = 11;
            TrackBarSpeed.TickFrequency = 5555;
            TrackBarSpeed.Scroll += TrackBarSpeed_Scroll;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 149);
            label1.Name = "label1";
            label1.Size = new Size(177, 25);
            label1.TabIndex = 12;
            label1.Text = "Controller to Vibrate:";
            // 
            // LabelSpeed
            // 
            LabelSpeed.AutoSize = true;
            LabelSpeed.Location = new Point(182, 238);
            LabelSpeed.Name = "LabelSpeed";
            LabelSpeed.Size = new Size(103, 25);
            LabelSpeed.TabIndex = 13;
            LabelSpeed.Text = "LabelSpeed";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(LabelSpeed);
            Controls.Add(label1);
            Controls.Add(TrackBarSpeed);
            Controls.Add(ButtonVibrateRight);
            Controls.Add(ButtonVibrateLeft);
            Controls.Add(NumControllerToVib);
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
    }
}
