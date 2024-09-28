# List to Table Formatter for .NET

The List to Table Formatter for .NET is a versatile library designed to convert lists of objects into well-formatted table displays. It supports customizable padding, border styles, and header text styles to enhance readability. Ideal for console applications and logging, this library ensures your data is presented in a clear and structured manner.

## Installation

You can install the package via NuGet:


## Usage

Here is a basic example of how to use the List to Table Formatter:

```csharp
// Pass your list to the DisplayTable method and write the table to the console. (Cast the list as an object)
var table = listToTableDisplay.DisplayTable(MyList.Cast<object>().ToList());
System.Console.WriteLine(table);
```
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
## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.



