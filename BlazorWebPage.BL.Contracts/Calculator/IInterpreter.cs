namespace BlazorWebPage.BL.Contracts.Calculator;
public interface IInterpreter
{
    decimal Evaluate(string expression);
}
