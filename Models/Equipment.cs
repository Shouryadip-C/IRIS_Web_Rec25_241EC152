using System.ComponentModel.DataAnnotations;

namespace IRIS_Web_Rec25_241EC152.Models
{
    public class Equipment
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Range(1, 100)]
        public int TotalQuantity { get; set; }

        public int AvailableQuantity { get; set; }

        public string Category { get; set; } // Football, Basketball, Fitness, Tennis, Table-Tennis, Volleyball, Cricket

        public string Condition { get; set; } // Good, New, Used, etc.

        [Required]
        public string Status { get; set; } // Available, Not Available, Under Maintenance
    }
}
