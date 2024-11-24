# List to Table Formatter for .NET

For Console Applications and Logging

The List to Table Formatter for .NET is a versatile library designed to convert lists of objects into well-formatted table displays. It supports customizable padding, border styles, and header text styles to enhance readability. Ideal for console applications and logging, this library ensures your data is presented in a clear and structured manner.

For Web Applications

Easily converts a list of objects into an HTML table for web applications. 
The List to Table Formatter for .NET generates clean and responsive tables that can be styled with a popular framework like Bootstrap or your own CSS to match your website's design. 

## Installation

You can install the lastest package via NuGet:
```
dotnet add package MeineGlock.ListToTableDisplay --version 1.1.0
```

## Usage

### Console Application

Basic example of how to use the List to Table Formatter in a console application:
See the ConsoleDemo for more advanced implementation.

```csharp
using ListToTableDisplay;

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
### Web Application

Basic example of how to use the List to Table Formatter in a ASP.NET RAZOR web application:
See the WebDemo for more information.
```csharp
// Create a property for the generated table.
public string? GeneratedTable { get; set; }

// In the OnGet method, create a list of objects and generate the table.
public async Task OnGetAsync()
{
    // Create a list of objects.
    var people = new List<Person>
    {
        new() { Name = "John", Age = 25, City = "New York" },
        new() { Name = "Jane", Age = 27, City = "Chicago" },
        new() { Name = "Tom", Age = 30, City = "Los Angeles" },
        new() { Name = "Lucy", Age = 35, City = "San Francisco" }
    };

    // Pass your list to the DisplayTable method and display the table.
    var htmlTable = ListToTableDisplay.ListToHtmlTableDisplay.DisplayTable(
            people
                .Cast<object>()
                .ToList()
        , ListToTableDisplay.HeaderTextStyle.SplitPascalCase, minify: false, tableClass: "table table-striped table-hover", tableId: "peopleTable");

    // Set the generated table to the property to display in the Razor Page.
    GeneratedTable = htmlTable;
}
```
```html
<!-- Display the generated table in the Razor Page and style with Bootstrap. -->
@page
@model WebApplicationDemo.Pages.IndexModel
@{
}
<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Web Application Demo</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet"
        integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">
</head>

<body>
    <div id="table_container" class="container">
        @* This is where the table will be displayed *@
        @Html.Raw(Model.GeneratedTable)
    </div>
</body>

</html>
```

## Customization Options for Console Applications

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
## Formatting Notes for Console Applications
- A monospaced font is required for proper table formatting.
- UTF-8 encoding is required for the modern table format to display correctly.
- If your console is not formatting the modern table correctly, try setting the console to output UTF-8.
```
Console.OutputEncoding = System.Text.Encoding.UTF8;
```


## Release Notes
- 1.1.0 - 2024-11-24 - Added the ability to display a list of objects as an HTML table for web applications.
- 1.0.3 - 2024-11-10 - Fixed an issue where a null value in the list would cause an exception.
- 1.0.2 - 2024-09-30 - Internal improvements.
- 1.0.1 - 2024-09-30 - Internal improvements.
- 1.0.0 - 2024-09-29 - Initial release.

## Home Page
https://github.com/meineGlock20/ListToTableDisplay

## License

This project is licensed under the MIT License.

![](https://img.shields.io/badge/License-MIT-blue.svg)

https://github.com/meineGlock20/ListToTableDisplay/blob/main/ListToTableDisplay/LICENSE

