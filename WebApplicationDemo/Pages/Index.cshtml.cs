using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplicationDemo.Pages;

public class IndexModel : PageModel
{
    public string? GeneratedTable { get; set; }

    public void OnGet()
    {
        var people = new List<Person>();

        // JsonSerializerOptions options;
        var options = new JsonSerializerOptions
        {
            Converters = { new Core.DecimalConverter() }
        };

        using (var reader = new StreamReader("./Data/MOCK_DATA.json"))
        {
            var json = reader.ReadToEnd();
            people = JsonSerializer.Deserialize<List<Person>>(json, options);
        }

        ListToTableDisplay.ListToTableDisplay listToTableDisplay = new()
        {
            // Left and right paddding. Value of 1 to 10, 1 is the default.
            Padding = 1,
            // Split the header text by PascalCase or underscore. None is the default.
            HeaderTextStyle = ListToTableDisplay.HeaderTextStyle.SplitPascalCase,
            // Set the border style to classic or modern. Modern is the default.
            BorderStyle = ListToTableDisplay.BorderStyle.Modern,
        };

        if (people is null || people.Count == 0)
        {
            GeneratedTable = "No data found.";
            return;
        }

        // Pass your list to the DisplayTable method and display the table.
        var tablec = listToTableDisplay.DisplayTable(
            people
                .OrderByDescending(x => x.Amount)
                .Take(5)
                .Select(x => new { LastName = x.LastName.ToUpperInvariant(), x.FirstName, x.StockName, Amount = x.Amount.ToString("C"), x.City, x.Country })
                .Cast<object>()
                .ToList()
        );

        GeneratedTable = tablec;

    }
}

public record Person
{
    [JsonPropertyName("last_name")]
    public required string LastName { get; init; }

    [JsonPropertyName("first_name")]
    public required string FirstName { get; init; }

    [JsonPropertyName("email")]
    public required string Email { get; init; }

    [JsonPropertyName("gender")]
    public string? Gender { get; init; }

    [JsonPropertyName("stock_name")]
    public required string StockName { get; init; }

    [JsonPropertyName("amount")]
    public required decimal Amount { get; init; }

    [JsonPropertyName("city")]
    public required string City { get; init; }

    [JsonPropertyName("country")]
    public required string Country { get; init; }
}