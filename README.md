# Unit Conversion API

A REST API built with **ASP.NET Core .NET 10** to convert Length, Temperature, and Weight units. The application uses the Strategy Pattern to separate conversion logic by category and make the solution easy to extend.

## Features

- Convert Length: Meter, Kilometer, Centimeter, Mile
- Convert Temperature: Celsius, Fahrenheit, Kelvin
- Convert Weight: Kilogram, Gram, Pound, Ounce
- Centralized exception handling with consistent error responses
- Dependency Injection for loosely coupled components
- Hardcoded conversion rules; no database required

## Prerequisites

- .NET 10 SDK
- Visual Studio 2026, Visual Studio Code, or another C# IDE
- Postman for API testing

Check the installed SDK:

```bash
dotnet --version
```

## How to Run

Run these commands from the directory containing the `.csproj` file:

```bash
dotnet restore
dotnet build
dotnet run
```

Use the Postman to call the bellow api.

## API Usage
Postman Testing – Unit Conversion API
Steps
1. Open Postman.
2. Click New → HTTP Request.
3. Select method POST.
4. Enter URL: http://localhost:5285/api/Conversion/Convert
5. Open Body → raw → JSON.
6. Paste the input JSON below and click Send.

### Convert Units

**Method:** `POST`

**Route:** `http://localhost:5285/api/Conversion/Convert`

Example request:

```json
{
  "category": "Length",
  "fromUnit": "kilometer",
  "toUnit": "meter",
  "value": 5
}
```

```json
{
  "category": "Temperature",
  "fromUnit": "celsius",
  "toUnit": "fahrenheit",
  "value": 100
}
```

```json
{
  "category": "Weight",
  "fromUnit": "kilogram",
  "toUnit": "pound",
  "value": 10
}
```





### Supported Categories and Units

| Category | Base Unit | Supported Units |
|---|---|---|
| Length | Meter | Meter, Kilometer, Centimeter, Mile |
| Temperature | Kelvin | Celsius, Fahrenheit, Kelvin |
| Weight | Kilogram | Kilogram, Gram, Pound, Ounce |

Unit names are case-insensitive. The current strategies use the singular names listed above; abbreviations such as `km`, `m`, and `kg` must be added as aliases if required.

## Architecture and Design Decisions

### Strategy Pattern

Each category has a dedicated strategy implementing `IConversionStrategy`:

- `LengthConversionStrategy`
- `TemperatureConversionStrategy`
- `WeightConversionStrategy`

Each strategy owns its conversion rules and formulas, keeping category-specific logic isolated.

### Separation of Responsibilities

- **Controller:** Accepts HTTP requests and returns HTTP responses.
- **ConversionService:** Coordinates the conversion operation.
- **ConversionStrategyEngine:** Selects a strategy based on the requested category.
- **Strategies:** Validate units and perform the conversion.
- **DTOs:** Define request and response shapes.
- **GlobalExceptionHandler:** Maps exceptions to consistent error responses.

### Dependency Injection

The strategies are registered with ASP.NET Core's built-in dependency injection container. The engine receives the registered strategies and selects the matching implementation. The service depends on the engine interface.

### Base-Unit Conversion

Length conversions use meters as the base unit, and Weight conversions use kilograms. The input is converted to the base unit and then to the target unit.

Temperature uses Kelvin as its base unit. Conversion functions are used because temperature conversion includes offsets as well as scaling factors.

### Hardcoded Conversion Rules

Conversion factors and formulas are stored in dictionaries inside the corresponding strategy classes.

**Trade-off:** This is straightforward and efficient for a small, fixed set of units. A database or external configuration would be more suitable if conversion rules needed to change without redeploying the application.

### Global Exception Handling

The global exception handler logs exceptions and returns consistent error responses. Expected invalid requests should return HTTP 400, while unexpected errors should return HTTP 500 with a generic message. Detailed exception information should remain in server logs, not API responses.

## Design Trade-offs
- **Single endpoint:** Provides one consistent API contract for all categories; separate endpoints could be useful if categories later require different request shapes.
- **Hardcoded rules:** Avoids database complexity but requires code changes and redeployment to update formulas.
- **Global exception handling:** Centralizes errors, but request validation should still catch invalid input early and produce clear client errors.

