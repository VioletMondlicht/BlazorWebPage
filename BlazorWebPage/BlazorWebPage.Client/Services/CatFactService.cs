using BlazorWebPage.Client.Pages;
using System.Net.Http.Json;
using BlazorWebPage.Client.Models;

namespace BlazorWebPage.Client.Services;

public class CatFactService
{
    private readonly HttpClient _httpClient;

    public CatFactService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<(CatFact, int StatusCode)> GetCatFactAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("https://catfact.ninja/fact");
            int statusCode = (int)response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                var catFact = await response.Content.ReadFromJsonAsync<CatFact>();
                return (catFact, statusCode);
            }
            return (new CatFact { Fact = "Sowwy, Unable to fetch cat fact. :(" }, statusCode);
        }
        catch (Exception)
        {
            return (new CatFact { Fact = "Error connecting to the cat facts API. Meow :(" }, 0);

        }

    }
}
