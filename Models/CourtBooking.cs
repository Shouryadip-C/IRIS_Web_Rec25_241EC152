using System.ComponentModel.DataAnnotations;

namespace IRIS_Web_Rec25_241EC152.Models
{
    public class CourtBooking
    {
        public int Id { get; set; }

        [Required]
        public int CourtId { get; set; }
        public Court Court { get; set; }

        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [DataType(DataType.Date)]
        public DateTime BookingDate { get; set; }

        [Range(9, 21)] // 9 AM to 9 PM slots
        public int HourSlot { get; set; } // 9 = 9-10 AM, 15 = 3-4 PM

        public string Status { get; set; } = "Pending"; // Approved/Rejected/Cancelled
        public bool ReminderSent { get; set; }

        // Computed properties
        public DateTime SlotStart => BookingDate.AddHours(HourSlot);
        public DateTime SlotEnd => SlotStart.AddHours(1);
    }
}
