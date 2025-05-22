namespace BlazorWebPage.BL.Contracts.Calculator;
public interface INumberExpression : IExpression
{
    decimal Number { get; }
}
