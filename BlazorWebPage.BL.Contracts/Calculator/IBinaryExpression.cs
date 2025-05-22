namespace BlazorWebPage.BL.Contracts.Calculator;
public interface IBinaryExpression : IExpression
{
    IExpression Left { get; }
    IExpression Right { get; }
    TokenType TokenType { get; }
}
