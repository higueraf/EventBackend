namespace Event.Application.Dtos.Occurrence.Request
{
    public class OccurrenceRequestDto
    {
        public DateTime? OccurrenceDate { get; set; }
        public string? Spot { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? State { get; set; }
    }
}
