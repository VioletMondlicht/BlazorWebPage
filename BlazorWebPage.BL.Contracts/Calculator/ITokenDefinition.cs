namespace BlazorWebPage.BL.Contracts.Calculator;
public interface ITokenDefinition
{
    TokenMatch Match(string input);
}
