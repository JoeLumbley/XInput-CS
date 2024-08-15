// XInput

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
        // XInput1_4.dll seems to be the current version
        // XInput9_1_0.dll is maintained primarily for backward compatibility. 

        [StructLayout(LayoutKind.Sequential)]
        public struct XINPUT_STATE
        {
            public uint PacketNumber; // Unsigned 32-bit (4-byte) integer range 0 through 4,294,967,295.
            public Gamepad Gamepad;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Gamepad
        {
            public ushort Buttons; // Unsigned 16-bit (2-byte) integer range 0 through 65,535.
            public byte LeftTrigger; // Unsigned 8-bit (1-byte) integer range 0 through 255.
            public byte RightTrigger;
            public short LeftThumbX; // Signed 16-bit (2-byte) integer range -32,768 through 32,767.
            public short LeftThumbY;
            public short RightThumbX;
            public short RightThumbY;
        }

        private ushort[] ConButtons = new ushort[4];

        private bool[] IsConLeftTriggerNeutral = new bool[4];
        // A boolean is a logical value that is either true or false.

        private bool[] IsConRightTriggerNeutral = new bool[4];

        private bool[] IsConThumbLXNeutral = new bool[4];

        private bool[] IsConThumbLYNeutral = new bool[4];

        private bool[] IsConThumbRXNeutral = new bool[4];

        private bool[] IsConThumbRYNeutral = new bool[4];

        private XINPUT_STATE ControllerPosition;

        // Set the start of the thumbstick neutral zone to 1/2 over.
        private const short NeutralStart = -16384; // -16,384 = -32,768 / 2
        // The thumbstick position must be more than 1/2 over the neutral start to register as moved.
        // A short is a signed 16-bit (2-byte) integer range -32,768 through 32,767. This gives us 65,536 values.

        // Set the end of the thumbstick neutral zone to 1/2 over.
        private const short NeutralEnd = 16384; // 16,383.5 = 32,767 / 2
        // The thumbstick position must be more than 1/2 over the neutral end to register as moved.

        // Set the trigger threshold to 1/4 pull.
        private const byte TriggerThreshold = 64; // 64 = 256 / 4
        // The trigger position must be greater than the trigger threshold to register as pulled.
        // A byte is a unsigned 8-bit (1-byte) integer range 0 through 255. This gives us 256 values.

        private readonly bool[] Connected = new bool[4];

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
            for (int controllerNumber = 0; controllerNumber < 4; controllerNumber++) // Up to 4 controllers
            {
                try
                {
                    if (IsControllerConnected(controllerNumber))
                    {

                        UpdateControllerState(controllerNumber);

                        Connected[controllerNumber] = true;
                    }
                    else
                    {
                        Connected[controllerNumber] = false;
                    }
                }
                catch (Exception ex)
                {   // Something went wrong (An exception occurred).

                    DisplayError(ex);

                    return; // Exit the method on error
                }
            }
        }

        private void UpdateControllerState(int controllerNumber)
        {

            UpdateButtonPosition(controllerNumber);

            UpdateLeftThumbstickPosition(controllerNumber);

            UpdateRightThumbstickPosition(controllerNumber);

            UpdateLeftTriggerPosition(controllerNumber);

            UpdateRightTriggerPosition(controllerNumber);

        }

        private void UpdateButtonPosition(int controllerNumber)
        {

            // Update button states based on the current button state
            DPadUpPressed = (ControllerPosition.Gamepad.Buttons & DPadUp) != 0;

            DPadDownPressed = (ControllerPosition.Gamepad.Buttons & DPadDown) != 0;

            DPadLeftPressed = (ControllerPosition.Gamepad.Buttons & DPadLeft) != 0;

            DPadRightPressed = (ControllerPosition.Gamepad.Buttons & DPadRight) != 0;

            StartButtonPressed = (ControllerPosition.Gamepad.Buttons & StartButton) != 0;

            BackButtonPressed = (ControllerPosition.Gamepad.Buttons & BackButton) != 0;

            LeftStickButtonPressed = (ControllerPosition.Gamepad.Buttons & LeftStickButton) != 0;

            RightStickButtonPressed = (ControllerPosition.Gamepad.Buttons & RightStickButton) != 0;

            LeftBumperButtonPressed = (ControllerPosition.Gamepad.Buttons & LeftBumperButton) != 0;

            RightBumperButtonPressed = (ControllerPosition.Gamepad.Buttons & RightBumperButton) != 0;

            AButtonPressed = (ControllerPosition.Gamepad.Buttons & AButton) != 0;

            BButtonPressed = (ControllerPosition.Gamepad.Buttons & BButton) != 0;

            XButtonPressed = (ControllerPosition.Gamepad.Buttons & XButton) != 0;

            YButtonPressed = (ControllerPosition.Gamepad.Buttons & YButton) != 0;

            ConButtons[controllerNumber] = ControllerPosition.Gamepad.Buttons;

            ClearButtonsLabel();

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
            if (DPadUpPressed)
            {
                if (DPadLeftPressed)
                {
                    LabelButtons.Text = "Controller " + controllerNumber + " Button: Left+Up";
                }
                else if (DPadRightPressed)
                {
                    LabelButtons.Text = "Controller " + controllerNumber + " Button: Right+Up";
                }
                else
                {
                    LabelButtons.Text = "Controller " + controllerNumber + " Button: Up";
                }
            }
            else if (DPadDownPressed)
            {
                if (DPadLeftPressed)
                {
                    LabelButtons.Text = "Controller " + controllerNumber + " Button: Left+Down";
                }
                else if (DPadRightPressed)
                {
                    LabelButtons.Text = "Controller " + controllerNumber + " Button: Right+Down";
                }
                else
                {
                    LabelButtons.Text = "Controller " + controllerNumber + " Button: Down";
                }
            }
            else if (DPadLeftPressed && !DPadDownPressed && !DPadUpPressed)
            {
                LabelButtons.Text = "Controller " + controllerNumber + " Button: Left";
            }
            else if (DPadRightPressed && !DPadDownPressed && !DPadUpPressed)
            {
                LabelButtons.Text = "Controller " + controllerNumber + " Button: Right";
            }
        }

        private void DoLetterButtonLogic(int controllerNumber)
        {
            if (AButtonPressed)
            {
                if (BButtonPressed)
                {
                    if (XButtonPressed)
                    {
                        if (YButtonPressed)
                        {
                            LabelButtons.Text = "Controller " + controllerNumber + " Buttons: A+B+X+Y";
                        }
                        else
                        {
                            LabelButtons.Text = "Controller " + controllerNumber + " Buttons: A+B+X";
                        }
                    }
                    else
                    {
                        if (YButtonPressed)
                        {
                            LabelButtons.Text = "Controller " + controllerNumber + " Buttons: A+B+Y";
                        }
                        else
                        {
                            LabelButtons.Text = "Controller " + controllerNumber + " Buttons: A+B";
                        }
                    }
                }
                else
                {
                    if (XButtonPressed)
                    {
                        if (YButtonPressed)
                        {
                            LabelButtons.Text = "Controller " + controllerNumber + " Buttons: A+X+Y";
                        }
                        else
                        {
                            LabelButtons.Text = "Controller " + controllerNumber + " Buttons: A+X";
                        }
                    }
                    else
                    {
                        if (YButtonPressed)
                        {
                            LabelButtons.Text = "Controller " + controllerNumber + " Buttons: A+Y";
                        }
                        else
                        {
                            LabelButtons.Text = "Controller " + controllerNumber + " Buttons: A";
                        }
                    }
                }
            }
            else if (BButtonPressed)
            {
                if (XButtonPressed)
                {
                    if (YButtonPressed)
                    {
                        LabelButtons.Text = "Controller " + controllerNumber + " Buttons: B+X+Y";
                    }
                    else
                    {
                        LabelButtons.Text = "Controller " + controllerNumber + " Buttons: B+X";
                    }
                }
                else
                {
                    if (YButtonPressed)
                    {
                        LabelButtons.Text = "Controller " + controllerNumber + " Buttons: B+Y";
                    }
                    else
                    {
                        LabelButtons.Text = "Controller " + controllerNumber + " Buttons: B";
                    }
                }
            }
            else if (XButtonPressed)
            {
                if (YButtonPressed)
                {
                    LabelButtons.Text = "Controller " + controllerNumber + " Buttons: X+Y";
                }
                else
                {
                    LabelButtons.Text = "Controller " + controllerNumber + " Buttons: X";
                }
            }
            else if (YButtonPressed)
            {
                LabelButtons.Text = "Controller " + controllerNumber + " Buttons: Y";
            }
        }

        private void DoStartBackLogic(int controllerNumber)
        {
            if (StartButtonPressed)
            {
                if (BackButtonPressed)
                {
                    LabelButtons.Text = "Controller " + controllerNumber + " Buttons: Start+Back";
                }
                else
                {
                    LabelButtons.Text = "Controller " + controllerNumber + " Buttons: Start";
                }
            }
            else if (BackButtonPressed)
            {
                LabelButtons.Text = "Controller " + controllerNumber + " Buttons: Back";
            }
        }

        private void DoBumperLogic(int controllerNumber)
        {
            if (LeftBumperButtonPressed)
            {
                if (RightBumperButtonPressed)
                {
                    LabelButtons.Text = "Controller " + controllerNumber + " Buttons: Left Bumper+Right Bumper";
                }
                else
                {
                    LabelButtons.Text = "Controller " + controllerNumber + " Buttons: Left Bumper";
                }
            }
            else if (RightBumperButtonPressed)
            {
                LabelButtons.Text = "Controller " + controllerNumber + " Buttons: Right Bumper";
            }
        }

        private void DoStickLogic(int controllerNumber)
        {
            if (LeftStickButtonPressed)
            {
                if (RightStickButtonPressed)
                {
                    LabelButtons.Text = "Controller " + controllerNumber + " Buttons: Left Stick+Right Stick";
                }
                else
                {
                    LabelButtons.Text = "Controller " + controllerNumber + " Buttons: Left Stick";
                }
            }
            else if (RightStickButtonPressed)
            {
                LabelButtons.Text = "Controller " + controllerNumber + " Buttons: Right Stick";
            }
        }

        private void UpdateLeftThumbstickPosition(int ControllerNumber)
        {   // The range on the X-axis is -32,768 through 32,767. Signed 16-bit (2-byte) integer.
            // The range on the Y-axis is -32,768 through 32,767. Signed 16-bit (2-byte) integer.

            // What position is the left thumbstick in on the X-axis?
            if (ControllerPosition.Gamepad.LeftThumbX <= NeutralStart)
            {   // The left thumbstick is in the left position.

                LabelLeftThumbX.Text = "Controller " + ControllerNumber.ToString() + " Left Thumbstick: Left";

                IsConThumbLXNeutral[ControllerNumber] = false;
            }
            else if (ControllerPosition.Gamepad.LeftThumbX >= NeutralEnd)
            {   // The left thumbstick is in the right position.

                LabelLeftThumbX.Text = "Controller " + ControllerNumber.ToString() + " Left Thumbstick: Right";

                IsConThumbLXNeutral[ControllerNumber] = false;
            }
            else
            {   // The left thumbstick is in the neutral position.

                IsConThumbLXNeutral[ControllerNumber] = true;

            }

            ClearLeftThumbstickXLabel();

            // What position is the left thumbstick in on the Y-axis?
            if (ControllerPosition.Gamepad.LeftThumbY <= NeutralStart)
            {   // The left thumbstick is in the down position.

                LabelLeftThumbY.Text = "Controller " + ControllerNumber.ToString() + " Left Thumbstick: Down";

                IsConThumbLYNeutral[ControllerNumber] = false;
            }
            else if (ControllerPosition.Gamepad.LeftThumbY >= NeutralEnd)
            {   // The left thumbstick is in the up position.

                LabelLeftThumbY.Text = "Controller " + ControllerNumber.ToString() + " Left Thumbstick: Up";

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
            if (ControllerPosition.Gamepad.RightThumbX <= NeutralStart)
            {   // The right thumbstick is in the left position.

                LabelRightThumbX.Text = "Controller " + controllerNumber.ToString() + " Right Thumbstick: Left";

                IsConThumbRXNeutral[controllerNumber] = false;
            }
            else if (ControllerPosition.Gamepad.RightThumbX >= NeutralEnd)
            {   // The right thumbstick is in the right position.

                LabelRightThumbX.Text = "Controller " + controllerNumber.ToString() + " Right Thumbstick: Right";

                IsConThumbRXNeutral[controllerNumber] = false;
            }
            else
            {   // The right thumbstick is in the neutral position.

                IsConThumbRXNeutral[controllerNumber] = true;

            }

            ClearRightThumbstickXLabel();

            // What position is the right thumbstick in on the Y-axis?
            if (ControllerPosition.Gamepad.RightThumbY <= NeutralStart)
            {   // The right thumbstick is in the up position.

                LabelRightThumbY.Text = "Controller " + controllerNumber.ToString() + " Right Thumbstick: Down";

                IsConThumbRYNeutral[controllerNumber] = false;
            }
            else if (ControllerPosition.Gamepad.RightThumbY >= NeutralEnd)
            {   // The right thumbstick is in the down position.

                LabelRightThumbY.Text = "Controller " + controllerNumber.ToString() + " Right Thumbstick: Up";

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
            if (ControllerPosition.Gamepad.RightTrigger > TriggerThreshold)
            {   // The right trigger is in the down position. Trigger Break. Bang!

                LabelRightTrigger.Text = "Controller " + controllerNumber.ToString() + " Right Trigger";

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
            if (ControllerPosition.Gamepad.LeftTrigger > TriggerThreshold)
            {   // The left trigger is in the down position. Trigger Break. Bang!

                LabelLeftTrigger.Text = "Controller " + controllerNumber.ToString() + " Left Trigger";

                IsConLeftTriggerNeutral[controllerNumber] = false;
            }
            else
            {   // The left trigger is in the neutral position. Pre-Travel.

                IsConLeftTriggerNeutral[controllerNumber] = true;

            }

            ClearLeftTriggerLabel();

        }

        private void ClearButtonsLabel()
        {   // Clears the buttons label when all controllers buttons are up.

            int ConSum = 0;

            foreach (var con in ConButtons)
            {
                ConSum += con;

            }

            // Are all controllers buttons up?
            if (ConSum == 0)
            {   // Yes, all controller buttons are up.

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
                    //if (!IsConThumbRYNeutral[i])
                    //{   // A non-neutral thumbstick was found.

                    ConSum = false; // Report the non-neutral thumbstick.

                    break; // No need to search further, so stop the search.
                    //}
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
                if (Connected[i])
                {
                    if (!IsConRightTriggerNeutral[i])
                    {   // A non-neutral right trigger was found.

                        ConSum = false; // Report the non-neutral right trigger.

                        break; // No need to search further, so stop the search.
                    }
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
                if (Connected[i])
                {
                    if (!IsConLeftTriggerNeutral[i])
                    {   // A non-neutral left trigger was found.

                        ConSum = false; // Report the non-neutral left trigger.

                        break; // No need to search further, so stop the search.
                    }
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

        private bool IsControllerConnected(int controllerNumber)
        {
            return XInputGetState(controllerNumber, ref ControllerPosition) == 0; // Returns 0 if connected
        }

        private void DisplayError(Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void InitializeApp()
        {
            Text = "XInput - Code with Joe";

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
        }


        private void UpdateSpeedLabel()
        {
            LabelSpeed.Text = "Vibration Speed: " + TrackBarSpeed.Value;
        }

        public Form1()
        {
            InitializeComponent();
        }


        private void VibrateLeft(int cid, ushort speed)
        {
            // The range of speed is 0 through 65,535. Unsigned 16-bit (2-byte) integer.
            // The left motor is the low-frequency rumble motor.

            // Set left motor speed.
            Vibration.wLeftMotorSpeed = speed;

            SendVibrationMotorCommand(cid);

            LeftVibrateStart[cid] = DateTime.Now;

            IsLeftVibrating[cid] = true;
        }

        private void SendVibrationMotorCommand(int controllerID)
        {
            // Sends vibration motor speed command to the specified controller.

            try
            {
                // Send motor speed command to the specified controller.
                if (XInputSetState(controllerID, ref Vibration) == 0)
                {
                    // The motor speed was set. Success.
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

        private void UpdateRightVibrateTimer()
        {
            foreach (var isConVibrating in IsRightVibrating)
            {
                int index = Array.IndexOf(IsRightVibrating, isConVibrating);

                if (index != -1 && isConVibrating)
                {
                    TimeSpan elapsedTime = DateTime.Now - RightVibrateStart[index];

                    if (elapsedTime.TotalSeconds >= 1)
                    {
                        IsRightVibrating[index] = false;

                        // Turn right motor off (set zero speed).
                        Vibration.wRightMotorSpeed = 0;

                        SendVibrationMotorCommand(index);
                    }
                }
            }
        }

        private void UpdateLeftVibrateTimer()
        {
            foreach (var isConVibrating in IsLeftVibrating)
            {
                int index = Array.IndexOf(IsLeftVibrating, isConVibrating);

                if (index != -1 && isConVibrating)
                {
                    TimeSpan elapsedTime = DateTime.Now - LeftVibrateStart[index];

                    if (elapsedTime.TotalSeconds >= 1)
                    {
                        IsLeftVibrating[index] = false;

                        // Turn left motor off (set zero speed).
                        Vibration.wLeftMotorSpeed = 0;

                        SendVibrationMotorCommand(index);
                    }
                }
            }
        }


        private void VibrateRight(int cid, ushort speed)
        {
            // The range of speed is 0 through 65,535. Unsigned 16-bit (2-byte) integer.
            // The right motor is the high-frequency rumble motor.

            // Set right motor speed.
            Vibration.wRightMotorSpeed = speed;

            SendVibrationMotorCommand(cid);

            RightVibrateStart[cid] = DateTime.Now;

            IsRightVibrating[cid] = true;
        }


    }
}



