# XInput C#

🎮 Welcome to XInput C#, your go-to solution for integrating Xbox controller support into your applications! This feature-rich application showcases the seamless integration of Xbox controllers, complete with vibration effects and real-time controller state monitoring.


![002](https://github.com/user-attachments/assets/a2e785c8-6ba1-4075-b337-2aaee643cd30)



With a clean and well-commented codebase, this project serves as an invaluable resource for developers looking to harness the power of XInput in their Windows applications. Whether you're a seasoned developer or just getting started, the XInput app provides a solid foundation for building immersive gaming experiences and beyond.



I converted XInput from VB .NET to C#.



## Things to watch out for when converting from VB .NET to C#

Here are some key syntax differences.

### 1. Imports and Namespace Declarations

VB.NET:

```vb

Imports System.Runtime.InteropServices

```
C#:

```csharp

using System.Runtime.InteropServices;

```

### 2. Class Declaration

VB.NET:

```vb

Public Class Form1

```

C#:

```csharp

public class Form1

```

### 3. Attributes

VB.NET:

```vb

<DllImport("XInput1_4.dll")>

```

C#:

```csharp

[DllImport("XInput1_4.dll")]

```

### 4. Function Declaration

VB.NET:

```vb

Private Shared Function XInputGetState(dwUserIndex As Integer, ByRef pState As XINPUT_STATE) As Integer

```

C#:

```csharp

private static extern int XInputGetState(int dwUserIndex, ref XINPUT_STATE pState);

```

### 5. Structure Declaration

VB.NET:

```vb

<StructLayout(LayoutKind.Explicit)>
Public Structure XINPUT_STATE

```

C#:

```csharp

[StructLayout(LayoutKind.Explicit)]
public struct XINPUT_STATE

```

### 6. Field Declaration

VB.NET:

```vb

<FieldOffset(0)>
Public dwPacketNumber As UInteger

```

C#:

```csharp

[FieldOffset(0)]
public uint dwPacketNumber;

```

### 7. Arrays Declaration

VB.NET:

```vb

Private ConButtons(0 To 3) As UShort

```

C#:

```csharp

private ushort[] ConButtons = new ushort[4];

```

### 8. Constants Declaration

VB.NET:

```vb

Private Const NeutralStart As Short = -16384

```

C#:

```csharp

private const short NeutralStart = -16384;

```

### 9. Enum Declaration

VB.NET:

```vb

Public Enum BATTERY_TYPE As Byte
    DISCONNECTED = 0
    WIRED = 1
End Enum

```

C#:

```csharp

public enum BATTERY_TYPE : byte
{
    DISCONNECTED = 0,
    WIRED = 1
}

```

### 10. Subroutine Declaration

VB.NET:

```vb

Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

```

C#:

```csharp

private void Form1_Load(object sender, EventArgs e)

```

### 11. If Statement with AndAlso

VB.NET:

```vb

If DPadUpPressed = True AndAlso DPadDownPressed = False Then

```

C#:

```csharp

if (DPadUpPressed && !DPadDownPressed)

```

### 12. Try-Catch Block

VB.NET:

```vb

Try
    ' Code
Catch ex As Exception
    DisplayError(ex)
End Try

```

C#:

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

VB.NET:

```vb

For Each Con In ConButtons

```

C#:

```csharp

foreach (var con in ConButtons)

```

### 14. Return Statement

VB.NET:

```vb

Return XInputGetState(controllerNumber, ControllerPosition) = 0

```

C#:

```csharp

return XInputGetState(controllerNumber, ControllerPosition) == 0;

```

### 15. String Concatenation

VB.NET:

```vb

LabelButtons.Text = "Controller " & ControllerNumber.ToString & " Button: Up"

```

C#:

```csharp

LabelButtons.Text = "Controller " + ControllerNumber.ToString() + " Button: Up";

```

These examples illustrate some of the common syntax differences you'll encounter when converting VB .NET code to C#.




