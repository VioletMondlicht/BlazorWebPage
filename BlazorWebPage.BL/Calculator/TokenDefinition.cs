using System.Text.RegularExpressions;
using BlazorWebPage.BL.Contracts.Calculator;

namespace BlazorWebPage.BL.Calculator;

public class TokenDefinition(TokenType returnTokenType, string regexPattern) : ITokenDefinition
{
    private readonly TokenType _returnTokenType = returnTokenType;
    private readonly Regex _regex = new(regexPattern, RegexOptions.IgnoreCase);

    public TokenMatch Match(string input)
    {
        var match = _regex.Match(input);
        if(match.Success)
        {
            var remainingText = input[match.Length..];
            return new TokenMatch
            {
                IsMatch = true,
                TokenType = _returnTokenType,
                Value = match.Value,
                RemainingText = remainingText
            };
        }
        return new TokenMatch { IsMatch = false };
    }
}
