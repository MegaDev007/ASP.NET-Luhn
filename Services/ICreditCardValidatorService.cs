namespace Card_Validate_Luhn.Services;

public interface ICreditCardValidatorService
{
    bool Validate(string cardNumber);
}