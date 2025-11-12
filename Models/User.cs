namespace travel_planner.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigációs propertek
        public ICollection<Trip> CreatedTrips { get; set; } = new List<Trip>();
        public ICollection<TripParticipant> TripParticipations { get; set; } = new List<TripParticipant>();
    }
}