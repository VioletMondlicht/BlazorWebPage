using BlazorWebPage.Client.Services;
using BlazorWebPage.Client.Models;
using static System.Net.WebRequestMethods;

namespace BlazorWebPage.Client.Pages;


public partial class CatFacts
{
    private CatFact? catFact = null;
    private int statusCode;
    private bool isLoading;


    private async Task FetchCatFact()
    {
        isLoading = true;

        try
        {
            var result = await CatFactService.GetCatFactAsync();
            catFact = result.Item1;
            statusCode = result.Item2;
        }
        finally
        {
            isLoading = false;
        }
    }
}