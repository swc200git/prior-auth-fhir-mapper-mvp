# Prior Auth FHIR Mapper MVP

A .NET-based microservice that converts prior authorization requests into FHIR R4 Claim resources.

## Features

- Convert prior authorization requests into standard FHIR R4 Claim resources
- Persist both original request and FHIR output for auditing
- Support for standard medical coding systems (CPT, ICD-10)
- SQLite for local development, configurable for SQL Server in production
- Built with .NET 9.0 Minimal API pattern

## Prerequisites

- .NET 9.0 SDK
- Visual Studio 2025 or VS Code with C# extensions
- SQLite (included) or SQL Server for database

## Getting Started

1. Clone the repository:
```powershell
git clone https://github.com/swc200git/prior-auth-fhir-mapper-mvp.git
cd prior-auth-fhir-mapper-mvp
```

2. Run the application:
```powershell
dotnet run --project src/Api/Api.csproj
```

3. Test the API with a sample request:
```http
POST /prior-auth HTTP/1.1
Content-Type: application/json

{
  "patientId": "pat01",
  "memberId": "mem01",
  "providerNpi": "1234567890",
  "serviceCode": "99213",
  "diagnosisCode": "Z01.1",
  "serviceDate": "2025-09-24",
  "placeOfService": "11",
  "priority": "normal"
}
```

## Project Structure

- `src/Api/` - Main API project
  - `Endpoints/` - API endpoint definitions
  - `Services/` - Business logic and FHIR mapping
  - `Models/` - Data models and DTOs
  - `Configuration/` - App configuration and DI setup
- `tests/Api.IntegrationTests/` - Integration tests

## Configuration

The application uses SQLite by default. To use SQL Server, set the connection string in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=...;Database=...;Trusted_Connection=True;"
  }
}
```

## Running Tests

```powershell
dotnet test
```

## Contributing

1. Create a feature branch
2. Make your changes
3. Add tests
4. Create a pull request

## License

[MIT License](LICENSE)