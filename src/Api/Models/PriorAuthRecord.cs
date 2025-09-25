namespace Api.Models;

public class PriorAuthRecord
{
    public Guid Id { get; set; }
    public DateTime ReceivedAtUtc { get; set; }
    public string PatientId { get; set; } = "";
    public string ProviderNpi { get; set; } = "";
    public string ServiceCode { get; set; } = "";
    public string? DiagnosisCode { get; set; }
    public string Status { get; set; } = "Submitted";
    public string RawRequestJson { get; set; } = "";
    public string FhirClaimJson { get; set; } = "";
}
