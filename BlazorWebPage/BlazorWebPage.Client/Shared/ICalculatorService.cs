namespace BlazorWebPage.Client.Shared;

public interface ICalculatorService
{
    Task<decimal> Calculate(string expression);
}
