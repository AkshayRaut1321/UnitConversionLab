# Unit Conversion API

## Overview

Unit Conversion API is an ASP.NET Core Web API that converts values between different units of measurement.

Supported conversion categories:

* Length
* Mass
* Temperature

The application uses a JSON-based configuration file to define categories, units, aliases, and conversion factors. This allows new units to be added without modifying application code.

## Features

* Convert values between supported units
* Support for unit aliases (e.g. km, m, kg, lb)
* Category validation
* JSON-driven unit configuration
* OpenAPI/Swagger documentation using NSwag
* Dependency Injection
* SOLID-oriented design
* Unit-tested business logic

## Solution Structure

```text
UnitConversion
│
├── UnitConversion.Api
│   ├── Controllers
│   ├── Repositories
│   ├── Config
│   └── Program.cs
│
├── UnitConversion.Core
│   ├── Interfaces
│   ├── Models
│   └── Services
│
└── UnitConversion.Tests
    ├── Services
    └── Converters
```

### Responsibilities

**UnitConversion.Api**

* API endpoints
* Dependency Injection configuration
* JSON configuration loading
* OpenAPI documentation

**UnitConversion.Core**

* Domain models
* Business logic
* Conversion services
* Conversion strategies
* Interfaces

**UnitConversion.Tests**

* Unit tests for conversion services
* Unit tests for conversion strategies
* Validation and error scenario coverage

## Running Locally

### Prerequisites

* .NET 10 SDK

### Build

```bash
dotnet build
```

### Run

```bash
dotnet run --project UnitConversion.Api
```

### Open API Documentation

Browse to:

```text
/swagger
```

Example:

```text
https://localhost:<port>/swagger
```

## Running Tests

Execute all tests:

```bash
dotnet test
```

## Example Requests

### Convert Length

```http
POST /api/conversions
```

Request:

```json
{
  "value": 1,
  "fromUnit": "km",
  "toUnit": "cm"
}
```

Response:

```json
{
  "originalValue": 1,
  "convertedValue": 100000,
  "fromUnit": "km",
  "toUnit": "cm",
  "category": "length"
}
```

### Get Categories

```http
GET /api/categories
```

### Get Units

```http
GET /api/categories/length/units
```

## Design Decisions

### JSON-Based Configuration

Units and conversion metadata are stored in a JSON file instead of a database.

Benefits:

* Simple deployment
* Easy to modify and extend
* Suitable for the scope of this challenge
* Provides a path for future expansion without changing business logic

### Conversion Strategy

The application separates conversion algorithms through the `IUnitConverter` abstraction.

Current implementations:

* LinearUnitConverter
* TemperatureConverter

This allows additional conversion algorithms to be introduced without modifying the main conversion service.

### Repository Abstraction

The conversion logic depends on abstractions rather than concrete implementations.

This keeps business logic independent from configuration storage details and supports future replacement of the JSON configuration source.

## Testing

The solution includes NUnit-based unit tests covering:

* Successful conversions
* Invalid source units
* Invalid target units
* Cross-category validation
* Unsupported converter scenarios
* Conversion strategy behavior

## Trade-Offs

For simplicity, the solution intentionally does not include:

* Database persistence
* Authentication or authorization
* Caching
* Docker support
* CI/CD pipelines

These can be added if future requirements justify them.

## Future Enhancements

* Additional conversion categories
* Database-backed configuration
* Administrative management of units
* Caching for large configuration sets
* API versioning
* Integration and end-to-end API testing
