using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplicationDemo.Pages;

public class IndexModel : PageModel
{
    public string? GeneratedTable { get; set; }

    public async Task OnGetAsync()
    {
        var people = new List<Person>();

        var options = new JsonSerializerOptions
        {
            Converters = { new Core.DecimalConverter() }
        };

        // Read the JSON file and deserialize it into a list of Person objects.
        using (var reader = new StreamReader("./Data/MOCK_DATA.json"))
        {
            var json = await reader.ReadToEndAsync();
            people = JsonSerializer.Deserialize<List<Person>>(json, options);
        }

        if (people is null || people.Count == 0)
        {
            GeneratedTable = "No data found.";
            return;
        }

        // Pass your list to the DisplayTable method and display the table.
        // In this case, I'm modifying the list with LINQ but regardless, you must cast the list to an object. .Cast<object>().ToList()
        var htmlTable = ListToTableDisplay.ListToHtmlTableDisplay.DisplayTable(
            people
                .OrderByDescending(x => x.Amount)
                .Take(5)
                .Select(x => new { LastName = x.LastName.ToUpperInvariant(), x.FirstName, x.StockName, Amount = x.Amount.ToString("C"), x.City, x.Country })
                .Cast<object>()
                .ToList()
        , ListToTableDisplay.HeaderTextStyle.SplitPascalCase, minify: false, tableClass: "table table-striped table-hover", tableId: "peopleTable");

        // Set the generated table to the property to display in the Razor Page.
        GeneratedTable = htmlTable;
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