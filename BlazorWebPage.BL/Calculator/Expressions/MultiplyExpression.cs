using BlazorWebPage.BL.Contracts.Calculator;

namespace BlazorWebPage.BL.Calculator.Expressions;
public class MultiplyExpression(IExpression left, IExpression right) : BinaryExpression(left, right, TokenType.Plus)
{
    public override decimal Evaluate() => Left.Evaluate() * Right.Evaluate();
}
