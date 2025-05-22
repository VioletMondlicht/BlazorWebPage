using BlazorWebPage.BL.Calculator.Expressions;
using BlazorWebPage.BL.Calculator.Exceptions;
using BlazorWebPage.BL.Contracts.Calculator;

namespace BlazorWebPage.BL.Calculator;

public class Parser(IEnumerable<DslToken> tokens) : IParser
{
    private IEnumerator<DslToken> _tokenEnumerator;
    private DslToken _currentToken;

    public IExpression Parse()
    {
        _tokenEnumerator = tokens.GetEnumerator();
        if(!_tokenEnumerator.MoveNext())
            throw new DslParserException("No tokens to parse");

        _currentToken = _tokenEnumerator.Current;
        ConsumeSpaceTokens();
        var result = ParseExpression();
     
        if(_currentToken.TokenType != TokenType.SequenceTerminator)
            throw new DslParserException($"Unexpected token: {_currentToken.Value}");

        return result;
    }

    private IExpression ParseExpression() => ParseAdditionExpression();

    private IExpression ParseAdditionExpression()
    {
        var left = ParseTerm();

        ConsumeSpaceTokens();

        while(_currentToken.TokenType == TokenType.Plus ||
               _currentToken.TokenType == TokenType.Minus)
        {
            var operatorType = _currentToken.TokenType;
            ConsumeToken(); 

            ConsumeSpaceTokens();

            var right = ParseTerm();

            if(operatorType == TokenType.Plus)
                left = new AddExpression((Expression)left, (Expression)right);
            else
                left = new SubtractExpression((Expression)left, (Expression)right);

        
            ConsumeSpaceTokens();
        }

        return left;
    }

    private IExpression ParseTerm()
    {
        var left = ParseFactor();

        ConsumeSpaceTokens();

        while(_currentToken.TokenType == TokenType.Multiply ||
               _currentToken.TokenType == TokenType.Divide)
        {
            var operatorType = _currentToken.TokenType;
            ConsumeToken(); 

 
            ConsumeSpaceTokens();

            var right = ParseFactor();

           
            if(operatorType == TokenType.Multiply)
                left = new MultiplyExpression((Expression)left, (Expression)right);
            else
                left = new DivideExpression((Expression)left, (Expression)right);

           
            ConsumeSpaceTokens();
        }
        return left;
    }

    private IExpression ParseFactor()
    {
        
        ConsumeSpaceTokens();

        if(_currentToken.TokenType == TokenType.Number)
        {
            
            var numberString = _currentToken.Value.Replace(',', '.');
            var number = decimal.Parse(numberString, System.Globalization.CultureInfo.InvariantCulture);
            ConsumeToken(); 
            return new NumberExpression(number);
        }
        else if(_currentToken.TokenType == TokenType.OpenParenthesis)
        {
            
            ConsumeToken(); 
            ConsumeSpaceTokens(); 

            var expression = ParseExpression();

            ConsumeSpaceTokens();
            if(_currentToken.TokenType != TokenType.CloseParenthesis)
                throw new DslParserException("Expected closing parenthesis");

            ConsumeToken();
            return expression;
        }
        else if(_currentToken.TokenType == TokenType.Minus)
        {
           
            ConsumeToken(); 
            ConsumeSpaceTokens(); 

            var operand = ParseFactor();
            return new NegateExpression((Expression)operand);
        }

        throw new DslParserException($"Unexpected token: {_currentToken.Value}");
    }
    private void ConsumeToken()
    {
        if(_tokenEnumerator.MoveNext())
        {
            _currentToken = _tokenEnumerator.Current;
            Console.WriteLine($"-- Parser: Consumed token: Type={_currentToken.TokenType}, Value='{_currentToken.Value}'");
        }
        else
        {
            _currentToken = new DslToken(TokenType.SequenceTerminator, string.Empty);
            Console.WriteLine("-- Parser: Reached end of tokens");
        }
    }

    private void ConsumeSpaceTokens()
    {
        while(_currentToken.TokenType == TokenType.Space)
        {
            ConsumeToken();
        }
    }
}
