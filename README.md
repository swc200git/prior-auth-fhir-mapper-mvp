# Prior Auth FHIR Mapper MVP

A .NET 9.0 microservice that converts prior authorization requests into FHIR R4 Claim resources, persisting both the original request and FHIR output for auditing. Built with a focus on clean architecture, testability, and modern .NET backend practices.

---

## Features

- Transforms prior authorization requests into standard FHIR R4 Claims using the Hl7.Fhir.R4 library  
- Persists raw requests and FHIR JSON for auditing and traceability  
- Supports standard medical coding systems (CPT for services, ICD-10 for diagnoses)  
- Uses SQLite for local development with seamless switch to SQL Server for production  
- Utilizes Minimal API pattern for lightweight and maintainable endpoint setup  
- Includes integration tests with EF Core InMemory database provider  
- Implements environment-aware service registration and database migration  

---

## Design Choices & Architecture

### Minimal API with Extension Methods  
Endpoints and service registrations are structured as extension methods on standard interfaces (`IEndpointRouteBuilder`, `IServiceCollection`) for modularity and testability. This promotes clear separation between configuration and route definitions.

### Dependency Injection & Service Layer  
All services, including data access (`AppDbContext`), business logic (`PriorAuthService`, `FhirMappingService`), and health checks, are registered using the built-in DI container with scoped lifetimes. Interfaces (`IPriorAuthService`) abstract implementations, enabling mocking and flexible testing.

### Entity Framework Core 9  
- EF Core manages the relational model and migrations.
- The model enforces data integrity with explicit key and index configurations.
- Environment-based conditional registration allows switching between InMemory database (for tests) and relational database providers (SQLite or SQL Server).
- Database migrations apply automatically in the development environment to simplify local setup.

### Async/Await Everywhere  
End-to-end asynchronous API and database calls ensure scalable, non-blocking request processing.

### Validation & Error Handling  
Basic input validation occurs within the service layer, with clear error message returns. For production scenarios, this can be enhanced with validation libraries and middleware error handling.

### Testing  
Integration tests use a custom `WebApplicationFactory` overriding services to replace the real database context with EF Core InMemory provider, isolating tests and avoiding external dependencies.

---

## Prerequisites

- .NET 9.0 SDK  
- Visual Studio 2025 or VS Code with C# extensions  
- SQLite (included) or SQL Server for production  

---

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

3. Test the API with a sample POST request:
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

---

## Project Structure

- `src/Api/`  
  - `Endpoints/` - API route definitions organized by domain  
  - `Services/` - Business logic and FHIR JSON mapping  
  - `Data/` - EF Core DbContext and data models  
  - `Configuration/` - DI and migration helpers  
  - `Models/` - Request and domain model classes  

- `tests/Api.IntegrationTests/` - Integration tests  

---

## Configuration

By default, SQLite is used for local development. To use SQL Server in production, configure your connection string in `appsettings.json` or environment variables:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=your_server;Database=your_db;Trusted_Connection=True;"
  }
}
```

---

## Running Tests

Run all integration tests with:

```powershell
dotnet test
```
Tests use EF Core InMemory database for isolated and fast execution.

---

## Contributing

1. Fork the repository and create a feature branch.  
2. Make improvements or bug fixes.  
3. Add or update tests accordingly.  
4. Submit a pull request for review.

---

## License

This project is licensed under the MIT License.

---

## Contact

For questions or feedback, please open an issue or contact the maintainer.