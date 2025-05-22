using BlazorWebPage.BL.Contracts.Calculator;

namespace BlazorWebPage.BL.Calculator.Expressions;
public class DivideExpression(IExpression left, IExpression right) : BinaryExpression(left, right, TokenType.Plus)
{
    public override decimal Evaluate()
    {
        var divisor = Right.Evaluate();
        return divisor == 0 ? throw new DivideByZeroException() : Left.Evaluate() / divisor;
    }
}
