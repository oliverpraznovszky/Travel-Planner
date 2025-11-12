namespace travel_planner.Models
{
    public class Trip
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Description { get; set; }
        public decimal Budget { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigációs propertek
        public User CreatedBy { get; set; } = null!;
        public ICollection<TripParticipant> Participants { get; set; } = new List<TripParticipant>();
        public ICollection<Location> Locations { get; set; } = new List<Location>();
        public ICollection<Itinerary> Itineraries { get; set; } = new List<Itinerary>();
    }
}