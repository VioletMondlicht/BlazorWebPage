using System.Collections.Immutable;
using BlazorWebPage.BL.Contracts.Calculator;

namespace BlazorWebPage.BL.Calculator;

public class Tokenizer : ITokenizer
{
    private readonly List<ITokenDefinition> _tokenDefinitions;
    public Tokenizer() => _tokenDefinitions = new List<ITokenDefinition>
        {
            new TokenDefinition(TokenType.Number, "^\\d+(\\,\\d+)?"),
            new TokenDefinition(TokenType.Plus, "^\\+"),
            new TokenDefinition(TokenType.Minus, "^\\-"),
            new TokenDefinition(TokenType.Multiply, "^\\*"),
            new TokenDefinition(TokenType.Divide, "^\\/"),
            new TokenDefinition(TokenType.Space, "^\\ "),
            new TokenDefinition(TokenType.CloseParenthesis, "^\\)"),
            new TokenDefinition(TokenType.OpenParenthesis, "^\\("),
            new TokenDefinition(TokenType.Comma, "^,")
        };
    public IEnumerable<DslToken> Tokenize(string lqlText)
    {
        var tokens = ImmutableList.Create<DslToken>();
        var remainingText = lqlText;

        while(!string.IsNullOrWhiteSpace(remainingText))
        {
            var match = FindMatch(remainingText);
            if(match.IsMatch)
            {
                var token = new DslToken(match.TokenType, match.Value);
                Console.WriteLine($"-- Tokenizer: Matched Type={token.TokenType}, Value='{token.Value}'");
                tokens = tokens.Add(token);
                remainingText = match.RemainingText;
            }
            else
            {
                remainingText = remainingText.Substring(1);
            }
        }
        tokens = tokens.Add(new DslToken(TokenType.SequenceTerminator, string.Empty));
        return tokens;
    }

    private TokenMatch FindMatch(string lqlText)
    {
        foreach(var tokenDefinition in _tokenDefinitions)
        {
            var match = tokenDefinition.Match(lqlText);
            if(match.IsMatch)
                return match;
        }
        return new TokenMatch { IsMatch = false };
    }
}
