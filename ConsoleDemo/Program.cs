﻿// See https://aka.ms/new-console-template for more information
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using ConsoleDemo.Models;

Console.WriteLine("Displays the TOP 5 people with the highest amount of money in their stock account.");
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
    var json = await reader.ReadToEndAsync();
    people = JsonSerializer.Deserialize<List<Person>>(json, options);
}

if (people is null) throw new Exception("No data found!");

System.Console.WriteLine("Unformatted data:");
foreach (var person in people.OrderByDescending(x => x.Amount).Take(5))
{
    Console.WriteLine($"{person.FirstName} {person.LastName} - {person.Amount.ToString("C")} - {person.StockName} - {person.City}, {person.Country}");
}
