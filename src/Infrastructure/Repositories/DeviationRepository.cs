using Application.Deviations;
using Domain.Deviations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class DeviationRepository(DatabaseContext context) : IDeviationRepository
{
    public Task<Deviation?> GetDeviationAsync(Guid publicId)
    {
        return context.Deviations.FirstOrDefaultAsync(d => d.PublicId == publicId);
    }

    public Task<List<Deviation>> GetDeviationsAsync()
    {
        return context.Deviations.ToListAsync();
    }

    public Task AddDeviationAsync(Deviation resultValue)
    {
        context.Deviations.Add(resultValue);
        return context.SaveChangesAsync();
    }
}