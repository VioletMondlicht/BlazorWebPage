namespace BlazorWebPage.BL.Calculator.Exceptions;

[Serializable]
public class DslParserException : Exception
{
    public DslParserException(string message) : base(message) { }
    public DslParserException(string message, Exception innerException) : base(message, innerException) { }
}
