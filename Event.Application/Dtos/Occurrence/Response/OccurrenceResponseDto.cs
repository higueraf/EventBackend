namespace Event.Application.Dtos.Occurrence.Response
{
    public class OccurrenceResponseDto
    {
        public int OccurrenceId { get; set; }
        public string? Spot { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public DateTime? OccurrenceDate { get; set; }
        public int? State { get; set; }
        public string? StateOccurrence { get; set; }
    }
}
