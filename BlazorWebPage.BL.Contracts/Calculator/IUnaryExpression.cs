namespace BlazorWebPage.BL.Contracts.Calculator;
public interface IUnaryExpression : IExpression
{
    IExpression Operand { get; }
}
