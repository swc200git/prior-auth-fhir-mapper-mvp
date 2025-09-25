using Api.Data;
using Api.Models;
using Api.Services;
using System.Text.Json;

namespace Api.Endpoints;

public static class PriorAuthEndpoints
{
    public static void MapPriorAuthEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/prior-auth", async (
            PriorAuthRequest req, AppDbContext db, FhirMappingService mapper) =>
        {
            if (string.IsNullOrWhiteSpace(req.PatientId) ||
                string.IsNullOrWhiteSpace(req.ProviderNpi) ||
                string.IsNullOrWhiteSpace(req.ServiceCode))
                return Results.BadRequest("patientId, providerNpi, and serviceCode are required.");

            var fhirJson = mapper.ToClaimJson(req);
            var record = new PriorAuthRecord
            {
                Id = Guid.NewGuid(),
                ReceivedAtUtc = DateTime.UtcNow,
                PatientId = req.PatientId,
                ProviderNpi = req.ProviderNpi,
                ServiceCode = req.ServiceCode,
                DiagnosisCode = req.DiagnosisCode,
                Status = "Submitted",
                RawRequestJson = JsonSerializer.Serialize(req),
                FhirClaimJson = fhirJson
            };

            db.PriorAuthRecords.Add(record);
            await db.SaveChangesAsync();

            return Results.Created($"/prior-auth/{record.Id}", new { id = record.Id, status = record.Status });
        });
    }
}
