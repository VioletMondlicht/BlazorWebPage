using System.Text.Json.Serialization;

namespace BlazorWebPage.Client.Models;

public class CatFact
{
    [JsonPropertyName("fact")]
    public string? Fact { get; set; }

    [JsonPropertyName("length")]
    public int Lenght { get; set; }
}