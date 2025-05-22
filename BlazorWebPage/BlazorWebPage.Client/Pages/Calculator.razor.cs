using BlazorWebPage.Client.Shared;
using Microsoft.AspNetCore.Components;

namespace BlazorWebPage.Client.Pages;

public partial class Calculator : ComponentBase
{
    [Inject] public ICalculatorService CalculatorService { get; set; } = null!;

    private string InputNumber { get; set; } = "";
    private string Calculation { get; set; } = "";
    private bool waitingForNewInput = false;
    private bool operationJustAdded = false;

    private void ClearNum() => InputNumber = "";
    private void ClearInputs()
    {
        InputNumber = "";
        Calculation = "";
        waitingForNewInput = false;
        operationJustAdded = false;
    }

    public async Task Calculate()
    {
        Console.WriteLine($"Calculate Method called. Current Calculation string: '{Calculation}', InputNumber: '{InputNumber}'");

        if (!string.IsNullOrWhiteSpace(InputNumber) && !waitingForNewInput)
        {
            Calculation += InputNumber;
            InputNumber = "";
        }
        else if (!string.IsNullOrWhiteSpace(InputNumber) && waitingForNewInput)
        {
            Calculation += InputNumber;
            InputNumber = "";
        }
        var expressionToSend = Calculation.Trim();

        if (string.IsNullOrWhiteSpace(expressionToSend))
        {
            Console.WriteLine("Calculation requested with empty expression.");
            InputNumber = "0";
            Calculation = "";
            StateHasChanged();
            return;
        }
        Console.WriteLine($"Attempting to calculate expression: '{expressionToSend}'");

        try
        {
            var result = await CalculatorService.Calculate(expressionToSend);
            Console.WriteLine($"Calculation successful. Result: {result}");
            Calculation = result.ToString(System.Globalization.CultureInfo.InvariantCulture);
            InputNumber = "";

            waitingForNewInput = false;
            operationJustAdded = false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during calculation: {ex}");
            Calculation = "Error!";
            InputNumber = "";
        }
        StateHasChanged();
    }

    private void DeleteDigit() => InputNumber = InputNumber.Length > 1 ? InputNumber[..^1] : "";
    public void AddDigit(string digit)
    {
        if (digit == ",")
        {
            if (InputNumber.Contains(','))
            {
                return;
            }
            if (string.IsNullOrEmpty(InputNumber) || InputNumber == "-")
            {
                InputNumber += "0,";
            }
            else InputNumber += ",";
        }
        else if (waitingForNewInput)
        {
            InputNumber = digit;
            waitingForNewInput = false;
        }
        else
        {
            if (InputNumber == "")
                InputNumber = digit;
            else InputNumber += digit;
        }
        operationJustAdded = false;
    }
    private void SwitchNegative(string neg) => InputNumber = !InputNumber.Contains('-') ? "-" + InputNumber : InputNumber.TrimStart('-');
    private void AddOperation(string op)
    {
        if (op == "(")
        {
            Calculation += op;
            operationJustAdded = true;
            waitingForNewInput = false;
            return;
        }

        if (!string.IsNullOrEmpty(InputNumber))
        {
            Calculation += InputNumber;
            InputNumber = "";
        }
        else if (string.IsNullOrEmpty(Calculation) || Calculation.EndsWith("("))
        {
            if (op != ")")
            {
                return;
            }
        }
        if (op == ")")
        {
            Calculation += op;
            operationJustAdded = false;
            waitingForNewInput = false;
            InputNumber = "";
        }
        else
        {
            if (operationJustAdded && Calculation.Length > 1 && Calculation.EndsWith(" "))
            {

                Calculation = Calculation.Substring(0, Calculation.Length - 3);
            }

            Calculation += " " + op + " ";
            waitingForNewInput = true;
            operationJustAdded = true;
            InputNumber = "";
        }
    }
}
