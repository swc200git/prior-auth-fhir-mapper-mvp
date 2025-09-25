namespace Api.Models;

public record PriorAuthRequest(
    string PatientId,
    string MemberId,
    string ProviderNpi,
    string ServiceCode,
    string DiagnosisCode,
    DateTime ServiceDate,
    string PlaceOfService,
    string Priority
);
