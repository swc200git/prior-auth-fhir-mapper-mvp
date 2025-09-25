using System.Text.Json;
using Api.Data;
using Api.Interfaces;
using Api.Models;

namespace Api.Services;

public class PriorAuthService(AppDbContext dbContext, FhirMappingService mapper) : IPriorAuthService
{
    private readonly AppDbContext _dbContext = dbContext;
    private readonly FhirMappingService _mapper = mapper;

    public async Task<PriorAuthRecord?> GetByIdAsync(Guid id)
    {
        return await _dbContext.PriorAuthRecords.FindAsync(id);
    }
    public async Task<(bool IsSuccess, string? ErrorMessage, Guid? RecordId, string? Status)> CreateAsync(PriorAuthRequest req)
    {
        if (string.IsNullOrWhiteSpace(req.PatientId) ||
            string.IsNullOrWhiteSpace(req.ProviderNpi) ||
            string.IsNullOrWhiteSpace(req.ServiceCode))
        {
            return (false, "patientId, providerNpi, and serviceCode are required.", null, null);
        }

        var fhirJson = _mapper.ToClaimJson(req);
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

        _dbContext.PriorAuthRecords.Add(record);
        await _dbContext.SaveChangesAsync();

        return (true, null, record.Id, record.Status);
    }
}
