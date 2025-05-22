using BlazorWebPage.Client.Shared;
using System.Net.Http.Json;

namespace BlazorWebPage.Client.Services;

public class CalculatorService : ICalculatorService
{
    private readonly HttpClient _httpClient;

    public CalculatorService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<decimal> Calculate(string expression)
    {
        try
        {
            var request = new CalculationRequest { Expression = expression };

            var response = await _httpClient.PostAsJsonAsync("api/calculator", request);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<decimal>();
        }
        catch (HttpRequestException httpEx)
        {

            string errorContent = "No details";
            if (httpEx.StatusCode != null && httpEx.StatusCode != System.Net.HttpStatusCode.NoContent && httpEx.Message.Contains("response status code does not indicate success"))
            {

                errorContent = $"Server returned status code {httpEx.StatusCode}";
            }
            Console.WriteLine($"API HTTP Error: {httpEx.Message}");
            throw new Exception($"API Error: {errorContent} ({httpEx.Message})");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"API Call/Processing Error: {ex}");
            throw new Exception($"API Error: {ex.Message}");
        }
    }
}
