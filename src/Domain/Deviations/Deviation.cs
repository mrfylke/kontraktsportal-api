using FluentResults;

namespace Domain.Deviations;

public class Deviation
{
    public int Id { get; }
    public Guid PublicId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string Description { get; private set; }
    public string LineId { get; private set; }
    public string? StopPlace { get; private set; }

    public DeviationType DeviationType { get; private set; }

    public static Result<Deviation> New(string description, string lineId, string? stopPlace,
        DeviationType deviationType)
    {
        return new Deviation
        {
            PublicId = Guid.NewGuid(),
            CreatedAt = DateTime.Now,
            Description = description,
            LineId = lineId,
            StopPlace = stopPlace,
            DeviationType = deviationType
        };
    }
}