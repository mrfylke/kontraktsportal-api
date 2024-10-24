using FluentResults;

namespace Domain.Deviations;

public class Deviation
{
    public int Id { get; }
    public Guid PublicId { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime ReportedAt { get; private set; }
    public string Description { get; private set; }
    public string LineId { get; private set; }
    public string? StopPlace { get; private set; }

    public DeviationType DeviationType { get; private set; }

    public static Result<Deviation> New(DateTime reportedAt, string description, string lineId,
        DeviationType deviationType, string? stopPlace
    )
    {
        return new Deviation
        {
            PublicId = Guid.NewGuid(),
            ReportedAt = reportedAt,
            Description = description,
            LineId = lineId,
            StopPlace = stopPlace,
            DeviationType = deviationType
        };
    }
}