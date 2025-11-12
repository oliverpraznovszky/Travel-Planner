using System.Diagnostics;

namespace travel_planner.Models
{
    public class Itinerary
    {
        public int Id { get; set; }
        public int TripId { get; set; }
        public int DayNumber { get; set; } // 1, 2, 3...
        public DateTime Date { get; set; }
        public string? Notes { get; set; }

        // Navigációs propertek
        public Trip Trip { get; set; } = null!;
        public ICollection<Activity> Activities { get; set; } = new List<Activity>();
    }
}