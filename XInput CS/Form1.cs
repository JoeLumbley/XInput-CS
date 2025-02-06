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

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace XInput_CS
{

    public struct XboxControllers
    {
        [DllImport("XInput1_4.dll")]
        private static extern int XInputGetState(int dwUserIndex, 
                                                 ref XINPUT_STATE pState);


        [StructLayout(LayoutKind.Explicit)]
        public struct XINPUT_STATE
        {
            [FieldOffset(0)]
            public uint dwPacketNumber; // Unsigned integer range 0 through 4,294,967,295.
            [FieldOffset(4)]
            public XINPUT_GAMEPAD Gamepad;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct XINPUT_GAMEPAD
        {
            public ushort wButtons; // Unsigned integer range 0 through 65,535.
            public byte bLeftTrigger; // Unsigned integer range 0 through 255.
            public byte bRightTrigger;
            public short sThumbLX; // Signed integer range -32,768 through 32,767.
            public short sThumbLY;
            public short sThumbRX;
            public short sThumbRY;
        }

        private XINPUT_STATE State;

        enum Button
        {
            DPadUp = 1,
            DPadDown = 2,
            DPadLeft = 4,
            DPadRight = 8,
            Start = 16,
            Back = 32,
            LeftStick = 64,
            RightStick = 128,
            LeftBumper = 256,
            RightBumper = 512,
            A = 4096,
            B = 8192,
            X = 16384,
            Y = 32768,
        }

        // Set the start of the thumbstick neutral zone to 1/2 over.
        private const short NeutralStart = -16384; // -16,384 = -32,768 / 2
        // The thumbstick position must be more than 1/2 over the neutral start to
        // register as moved.
        // A short is a signed 16-bit (2-byte) integer range -32,768 through 32,767.
        // This gives us 65,535 values.

        // Set the end of the thumbstick neutral zone to 1/2 over.
        private const short NeutralEnd = 16384; // 16,383.5 = 32,767 / 2
        // The thumbstick position must be more than 1/2 over the neutral end to
        // register as moved.

        // Set the trigger threshold to 1/4 pull.
        private const byte TriggerThreshold = 64; // 64 = 256 / 4
        // The trigger position must be greater than the trigger threshold to
        // register as pulled.
        // A byte is a unsigned 8-bit (1-byte) integer range 0 through 255.
        // This gives us 256 values.

        public bool[] Connected;

        private DateTime TimeSinceLastConnectionCheck;
        private DateTime ExpectedTimeSinceLastConnectionCheck;

        public ushort[] Buttons;

        public bool[] LeftThumbstickXaxisNeutral;
        public bool[] LeftThumbstickYaxisNeutral;

        public bool[] RightThumbstickXaxisNeutral;
        public bool[] RightThumbstickYaxisNeutral;

        public bool[] DPadNeutral;

        public bool[] LetterButtonsNeutral;

        public bool[] DPadUp;
        public bool[] DPadDown;
        public bool[] DPadLeft;
        public bool[] DPadRight;

        public bool[] Start;
        public bool[] Back;

        public bool[] LeftStick;
        public bool[] RightStick;

        public bool[] LeftBumper;
        public bool[] RightBumper;

        public bool[] A;
        public bool[] B;
        public bool[] X;
        public bool[] Y;

        public bool[] RightThumbstickUp;
        public bool[] RightThumbstickDown;
        public bool[] RightThumbstickLeft;
        public bool[] RightThumbstickRight;

        public bool[] LeftThumbstickUp;
        public bool[] LeftThumbstickDown;
        public bool[] LeftThumbstickLeft;
        public bool[] LeftThumbstickRight;

        public bool[] LeftTrigger;
        public bool[] RightTrigger;

        public int TimeToVibe;

        private DateTime[] LeftVibrateStart;

        private DateTime[] RightVibrateStart;

        private bool[] IsLeftVibrating;

        private bool[] IsRightVibrating;

        public void Initialize()
        {
            // Initialize the Connected array to indicate whether controllers are connected.
            Connected = new bool[4];

            // Record the current date and time when initialization starts.
            ExpectedTimeSinceLastConnectionCheck = DateTime.Now;
            TimeSinceLastConnectionCheck = DateTime.Now;

            // Initialize the Buttons array to store the state of controller buttons.
            Buttons = new ushort[4];

            // Initialize arrays to check if thumbstick axes are in the neutral position.
            LeftThumbstickXaxisNeutral = new bool[4];
            LeftThumbstickYaxisNeutral = new bool[4];
            RightThumbstickXaxisNeutral = new bool[4];
            RightThumbstickYaxisNeutral = new bool[4];

            // Initialize array to check if the D-Pad is in the neutral position.
            DPadNeutral = new bool[4];

            // Initialize array to check if letter buttons are in the neutral position.
            LetterButtonsNeutral = new bool[4];

            // Set all thumbstick axes, triggers, D-Pad, letter buttons, start/back buttons,
            // bumpers, and stick buttons to neutral for all controllers (indices 0 to 3).
            for (int i = 0; i < 4; i++)
            {
                LeftThumbstickXaxisNeutral[i] = true;
                LeftThumbstickYaxisNeutral[i] = true;
                RightThumbstickXaxisNeutral[i] = true;
                RightThumbstickYaxisNeutral[i] = true;

                DPadNeutral[i] = true;

                LetterButtonsNeutral[i] = true;

            }

            // Initialize arrays for thumbstick directional states.
            RightThumbstickLeft = new bool[4];
            RightThumbstickRight = new bool[4];
            RightThumbstickDown = new bool[4];
            RightThumbstickUp = new bool[4];
            LeftThumbstickLeft = new bool[4];
            LeftThumbstickRight = new bool[4];
            LeftThumbstickDown = new bool[4];
            LeftThumbstickUp = new bool[4];

            // Initialize arrays for trigger states.
            LeftTrigger = new bool[4];
            RightTrigger = new bool[4];

            // Initialize arrays for letter button states (A, B, X, Y).
            A = new bool[4];
            B = new bool[4];
            X = new bool[4];
            Y = new bool[4];

            // Initialize arrays for bumper button states.
            LeftBumper = new bool[4];
            RightBumper = new bool[4];

            // Initialize arrays for D-Pad directional states.
            DPadUp = new bool[4];
            DPadDown = new bool[4];
            DPadLeft = new bool[4];
            DPadRight = new bool[4];

            // Initialize arrays for start and back button states.
            Start = new bool[4];
            Back = new bool[4];

            // Initialize arrays for stick button states.
            LeftStick = new bool[4];
            RightStick = new bool[4];

            TimeToVibe = 1000; // ms

            LeftVibrateStart = new DateTime[4];
            RightVibrateStart = new DateTime[4];

            for (int ControllerNumber = 0; ControllerNumber < 4; ControllerNumber++)
            {
                LeftVibrateStart[ControllerNumber] = DateTime.Now;

                RightVibrateStart[ControllerNumber] = DateTime.Now;

            }

            IsLeftVibrating = new bool[4];
            IsRightVibrating = new bool[4];

            // Call the TestInitialization method to verify the initial state of the controllers.
            TestInitialization();

        }

        public void Update()
        {
            TimeSpan ElapsedTime = DateTime.Now - TimeSinceLastConnectionCheck;

            // Every second check for connected controllers.
            if (ElapsedTime.TotalSeconds >= 1)
            {
                for (int controllerNumber = 0; controllerNumber <= 3; controllerNumber++) // Up to 4 controllers
                {
                    Connected[controllerNumber] = IsConnected(controllerNumber);

                }

                TimeSinceLastConnectionCheck = DateTime.Now;

            }

            for (int controllerNumber = 0; controllerNumber <= 3; controllerNumber++)
            {
                if (Connected[controllerNumber])
                {
                    UpdateState(controllerNumber);

                }

            }

            UpdateVibrateTimers();

        }

        private void UpdateState(int controllerNumber)
        {
            try
            {
                XInputGetState(controllerNumber, ref State);

                UpdateButtons(controllerNumber);

                UpdateThumbsticks(controllerNumber);

                UpdateTriggers(controllerNumber);
            }
            catch (Exception ex)
            {   // Something went wrong (An exception occurred).

                Debug.Print($"Error getting XInput state: {controllerNumber} | {ex.Message}");

            }

        }

        private void UpdateButtons(int controllerNumber)
        {   
            UpdateDPadButtons(controllerNumber);

            UpdateLetterButtons(controllerNumber);

            UpdateBumperButtons(controllerNumber);

            UpdateStickButtons(controllerNumber);

            UpdateStartBackButtons(controllerNumber);

            UpdateDPadNeutral(controllerNumber);

            UpdateLetterButtonsNeutral(controllerNumber);

            Buttons[controllerNumber] = State.Gamepad.wButtons;

        }

        private void UpdateThumbsticks(int controllerNumber)
        {
            UpdateLeftThumbstick(controllerNumber);

            UpdateRightThumbstickPosition(controllerNumber);

        }

        private void UpdateTriggers(int controllerNumber)
        {
            UpdateLeftTriggerPosition(controllerNumber);

            UpdateRightTriggerPosition(controllerNumber);

        }

        private readonly void UpdateDPadButtons(int CID)
        {
            DPadUp[CID] = (State.Gamepad.wButtons & (ushort)Button.DPadUp) != 0;
            DPadDown[CID] = (State.Gamepad.wButtons & (ushort)Button.DPadDown) != 0;
            DPadLeft[CID] = (State.Gamepad.wButtons & (ushort)Button.DPadLeft) != 0;
            DPadRight[CID] = (State.Gamepad.wButtons & (ushort)Button.DPadRight) != 0;

        }

        private readonly void UpdateLetterButtons(int CID)
        {
            A[CID] = (State.Gamepad.wButtons & (ushort)Button.A) != 0;
            B[CID] = (State.Gamepad.wButtons & (ushort)Button.B) != 0;
            X[CID] = (State.Gamepad.wButtons & (ushort)Button.X) != 0;
            Y[CID] = (State.Gamepad.wButtons & (ushort)Button.Y) != 0;

        }

        private readonly void UpdateBumperButtons(int CID)
        {
            LeftBumper[CID] = (State.Gamepad.wButtons & (ushort)Button.LeftBumper) != 0;
            RightBumper[CID] = (State.Gamepad.wButtons & (ushort)Button.RightBumper) != 0;

        }

        private readonly void UpdateStickButtons(int CID)
        {
            LeftStick[CID] = (State.Gamepad.wButtons & (ushort)Button.LeftStick) != 0;
            RightStick[CID] = (State.Gamepad.wButtons & (ushort)Button.RightStick) != 0;

        }

        private readonly void UpdateStartBackButtons(int CID)
        {
            Start[CID] = (State.Gamepad.wButtons & (ushort)Button.Start) != 0;
            Back[CID] = (State.Gamepad.wButtons & (ushort)Button.Back) != 0;

        }

        private void UpdateLeftThumbstick(int ControllerNumber)
        {
            UpdateLeftThumbstickXaxis(ControllerNumber);

            UpdateLeftThumbstickYaxis(ControllerNumber);

        }

        private readonly void UpdateLeftThumbstickYaxis(int ControllerNumber)
        {   // The range on the Y-axis is -32,768 through 32,767.
            // Signed 16-bit (2-byte) integer.

            // What position is the left thumbstick in on the Y-axis?
            if (State.Gamepad.sThumbLY <= NeutralStart)
            {   // The left thumbstick is in the down position.

                LeftThumbstickUp[ControllerNumber] = false;

                LeftThumbstickYaxisNeutral[ControllerNumber] = false;

                LeftThumbstickDown[ControllerNumber] = true;
            }
            else if (State.Gamepad.sThumbLY >= NeutralEnd)
            {   // The left thumbstick is in the up position.

                LeftThumbstickDown[ControllerNumber] = false;

                LeftThumbstickYaxisNeutral[ControllerNumber] = false;

                LeftThumbstickUp[ControllerNumber] = true;
            }
            else
            {   // The left thumbstick is in the neutral position.

                LeftThumbstickUp[ControllerNumber] = false;

                LeftThumbstickDown[ControllerNumber] = false;

                LeftThumbstickYaxisNeutral[ControllerNumber] = true;

            }

        }

        private readonly void UpdateLeftThumbstickXaxis(int ControllerNumber)
        {   // The range on the X-axis is -32,768 through 32,767.
            // Signed 16-bit (2-byte) integer.

            // What position is the left thumbstick in on the X-axis?
            if (State.Gamepad.sThumbLX <= NeutralStart)
            {   // The left thumbstick is in the left position.

                LeftThumbstickRight[ControllerNumber] = false;

                LeftThumbstickXaxisNeutral[ControllerNumber] = false;

                LeftThumbstickLeft[ControllerNumber] = true;
            }
            else if (State.Gamepad.sThumbLX >= NeutralEnd)
            {   // The left thumbstick is in the right position.

                LeftThumbstickLeft[ControllerNumber] = false;

                LeftThumbstickXaxisNeutral[ControllerNumber] = false;

                LeftThumbstickRight[ControllerNumber] = true;
            }
            else
            {   // The left thumbstick is in the neutral position.

                LeftThumbstickLeft[ControllerNumber] = false;

                LeftThumbstickRight[ControllerNumber] = false;

                LeftThumbstickXaxisNeutral[ControllerNumber] = true;

            }

        }

        private void UpdateRightThumbstickPosition(int controllerNumber)
        {
            UpdateRightThumbstickXaxis(controllerNumber);

            UpdateRightThumbstickYaxis(controllerNumber);

        }

        private readonly void UpdateRightThumbstickYaxis(int controllerNumber)
        {   // The range on the Y-axis is -32,768 through 32,767.
            // Signed 16-bit (2-byte) integer.

            // What position is the right thumbstick in on the Y-axis?
            if (State.Gamepad.sThumbRY <= NeutralStart)
            {   // The right thumbstick is in the up position.

                RightThumbstickDown[controllerNumber] = false;

                RightThumbstickYaxisNeutral[controllerNumber] = false;

                RightThumbstickUp[controllerNumber] = true;
            }
            else if (State.Gamepad.sThumbRY >= NeutralEnd)
            {   // The right thumbstick is in the down position.

                RightThumbstickUp[controllerNumber] = false;

                RightThumbstickYaxisNeutral[controllerNumber] = false;

                RightThumbstickDown[controllerNumber] = true;
            }
            else
            {   // The right thumbstick is in the neutral position.

                RightThumbstickUp[controllerNumber] = false;

                RightThumbstickDown[controllerNumber] = false;

                RightThumbstickYaxisNeutral[controllerNumber] = true;

            }

        }

        private readonly void UpdateRightThumbstickXaxis(int controllerNumber)
        {   // The range on the X-axis is -32,768 through 32,767.
            // Signed 16-bit (2-byte) integer.

            // What position is the right thumbstick in on the X-axis?
            if (State.Gamepad.sThumbRX <= NeutralStart)
            {   // The right thumbstick is in the left position.

                RightThumbstickRight[controllerNumber] = false;

                RightThumbstickXaxisNeutral[controllerNumber] = false;

                RightThumbstickLeft[controllerNumber] = true;
            }
            else if (State.Gamepad.sThumbRX >= NeutralEnd)
            {   // The right thumbstick is in the right position.

                RightThumbstickLeft[controllerNumber] = false;

                RightThumbstickXaxisNeutral[controllerNumber] = false;

                RightThumbstickRight[controllerNumber] = true;
            }
            else
            {   // The right thumbstick is in the neutral position.

                RightThumbstickLeft[controllerNumber] = false;

                RightThumbstickRight[controllerNumber] = false;

                RightThumbstickXaxisNeutral[controllerNumber] = true;

            }

        }

        private void UpdateRightTriggerPosition(int controllerNumber)
        {   // The range of right trigger is 0 to 255. Unsigned 8-bit (1-byte) integer.
            // The trigger position must be greater than the trigger threshold to
            // register as pressed.

            // What position is the right trigger in?
            if (State.Gamepad.bRightTrigger > TriggerThreshold)
            {   // The right trigger is in the fire position. Trigger Break. Bang!

                RightTrigger[controllerNumber] = true;
            }
            else
            {   // The right trigger is in the neutral position. Pre-Travel.

                RightTrigger[controllerNumber] = false;

            }

        }

        private void UpdateLeftTriggerPosition(int controllerNumber)
        {   // The range of left trigger is 0 to 255. Unsigned 8-bit (1-byte) integer.
            // The trigger position must be greater than the trigger threshold to
            // register as pressed.

            // What position is the left trigger in?
            if (State.Gamepad.bLeftTrigger > TriggerThreshold)
            {   // The left trigger is in the fire position. Trigger Break. Bang!

                LeftTrigger[controllerNumber] = true;
            }
            else
            {   // The left trigger is in the neutral position. Pre-Travel.

                LeftTrigger[controllerNumber] = false;

            }

        }

        private void UpdateDPadNeutral(int controllerNumber)
        {
            if (DPadDown[controllerNumber] ||
                DPadLeft[controllerNumber] ||
                DPadRight[controllerNumber] ||
                DPadUp[controllerNumber])
            {
                DPadNeutral[controllerNumber] = false;
            }
            else
            {
                DPadNeutral[controllerNumber] = true;

            }

        }

        private void UpdateLetterButtonsNeutral(int controllerNumber)
        {
            if (A[controllerNumber] ||
                B[controllerNumber] ||
                X[controllerNumber] ||
                Y[controllerNumber])
            {
                LetterButtonsNeutral[controllerNumber] = false;
            }
            else
            {
                LetterButtonsNeutral[controllerNumber] = true;

            }

        }

        public bool IsConnected(int controllerNumber)
        {
            try
            {
                return XInputGetState(controllerNumber, ref State) == 0;
                // 0 means the controller is connected.
                // Anything else (a non-zero value) means the controller is not
                // connected.
            }
            catch (Exception ex)
            {   // Something went wrong (An exception occurred).

                Debug.Print($"Error getting XInput state: {controllerNumber} | {ex.Message}");

                return false;

            }

        }

        public void TestInitialization()
        {
            //// Check that ConnectionStart is not null (initialization was successful)
            //Debug.Assert(ConnectionStart != null,
            //             "Connection Start should not be null.");



            // Allow a small tolerance for any slight delay between recording the expected time and setting the variable
            TimeSpan tolerance = TimeSpan.FromMilliseconds(100);

            // Calculate the difference between the expected time and the actual time
            TimeSpan difference = ExpectedTimeSinceLastConnectionCheck - TimeSinceLastConnectionCheck;

            // Assert that the timeSinceLastConnectionCheck time is within the tolerance of the expected time
            Debug.Assert(difference.Duration() <= tolerance, $"Difference {difference} exceeds tolerance {tolerance}");









            // Check that Buttons array is initialized
            Debug.Assert(Buttons != null,
                         "Buttons should not be null.");

            //Debug.Assert(TimeToVibe != null,
            //             "TimeToVibe should not be null.");

            for (int i = 0; i < 4; i++)
            {
                // Check that all controllers are initialized as not connected
                Debug.Assert(!Connected[i],
                             $"Controller {i} should not be connected after initialization.");

                // Check that all axes of the Left Thumbsticks are initialized as neutral.
                Debug.Assert(LeftThumbstickXaxisNeutral[i],
                             $"Left Thumbstick X-axis for Controller {i} should be neutral.");
                Debug.Assert(LeftThumbstickYaxisNeutral[i],
                             $"Left Thumbstick Y-axis for Controller {i} should be neutral.");

                // Check that all axes of the Right Thumbsticks are initialized as neutral.
                Debug.Assert(RightThumbstickXaxisNeutral[i],
                             $"Right Thumbstick X-axis for Controller {i} should be neutral.");
                Debug.Assert(RightThumbstickYaxisNeutral[i],
                             $"Right Thumbstick Y-axis for Controller {i} should be neutral.");

                // Check that all DPads are initialized as neutral.
                Debug.Assert(DPadNeutral[i],
                             $"DPad for Controller {i} should be neutral.");

                // Check that all Letter Buttons are initialized as neutral.
                Debug.Assert(LetterButtonsNeutral[i],
                             $"Letter Buttons for Controller {i} should be neutral.");

                // Check that additional Right Thumbstick states are not active.
                Debug.Assert(!RightThumbstickLeft[i],
                             $"Right Thumbstick Left for Controller {i} should not be true.");
                Debug.Assert(!RightThumbstickRight[i],
                             $"Right Thumbstick Right for Controller {i} should not be true.");
                Debug.Assert(!RightThumbstickDown[i],
                             $"Right Thumbstick Down for Controller {i} should not be true.");
                Debug.Assert(!RightThumbstickUp[i],
                             $"Right Thumbstick Up for Controller {i} should not be true.");

                // Check that additional Left Thumbstick states are not active.
                Debug.Assert(!LeftThumbstickLeft[i],
                             $"Left Thumbstick Left for Controller {i} should not be true.");
                Debug.Assert(!LeftThumbstickRight[i],
                             $"Left Thumbstick Right for Controller {i} should not be true.");
                Debug.Assert(!LeftThumbstickDown[i],
                             $"Left Thumbstick Down for Controller {i} should not be true.");
                Debug.Assert(!LeftThumbstickUp[i],
                             $"Left Thumbstick Up for Controller {i} should not be true.");

                // Check that trigger states are not active.
                Debug.Assert(!LeftTrigger[i],
                             $"Left Trigger for Controller {i} should not be true.");
                Debug.Assert(!RightTrigger[i],
                             $"Right Trigger for Controller {i} should not be true.");

                // Check that letter button states (A, B, X, Y) are not active.
                Debug.Assert(!A[i],
                             $"A for Controller {i} should not be true.");
                Debug.Assert(!B[i],
                             $"B for Controller {i} should not be true.");
                Debug.Assert(!X[i],
                             $"X for Controller {i} should not be true.");
                Debug.Assert(!Y[i],
                             $"Y for Controller {i} should not be true.");

                // Check that bumper button states are not active.
                Debug.Assert(!LeftBumper[i],
                             $"Left Bumper for Controller {i} should not be true.");
                Debug.Assert(!RightBumper[i],
                             $"Right Bumper for Controller {i} should not be true.");

                // Check that D-Pad directional states are not active.
                Debug.Assert(!DPadUp[i],
                             $"D-Pad Up for Controller {i} should not be true.");
                Debug.Assert(!DPadDown[i],
                             $"D-Pad Down for Controller {i} should not be true.");
                Debug.Assert(!DPadLeft[i],
                             $"D-Pad Left for Controller {i} should not be true.");
                Debug.Assert(!DPadRight[i],
                             $"D-Pad Right for Controller {i} should not be true.");

                // Check that start and back button states are not active.
                Debug.Assert(!Start[i],
                             $"Start Button for Controller {i} should not be true.");
                Debug.Assert(!Back[i],
                             $"Back Button for Controller {i} should not be true.");

                // Check that stick button states are not active.
                Debug.Assert(!LeftStick[i],
                             $"Left Stick for Controller {i} should not be true.");
                Debug.Assert(!RightStick[i],
                             $"Right Stick for Controller {i} should not be true.");

                //Debug.Assert(LeftVibrateStart[i] != null,
                //             $"Left Vibrate Start for Controller {i} should not be null.");
                //Debug.Assert(RightVibrateStart[i] != null,
                //             $"Right Vibrate Start for Controller {i} should not be null.");

                Debug.Assert(!IsLeftVibrating[i],
                             $"Is Left Vibrating for Controller {i} should not be true.");
                Debug.Assert(!IsRightVibrating[i],
                             $"Is Right Vibrating for Controller {i} should not be true.");

            }

            // For Lex

        }

        [DllImport("XInput1_4.dll")]
        private static extern int XInputSetState(int playerIndex, 
                                                 ref XINPUT_VIBRATION vibration);


        public struct XINPUT_VIBRATION
        {
            public ushort wLeftMotorSpeed;
            public ushort wRightMotorSpeed;
        }

        private XINPUT_VIBRATION Vibration;

        public void VibrateLeft(int cid, ushort speed)
        {   // The range of speed is 0 through 65,535. Unsigned 16-bit (2-byte) integer.
            // The left motor is the low-frequency rumble motor.

            // Set left motor speed.
            Vibration.wLeftMotorSpeed = speed;

            LeftVibrateStart[cid] = DateTime.Now;

            IsLeftVibrating[cid] = true;

        }

        public void VibrateRight(int cid, ushort speed)
        {   // The range of speed is 0 through 65,535. Unsigned 16-bit (2-byte) integer.
            // The right motor is the high-frequency rumble motor.

            // Set right motor speed.
            Vibration.wRightMotorSpeed = speed;

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
                {   // The motor speed was not set. Fail.

                    Debug.Print($"{controllerID} did not vibrate.  {Vibration.wLeftMotorSpeed} |  {Vibration.wRightMotorSpeed} ");

                }
            }
            catch (Exception ex)
            {
                Debug.Print($"Error sending vibration motor command: {controllerID} | {Vibration.wLeftMotorSpeed} |  {Vibration.wRightMotorSpeed} | {ex.Message}");

                return; // Exit the method.

            }

        }

        private void UpdateVibrateTimers()
        {
            UpdateLeftVibrateTimer();

            UpdateRightVibrateTimer();

        }

        private void UpdateLeftVibrateTimer()
        {
            for (int ControllerNumber = 0; ControllerNumber < 4; ControllerNumber++)
            {
                if (IsLeftVibrating[ControllerNumber])
                {
                    TimeSpan ElapsedTime = DateTime.Now - LeftVibrateStart[ControllerNumber];

                    if (ElapsedTime.TotalMilliseconds >= TimeToVibe)
                    {
                        IsLeftVibrating[ControllerNumber] = false;

                        // Turn left motor off (set zero speed).
                        Vibration.wLeftMotorSpeed = 0;

                    }

                    SendVibrationMotorCommand(ControllerNumber);

                }

            }

        }

        private void UpdateRightVibrateTimer()
        {
            for (int ControllerNumber = 0; ControllerNumber < 4; ControllerNumber++)
            {
                if (IsRightVibrating[ControllerNumber])
                {
                    TimeSpan ElapsedTime = DateTime.Now - RightVibrateStart[ControllerNumber];

                    if (ElapsedTime.TotalMilliseconds >= TimeToVibe)
                    {
                        IsRightVibrating[ControllerNumber] = false;

                        // Turn right motor off (set zero speed).
                        Vibration.wRightMotorSpeed = 0;

                    }

                    SendVibrationMotorCommand(ControllerNumber);

                }

            }

        }

    }

    public partial class Form1 : Form
    {
        private XboxControllers Controllers;

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeApp();

            Controllers.Initialize();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Controllers.Update();

            UpdateLabels();

            UpdateRumbleGroupUI();

        }

        private void ButtonVibrateLeft_Click(object sender, EventArgs e)
        {
            if (Controllers.Connected[(int)NumControllerToVib.Value])
            {
                Controllers.VibrateLeft((int)NumControllerToVib.Value, (ushort)TrackBarSpeed.Value);

            }

        }

        private void ButtonVibrateRight_Click(object sender, EventArgs e)
        {
            if (Controllers.Connected[(int)NumControllerToVib.Value])
            {
                Controllers.VibrateRight((int)NumControllerToVib.Value, (ushort)TrackBarSpeed.Value);

            }

        }

        private void TrackBarSpeed_Scroll(object sender, EventArgs e)
        {
            UpdateSpeedLabel();

        }

        private void NumericUpDownTimeToVib_ValueChanged(object sender, EventArgs e)
        {
            Controllers.TimeToVibe = (int)NumericUpDownTimeToVib.Value;

        }

        private void UpdateLabels()
        {
            for (int ControllerNumber = 0; ControllerNumber < 4; ControllerNumber++)
            {
                UpdateControllerStatusLabel(ControllerNumber);

                if (Controllers.Connected[ControllerNumber])
                {
                    UpdateThumbstickLabels(ControllerNumber);

                    UpdateTriggerLabels(ControllerNumber);

                    UpdateDPadLabel(ControllerNumber);

                    UpdateLetterButtonLabel(ControllerNumber);

                    UpdateStartBackLabels(ControllerNumber);

                    UpdateBumperLabels(ControllerNumber);

                    UpdateStickLabels(ControllerNumber);

                }

            }

        }

        private void UpdateTriggerLabels(int controllerNumber)
        {
            UpdateLeftTriggerLabel(controllerNumber);

            UpdateRightTriggerLabel(controllerNumber);

        }

        private void UpdateThumbstickLabels(int controllerNumber)
        {
            UpdateRightThumbstickLabels(controllerNumber);

            UpdateLeftThumbstickLabels(controllerNumber);

        }

        private void UpdateLeftThumbstickLabels(int controllerNumber)
        {
            UpdateLeftThumbstickXAxisLabel(controllerNumber);

            UpdateLeftThumbstickYAxisLabel(controllerNumber);

        }

        private void UpdateRightThumbstickLabels(int controllerNumber)
        {
            UpdateRightThumbstickXAxisLabel(controllerNumber);

            UpdateRightThumbstickYAxisLabel(controllerNumber);

        }

        private void UpdateRightTriggerLabel(int controllerNumber)
        {
            if (Controllers.RightTrigger[controllerNumber])
            {
                LabelRightTrigger.Text = $"Controller {controllerNumber} Right Trigger";

            }

            ClearRightTriggerLabel();

        }

        private void UpdateLeftTriggerLabel(int controllerNumber)
        {
            if (Controllers.LeftTrigger[controllerNumber])
            {
                LabelLeftTrigger.Text = 
                    $"Controller {controllerNumber} Left Trigger";

            }

            ClearLeftTriggerLabel();

        }

        private void UpdateLeftThumbstickYAxisLabel(int controllerNumber)
        {
            if (Controllers.LeftThumbstickUp[controllerNumber])
            {
                LabelLeftThumbY.Text = 
                    $"Controller {controllerNumber} Left Thumbstick Up";

            }

            if (Controllers.LeftThumbstickDown[controllerNumber])
            {
                LabelLeftThumbY.Text = 
                    $"Controller {controllerNumber} Left Thumbstick Down";

            }

            ClearLeftThumbstickYLabel();

        }

        private void UpdateLeftThumbstickXAxisLabel(int controllerNumber)
        {
            if (Controllers.LeftThumbstickLeft[controllerNumber])
            {
                LabelLeftThumbX.Text = 
                    $"Controller {controllerNumber} Left Thumbstick Left";

            }

            if (Controllers.LeftThumbstickRight[controllerNumber])
            {
                LabelLeftThumbX.Text = 
                    $"Controller {controllerNumber} Left Thumbstick Right";

            }

            ClearLeftThumbstickXLabel();

        }

        private void UpdateRightThumbstickYAxisLabel(int controllerNumber)
        {
            if (Controllers.RightThumbstickUp[controllerNumber])
            {
                LabelRightThumbY.Text = 
                    $"Controller {controllerNumber} Right Thumbstick Up";

            }

            if (Controllers.RightThumbstickDown[controllerNumber])
            {
                LabelRightThumbY.Text = 
                    $"Controller {controllerNumber} Right Thumbstick Down";

            }

            ClearRightThumbstickYLabel();

        }

        private void UpdateRightThumbstickXAxisLabel(int controllerNumber)
        {
            if (Controllers.RightThumbstickLeft[controllerNumber])
            {
                LabelRightThumbX.Text = 
                    $"Controller {controllerNumber} Right Thumbstick Left";

            }

            if (Controllers.RightThumbstickRight[controllerNumber])
            {
                LabelRightThumbX.Text = 
                    $"Controller {controllerNumber} Right Thumbstick Right";

            }

            ClearRightThumbstickXLabel();

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

        private void UpdateControllerStatusLabel(int controllerNumber)
        {   // Update the status label based on connection state.

            string status = Controllers.Connected[controllerNumber] ? "Connected" : "Not Connected";



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

        private void UpdateRumbleGroupUI()
        {
            int NumberOfConnectedControllers = 0;

            int HighestConnectedControllerNumber = 0;

            for (int ControllerNumber = 0; ControllerNumber < 4; ControllerNumber++)
            {
                if (Controllers.Connected[ControllerNumber])
                {
                    NumberOfConnectedControllers++;

                    HighestConnectedControllerNumber = ControllerNumber;

                }

            }

            if (NumberOfConnectedControllers > 0)
            {
                NumControllerToVib.Maximum = HighestConnectedControllerNumber;

                RumbleGroupBox.Enabled = true;

                if (Controllers.Connected[(int)NumControllerToVib.Value])
                {
                    ButtonVibrateLeft.Enabled = true;

                    ButtonVibrateRight.Enabled = true;

                    TrackBarSpeed.Enabled = true;

                    LabelSpeed.Enabled = true;

                    NumericUpDownTimeToVib.Enabled = true;

                    LabelTimeToVibe.Enabled = true;
                }
                else
                {
                    ButtonVibrateLeft.Enabled = false;

                    ButtonVibrateRight.Enabled = false;

                    TrackBarSpeed.Enabled = false;

                    LabelSpeed.Enabled = false;

                    NumericUpDownTimeToVib.Enabled = false;

                    LabelTimeToVibe.Enabled = false;

                }
            }
            else
            {
                NumControllerToVib.Maximum = 0;

                RumbleGroupBox.Enabled = false;

            }

        }

        private void UpdateDPadLabel(int controllerNumber)
        {
            string direction = GetDPadDirection(controllerNumber);

            if (!Controllers.DPadNeutral[controllerNumber])
            {
                LabelDPad.Text = $"Controller {controllerNumber} DPad {direction}";

            }

            ClearDPadLabel();

        }

        private void UpdateLetterButtonLabel(int controllerNumber)
        {
            string buttonText = GetButtonText(controllerNumber);

            // Are any letter buttons pressed?
            if (!Controllers.LetterButtonsNeutral[controllerNumber])
            {   // Yes, letter buttons are pressed.

                LabelButtons.Text = buttonText;

            }

            ClearLetterButtonsLabel();

        }

        private void UpdateStartBackLabels(int controllerNumber)
        {
            if (Controllers.Start[controllerNumber])
            {
                LabelStart.Text = $"Controller {controllerNumber} Start";

            }

            ClearStartLabel();

            if (Controllers.Back[controllerNumber])
            {
                LabelBack.Text = $"Controller {controllerNumber} Back";

            }

            ClearBackLabel();

        }

        private void UpdateBumperLabels(int controllerNumber)
        {
            if (Controllers.LeftBumper[controllerNumber])
            {
                LabelLeftBumper.Text = 
                    $"Controller {controllerNumber} Left Bumper";

            }

            ClearLeftBumperLabel();

            if (Controllers.RightBumper[controllerNumber])
            {
                LabelRightBumper.Text = 
                    $"Controller {controllerNumber} Right Bumper";

            }

            ClearRightBumperLabel();

        }

        private void UpdateStickLabels(int controllerNumber)
        {
            if (Controllers.LeftStick[controllerNumber])
            {
                LabelLeftThumbButton.Text = 
                    $"Controller {controllerNumber} Left Thumbstick Button";

            }

            ClearLeftThumbstickButtonLabel();

            if (Controllers.RightStick[controllerNumber])
            {
                LabelRightThumbButton.Text = 
                    $"Controller {controllerNumber} Right Thumbstick Button";

            }

            ClearRightThumbstickButtonLabel();

        }

        private void ClearLetterButtonsLabel()
        {   // Clears the letter buttons label when all controllers letter buttons
            // are up.

            // Assume all controllers' letter buttons neutral initially.
            bool Neutral = true; 

            // Search for a non-neutral letter button.
            for (int i = 0; i < 4; i++)
            {
                if (Controllers.Connected[i] && !Controllers.LetterButtonsNeutral[i])
                {   // A non-neutral letter button was found.

                    Neutral = false; // Report the non-neutral letter button.

                    break; // No need to search further, so stop the search.

                }

            }

            // Are all controllers' letter buttons in the neutral position?
            if (Neutral)
            {   // Yes, all controllers' letter buttons are in the neutral position.

                LabelButtons.Text = string.Empty; // Clear label.

            }

        }

        private void ClearLeftThumbstickYLabel()
        { // Clears the left thumbstick Y-axis label when all controllers left
          // thumbsticks on the Y-axis are neutral.

          // Assume all controllers left thumbsticks on the Y-axis are neutral initially.
            bool Neutral = true; 

            // Search for a non-neutral left thumbstick on the Y-axis.
            for (int i = 0; i < 4; i++)
            {
                if (Controllers.Connected[i] && !Controllers.LeftThumbstickYaxisNeutral[i])
                {   // A non-neutral thumbstick was found.

                    Neutral = false; // Report the non-neutral thumbstick.

                    break; // No need to search further so stop the search.

                }

            }

            // Are all controllers left thumbsticks on the Y-axis in the neutral
            // position?
            if (Neutral)
            {   // Yes, all controllers left thumbsticks on the Y-axis are in the
                // neutral position.

                LabelLeftThumbY.Text = string.Empty; // Clear label.

            }

        }

        private void ClearLeftThumbstickXLabel()
        {   // Clears the left thumbstick X-axis label when all controllers left
            // thumbsticks on the X-axis are neutral.

            // Assume all controllers left thumbsticks on the X-axis are neutral initially.
            bool Neutral = true; 

            // Search for a non-neutral left thumbstick on the X-axis.
            for (int i = 0; i < 4; i++)
            {
                if (Controllers.Connected[i] && !Controllers.LeftThumbstickXaxisNeutral[i])
                {   // A non-neutral thumbstick was found.

                    Neutral = false; // Report the non-neutral thumbstick.

                    break; // No need to search further so stop the search.

                }

            }

            // Are all controllers left thumbsticks on the X-axis in the neutral
            // position?
            if (Neutral)
            {   // Yes, all controllers left thumbsticks on the X-axis are in the
                // neutral position.

                LabelLeftThumbX.Text = string.Empty; // Clear label.

            }

        }

        private void ClearRightThumbstickXLabel()
        {   // Clears the right thumbstick X-axis label when all controllers' right
            // thumbsticks on the X-axis are neutral.

            // Assume all controllers' right thumbsticks on the X-axis are neutral initially.
            bool Neutral = true; 

            // Search for a non-neutral right thumbstick on the X-axis.
            for (int i = 0; i < 4; i++)
            {
                if (Controllers.Connected[i] && !Controllers.RightThumbstickXaxisNeutral[i])
                {   // A non-neutral thumbstick was found.

                    Neutral = false; // Report the non-neutral thumbstick.

                    break; // No need to search further, so stop the search.

                }

            }

            // Are all controllers' right thumbsticks on the X-axis in the neutral
            // position?
            if (Neutral)
            { // Yes, all controllers' right thumbsticks on the X-axis are in the
              // neutral position.

                LabelRightThumbX.Text = string.Empty; // Clear label.

            }

        }

        private void ClearRightThumbstickYLabel()
        {   // Clears the right thumbstick Y-axis label when all controllers' right
            // thumbsticks on the Y-axis are neutral.

            // Assume all controllers' right thumbsticks on the Y-axis are neutral initially.
            bool Neutral = true; 

            // Search for a non-neutral right thumbstick on the Y-axis.
            for (int i = 0; i < 4; i++)
            {
                if (Controllers.Connected[i] && !Controllers.RightThumbstickYaxisNeutral[i])
                {   // A non-neutral thumbstick was found.

                    Neutral = false; // Report the non-neutral thumbstick.

                    break; // No need to search further, so stop the search.

                }

            }

            // Are all controllers' right thumbsticks on the Y-axis in the neutral
            // position?
            if (Neutral)
            {   // Yes, all controllers' right thumbsticks on the Y-axis are in the
                // neutral position.

                LabelRightThumbY.Text = string.Empty; // Clear label.

            }

        }

        private void ClearRightTriggerLabel()
        {   // Clears the right trigger label when all controllers' right triggers
            // are neutral.

            // Assume all controllers' right triggers are neutral initially.
            bool NotActive = true; 

            // Search for a non-neutral right trigger.
            for (int i = 0; i < 4; i++)
            {
                if (Controllers.Connected[i] && Controllers.RightTrigger[i])
                {   // A active right trigger was found.

                    NotActive = false; // Report the non-neutral right trigger.

                    break; // No need to search further, so stop the search.

                }

            }

            // Are all controllers' right triggers in the neutral position?
            if (NotActive)
            {   // Yes, all controllers' right triggers are in the neutral position.

                LabelRightTrigger.Text = string.Empty; // Clear label.

            }

        }

        private void ClearLeftTriggerLabel()
        {   // Clears the left trigger label when all controllers' left triggers are
            // not active.

            // Assume all controllers' left triggers are not active initially.
            bool NotActive = true;

            // Search for a active left trigger.
            for (int i = 0; i < 4; i++)
            {
                if (Controllers.Connected[i] && Controllers.LeftTrigger[i])
                {   // A active left trigger was found.

                    NotActive = false; // Report the active left trigger.

                    break; // No need to search further, so stop the search.

                }

            }

            // Are all controllers' left triggers not active?
            if (NotActive)
            {   // Yes, all controllers' left triggers are not active.

                LabelLeftTrigger.Text = string.Empty; // Clear label.

            }

        }

        private void ClearLabels()
        {
            LabelButtons.Text = string.Empty;

            LabelDPad.Text = string.Empty;

            LabelLeftThumbX.Text = string.Empty;
            LabelLeftThumbY.Text = string.Empty;

            LabelRightThumbX.Text = string.Empty;
            LabelRightThumbY.Text = string.Empty;

            LabelLeftTrigger.Text = string.Empty;
            LabelRightTrigger.Text = string.Empty;

            LabelStart.Text = string.Empty;
            LabelBack.Text = string.Empty;

            LabelLeftBumper.Text = string.Empty;
            LabelRightBumper.Text = string.Empty;

            LabelLeftThumbButton.Text = string.Empty;
            LabelRightThumbButton.Text = string.Empty;

        }

        private void ClearDPadLabel()
        {   // Clears the DPad label when all controllers' DPad are neutral.

            // Assume all controllers' DPad are neutral initially.
            bool Neutral = true; 

            // Search for a non-neutral DPad.
            for (int i = 0; i < 4; i++)
            {
                if (Controllers.Connected[i] && !Controllers.DPadNeutral[i])
                {   // A non-neutral DPad was found.

                    Neutral = false; // Report the non-neutral DPad.

                    break; // No need to search further, so stop the search.

                }

            }

            // Are all controllers' DPad in the neutral position?
            if (Neutral)
            {   // Yes, all controllers' DPad are in the neutral position.

                LabelDPad.Text = string.Empty; // Clear label.

            }

        }

        private void ClearStartLabel()
        {   // Clears the start label when all controllers' start buttons are not active.

            // Assume all controllers' start buttons are not active initially.
            bool NotActive = true;

            // Search for a active start buttons.
            for (int i = 0; i < 4; i++)
            {
                if (Controllers.Connected[i] && Controllers.Start[i])
                {   // A active start buttons was found.

                    NotActive = false; // Report the active start buttons.

                    break; // No need to search further, so stop the search.

                }

            }

            // Are all controllers' start buttons not active?
            if (NotActive)
            {   // Yes, all controllers' start buttons are not active.

                LabelStart.Text = string.Empty; // Clear label.

            }

        }

        private void ClearBackLabel()
        {   // Clears the back buttons label when all controllers' back buttons are not active.

            // Assume all controllers' back buttons are not active initially.
            bool NotActive = true; 

            // Search for a active back button.
            for (int i = 0; i < 4; i++)
            {
                if (Controllers.Connected[i] && Controllers.Back[i])
                {   // A active back buttons was found.

                    NotActive = false; // Report the active back button.

                    break; // No need to search further, so stop the search.

                }

            }

            // Are all controllers' back buttons not active?
            if (NotActive)
            {   // Yes, all controllers' back buttons are not active.

                LabelBack.Text = string.Empty; // Clear label.

            }

        }

        private void ClearLeftBumperLabel()
        {   // Clears the left bumper label when all controllers' left bumper are
            // not active.

            // Assume all controllers' left bumper are not active initially.
            bool NotActive = true;

            // Search for a active left bumper.
            for (int i = 0; i < 4; i++)
            {
                if (Controllers.Connected[i] && Controllers.LeftBumper[i])
                {   // A active left bumper was found.

                    NotActive = false; // Report the active left bumper.

                    break; // No need to search further, so stop the search.

                }

            }

            // Are all controllers' left bumpers not active?
            if (NotActive)
            {   // Yes, all controllers' left bumpers are not active.

                LabelLeftBumper.Text = string.Empty; // Clear label.

            }

        }

        private void ClearRightBumperLabel()
        {   // Clears the right bumper label when all controllers' right bumpers are
            // not active.

            // Assume all controllers' right bumpers are not active initially.
            bool NotActive = true;

            // Search for a active right bumper.
            for (int i = 0; i < 4; i++)
            {
                if (Controllers.Connected[i] && Controllers.RightBumper[i])
                {   // A active right bumper was found.


                    NotActive = false; // Report the active right bumper.

                    break; // No need to search further, so stop the search.

                }

            }

            // Are all controllers' right bumpers not active?
            if (NotActive)
            {   // Yes, all controllers' right bumpers are not active.

                LabelRightBumper.Text = string.Empty; // Clear label.

            }

        }

        private void ClearLeftThumbstickButtonLabel()
        {   // Clears the left thumbstick button label when all controllers' left thumbstick
            // buttons are not active.

            // Assume all controllers' left thumbstick buttons are not active initially.
            bool NotActive = true;

            // Search for a active left thumbstick button.
            for (int i = 0; i < 4; i++)
            {
                if (Controllers.Connected[i] && Controllers.LeftStick[i])
                {   // A active left thumbstick button was found.

                    NotActive = false; // Report the active left thumbstick button.

                    break; // No need to search further, so stop the search.

                }

            }

            // Are all controllers' left thumbstick buttons not active?
            if (NotActive)
            {   // Yes, all controllers' left thumbstick buttons are not active.

                LabelLeftThumbButton.Text = string.Empty; // Clear label.

            }

        }

        private void ClearRightThumbstickButtonLabel()
        {   // Clears the right thumbstick button label when all controllers' right
            // thumbstick buttons are not active.

            // Assume all controllers' right thumbstick buttons are not active initially.
            bool NotActive = true;

            // Search for a active right thumbstick button.
            for (int i = 0; i < 4; i++)
            {
                if (Controllers.Connected[i] && Controllers.RightStick[i])
                {   // A active right thumbstick button was found.

                    NotActive = false; // Report the active right thumbstick button.

                    break; // No need to search further, so stop the search.

                }

            }

            // Are all controllers' right thumbstick buttons not active?
            if (NotActive)
            {   // Yes, all controllers' right thumbstick buttons are not active.

                LabelRightThumbButton.Text = string.Empty; // Clear label.

            }

        }

        private string GetDPadDirection(int controllerNumber)
        {
            if (Controllers.DPadUp[controllerNumber])
            {
                if (Controllers.DPadLeft[controllerNumber]) return "Left+Up";

                if (Controllers.DPadRight[controllerNumber]) return "Right+Up";

                return "Up";

            }

            if (Controllers.DPadDown[controllerNumber])
            {
                if (Controllers.DPadLeft[controllerNumber]) return "Left+Down";

                if (Controllers.DPadRight[controllerNumber]) return "Right+Down";

                return "Down";

            }

            if (Controllers.DPadLeft[controllerNumber]) return "Left";

            if (Controllers.DPadRight[controllerNumber]) return "Right";

            return string.Empty; // Return an empty string if no buttons are pressed.

        }

        private string GetButtonText(int controllerNumber)
        {
            List<string> buttons = new List<string>();

            if (Controllers.A[controllerNumber]) buttons.Add("A");

            if (Controllers.B[controllerNumber]) buttons.Add("B");

            if (Controllers.X[controllerNumber]) buttons.Add("X");

            if (Controllers.Y[controllerNumber]) buttons.Add("Y");

            if (buttons.Count > 0)
            {
                return $"Controller {controllerNumber} Buttons: {string.Join("+", buttons)}";

            }

            return string.Empty; // Return an empty string if no buttons are pressed

        }

        private void UpdateSpeedLabel()
        {
            LabelSpeed.Text = $"Speed: {TrackBarSpeed.Value}";

        }

        private void InitializeApp()
        {
            Text = "XInput C# - Code with Joe";

            InitializeTimer1();

            ClearLabels();

            TrackBarSpeed.Value = 32767;

            UpdateSpeedLabel();

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

            ToolTipRumbleGroup.SetToolTip(RumbleGroupBox, TipText);

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


