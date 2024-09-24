using System;
using System.Text.Json.Serialization;

namespace ConsoleDemo.Models;

public record Person
{
     [JsonPropertyName("first_name")]
    public required string LastName { get; init; }

    [JsonPropertyName("last_name")]
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