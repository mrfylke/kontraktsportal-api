using FluentResults;

namespace Domain.Deviations;

public class DeviationType
{
    public int Id { get; }
    public Guid PublicId { get; private set; }
    public string Name { get; private set; }

    public ICollection<Deviation> Deviations { get; private set; }
    public DeviationCategory DeviationCategory { get; private set; }

    public static Result<DeviationType> New(string name, DeviationCategory deviationCategory)
    {
        return new DeviationType
        {
            PublicId = Guid.NewGuid(),
            Name = name,
            DeviationCategory = deviationCategory,
            Deviations = []
        };
    }
}