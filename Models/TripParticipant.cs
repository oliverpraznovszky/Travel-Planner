namespace travel_planner.Models
{
    public class TripParticipant
    {
        public int Id { get; set; }
        public int TripId { get; set; }
        public int UserId { get; set; }
        public ParticipantRole Role { get; set; } = ParticipantRole.Viewer;
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

        // Navigációs propertek
        public Trip Trip { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}