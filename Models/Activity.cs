namespace travel_planner.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public int ItineraryId { get; set; }
        public int? LocationId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public decimal? EstimatedCost { get; set; }
        public ActivityPriority Priority { get; set; } = ActivityPriority.Medium;
        public int OrderIndex { get; set; } // Sorrend a napon belül
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigációs propertek
        public Itinerary Itinerary { get; set; } = null!;
        public Location? Location { get; set; }
    }
}