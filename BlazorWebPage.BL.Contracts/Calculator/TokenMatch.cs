﻿namespace BlazorWebPage.BL.Contracts.Calculator;
public class TokenMatch
{
    public bool IsMatch { get; set; }
    public TokenType TokenType { get; set; }
    public string Value { get; set; }
    public string RemainingText { get; set; }
}
