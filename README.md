# List to Table Formatter for .NET

The List to Table Formatter for .NET is a versatile library designed to convert lists of objects into well-formatted table displays. It supports customizable padding, border styles, and header text styles to enhance readability. Ideal for console applications and logging, this library ensures your data is presented in a clear and structured manner.

## Installation

You can install the lastest package via NuGet:
```
dotnet add package MeineGlock.ListToTableDisplay --version 1.0.2
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
![Screenshot 2024-09-28 102758](https://github.com/meineGlock20/ListToTableDisplay/blob/main/images/Screenshot%202024-09-28%20102758.png)

### Notepad
![Screenshot 2024-09-28 084009](https://github.com/meineGlock20/ListToTableDisplay/blob/main/images/Screenshot%202024-09-28%20084009.png)

### Word
![Screenshot 2024-09-28 084532](https://github.com/meineGlock20/ListToTableDisplay/blob/main/images/Screenshot%202024-09-28%20084532.png)

## Home Page
https://github.com/meineGlock20/ListToTableDisplay

## License

This project is licensed under the MIT License.

![](https://img.shields.io/badge/License-MIT-blue.svg)

https://github.com/meineGlock20/ListToTableDisplay/blob/main/ListToTableDisplay/LICENSE

