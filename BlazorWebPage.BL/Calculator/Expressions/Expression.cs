using BlazorWebPage.BL.Contracts.Calculator;

namespace BlazorWebPage.BL.Calculator.Expressions;

public abstract class Expression : IExpression
{
    public abstract decimal Evaluate();
}
