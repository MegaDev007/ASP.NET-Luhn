namespace Card_Validate_Luhn.Services;

public class LuhnValidatorService : ICreditCardValidatorService
{
    private readonly ILogger<LuhnValidatorService> _logger;

    public LuhnValidatorService(ILogger<LuhnValidatorService> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public bool Validate(string cardNumber)
    {
        _logger.LogDebug("Validating card number with Luhn algorithm");

        if (string.IsNullOrWhiteSpace(cardNumber) || !cardNumber.All(char.IsDigit))
        {
            _logger.LogWarning("Invalid card number format provided to validator");
            return false;
        }

        try
        {
            int sum = 0;
            bool alternate = false;

            for (int i = cardNumber.Length - 1; i >= 0; i--)
            {
                int digit = int.Parse(cardNumber[i].ToString());

                if (alternate)
                {
                    digit *= 2;
                    if (digit > 9)
                    {
                        digit -= 9;
                    }
                }

                sum += digit;
                alternate = !alternate;
            }

            bool isValid = (sum % 10 == 0);
            _logger.LogDebug("Card number validation result: {IsValid}", isValid);
            
            return isValid;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while validating card number");
            throw new InvalidOperationException("Error during credit card validation", ex);
        }
    }
}