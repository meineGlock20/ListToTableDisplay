/*
    .NET 8 ConsoleDemo to demonstrate the ListToTableDisplay library.
    https://github.com/meineGlock20/ListToTableDisplay
    2024-09-28

    IMPORTANT NOTES:
    In order for the table to display correctly, you must be using a monospaced font where the table is to be displayed.
    For the Modern table style, you must use UTF-8 encoding to display the table correctly.
*/

using ConsoleDemo.Models;
using System.Text.Json;

// 📌 Set the console output encoding to UTF-8 to display the MODERN table correctly.
Console.OutputEncoding = System.Text.Encoding.UTF8;

System.Console.WriteLine();

// Get the data from the MOCK_DATA.json file and build a list of Person objects.
var people = new List<Person>();

// JsonSerializerOptions options;
var options = new JsonSerializerOptions
{
    Converters = { new ConsoleDemo.Core.DecimalConverter() }
};

using (var reader = new StreamReader("MOCK_DATA.json"))
{
    var json = reader.ReadToEnd();
    people = JsonSerializer.Deserialize<List<Person>>(json, options);
}

if (people is null) throw new Exception("No data found!");

// Display the top 5 people with the highest amount of money in their stock account, the usual way.
Console.ForegroundColor = ConsoleColor.Blue;
System.Console.WriteLine("Summary of top 5 stock holders: (Unformatted data)");
Console.ResetColor();
foreach (var person in people.OrderByDescending(x => x.Amount).Take(5))
{
    Console.WriteLine($"{person.LastName.ToUpperInvariant()} {person.FirstName} - {person.StockName} - {person.Amount.ToString("C")} - {person.City}, {person.Country}");
}

// ✨ Display the top 5 people with the highest amount of money in their stock account, using the ListToTableDisplay class.

// Create a new instance of the ListToTableDisplay class and set the optional properties.
ListToTableDisplay.ListToTableDisplay listToTableDisplay = new()
{
    Padding = 1,
    HeaderTextStyle = ListToTableDisplay.HeaderTextStyle.SplitPascalCase,
    BorderStyle = ListToTableDisplay.BorderStyle.Classic,
};

// Pass your list to the DisplayTable method and display the table.
var tablec = listToTableDisplay.DisplayTable(
    people
        .OrderByDescending(x => x.Amount)
        .Take(5)
        .Select(x => new { LastName = x.LastName.ToUpperInvariant(), x.FirstName, x.StockName, Amount = x.Amount.ToString("C"), x.City, x.Country })
        .Cast<object>()
        .ToList()
);
System.Console.WriteLine();
Console.ForegroundColor = ConsoleColor.Blue;
System.Console.WriteLine("Summary of top 5 stock holders: (ListToTableDisplay CLASSIC)");
Console.ResetColor();
System.Console.WriteLine(tablec);

// Set the BorderStyle property to Modern.
listToTableDisplay.BorderStyle = ListToTableDisplay.BorderStyle.Modern;

// Pass your list to the DisplayTable method and display the table.
var tablem = listToTableDisplay.DisplayTable(
    people
        .OrderByDescending(x => x.Amount)
        .Take(5)
        .Select(x => new { LastName = x.LastName.ToUpperInvariant(), x.FirstName, x.StockName, Amount = x.Amount.ToString("C"), x.City, x.Country })
        .Cast<object>()
        .ToList()
);
System.Console.WriteLine();
Console.ForegroundColor = ConsoleColor.Blue;
System.Console.WriteLine("Summary of top 5 stock holders: (ListToTableDisplay MODERN)");
Console.ResetColor();
System.Console.WriteLine(tablem);

Console.ReadKey();