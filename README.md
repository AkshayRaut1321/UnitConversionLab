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
└── UnitConversion.Core
    ├── Interfaces
    ├── Models
    └── Services
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
* Interfaces

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

* Simpler deployment
* Easy to modify
* Suitable for the scope of this challenge
* Demonstrates a path for future expansion

### Conversion Strategy

The application separates conversion algorithms through the `IUnitConverter` abstraction.

Current implementations:

* LinearUnitConverter
* TemperatureConverter

This allows additional conversion algorithms to be introduced without modifying the main conversion service.

### Repository Abstraction

The conversion logic depends on abstractions rather than concrete implementations.

This keeps business logic independent from configuration storage details.

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
* Automated unit and integration tests
* Caching for large configuration sets
* Versioned APIs
