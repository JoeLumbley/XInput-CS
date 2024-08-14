# XInput C#

🎮 Welcome to XInput C#, your go-to solution for integrating Xbox controller support into your applications! This feature-rich application showcases the seamless integration of Xbox controllers, complete with vibration effects and real-time controller state monitoring.


![002](https://github.com/user-attachments/assets/a2e785c8-6ba1-4075-b337-2aaee643cd30)



With a clean and well-commented codebase, this project serves as an invaluable resource for developers looking to harness the power of XInput in their Windows applications. Whether you're a seasoned developer or just getting started, the XInput app provides a solid foundation for building immersive gaming experiences and beyond.



I converted XInput from VB .NET to C#.



## Things to watch out for when converting from VB .NET to C#


### 1. DLL Imports:

Pay attention to the syntax when importing DLLs in VB .NET and C#.

**Example:**

VB .NET Syntax:

```vb

<DllImport("XInput1_4.dll")>
Private Shared Function XInputGetState(dwUserIndex As Integer, ByRef pState As XINPUT_STATE) As Integer
End Function

```

C# Syntax:

```csharp

[DllImport("XInput1_4.dll")]
private static extern int XInputGetState(int dwUserIndex, ref XINPUT_STATE pState);

```

In C#, attributes are defined using ```[]``` square brackets, while VB .NET uses ```<>``` angle brackets. C# employs ```extern``` to indicate external functions, whereas VB .NET uses ```Shared``` . The parameter passing also differs: C# uses ```ref``` , while VB .NET uses ```ByRef``` .

### 2. Boolean Values:

Pay attention to the syntax when defining and using boolean values in conditions.

**Example:**

VB .NET Syntax:

```vb .NET

Dim isConnected As Boolean = True  ' Defining a boolean value

If isConnected Then                  ' Using the value in a condition
    MessageBox.Show("Controller is connected!")
Else
    MessageBox.Show("Controller is not connected.")
End If

```


C# Syntax:



```c#


bool isConnected = true;            // Defining a boolean value

if (isConnected)                    // Using the value in a condition
{
    MessageBox.Show("Controller is connected!");
}
else
{
    MessageBox.Show("Controller is not connected.");
}

```




In VB .NET, you use ```Dim``` for declaration, while in C#, you declare with the type directly. Additionally, C# requires ```()``` parentheses around the condition in the if statement.










