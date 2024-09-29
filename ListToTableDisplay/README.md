# List to Table Formatter for .NET

The List to Table Formatter for .NET is a versatile library designed to convert lists of objects into well-formatted table displays. It supports customizable padding, border styles, and header text styles to enhance readability. Ideal for console applications and logging, this library ensures your data is presented in a clear and structured manner.

## Installation

You can install the package via NuGet:

## Usage

Here is a basic example of how to use the List to Table Formatter:
See the ConsoleDemo for more advanced implementation.

```csharp
// Pass your list to the DisplayTable method and write the table to the console. (Cast the list as an object)
var table = listToTableDisplay.DisplayTable(MyList.Cast<object>().ToList());
System.Console.WriteLine(table);
```
## Formatting Notes
- A monospaced font is required for proper table formatting.
- UTF-8 encoding is required for the modern table format to display correctly.

## Customization

You can customize the table display by setting various properties on the `TableDisplay` object. For example, you can change the padding, border style, and header text style:

```csharp
ListToTableDisplay.ListToTableDisplay listToTableDisplay = new()
{
    Padding = 1,
    HeaderTextStyle = ListToTableDisplay.HeaderTextStyle.SplitPascalCase,
    BorderStyle = ListToTableDisplay.BorderStyle.Classic,
};
```
## Output Examples
### Terminal

![Screenshot 2024-09-28 102758](https://github.com/user-attachments/assets/6c1cb9c5-47db-4a83-a1e6-b6c34e5fa595)

### Notepad
![Screenshot 2024-09-28 084009](https://github.com/user-attachments/assets/9a9f1f23-3ec8-4d55-85c4-b70770d7b094)

### Word
![Screenshot 2024-09-28 084532](https://github.com/user-attachments/assets/14ffbaf6-d3bc-4710-b8b0-3d5266b0e060)

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.



