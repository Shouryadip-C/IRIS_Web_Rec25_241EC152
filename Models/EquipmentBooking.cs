using System.ComponentModel.DataAnnotations;

namespace IRIS_Web_Rec25_241EC152.Models
{
    public class EquipmentBooking
    {
        public int Id { get; set; }

        [Required]
        public int EquipmentId { get; set; }
        public Equipment Equipment { get; set; }

        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Range(1, 5)]
        public int Quantity { get; set; }

        public DateTime BookingDate { get; set; }

        [Range(1, 4)]
        public int DurationHours { get; set; } // Max 4 hours

        public string Status { get; set; } = "Pending"; // Approved/Rejected/Cancelled
    }

}
