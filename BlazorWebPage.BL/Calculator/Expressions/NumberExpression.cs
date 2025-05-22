namespace BlazorWebPage.BL.Calculator.Expressions;

public class NumberExpression(decimal number) : Expression
{
    public decimal Number { get; } = number;

    public override decimal Evaluate() => Number;
}
