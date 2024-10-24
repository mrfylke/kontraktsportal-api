using Domain.Deviations;

namespace Application.Deviations;

public interface IDeviationRepository
{
    Task<Deviation?> GetDeviationAsync(Guid publicId);
    Task<List<Deviation>> GetDeviationsAsync();
    Task AddDeviationAsync(Deviation resultValue);
}