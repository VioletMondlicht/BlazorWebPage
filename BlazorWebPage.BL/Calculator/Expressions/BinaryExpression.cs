using BlazorWebPage.BL.Contracts.Calculator;

namespace BlazorWebPage.BL.Calculator.Expressions;

public abstract class BinaryExpression(IExpression left, IExpression right, TokenType tokenType) : Expression, IBinaryExpression
{
    public IExpression Left { get; } = left;
    public IExpression Right { get; } = right;
    public TokenType TokenType { get; } = tokenType;
}

