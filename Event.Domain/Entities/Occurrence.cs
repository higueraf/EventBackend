namespace Event.Domain.Entities;

public partial class Occurrence : BaseEntity
{
    public DateTime? OccurrenceDate { get; set; }
    public string? Spot { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }

}
