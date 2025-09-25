using Api.Models;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;

namespace Api.Services;

public class FhirMappingService
{
    public string ToClaimJson(PriorAuthRequest r)
    {
        var claim = new Claim
        {
            Status = FinancialResourceStatusCodes.Active,
            Type = new CodeableConcept("http://terminology.hl7.org/CodeSystem/claim-type", "professional"),
            Use = ClaimUseCode.Preauthorization, 
            Patient = new ResourceReference($"Patient/{r.PatientId}"),
            Provider = new ResourceReference
            {
                Identifier = new Identifier("http://hl7.org/fhir/sid/us-npi", r.ProviderNpi)
            },
            BillablePeriod = new Period { Start = r.ServiceDate.ToString("yyyy-MM-dd") }
        };

        if (!string.IsNullOrWhiteSpace(r.DiagnosisCode))
        {
            claim.Diagnosis =
            [
                new Claim.DiagnosisComponent
                {
                    Diagnosis = new CodeableConcept("http://hl7.org/fhir/sid/icd-10-cm", r.DiagnosisCode)
                }
            ];
        }

        claim.Item =
        [
            new Claim.ItemComponent
            {
                ProductOrService = new CodeableConcept("http://www.ama-assn.org/go/cpt", r.ServiceCode),
                Location = new CodeableConcept("http://terminology.hl7.org/CodeSystem/ex-serviceplace", r.PlaceOfService)
            }
        ];

        claim.Priority = new CodeableConcept(
            "http://terminology.hl7.org/CodeSystem/processpriority",
            (r.Priority?.ToLowerInvariant()) == "urgent" ? "stat" : "normal"
        );

        var serializer = new FhirJsonSerializer();
        return serializer.SerializeToString(claim);
    }
}

