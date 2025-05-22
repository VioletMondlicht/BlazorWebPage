namespace BlazorWebPage.BL.Contracts.Calculator;
public interface ITokenizer
{
    IEnumerable<DslToken> Tokenize(string lqlText);
}
