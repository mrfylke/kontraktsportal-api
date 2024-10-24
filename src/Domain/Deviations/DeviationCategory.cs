using FluentResults;

namespace Domain.Deviations;

public class DeviationCategory
{
    public int Id { get; }
    public Guid PublicId { get; private set; }
    public string Name { get; private set; }
    public decimal Fee { get; private set; }

    public ICollection<DeviationType> DeviationTypes { get; private set; }

    public static Result<DeviationCategory> New(string name, decimal fee)
    {
        return new DeviationCategory
        {
            PublicId = Guid.NewGuid(),
            Name = name,
            Fee = fee,
            DeviationTypes = []
        };
    }
}