using System.ComponentModel.DataAnnotations;

namespace Card_Validate_Luhn.Models;

public class CreditCardRequest
{
    [Required(ErrorMessage = "Credit card number is required")]
    public string CardNumber { get; set; } = string.Empty;
}