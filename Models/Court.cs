using System.ComponentModel.DataAnnotations;

namespace IRIS_Web_Rec25_241EC152.Models
{
    public class Court
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string Location { get; set; }  // New field

        [Required]
        public CourtAvailability Availability { get; set; }  // Enum based status

        [Range(1, 20)]
        public int Capacity { get; set; }  // Max number of players

        [DataType(DataType.Time)]
        public TimeSpan OpeningTime { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan ClosingTime { get; set; }

        [Required]
        public string Type { get; set; } // Badminton, Tennis, etc.

        // Navigation property
        public List<CourtBooking> Bookings { get; set; } = [];
    }

    public enum CourtAvailability
    {
        Available,
        UnderMaintenance,
        Reserved,
        Closed
    }
}
