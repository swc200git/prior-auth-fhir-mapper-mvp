using Api.Models;

namespace Api.Interfaces;

public interface IPriorAuthService
{
    Task<PriorAuthRecord?> GetByIdAsync(Guid id);
    Task<(bool IsSuccess, string? ErrorMessage, Guid? RecordId, string? Status)> CreateAsync(PriorAuthRequest request);
}