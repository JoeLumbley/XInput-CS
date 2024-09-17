// XInput CS

// This is an example application that demonstrates the use of Xbox controllers,
// including the vibration effect (rumble).

// MIT License
// Copyright(c) 2023 Joseph W. Lumbley

// Permission Is hereby granted, free Of charge, to any person obtaining a copy
// of this software And associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, And/Or sell
// copies of the Software, And to permit persons to whom the Software Is
// furnished to do so, subject to the following conditions:

// The above copyright notice And this permission notice shall be included In all
// copies Or substantial portions of the Software.

// THE SOFTWARE Is PROVIDED "AS IS", WITHOUT WARRANTY Of ANY KIND, EXPRESS Or
// IMPLIED, INCLUDING BUT Not LIMITED To THE WARRANTIES Of MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE And NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS Or COPYRIGHT HOLDERS BE LIABLE For ANY CLAIM, DAMAGES Or OTHER
// LIABILITY, WHETHER In AN ACTION Of CONTRACT, TORT Or OTHERWISE, ARISING FROM,
// OUT OF Or IN CONNECTION WITH THE SOFTWARE Or THE USE Or OTHER DEALINGS IN THE
// SOFTWARE.

using System.Runtime.InteropServices;

namespace XInput_CS
{
    public partial class Form1 : Form
    {
        [DllImport("XInput1_4.dll")]
        private static extern int XInputGetState(int dwUserIndex, ref XINPUT_STATE pState);

        //// XInput1_4.dll seems to be the current version
        //// XInput9_1_0.dll is maintained primarily for backward compatibility. 
        [StructLayout(LayoutKind.Explicit)]
        public struct XINPUT_STATE
        {
            [FieldOffset(0)]
            public uint dwPacketNumber; // Unsigned 32-bit (4-byte) integer range 0 through 4,294,967,295.
            [FieldOffset(4)]
            public XINPUT_GAMEPAD Gamepad;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct XINPUT_GAMEPAD
        {
            public ushort wButtons; // Unsigned 16-bit (2-byte) integer range 0 through 65,535.
            public byte bLeftTrigger; // Unsigned 8-bit (1-byte) integer range 0 through 255.
            public byte bRightTrigger;
            public short sThumbLX; // Signed 16-bit (2-byte) integer range -32,768 through 32,767.
            public short sThumbLY;
            public short sThumbRX;
            public short sThumbRY;
        }

        private ushort[] ConButtons = new ushort[4];

        private bool[] IsConLeftTriggerNeutral = new bool[4];
        // A boolean is a logical value that is either true or false.

        private bool[] IsConRightTriggerNeutral = new bool[4];

        private bool[] IsConThumbLXNeutral = new bool[4];

        private bool[] IsConThumbLYNeutral = new bool[4];

        private bool[] IsConThumbRXNeutral = new bool[4];

        private bool[] IsConThumbRYNeutral = new bool[4];

        private bool[] IsDPadNeutral = new bool[4];

        private bool[] IsLeftBumperNeutral = new bool[4];

        private bool[] IsRightBumperNeutral = new bool[4];

        private bool[] IsLetterButtonsNeutral = new bool[4];

        private bool[] IsStartButtonsNeutral = new bool[4];

        private bool[] IsBackButtonsNeutral = new bool[4];

        private bool[] IsLeftStickButtonsNeutral = new bool[4];

        private bool[] IsRightStickButtonsNeutral = new bool[4];

        private XINPUT_STATE ControllerPosition;

        // Set the start of the thumbstick neutral zone to 1/2 over.
        private const short NeutralStart = -16384; // -16,384 = -32,768 / 2
        // The thumbstick position must be more than 1/2 over the neutral start to register as moved.
        // A short is a signed 16-bit (2-byte) integer range -32,768 through 32,767. This gives us 65,535 values.

        // Set the end of the thumbstick neutral zone to 1/2 over.
        private const short NeutralEnd = 16384; // 16,383.5 = 32,767 / 2
        // The thumbstick position must be more than 1/2 over the neutral end to register as moved.

        // Set the trigger threshold to 1/4 pull.
        private const byte TriggerThreshold = 64; // 64 = 256 / 4
        // The trigger position must be greater than the trigger threshold to register as pulled.
        // A byte is a unsigned 8-bit (1-byte) integer range 0 through 255. This gives us 256 values.

        private readonly bool[] Connected = new bool[4];

        private DateTime ConnectionStart = DateTime.Now;

        private const int DPadUp = 1;
        private const int DPadDown = 2;

        private const int DPadLeft = 4;
        private const int DPadRight = 8;

        private const int StartButton = 16;
        private const int BackButton = 32;

        private const int LeftStickButton = 64;
        private const int RightStickButton = 128;

        private const int LeftBumperButton = 256;
        private const int RightBumperButton = 512;

        private const int AButton = 4096;
        private const int BButton = 8192;
        private const int XButton = 16384;
        private const int YButton = 32768;

        private bool DPadUpPressed = false;
        private bool DPadDownPressed = false;
        private bool DPadLeftPressed = false;
        private bool DPadRightPressed = false;

        private bool StartButtonPressed = false;
        private bool BackButtonPressed = false;

        private bool LeftStickButtonPressed = false;
        private bool RightStickButtonPressed = false;

        private bool LeftBumperButtonPressed = false;
        private bool RightBumperButtonPressed = false;

        private bool AButtonPressed = false;
        private bool BButtonPressed = false;
        private bool XButtonPressed = false;
        private bool YButtonPressed = false;

        [DllImport("XInput1_4.dll")]
        private static extern int XInputSetState(int playerIndex, ref XINPUT_VIBRATION vibration);

        public struct XINPUT_VIBRATION
        {
            public ushort wLeftMotorSpeed;
            public ushort wRightMotorSpeed;
        }

        private XINPUT_VIBRATION Vibration;

        private DateTime[] LeftVibrateStart = new DateTime[4];

        private DateTime[] RightVibrateStart = new DateTime[4];

        private bool[] IsLeftVibrating = new bool[4];

        private bool[] IsRightVibrating = new bool[4];

        [DllImport("XInput1_4.dll")]
        private static extern int XInputGetBatteryInformation(int playerIndex, byte devType, ref XINPUT_BATTERY_INFORMATION batteryInfo);

        public struct XINPUT_BATTERY_INFORMATION
        {
            public byte BatteryType;
            public byte BatteryLevel;
        }

        public enum BATTERY_TYPE : byte
        {
            DISCONNECTED = 0,
            WIRED = 1,
            ALKALINE = 2,
            NIMH = 3,
            UNKNOWN = 4
        }
        public enum BatteryLevel : byte
        {
            EMPTY = 0,
            LOW = 1,
            MEDIUM = 2,
            FULL = 3
        }

        private XINPUT_BATTERY_INFORMATION batteryInfo;

        private const int BATTERY_DEVTYPE_GAMEPAD = 0;

        private void Form1_Load(object sender, EventArgs e)
        {

            InitializeApp();
        }

        private void InitializeTimer1()
        {
            //The tick frequency in milliseconds.
            //Also called the polling frequency.
            timer1.Interval = 15; // 1000/60 = 16.67 ms
            //To get 60 FPS (Frames Per Second) in milliseconds.
            //We divide 1000 (the number of milliseconds in a second) by 60 the FPS.

            timer1.Start();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateControllerData();

            UpdateVibrateTimer();

        }

        private void ButtonVibrateLeft_Click(object sender, EventArgs e)
        {

            VibrateLeft((int)NumControllerToVib.Value, (ushort)TrackBarSpeed.Value);
        }

        private void ButtonVibrateRight_Click(object sender, EventArgs e)
        {

            VibrateRight((int)NumControllerToVib.Value, (ushort)TrackBarSpeed.Value);
        }

        private void TrackBarSpeed_Scroll(object sender, EventArgs e)
        {

            UpdateSpeedLabel();
        }

        private void UpdateControllerData()
        {
            TimeSpan ElapsedTime = DateTime.Now - ConnectionStart;

            // Every second check for connected controllers.
            if (ElapsedTime.TotalSeconds >= 1)
            {
                for (int controllerNumber = 0; controllerNumber <= 3; controllerNumber++) // Up to 4 controllers
                {
                    Connected[controllerNumber] = IsControllerConnected(controllerNumber);

                    UpdateControllerStatusLabel(controllerNumber);

                }

                ConnectionStart = DateTime.Now;

            }

            for (int controllerNumber = 0; controllerNumber <= 3; controllerNumber++)
            {
                if (Connected[controllerNumber])
                {
                    UpdateControllerState(controllerNumber);

                }

            }

        }

        private void UpdateControllerStatusLabel(int controllerNumber)
        {   // Update the status label based on connection state.

            string status = Connected[controllerNumber] ? "Connected" : "Not Connected";

            string labelText = $"Controller {controllerNumber} {status}";

            switch (controllerNumber)
            {
                case 0:

                    LabelController0Status.Text = labelText;

                    break;

                case 1:

                    LabelController1Status.Text = labelText;

                    break;

                case 2:

                    LabelController2Status.Text = labelText;

                    break;

                case 3:

                    LabelController3Status.Text = labelText;

                    break;

            }

        }

        private void UpdateControllerState(int controllerNumber)
        {
            try
            {
                XInputGetState(controllerNumber, ref ControllerPosition);

                UpdateButtonPosition(controllerNumber);

                UpdateLeftThumbstickPosition(controllerNumber);

                UpdateRightThumbstickPosition(controllerNumber);

                UpdateLeftTriggerPosition(controllerNumber);

                UpdateRightTriggerPosition(controllerNumber);
            }
            catch (Exception ex)
            {   // Something went wrong (An exception occurred).

                DisplayError(ex);

            }

        }

        private void UpdateButtonPosition(int controllerNumber)
        {   // The range of buttons is 0 to 65,535. Unsigned 16-bit (2-byte) integer.

            DPadUpPressed = (ControllerPosition.Gamepad.wButtons & DPadUp) != 0;

            DPadDownPressed = (ControllerPosition.Gamepad.wButtons & DPadDown) != 0;

            DPadLeftPressed = (ControllerPosition.Gamepad.wButtons & DPadLeft) != 0;

            DPadRightPressed = (ControllerPosition.Gamepad.wButtons & DPadRight) != 0;

            StartButtonPressed = (ControllerPosition.Gamepad.wButtons & StartButton) != 0;

            BackButtonPressed = (ControllerPosition.Gamepad.wButtons & BackButton) != 0;

            LeftStickButtonPressed = (ControllerPosition.Gamepad.wButtons & LeftStickButton) != 0;

            RightStickButtonPressed = (ControllerPosition.Gamepad.wButtons & RightStickButton) != 0;

            LeftBumperButtonPressed = (ControllerPosition.Gamepad.wButtons & LeftBumperButton) != 0;

            RightBumperButtonPressed = (ControllerPosition.Gamepad.wButtons & RightBumperButton) != 0;

            AButtonPressed = (ControllerPosition.Gamepad.wButtons & AButton) != 0;

            BButtonPressed = (ControllerPosition.Gamepad.wButtons & BButton) != 0;

            XButtonPressed = (ControllerPosition.Gamepad.wButtons & XButton) != 0;

            YButtonPressed = (ControllerPosition.Gamepad.wButtons & YButton) != 0;

            ConButtons[controllerNumber] = ControllerPosition.Gamepad.wButtons;

            ClearLetterButtonsLabel();

            DoButtonLogic(controllerNumber);

        }

        private void DoButtonLogic(int controllerNumber)
        {
            DoDPadLogic(controllerNumber);

            DoLetterButtonLogic(controllerNumber);

            DoStartBackLogic(controllerNumber);

            DoBumperLogic(controllerNumber);

            DoStickLogic(controllerNumber);

        }

        private void DoDPadLogic(int controllerNumber)
        {
            string direction = GetDPadDirection();

            // Are all DPad buttons up?
            if (!string.IsNullOrEmpty(direction))
            {   // No, all DPad buttons are not up.

                LabelDPad.Text = $"Controller {controllerNumber} DPad {direction}";

                IsDPadNeutral[controllerNumber] = false;
            }
            else
            {   // Yes, all DPad buttons are up.

                IsDPadNeutral[controllerNumber] = true;

            }

            ClearDPadLabel();

        }

        private void DoLetterButtonLogic(int controllerNumber)
        {
            string buttonText = GetButtonText(controllerNumber);

            // Are any letter buttons pressed?
            if (!string.IsNullOrEmpty(buttonText))
            {   // Yes, letter buttons are pressed.

                LabelButtons.Text = buttonText;

                IsLetterButtonsNeutral[controllerNumber] = false;
            }
            else
            {   // No, letter buttons are not pressed.

                IsLetterButtonsNeutral[controllerNumber] = true;

            }

            ClearLetterButtonsLabel();

        }

        private void DoStartBackLogic(int controllerNumber)
        {
            if (StartButtonPressed)
            {
                LabelStart.Text = $"Controller {controllerNumber} Start";

                IsStartButtonsNeutral[controllerNumber] = false;
            }
            else
            {
                IsStartButtonsNeutral[controllerNumber] = true;

            }

            ClearStartLabel();

            if (BackButtonPressed)
            {
                LabelBack.Text = $"Controller {controllerNumber} Back";

                IsBackButtonsNeutral[controllerNumber] = false;
            }
            else
            {
                IsBackButtonsNeutral[controllerNumber] = true;

            }

            ClearBackLabel();

        }

        private void DoBumperLogic(int controllerNumber)
        {
            if (LeftBumperButtonPressed)
            {
                LabelLeftBumper.Text = $"Controller {controllerNumber} Left Bumper";

                IsLeftBumperNeutral[controllerNumber] = false;
            }
            else
            {
                IsLeftBumperNeutral[controllerNumber] = true;

            }

            ClearLeftBumperLabel();

            if (RightBumperButtonPressed)
            {
                LabelRightBumper.Text = $"Controller {controllerNumber} Right Bumper";

                IsRightBumperNeutral[controllerNumber] = false;
            }
            else
            {
                IsRightBumperNeutral[controllerNumber] = true;

            }

            ClearRightBumperLabel();

        }

        private void DoStickLogic(int controllerNumber)
        {
            if (LeftStickButtonPressed)
            {
                LabelLeftThumbButton.Text = $"Controller {controllerNumber} Left Thumbstick Button";

                IsLeftStickButtonsNeutral[controllerNumber] = false;
            }
            else
            {
                IsLeftStickButtonsNeutral[controllerNumber] = true;

            }

            ClearLeftThumbButtonLabel();

            if (RightStickButtonPressed)
            {
                LabelRightThumbButton.Text = $"Controller {controllerNumber} Right Thumbstick Button";

                IsRightStickButtonsNeutral[controllerNumber] = false;
            }
            else
            {
                IsRightStickButtonsNeutral[controllerNumber] = true;

            }

            ClearRightThumbButtonLabel();

        }

        private void UpdateLeftThumbstickPosition(int ControllerNumber)
        {   // The range on the X-axis is -32,768 through 32,767. Signed 16-bit (2-byte) integer.
            // The range on the Y-axis is -32,768 through 32,767. Signed 16-bit (2-byte) integer.

            // What position is the left thumbstick in on the X-axis?
            if (ControllerPosition.Gamepad.sThumbLX <= NeutralStart)
            {   // The left thumbstick is in the left position.

                LabelLeftThumbX.Text = $"Controller {ControllerNumber} Left Thumbstick Left";

                IsConThumbLXNeutral[ControllerNumber] = false;
            }
            else if (ControllerPosition.Gamepad.sThumbLX >= NeutralEnd)
            {   // The left thumbstick is in the right position.

                LabelLeftThumbX.Text = $"Controller {ControllerNumber} Left Thumbstick Right";

                IsConThumbLXNeutral[ControllerNumber] = false;
            }
            else
            {   // The left thumbstick is in the neutral position.

                IsConThumbLXNeutral[ControllerNumber] = true;

            }

            ClearLeftThumbstickXLabel();

            // What position is the left thumbstick in on the Y-axis?
            if (ControllerPosition.Gamepad.sThumbLY <= NeutralStart)
            {   // The left thumbstick is in the down position.

                LabelLeftThumbY.Text = $"Controller {ControllerNumber} Left Thumbstick Down";

                IsConThumbLYNeutral[ControllerNumber] = false;
            }
            else if (ControllerPosition.Gamepad.sThumbLY >= NeutralEnd)
            {   // The left thumbstick is in the up position.

                LabelLeftThumbY.Text = $"Controller {ControllerNumber} Left Thumbstick Up";

                IsConThumbLYNeutral[ControllerNumber] = false;
            }
            else
            {   // The left thumbstick is in the neutral position.

                IsConThumbLYNeutral[ControllerNumber] = true;

            }

            ClearLeftThumbstickYLabel();

        }

        private void UpdateRightThumbstickPosition(int controllerNumber)
        {   // The range on the X-axis is -32,768 through 32,767. Signed 16-bit (2-byte) integer.
            // The range on the Y-axis is -32,768 through 32,767. Signed 16-bit (2-byte) integer.

            // What position is the right thumbstick in on the X-axis?
            if (ControllerPosition.Gamepad.sThumbRX <= NeutralStart)
            {   // The right thumbstick is in the left position.

                LabelRightThumbX.Text = $"Controller {controllerNumber} Right Thumbstick Left";

                IsConThumbRXNeutral[controllerNumber] = false;
            }
            else if (ControllerPosition.Gamepad.sThumbRX >= NeutralEnd)
            {   // The right thumbstick is in the right position.

                LabelRightThumbX.Text = $"Controller {controllerNumber} Right Thumbstick Right";

                IsConThumbRXNeutral[controllerNumber] = false;
            }
            else
            {   // The right thumbstick is in the neutral position.

                IsConThumbRXNeutral[controllerNumber] = true;

            }

            ClearRightThumbstickXLabel();

            // What position is the right thumbstick in on the Y-axis?
            if (ControllerPosition.Gamepad.sThumbRY <= NeutralStart)
            {   // The right thumbstick is in the up position.

                LabelRightThumbY.Text = $"Controller {controllerNumber} Right Thumbstick Down";

                IsConThumbRYNeutral[controllerNumber] = false;
            }
            else if (ControllerPosition.Gamepad.sThumbRY >= NeutralEnd)
            {   // The right thumbstick is in the down position.

                LabelRightThumbY.Text = $"Controller {controllerNumber} Right Thumbstick Up";

                IsConThumbRYNeutral[controllerNumber] = false;
            }
            else
            {   // The right thumbstick is in the neutral position.

                IsConThumbRYNeutral[controllerNumber] = true;

            }

            ClearRightThumbstickYLabel();

        }

        private void UpdateRightTriggerPosition(int controllerNumber)
        {   // The range of right trigger is 0 to 255. Unsigned 8-bit (1-byte) integer.
            // The trigger position must be greater than the trigger threshold to register as pressed.

            // What position is the right trigger in?
            if (ControllerPosition.Gamepad.bRightTrigger > TriggerThreshold)
            {   // The right trigger is in the fire position. Trigger Break. Bang!

                LabelRightTrigger.Text = $"Controller {controllerNumber} Right Trigger";

                IsConRightTriggerNeutral[controllerNumber] = false;
            }
            else
            {   // The right trigger is in the neutral position. Pre-Travel.

                IsConRightTriggerNeutral[controllerNumber] = true;

            }

            ClearRightTriggerLabel();

        }

        private void UpdateLeftTriggerPosition(int controllerNumber)
        {   // The range of left trigger is 0 to 255. Unsigned 8-bit (1-byte) integer.
            // The trigger position must be greater than the trigger threshold to register as pressed.

            // What position is the left trigger in?
            if (ControllerPosition.Gamepad.bLeftTrigger > TriggerThreshold)
            {   // The left trigger is in the fire position. Trigger Break. Bang!

                LabelLeftTrigger.Text = $"Controller {controllerNumber} Left Trigger";

                IsConLeftTriggerNeutral[controllerNumber] = false;
            }
            else
            {   // The left trigger is in the neutral position. Pre-Travel.

                IsConLeftTriggerNeutral[controllerNumber] = true;

            }

            ClearLeftTriggerLabel();

        }

        private void ClearLetterButtonsLabel()
        {   // Clears the letter buttons label when all controllers letter buttons are up.

            bool ConSum = true; // Assume all controllers' letter buttons neutral initially.

            // Search for a non-neutral letter button.
            for (int i = 0; i < 4; i++)
            {
                if (Connected[i] && !IsLetterButtonsNeutral[i])
                {   // A non-neutral letter button was found.

                    ConSum = false; // Report the non-neutral letter button.

                    break; // No need to search further, so stop the search.

                }

            }

            // Are all controllers' letter buttons in the neutral position?
            if (ConSum)
            {   // Yes, all controllers' letter buttons are in the neutral position.

                LabelButtons.Text = string.Empty; // Clear label.

            }

        }

        private void ClearLeftThumbstickYLabel()
        { // Clears the left thumbstick Y-axis label when all controllers left thumbsticks on the Y-axis are neutral.

            bool ConSum = true; // Assume all controllers left thumbsticks on the Y-axis are neutral initially.

            // Search for a non-neutral left thumbstick on the Y-axis.
            for (int i = 0; i < 4; i++)
            {
                if (Connected[i] && !IsConThumbLYNeutral[i])
                { // A non-neutral thumbstick was found.

                    ConSum = false; // Report the non-neutral thumbstick.

                    break; // No need to search further so stop the search.

                }

            }

            // Are all controllers left thumbsticks on the Y-axis in the neutral position?
            if (ConSum)
            {   // Yes, all controllers left thumbsticks on the Y-axis are in the neutral position.

                LabelLeftThumbY.Text = string.Empty; // Clear label.

            }

        }

        private void ClearLeftThumbstickXLabel()
        {   // Clears the left thumbstick X-axis label when all controllers left thumbsticks on the X-axis are neutral.

            bool ConSum = true; // Assume all controllers left thumbsticks on the X-axis are neutral initially.

            // Search for a non-neutral left thumbstick on the X-axis.
            for (int i = 0; i < 4; i++)
            {
                if (Connected[i] && !IsConThumbLXNeutral[i])
                {   // A non-neutral thumbstick was found.

                    ConSum = false; // Report the non-neutral thumbstick.

                    break; // No need to search further so stop the search.

                }

            }

            // Are all controllers left thumbsticks on the X-axis in the neutral position?
            if (ConSum)
            {   // Yes, all controllers left thumbsticks on the X-axis are in the neutral position.

                LabelLeftThumbX.Text = string.Empty; // Clear label.

            }

        }

        private void ClearRightThumbstickXLabel()
        {   // Clears the right thumbstick X-axis label when all controllers' right thumbsticks on the X-axis are neutral.

            bool ConSum = true; // Assume all controllers' right thumbsticks on the X-axis are neutral initially.

            // Search for a non-neutral right thumbstick on the X-axis.
            for (int i = 0; i < 4; i++)
            {
                if (Connected[i] && !IsConThumbRXNeutral[i])
                {   // A non-neutral thumbstick was found.

                    ConSum = false; // Report the non-neutral thumbstick.

                    break; // No need to search further, so stop the search.

                }

            }

            // Are all controllers' right thumbsticks on the X-axis in the neutral position?
            if (ConSum)
            { // Yes, all controllers' right thumbsticks on the X-axis are in the neutral position.

                LabelRightThumbX.Text = string.Empty; // Clear label.

            }

        }

        private void ClearRightThumbstickYLabel()
        {   // Clears the right thumbstick Y-axis label when all controllers' right thumbsticks on the Y-axis are neutral.

            bool ConSum = true; // Assume all controllers' right thumbsticks on the Y-axis are neutral initially.

            // Search for a non-neutral right thumbstick on the Y-axis.
            for (int i = 0; i < 4; i++)
            {
                if (Connected[i] && !IsConThumbRYNeutral[i])
                {

                    ConSum = false; // Report the non-neutral thumbstick.

                    break; // No need to search further, so stop the search.

                }

            }

            // Are all controllers' right thumbsticks on the Y-axis in the neutral position?
            if (ConSum)
            {   // Yes, all controllers' right thumbsticks on the Y-axis are in the neutral position.

                LabelRightThumbY.Text = string.Empty; // Clear label.

            }

        }

        private void ClearRightTriggerLabel()
        {   // Clears the right trigger label when all controllers' right triggers are neutral.

            bool ConSum = true; // Assume all controllers' right triggers are neutral initially.

            // Search for a non-neutral right trigger.
            for (int i = 0; i < 4; i++)
            {
                if (Connected[i] && !IsConRightTriggerNeutral[i])
                {

                    ConSum = false; // Report the non-neutral right trigger.

                    break; // No need to search further, so stop the search.

                }

            }

            // Are all controllers' right triggers in the neutral position?
            if (ConSum)
            {   // Yes, all controllers' right triggers are in the neutral position.

                LabelRightTrigger.Text = string.Empty; // Clear label.

            }

        }

        private void ClearLeftTriggerLabel()
        {   // Clears the left trigger label when all controllers' left triggers are neutral.

            bool ConSum = true; // Assume all controllers' left triggers are neutral initially.

            // Search for a non-neutral left trigger.
            for (int i = 0; i < 4; i++)
            {
                if (Connected[i] && !IsConLeftTriggerNeutral[i])
                {   // A non-neutral left trigger was found.

                    ConSum = false; // Report the non-neutral left trigger.

                    break; // No need to search further, so stop the search.

                }

            }

            // Are all controllers' left triggers in the neutral position?
            if (ConSum)
            {   // Yes, all controllers' left triggers are in the neutral position.

                LabelLeftTrigger.Text = string.Empty; // Clear label.

            }

        }

        private void ClearLabels()
        {
            LabelButtons.Text = string.Empty;

            LabelLeftThumbX.Text = string.Empty;

            LabelLeftThumbY.Text = string.Empty;

            LabelLeftTrigger.Text = string.Empty;

            LabelRightThumbX.Text = string.Empty;

            LabelRightThumbY.Text = string.Empty;

            LabelRightTrigger.Text = string.Empty;

        }

        private void ClearDPadLabel()
        {   // Clears the DPad label when all controllers' DPad are neutral.

            bool ConSum = true; // Assume all controllers' DPad are neutral initially.

            // Search for a non-neutral DPad.
            for (int i = 0; i < 4; i++)
            {
                if (Connected[i] && !IsDPadNeutral[i])
                {   // A non-neutral DPad was found.

                    ConSum = false; // Report the non-neutral DPad.

                    break; // No need to search further, so stop the search.

                }

            }

            // Are all controllers' DPad in the neutral position?
            if (ConSum)
            {   // Yes, all controllers' DPad are in the neutral position.

                LabelDPad.Text = string.Empty; // Clear label.

            }

        }

        private void ClearStartLabel()
        {   // Clears the start label when all controllers' start buttons are neutral.

            bool ConSum = true; // Assume all controllers' start buttons are neutral initially.

            // Search for a non-neutral start buttons.
            for (int i = 0; i < 4; i++)
            {
                if (Connected[i] && !IsStartButtonsNeutral[i])
                {   // A non-neutral start buttons was found.

                    ConSum = false; // Report the non-neutral start buttons.

                    break; // No need to search further, so stop the search.

                }

            }

            // Are all controllers' start buttons in the neutral position?
            if (ConSum)
            {   // Yes, all controllers' start buttons are in the neutral position.

                LabelStart.Text = string.Empty; // Clear label.

            }

        }

        private void ClearBackLabel()
        {   // Clears the back buttons label when all controllers' back buttons are neutral.

            bool ConSum = true; // Assume all controllers' back buttons are neutral initially.

            // Search for a non-neutral back buttons.
            for (int i = 0; i < 4; i++)
            {
                if (Connected[i] && !IsBackButtonsNeutral[i])
                {   // A non-neutral back buttons was found.

                    ConSum = false; // Report the non-neutral back buttons.

                    break; // No need to search further, so stop the search.

                }

            }

            // Are all controllers' back buttons in the neutral position?
            if (ConSum)
            {   // Yes, all controllers' back buttons are in the neutral position.

                LabelBack.Text = string.Empty; // Clear label.

            }

        }

        private void ClearLeftBumperLabel()
        {   // Clears the left bumper label when all controllers' left bumper are neutral.

            bool ConSum = true; // Assume all controllers' left bumper are neutral initially.

            // Search for a non-neutral left bumper.
            for (int i = 0; i < 4; i++)
            {
                if (Connected[i] && !IsLeftBumperNeutral[i])
                {   // A non-neutral left bumper was found.

                    ConSum = false; // Report the non-neutral left bumper.

                    break; // No need to search further, so stop the search.

                }

            }

            // Are all controllers' left bumper in the neutral position?
            if (ConSum)
            {   // Yes, all controllers' left bumper are in the neutral position.

                LabelLeftBumper.Text = string.Empty; // Clear label.

            }

        }

        private void ClearRightBumperLabel()
        {   // Clears the right bumper label when all controllers' right bumper are neutral.

            bool ConSum = true; // Assume all controllers' right bumper are neutral initially.

            // Search for a non-neutral right bumper.
            for (int i = 0; i < 4; i++)
            {
                if (Connected[i] && !IsRightBumperNeutral[i])
                {   // A non-neutral right bumper was found.

                    ConSum = false; // Report the non-neutral right bumper.

                    break; // No need to search further, so stop the search.

                }

            }

            // Are all controllers' right bumper in the neutral position?
            if (ConSum)
            {   // Yes, all controllers' right bumper are in the neutral position.

                LabelRightBumper.Text = string.Empty; // Clear label.

            }

        }

        private void ClearLeftThumbButtonLabel()
        {   // Clears the left thumbstick button label when all controllers' left thumbstick buttons are neutral.

            bool ConSum = true; // Assume all controllers' left thumbstick buttons are neutral initially.

            // Search for a non-neutral left thumbstick buttons.
            for (int i = 0; i < 4; i++)
            {
                if (Connected[i] && !IsLeftStickButtonsNeutral[i])
                {   // A non-neutral left thumbstick buttons was found.

                    ConSum = false; // Report the non-neutral left thumbstick buttons.

                    break; // No need to search further, so stop the search.

                }

            }

            // Are all controllers' left thumbstick buttons in the neutral position?
            if (ConSum)
            {   // Yes, all controllers' left thumbstick buttons are in the neutral position.

                LabelLeftThumbButton.Text = string.Empty; // Clear label.

            }

        }

        private void ClearRightThumbButtonLabel()
        {   // Clears the right thumbstick button label when all controllers'right thumbstick buttons are neutral.

            bool ConSum = true; // Assume all controllers' right thumbstick buttons are neutral initially.

            // Search for a non-neutral right thumbstick buttons.
            for (int i = 0; i < 4; i++)
            {
                if (Connected[i] && !IsRightStickButtonsNeutral[i])
                {   // A non-neutral right thumbstick buttons was found.

                    ConSum = false; // Report the non-neutral right thumbstick buttons.

                    break; // No need to search further, so stop the search.

                }

            }

            // Are all controllers' right thumbstick buttons in the neutral position?
            if (ConSum)
            {   // Yes, all controllers' right thumbstick buttons are in the neutral position.

                LabelRightThumbButton.Text = string.Empty; // Clear label.

            }

        }

        private string GetDPadDirection()
        {
            if (DPadUpPressed)
            {
                if (DPadLeftPressed) return "Left+Up";

                if (DPadRightPressed) return "Right+Up";

                return "Up";

            }

            if (DPadDownPressed)
            {
                if (DPadLeftPressed) return "Left+Down";

                if (DPadRightPressed) return "Right+Down";

                return "Down";

            }

            if (DPadLeftPressed) return "Left";

            if (DPadRightPressed) return "Right";

            return string.Empty; // Return an empty string if no buttons are pressed.

        }

        private string GetButtonText(int controllerNumber)
        {
            var buttons = new List<string>();

            if (AButtonPressed) buttons.Add("A");

            if (BButtonPressed) buttons.Add("B");

            if (XButtonPressed) buttons.Add("X");

            if (YButtonPressed) buttons.Add("Y");

            if (buttons.Count > 0)
            {
                return $"Controller {controllerNumber} {string.Join("+", buttons)}";

            }

            return string.Empty; // Return an empty string if no buttons are pressed

        }

        private void VibrateLeft(int cid, ushort speed)
        {   // The range of speed is 0 through 65,535. Unsigned 16-bit (2-byte) integer.
            // The left motor is the low-frequency rumble motor.

            // Set left motor speed.
            Vibration.wLeftMotorSpeed = speed;

            SendVibrationMotorCommand(cid);

            LeftVibrateStart[cid] = DateTime.Now;

            IsLeftVibrating[cid] = true;

        }

        private void VibrateRight(int cid, ushort speed)
        {   // The range of speed is 0 through 65,535. Unsigned 16-bit (2-byte) integer.
            // The right motor is the high-frequency rumble motor.

            // Set right motor speed.
            Vibration.wRightMotorSpeed = speed;

            SendVibrationMotorCommand(cid);

            RightVibrateStart[cid] = DateTime.Now;

            IsRightVibrating[cid] = true;

        }

        private void SendVibrationMotorCommand(int controllerID)
        {   // Sends vibration motor speed command to the specified controller.

            try
            {
                // Send motor speed command to the specified controller.
                if (XInputSetState(controllerID, ref Vibration) == 0)
                {   // The motor speed was set. Success.
                }
                else
                {
                    // The motor speed was not set. Fail.
                    // You can log or handle the failure here if needed.
                    // Example: Console.WriteLine(XInputSetState(controllerID, Vibration).ToString());
                }
            }
            catch (Exception ex)
            {
                DisplayError(ex);

                return; // Exit the method.

            }

        }

        private void UpdateVibrateTimer()
        {
            UpdateLeftVibrateTimer();

            UpdateRightVibrateTimer();

        }

        private void UpdateLeftVibrateTimer()
        {
            foreach (var isConVibrating in IsLeftVibrating)
            {
                int index = Array.IndexOf(IsLeftVibrating, isConVibrating);

                if (index != -1 && isConVibrating)
                {
                    TimeSpan elapsedTime = DateTime.Now - LeftVibrateStart[index];

                    if (elapsedTime.TotalMilliseconds >= (double)NumericUpDownTimeToVib.Value)
                    {
                        IsLeftVibrating[index] = false;

                        // Turn left motor off (set zero speed).
                        Vibration.wLeftMotorSpeed = 0;

                        SendVibrationMotorCommand(index);

                    }

                }

            }

        }

        private void UpdateRightVibrateTimer()
        {
            foreach (var isConVibrating in IsRightVibrating)
            {
                int index = Array.IndexOf(IsRightVibrating, isConVibrating);

                if (index != -1 && isConVibrating)
                {
                    TimeSpan elapsedTime = DateTime.Now - RightVibrateStart[index];

                    if (elapsedTime.TotalMilliseconds >= (double)NumericUpDownTimeToVib.Value)
                    {
                        IsRightVibrating[index] = false;

                        // Turn right motor off (set zero speed).
                        Vibration.wRightMotorSpeed = 0;

                        SendVibrationMotorCommand(index);

                    }

                }

            }

        }

        private void UpdateSpeedLabel()
        {
            LabelSpeed.Text = $"Speed: {TrackBarSpeed.Value}";

        }

        private bool IsControllerConnected(int controllerNumber)
        {
            try
            {
                return XInputGetState(controllerNumber, ref ControllerPosition) == 0; // 0 means the controller is connected.
                // Anything else (a non-zero value) means the controller is not connected.
            }
            catch (Exception ex)
            {   // Something went wrong (An exception occurred).

                DisplayError(ex);

                return false;

            }

        }

        private void DisplayError(Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void InitializeApp()
        {
            Text = "XInput C# - Code with Joe";

            InitializeTimer1();

            ClearLabels();

            TrackBarSpeed.Value = 32767;

            UpdateSpeedLabel();

            for (int i = 0; i < IsLeftVibrating.Length; i++)
            {
                IsLeftVibrating[i] = false;

            }

            for (int i = 0; i < IsRightVibrating.Length; i++)
            {
                IsRightVibrating[i] = false;

            }

            //LabelBatteryLevel.Text = string.Empty;

            //LabelBatteryType.Text = string.Empty;

            InitializeToolTips();

        }

        private void InitializeToolTips()
        {
            ToolTip ToolTipTimeToVib = new()
            {
                AutoPopDelay = 8000,
                InitialDelay = 1000,
                ReshowDelay = 500
            };

            string TipText = $"Time to Vibrate {Environment.NewLine}Enter a value between 1 and 5000 milliseconds {Environment.NewLine}1 second = 1000 milliseconds";

            ToolTipTimeToVib.SetToolTip(NumericUpDownTimeToVib, TipText);

            ToolTip ToolTipConToVib = new()
            {
                AutoPopDelay = 8000,
                InitialDelay = 1000,
                ReshowDelay = 500
            };

            TipText = $"Controller to Vibrate {Environment.NewLine}Enter a value between 0 and 3 {Environment.NewLine}Supports up to 4 controllers";

            ToolTipConToVib.SetToolTip(NumControllerToVib, TipText);

            ToolTip ToolTipVibSpeed = new()
            {
                AutoPopDelay = 10000,
                InitialDelay = 1000,
                ReshowDelay = 500
            };

            TipText = $"Vibration Speed {Environment.NewLine}Enter a value between 1 and 65,535 {Environment.NewLine}Higher speeds can create stronger feedback {Environment.NewLine}while lower speeds produce a more subtle effect";

            ToolTipVibSpeed.SetToolTip(TrackBarSpeed, TipText);

            ToolTip ToolTipRumbleGroup = new()
            {
                AutoPopDelay = 10000,
                InitialDelay = 1000,
                ReshowDelay = 500
            };

            TipText = $"The vibration motors in controllers {Environment.NewLine}provide haptic feedback during gameplay {Environment.NewLine}enhancing the immersive experience";

            ToolTipRumbleGroup.SetToolTip(groupBox1, TipText);

        }

        public Form1()
        {
            InitializeComponent();
        }

    }
}



// Consuming Unmanaged DLL Functions

// Consuming unmanaged DLL functions refers to the process of using functions that are defined in a
// DLL (Dynamic Link Library) which is written in a language like C or C++. This involves using
// Platform Invocation Services(P / Invoke) to call functions in the unmanaged DLL from your managed
// C# code. To consume unmanaged DLL functions, use the DllImport attribute to declare the external
// functions from the DLL.

// https://learn.microsoft.com/en-us/dotnet/framework/interop/consuming-unmanaged-dll-functions


// Passing Structures

// Passing structures refers to the process of sending structured data as a parameter to a function
// or method. Structures, also known as structs, allow you to group related data together under a
// single name. When passing structures as parameters, you are essentially sending a block of data
// that contains multiple fields or members. This can be useful for organizing related data and
// passing them around your program efficiently.

// https://learn.microsoft.com/en-us/dotnet/framework/interop/passing-structures


// XInputGetState Function

// The XInputGetState function is used to retrieve the current state of an Xbox controller.

// https://learn.microsoft.com/en-us/windows/win32/api/xinput/nf-xinput-xinputgetstate


// XINPUT_STATE Structure

// The XINPUT_STATE structure is used to hold the current state of an Xbox controller.

// https://learn.microsoft.com/en-us/windows/win32/api/xinput/ns-xinput-xinput_state


// XINPUT_GAMEPAD Structure

// The XINPUT_GAMEPAD structure represents the state of the gamepad (Xbox controller) input,
// including information about button presses, trigger values, and thumbstick positions.

// https://learn.microsoft.com/en-us/windows/win32/api/xinput/ns-xinput-xinput_gamepad


// XInputSetState Function
// https://learn.microsoft.com/en-us/windows/win32/api/xinput/nf-xinput-xinputsetstate


// XINPUT_VIBRATION Structure
// https://learn.microsoft.com/en-us/windows/win32/api/xinput/ns-xinput-xinput_vibration


// XInputGetBatteryInformation Function
// https://learn.microsoft.com/en-us/windows/win32/api/xinput/nf-xinput-xinputgetbatteryinformation


// XINPUT_BATTERY_INFORMATION Structure
// https://learn.microsoft.com/en-us/windows/win32/api/xinput/ns-xinput-xinput_battery_information


// Getting Started with XInput in Windows Applications
// https://learn.microsoft.com/en-us/windows/win32/xinput/getting-started-with-xinput


// XInput Game Controller APIs
// https://learn.microsoft.com/en-us/windows/win32/api/_xinput/


// XInput Versions
// https://learn.microsoft.com/en-us/windows/win32/xinput/xinput-versions


// Comparison of XInput and DirectInput Features
// https://learn.microsoft.com/en-us/windows/win32/xinput/xinput-and-directinput


// Built-in types (C# reference)
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/built-in-types


// Monica is our an AI assistant.
// https://monica.im/


// I also make coding videos on my YouTube channel.
// https://www.youtube.com/@codewithjoe6074


