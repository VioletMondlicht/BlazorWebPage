using BlazorWebPage.BL.Contracts.Calculator;

namespace BlazorWebPage.BL.Calculator.Expressions;

public abstract class UnaryExpression(IExpression operand) : Expression, IUnaryExpression
{
    public IExpression Operand { get; } = operand;
}

