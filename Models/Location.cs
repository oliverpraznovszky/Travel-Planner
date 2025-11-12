using System.Diagnostics;

namespace travel_planner.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public LocationType Type { get; set; } = LocationType.Other;
        public int TripId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigációs propertek
        public Trip Trip { get; set; } = null!;
        public ICollection<Activity> Activities { get; set; } = new List<Activity>();
    }
}