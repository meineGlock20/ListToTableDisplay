# List to Table Formatter for .NET

The List to Table Formatter for .NET is a versatile library designed to convert lists of objects into well-formatted table displays. It supports customizable padding, border styles, and header text styles to enhance readability. Ideal for console applications and logging, this library ensures your data is presented in a clear and structured manner.

## Installation

You can install the lastest package via NuGet:
```
dotnet add package MeineGlock.ListToTableDisplay --version 1.0.1
```

## Usage

Basic example of how to use the List to Table Formatter:
See the ConsoleDemo for more advanced implementation.

```csharp
var people = new List<Person>
{
    new() { Name = "John", Age = 25, City = "New York" },
    new() { Name = "Jane", Age = 27, City = "Chicago" },
    new() { Name = "Tom", Age = 30, City = "Los Angeles" },
    new() { Name = "Lucy", Age = 35, City = "San Francisco" }
};

ListToTableDisplay listToTableDisplay = new();

// Cast to an object list and pass to the DisplayTable method.
Console.WriteLine(listToTableDisplay.DisplayTable(people.Cast<object>().ToList()));
```

## Customization

You can customize the table display by setting various properties on the `TableDisplay` object. For example, you can change the padding, border style, and header text style:

```csharp
ListToTableDisplay.ListToTableDisplay listToTableDisplay = new()
{
    // Left and right paddding. Value of 1 to 10, 1 is the default.
    Padding = 1,
    // Split the header text by PascalCase or underscore. None is the default.
    HeaderTextStyle = ListToTableDisplay.HeaderTextStyle.SplitPascalCase,
    // Set the border style to classic or modern. Modern is the default.
    BorderStyle = ListToTableDisplay.BorderStyle.Classic,
};
```
## Formatting Notes
- A monospaced font is required for proper table formatting.
- UTF-8 encoding is required for the modern table format to display correctly.

## Output Examples
### Terminal

![Screenshot 2024-09-28 102758](https://github.com/user-attachments/assets/6c1cb9c5-47db-4a83-a1e6-b6c34e5fa595)

### Notepad
![Screenshot 2024-09-28 084009](https://github.com/user-attachments/assets/9a9f1f23-3ec8-4d55-85c4-b70770d7b094)

### Word
![Screenshot 2024-09-28 084532](https://github.com/user-attachments/assets/14ffbaf6-d3bc-4710-b8b0-3d5266b0e060)

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.



