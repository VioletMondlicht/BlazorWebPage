using BlazorWebPage.BL.Calculator.Exceptions;
using BlazorWebPage.BL.Contracts.Calculator;

namespace BlazorWebPage.BL.Calculator;

public class Interpreter(ITokenizer tokenizer) : IInterpreter
{
    private readonly ITokenizer _tokenizer = tokenizer;

    public decimal Evaluate(string expression)
    {
        try
        {
            var tokens = _tokenizer.Tokenize(expression);

            var parser = new Parser((IEnumerable<DslToken>)tokens);
            var ast = parser.Parse();

            return ast.Evaluate();
        }
        catch(DslParserException ex)
        {
            throw new InterpreterException($"Parsing error: {ex.Message}", ex);
        }
        catch(DivideByZeroException ex)
        {
            throw new InterpreterException("Cannot divide by zero", ex);
        }
        catch(Exception ex)
        {
            throw new InterpreterException($"Evaluation error: {ex.Message}", ex);
        }
    }
}

