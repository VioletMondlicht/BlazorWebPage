using BlazorWebPage.BL.Contracts.Calculator;
using BlazorWebPage.Client.Shared;

namespace BlazorWebPage.BL.Calculator;

public class CalculatorService(ITokenizer tokenizer, IInterpreter interpreter) : ICalculatorService
{
    private readonly ITokenizer _tokenizer = tokenizer;
    private readonly IInterpreter _interpreter = interpreter;

    public Task<decimal> Calculate(string expression) => Task.FromResult(_interpreter.Evaluate(expression));
}
