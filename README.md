# XInput C#

🎮 Welcome to XInput C#, your go-to solution for integrating Xbox controller support into your applications! This feature-rich application showcases the seamless integration of Xbox controllers, complete with vibration effects and real-time controller state monitoring.


![002](https://github.com/user-attachments/assets/a2e785c8-6ba1-4075-b337-2aaee643cd30)



With a clean and well-commented codebase, this project serves as an invaluable resource for developers looking to harness the power of XInput in their Windows applications. Whether you're a seasoned developer or just getting started, the XInput app provides a solid foundation for building immersive gaming experiences and beyond.



I converted XInput from VB .NET to C#.



## Things to watch out for when converting from VB .NET to C#

Here are some key syntax differences.

### 1. Imports and Namespace Declarations

VB.NET: Uses ```Imports``` .

```vb

Imports System.Runtime.InteropServices

```
C#: Uses ```using``` and ends with a semicolon ```;``` .

```csharp

using System.Runtime.InteropServices;

```

### 2. Class Declaration

VB.NET: Capitalizes ```Public``` .

```vb

Public Class Form1

```

C#: Uses lowercase ```public``` .

```csharp

public class Form1

```

### 3. Attributes

VB.NET: Attributes are defined using angle brackets ```<>``` .

```vb

<DllImport("XInput1_4.dll")>

```

C#: Attributes are defined using square brackets ```[]``` .

```csharp

[DllImport("XInput1_4.dll")]

```

### 4. Function Declaration

VB.NET: Uses ```Shared``` to indicate that the function belongs to the class rather than an instance and ```ByRef```  to pass the parameter by reference.

```vb

Private Shared Function XInputGetState(dwUserIndex As Integer, ByRef pState As XINPUT_STATE) As Integer

```

C#: Uses ```extern``` to indicate external function, ```ref```  to pass the parameter by reference and ends with a semicolon ```;``` .

```csharp

private static extern int XInputGetState(int dwUserIndex, ref XINPUT_STATE pState);

```

### 5. Structure Declaration

VB.NET: Uses ```Structure``` .

```vb

<StructLayout(LayoutKind.Explicit)>
Public Structure XINPUT_STATE

```

C#: Uses ```struct``` .

```csharp

[StructLayout(LayoutKind.Explicit)]
public struct XINPUT_STATE

```

### 6. Field Declaration

VB.NET: Uses ```As``` to specify the type.

```vb

<FieldOffset(0)>
Public dwPacketNumber As UInteger

```

C#: Ends declarations with a semicolon ```;``` .

```csharp

[FieldOffset(0)]
public uint dwPacketNumber;

```

### 7. Arrays Declaration

VB.NET: Uses parentheses ```()``` and a range ```0 To 3``` .

```vb

Private ConButtons(0 To 3) As UShort

```

C#: Uses square brackets ```[]``` , ```new``` and ends with a semicolon ```;``` .

```csharp

private ushort[] ConButtons = new ushort[4];

```

### 8. Constants Declaration

VB.NET: Uses ```Const``` and ```As``` to specify the type.

```vb

Private Const NeutralStart As Short = -16384

```

C#: Uses ```const``` and ends with a semicolon ```;``` .

```csharp

private const short NeutralStart = -16384;

```

### 9. Enum Declaration

VB.NET: Uses ```End Enum``` .

```vb

Public Enum BATTERY_TYPE As Byte
    DISCONNECTED = 0
    WIRED = 1
End Enum

```

C#: Uses curly braces ```{}``` .

```csharp

public enum BATTERY_TYPE : byte
{
    DISCONNECTED = 0,
    WIRED = 1
}

```


### 10. Subroutine Declaration

VB.NET: Uses ```Sub``` for subroutines and includes event handlers with ```Handles``` .

```vb

Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

```

C#: Uses ```void``` for return types.

```csharp

private void Form1_Load(object sender, EventArgs e)

```

### 11. If Statement with AndAlso

VB.NET: Uses ```AndAlso``` for logical AND and ```=``` for comparisons.

```vb

If DPadUpPressed = True AndAlso DPadDownPressed = False Then

```

C#: Uses ```&&``` for logical AND and ```!``` for NOT.

```csharp

if (DPadUpPressed && !DPadDownPressed)

```

### 12. Try-Catch Block

VB.NET: Uses ```Try``` and ```End Try``` to define the block.

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

VB.NET: Uses ```For Each``` .

```vb

For Each Con In ConButtons

```

C#: Uses ```foreach``` .

```csharp

foreach (var con in ConButtons)

```

### 14. Return Statement

VB.NET: Uses ```Return``` .

```vb

Return XInputGetState(controllerNumber, ControllerPosition) = 0

```

C#: Uses ```return``` .

```csharp

return XInputGetState(controllerNumber, ControllerPosition) == 0;

```

### 15. String Concatenation

VB.NET: Uses ```&``` for concatenation.

```vb

LabelButtons.Text = "Controller " & ControllerNumber.ToString & " Button: Up"

```

C#: Uses ```+``` for concatenation.

```csharp

LabelButtons.Text = "Controller " + ControllerNumber.ToString() + " Button: Up";

```


These examples illustrate some of the common syntax differences you'll encounter when converting VB .NET code to C#.




