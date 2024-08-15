# XInput C#

🎮 Welcome to XInput C#, your go-to solution for integrating Xbox controller support into your applications! This feature-rich application showcases the seamless integration of Xbox controllers, complete with vibration effects and real-time controller state monitoring.


![002](https://github.com/user-attachments/assets/a2e785c8-6ba1-4075-b337-2aaee643cd30)



With a clean and well-commented codebase, this project serves as an invaluable resource for developers looking to harness the power of XInput in their Windows applications. Whether you're a seasoned developer or just getting started, the XInput app provides a solid foundation for building immersive gaming experiences and beyond.



I converted XInput from VB to C#.

![017](https://github.com/user-attachments/assets/e35bbf94-25a9-4dd9-8270-d2f1d5ec0f95)


## Things to watch out for when converting from VB .NET to C#

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
    Public dwPacketNumber As Integer
    Public GamePadState As GamePadState
End Structure

```

C#: ```struct``` keyword is followed by the struct name and its members are defined within curly braces ```{}``` . 

```csharp

[StructLayout(LayoutKind.Explicit)]
public struct XINPUT_STATE
{
    public int dwPacketNumber;
    public GamePadState GamePadState;
}

```

### 6. Field Declaration

VB: Uses ```As``` to specify the type. Fields are declared using the ```As``` keyword to specify the type. The ```FieldOffset``` attribute specifies the position of the field within the structure.

```vb

<FieldOffset(0)>
Public dwPacketNumber As UInteger

```

C#: Fields are declared with a semicolon ```;``` at the end. The ```FieldOffset``` attribute specifies the position of the field within the structure.

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

![016](https://github.com/user-attachments/assets/79987297-2c32-4b43-8bed-949c6ec9f204)


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


These examples illustrate some of the common syntax differences you'll encounter when converting VB .NET code to C#.




