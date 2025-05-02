using Card_Validate_Luhn.Models;
using Card_Validate_Luhn.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Card_Validate_Luhn.Controllers;

[ApiController]
[Route("api/credit-card")]
[Produces("application/json")]
public class CreditCardController : ControllerBase
{
    private readonly ICreditCardValidatorService _validatorService;
    private readonly ILogger<CreditCardController> _logger;

    public CreditCardController(
        ICreditCardValidatorService validatorService,
        ILogger<CreditCardController> logger)
    {
        _validatorService = validatorService ?? throw new ArgumentNullException(nameof(validatorService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Validates a credit card number using the Luhn algorithm
    /// </summary>
    [HttpPost("validate")]
    [ProducesResponseType(typeof(ValidationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<ValidationResponse> ValidateCreditCard([FromBody][Required] CreditCardRequest request)
    {
        _logger.LogInformation("Processing credit card validation request");

        if (string.IsNullOrWhiteSpace(request.CardNumber))
        {
            _logger.LogWarning("Empty card number provided");
            return BadRequest(new ValidationResponse
            {
                IsValid = false,
                Message = "Credit card number is required"
            });
        }

        // Sanitize input (remove spaces and dashes)
        var sanitizedCardNumber = request.CardNumber.Replace(" ", "").Replace("-", "");

        // Check that sanitized input contains only digits
        if (!sanitizedCardNumber.All(char.IsDigit))
        {
            _logger.LogWarning("Invalid characters in card number");
            return BadRequest(new ValidationResponse
            {
                IsValid = false,
                Message = "Credit card number must contain only digits, spaces, or dashes"
            });
        }

        // Check minimum and maximum length
        if (sanitizedCardNumber.Length < 13 || sanitizedCardNumber.Length > 19)
        {
            _logger.LogWarning("Invalid card number length: {Length}", sanitizedCardNumber.Length);
            return BadRequest(new ValidationResponse
            {
                IsValid = false,
                Message = "Credit card number must be between 13 and 19 digits"
            });
        }

        var isValid = _validatorService.Validate(sanitizedCardNumber);
        
        _logger.LogInformation("Credit card validation completed. Result: {IsValid}", isValid);
        
        return Ok(new ValidationResponse
        {
            IsValid = isValid,
            Message = isValid ? "Credit card number is valid" : "Credit card number is invalid"
        });
    }
}