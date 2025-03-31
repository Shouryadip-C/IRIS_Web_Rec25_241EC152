using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IRIS_Web_Rec25_241EC152.Models
{
    public class Equipment
    {
        public Equipment()
        {
            Condition = "New";
            Status = Status.Available;
        }
        
        public int Id { get; set; }

        [Required(ErrorMessage = "Equipment name is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be 3-100 characters")]
        [Display(Name = "Equipment Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, 100, ErrorMessage = "Quantity must be between 1-100")]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Please select a sport type")]
        [Display(Name = "Sport Type")]
        public string Type { get; set; } // Football, Basketball, Fitness, Tennis, Table-Tennis, Volleyball, Cricket

        [Required]
        public Status Status { get; set; } // Available, Reserved, Under Maintenance

        [Required(ErrorMessage = "Condition is required")]
        [Display(Name = "Condition")]
        public string Condition { get; set; }

        // Static list of allowed conditions
        public static readonly List<string> Conditions =
        [
            "New",
            "Used - Good",
            "Used - Fair",
            "Damaged"
        ];
    }

    public enum Status
    {
        Available=1,
        UnderMaintenance=2,
        Reserved=3        
    }
}
