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

        [Range(6, 21)]
        public int HourSlot { get; set; } 

        public BookingStatus Status { get; set; } = BookingStatus.Pending;

        public string? RejectionReason { get; set; } = null;
        public bool ReminderSent { get; set; }

        // Computed properties
        public DateTime SlotStart => BookingDate.AddHours(HourSlot);
        public DateTime SlotEnd => SlotStart.AddHours(1);
    }
}
