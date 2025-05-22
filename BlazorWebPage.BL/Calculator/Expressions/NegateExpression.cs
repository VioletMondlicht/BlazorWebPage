using BlazorWebPage.BL.Contracts.Calculator;

namespace BlazorWebPage.BL.Calculator.Expressions;
public class NegateExpression(IExpression operand) : UnaryExpression(operand)
{
    public override decimal Evaluate() => -Operand.Evaluate();
}
