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
        public string Location { get; set; }

        [Required]
        public CourtAvailability Availability { get; set; }

        [Range(1, 20)]
        public int Capacity { get; set; }  // Max number of players

        [DataType(DataType.Time)]
        public TimeSpan OpeningTime { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan ClosingTime { get; set; }

        [Required(ErrorMessage = "Please select a sport type")]
        [Display(Name = "Sport Type")]
        public string Type { get; set; } // Football, Basketball, Fitness, Tennis, Table-Tennis, Volleyball, Cricket
    }

    public enum CourtAvailability
    {
        Available,
        UnderMaintenance,
        Reserved,
        Closed
    }
}
