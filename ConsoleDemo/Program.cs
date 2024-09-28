// See https://aka.ms/new-console-template for more information
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Serialization;
using ConsoleDemo.Models;

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
    BorderStyle = ListToTableDisplay.BorderStyle.Modern,
};

// Pass your list to the DisplayTable method and display the table.
var table = listToTableDisplay.DisplayTable(
    people
        .OrderByDescending(x => x.Amount)
        .Take(5)
        .Select(x => new { LastName = x.LastName.ToUpperInvariant(), x.FirstName, x.StockName, Amount = x.Amount.ToString("C"), x.City, x.Country })
        .Cast<object>()
        .ToList()
);
System.Console.WriteLine();
Console.ForegroundColor = ConsoleColor.Blue;
System.Console.WriteLine("Summary of top 5 stock holders: (ListToTableDisplay)");
Console.ResetColor();
System.Console.WriteLine(table);

Console.ReadKey();