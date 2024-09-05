# XInput C#

🎮 Welcome to XInput C#, your go-to solution for integrating Xbox controller support into your applications! This feature-rich application showcases the seamless integration of controllers, complete with vibration effects and real-time controller state monitoring.



![025](https://github.com/user-attachments/assets/1c3e7e36-4070-4c47-8eea-23a77d317cee)


With a clean and well-commented codebase, this project serves as an invaluable resource for developers looking to harness the power of XInput in their Windows applications. Whether you're a seasoned developer or just getting started, the XInput app provides a solid foundation for building immersive gaming experiences and beyond.


![063](https://github.com/user-attachments/assets/a8d75d93-acac-4071-9e8c-fb60a82f4636)














# Code Walkthrough
### Imports and DLL Function Declarations
At the beginning of the ```Form1.vb``` file, we import necessary libraries and declare functions from the XInput DLL.

``` vbnet
Imports System.Runtime.InteropServices

<DllImport("XInput1_4.dll")>
Private Shared Function XInputGetState(dwUserIndex As Integer, ByRef pState As XINPUT_STATE) As Integer
End Function
```

**Imports System.Runtime.InteropServices:** This line allows us to use features that let managed code (like our VB.NET code) interact with unmanaged code (like the XInput DLL).

**DllImport:** This attribute tells the program that we want to use a function from an external library (the XInput DLL) to get the state of the Xbox controller.



### Defining Structures

Next, we define structures that represent the controller's state and input.

``` vbnet
<StructLayout(LayoutKind.Explicit)>
Public Structure XINPUT_STATE
    <FieldOffset(0)>
    Public dwPacketNumber As UInteger
    <FieldOffset(4)>
    Public Gamepad As XINPUT_GAMEPAD
End Structure
```
**StructLayout:** This attribute specifies how the fields of the structure are laid out in memory.

**XINPUT_STATE:** This structure holds the state of the controller, including a packet number (used to track changes) and the gamepad data.

``` vbnet
<StructLayout(LayoutKind.Sequential)>
Public Structure XINPUT_GAMEPAD
    Public wButtons As UShort
    Public bLeftTrigger As Byte
    Public bRightTrigger As Byte
    Public sThumbLX As Short
    Public sThumbLY As Short
    Public sThumbRX As Short
    Public sThumbRY As Short
End Structure
```

**XINPUT_GAMEPAD:** This structure contains information about the buttons pressed and the positions of the thumbsticks and triggers.


### Initializing the Application


When the form loads, we initialize the application.

``` vbnet
Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    InitializeApp()
End Sub
```

**Form1_Load:** This is an event handler that runs when the form is loaded. It calls the ```InitializeApp()``` method, which sets up the application.


### Timer for Polling Controller State

We use a timer to regularly check the controller's state.

``` vbnet
Private Sub InitializeTimer1()
    Timer1.Interval = 15 ' Set the timer to tick every 15 milliseconds
    Timer1.Start()       ' Start the timer
End Sub
```

**Timer1.Interval:** This sets how often the timer will trigger (every 15 milliseconds).

**Timer1.Start():** This starts the timer, which will call the ```Timer1_Tick``` method repeatedly.

### Updating Controller Data

In the timer's tick event, we update the controller data.

``` vbnet
Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
    UpdateControllerData()
End Sub
```

**UpdateControllerData():** This method checks the state of the controllers and updates the UI accordingly.

### Getting Controller State

Inside ```UpdateControllerData```, we retrieve the current state of each connected controller.

``` vbnet
For controllerNumber As Integer = 0 To 3
    Connected(controllerNumber) = IsControllerConnected(controllerNumber)
    If Connected(controllerNumber) Then
        UpdateControllerState(controllerNumber)
    End If
Next
```

**For loop:** This loop checks up to four controllers (0 to 3).

**IsControllerConnected(controllerNumber):** This function checks if a controller is connected and returns true or false.

**UpdateControllerState(controllerNumber):** If the controller is connected, this method retrieves its current state.


### Updating Button States



When we retrieve the controller state, we check which buttons are pressed.

``` vbnet
Private Sub UpdateButtonPosition(CID As Integer)
    DPadUpPressed = (ControllerPosition.Gamepad.wButtons And DPadUp) <> 0
    ' Similar checks for other buttons...
End Sub
```

**wButtons:** This field contains the state of all buttons as a number.

**Bitwise AND operator (```And```):** This checks if a specific button is pressed by comparing it to a constant (like ```DPadUp```).

### Vibration Control


To control the vibration of the controller, we have buttons in the UI.

``` vbnet
Private Sub ButtonVibrateLeft_Click(sender As Object, e As EventArgs) Handles ButtonVibrateLeft.Click
    VibrateLeft(NumControllerToVib.Value, TrackBarSpeed.Value)
End Sub
```

**ButtonVibrateLeft_Click:** This event runs when the "Vibrate Left" button is clicked.

**VibrateLeft():** This method triggers vibration on the specified controller with the desired intensity.




This application provides a hands-on way to interact with Xbox controllers using VB.NET. By understanding each section of the code, you can see how the application retrieves controller states, manages input, and provides feedback through vibration.

Feel free to experiment with the code, modify it, and add new features as you learn more about programming! If you have any questions, don’t hesitate to ask.













































# Things to watch out for when converting from VB to C#

Hi GitHub community! I’m thrilled to share my recent journey of porting my VB app, "XInput," into its new C# counterpart, "XInput CS." This experience has been both challenging and rewarding, and I’d love to share some insights that might help others considering a similar transition.

Here are some key syntax differences.

### 1. Imports and Namespace Declarations

VB: The ```Imports``` statement is used to include namespaces in the file. This allows you to use the classes and methods defined in the ```System.Runtime.InteropServices``` namespace without needing to fully qualify them.

```vb

Imports System.Runtime.InteropServices

```
C#: The ```using``` directive is used to include namespaces in the file. This allows you to use the classes and methods defined in the ```System.Runtime.InteropServices``` namespace without needing to fully qualify them.

```csharp

using System.Runtime.InteropServices;

```



![022](https://github.com/user-attachments/assets/afc2aced-e8eb-4156-95db-548bb54d4eba)





### 2. Class Declaration

VB: Classes are declared using the ```Class``` keyword. The visibility modifier ```Public``` is capitalized.

```vb

Public Class Form1

```

C#: Classes are declared using the ```class``` keyword. The visibility modifier ```public``` is in lowercase.

```csharp

public class Form1

```

### 3. Attributes

VB: Attributes are defined using angle brackets ```<>``` .

```vb

<DllImport("XInput1_4.dll")>

```

C#: Attributes are defined using square brackets ```[]``` .

```csharp

[DllImport("XInput1_4.dll")]

```

### 4. Function Declaration

VB: ```Shared``` keyword is used for static methods and ```ByRef``` is used to pass parameters by reference.

```vb

Private Shared Function XInputGetState(dwUserIndex As Integer, ByRef pState As XINPUT_STATE) As Integer

```

C#: ```extern``` keyword is used to indicate external function, ```static``` keyword is used for static methods, ```ref```  to pass the parameter by reference and ends with a semicolon ```;``` .

```csharp

private static extern int XInputGetState(int dwUserIndex, ref XINPUT_STATE pState);

```

### 5. Structure Declaration

VB: ```Structure``` keyword is followed by the struct name and its members are defined within ```Structure``` and  ```End Structure``` . 

```vb
<StructLayout(LayoutKind.Explicit)>
Public Structure XINPUT_STATE
    <FieldOffset(0)>
    Public dwPacketNumber As UInteger
    <FieldOffset(4)>
    Public Gamepad As XINPUT_GAMEPAD
End Structure

```

C#: ```struct``` keyword is followed by the struct name and its members are defined within curly braces ```{}``` . 

```csharp

[StructLayout(LayoutKind.Explicit)]
public struct XINPUT_STATE
{
    [FieldOffset(0)]
    public uint dwPacketNumber;
    [FieldOffset(4)]
    public XINPUT_GAMEPAD Gamepad;
}

```

### 6. Field Declaration

VB: Fields are declared using the ```As``` keyword to specify the type. The ```FieldOffset``` attribute specifies the position of the field within the structure. Attributes are defined using angle brackets ```<>``` .

```vb

<FieldOffset(0)>
Public dwPacketNumber As UInteger

```

C#: Fields are declared with a semicolon ```;``` at the end. The ```FieldOffset``` attribute specifies the position of the field within the structure. Attributes are defined using square brackets ```[]``` .

```csharp

[FieldOffset(0)]
public uint dwPacketNumber;

```

### 7. Arrays Declaration

VB: Arrays are declared using parentheses ```()``` and using a range ```(0 To 3)``` to define the size.

```vb

Private ConButtons(0 To 3) As UShort

```

C#: Arrays are declared using square brackets ```[]``` and initialized with the ```new``` keyword.

```csharp

private ushort[] ConButtons = new ushort[4];

```











### 8. Constants Declaration

VB: Constants are declared using the ```Const``` keyword and the ```As``` keyword to specify the type.

```vb

Private Const NeutralStart As Short = -16384

```

C#: Constants are declared using the ```const``` keyword and a semicolon ```;``` at the end.

```csharp

private const short NeutralStart = -16384;

```

### 9. Enum Declaration

VB: Enums are declared using the ```Enum``` keyword and ```End Enum``` to close the declaration.

```vb

Public Enum BATTERY_TYPE As Byte
    DISCONNECTED = 0
    WIRED = 1
End Enum

```

C#: Enums are declared using the ```enum``` keyword and curly braces ```{}``` to define the body.

```csharp

public enum BATTERY_TYPE : byte
{
    DISCONNECTED = 0,
    WIRED = 1
}

```


### 10. Subroutine Declaration

VB: Subroutines are declared using the ```Sub``` keyword. The ```Handles``` keyword is used to specify the event handler.

```vb

Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

```

C#: Subroutines (methods that do not return a value) are declared using the ```void``` keyword. The ```void``` keyword is used to indicate that a method does not return any value. 

```csharp

private void Form1_Load(object sender, EventArgs e)

```

### 11. If Statement with AndAlso

VB: Uses ```AndAlso``` for logical AND and ```=``` for comparisons.

```vb

If DPadUpPressed = True AndAlso DPadDownPressed = False Then

```

C#: Uses ```&&``` for logical AND and ```!``` for NOT.

```csharp

if (DPadUpPressed && !DPadDownPressed)

```

### 12. Try-Catch Block

VB: Uses ```Try``` and ```End Try``` to define the block.

```vb

Try
    ' Code
Catch ex As Exception
    DisplayError(ex)
End Try

```

C#: Uses braces ```{}``` to define the block.

```csharp

try
{
    // Code
}
catch (Exception ex)
{
    DisplayError(ex);
}

```

### 13. For Each Loop

VB: The ```For Each``` keyword is used for iteration.

```vb

For Each Con In ConButtons

```

C#: The ```foreach``` keyword is used to iterate through collections.

```csharp

foreach (var con in ConButtons)

```

### 14. Return Statement

VB: The ```Return``` keyword is used to return a value, and ```=``` is used for comparison.

```vb

Return XInputGetState(controllerNumber, ControllerPosition) = 0

```

C#: The ```return``` keyword is used to return a value from a function, and ```==``` is used for comparison.

```csharp

return XInputGetState(controllerNumber, ControllerPosition) == 0;

```

### 15. String Concatenation

VB: Strings are concatenated using the ```&``` operator.

```vb

LabelButtons.Text = "Controller " & ControllerNumber.ToString & " Button: Up"

```

C#: Strings are concatenated using the ```+``` operator.

```csharp

LabelButtons.Text = "Controller " + ControllerNumber.ToString() + " Button: Up";

```


### 16. DateTime Handling

VB: Uses ```Now``` to get the current date and time.

```vb

Dim currentTime As DateTime = Now

```

C#: Uses ```DateTime.Now``` to get the current date and time.

```csharp

DateTime currentTime = DateTime.Now;

```

These examples illustrate some of the common syntax differences you'll encounter when converting VB code to C#.



## A Funny Thing Happened on the Way to Porting My App

So, I embarked on a journey to port my app, XInput , from VB to C# with the help of my AI assistant, Monica. Let me tell you, Monica is a game changer!

She zipped through converting the VB code to C# at lightning speed, as AI assistants do. But where she really shines is in her suggestions. Every time I asked for C# code, she’d nudge me with ideas like, “How about a function?” And I’d be like, “Oh yeah! That does look better. Maybe I should use more functions?”

Monica was really pushing me ahead, keeping my code clean and efficient. Thanks, Monica! I guess? 😄

In the midst of all this, I got a little carried away and redesigned the app’s interface. Now, I have to go back and redo the original app’s interface to match! Because, you know, I’m that type of guy. They need to look good side by side!

![023](https://github.com/user-attachments/assets/fde768e4-e891-4da7-abb3-5b364e2233b5)





