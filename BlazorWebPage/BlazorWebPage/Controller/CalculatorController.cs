using BlazorWebPage.Client.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebPage.Controller;

[ApiController]
[Route("api/[controller]")]
public class CalculatorController : ControllerBase
{
    private readonly ILogger<CalculatorController> _logger;
    private readonly ICalculatorService _calculatorService;

    public CalculatorController(
        ILogger<CalculatorController> logger,
        ICalculatorService calculatorService)
    {
        _logger = logger;
        _calculatorService = calculatorService;
    }

    [HttpPost]
    public async Task<IActionResult> Calculate([FromBody] CalculationRequest model)
    {
        if (model == null || string.IsNullOrWhiteSpace(model.Expression))
        {
            return BadRequest("Expression is required.");
        }

        _logger.LogInformation($"Calculating expression: {model.Expression}");

        try
        {
            var result = await _calculatorService.Calculate(model.Expression);
            _logger.LogInformation($"Calculation result: {result}");
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error calculating expression: {model.Expression}");
            return BadRequest($"Calculation error: {ex.Message}");
        }
    }
}

