# Credit Card Validator API

## Overview

This is an ASP.NET Core Web API application that validates credit card numbers using the Luhn algorithm. The Luhn algorithm, also known as the "modulus 10" or "mod 10" algorithm, is a simple checksum formula used to validate various identification numbers, including credit card numbers.

## Features

- Credit card number validation using the Luhn algorithm
- Comprehensive input validation
- RESTful API design
- Swagger/OpenAPI documentation
- Health checks endpoint
- Global exception handling middleware
- Detailed logging
- JSON response formatting

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- IDE of your choice (Visual Studio, VS Code, JetBrains Rider, etc.)

## Getting Started

### Clone the Repository

```bash
git clone https://github.com/yourusername/ASP.NET-Luhn.git
cd ASP.NET-Luhn
```

### Build and Run

```bash
dotnet build
dotnet run --project Card_Validate(Luhn)/Card_Validate(Luhn).csproj
```

The API will be available at:
- HTTP: `http://localhost:5148`
- HTTPS: `https://localhost:7158`

After running the application, you can access the Swagger UI at:
- `http://localhost:5148/swagger` or
- `https://localhost:7158/swagger`

### Docker Support

If you have Docker installed, you can build and run the application in a container:

```bash
docker build -t card-validator .
docker run -p 5148:80 card-validator
```

## API Endpoints

### Validate Credit Card

```
POST /api/CreditCard/validate
```

#### Request Body

```json
{
  "cardNumber": "4532015112830366"
}
```

#### Response

```json
{
  "isValid": true,
  "message": "Credit card number is valid"
}
```

### Health Check

```
GET /health
```

## How the Luhn Algorithm Works

1. Starting from the rightmost digit and moving left, double the value of every second digit.
2. If doubling results in a two-digit number, add those digits together to get a single-digit number.
3. Sum all the digits in the modified number.
4. If the total is divisible by 10, the number is valid; otherwise, it's invalid.

## Project Structure

```
Card_Validate(Luhn)/
├── Controllers/
│   └── CreditCardController.cs    # API endpoints
├── Middleware/
│   └── ExceptionHandlingMiddleware.cs  # Global exception handling
├── Models/
│   ├── CreditCardRequest.cs      # Request data model
│   └── ValidationResponse.cs     # Response data model
├── Services/
│   ├── ICreditCardValidatorService.cs  # Service interface
│   └── LuhnValidatorService.cs   # Luhn algorithm implementation
├── Properties/
│   └── launchSettings.json       # Development environment settings
├── Program.cs                    # Application entry point and configuration
└── appsettings.json              # Configuration settings
```

## Security Considerations

- The API does not store credit card numbers
- All validation is performed in-memory
- HTTPS is enforced in production environments
- Input validation protects against malformed requests

## Testing

You can test the API using:

1. Swagger UI available at `/swagger` when running in development mode
2. Curl, Postman, or any other HTTP client
3. The included `.http` files if using Visual Studio or VS Code with the REST Client extension

### Valid Test Card Numbers

- `4532015112830366` (Visa)
- `4532 0151 1283 0366` (Visa with spaces)
- `5555555555554444` (MasterCard)
- `378282246310005` (American Express)
- `6011111111111117` (Discover)

## Technical Details

- Built with .NET 8.0
- Uses ASP.NET Core Web API
- Implements dependency injection pattern
- Includes comprehensive XML documentation
- Follows RESTful API best practices

## Contributing

1. Fork the repository
2. Create your feature branch: `git checkout -b feature/my-new-feature`
3. Commit your changes: `git commit -am 'Add some feature'`
4. Push to the branch: `git push origin feature/my-new-feature`
5. Submit a pull request

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Acknowledgments

- [Luhn Algorithm](https://en.wikipedia.org/wiki/Luhn_algorithm) on Wikipedia
- [ASP.NET Core Documentation](https://docs.microsoft.com/aspnet/core) 