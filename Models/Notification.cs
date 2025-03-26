using System.ComponentModel.DataAnnotations;

namespace IRIS_Web_Rec25_241EC152.Models
{
    public class Notification
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Required]
        public string Message { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsRead { get; set; }

        // Reference to either Equipment or Court booking
        public int? EquipmentBookingId { get; set; }
        public EquipmentBooking? EquipmentBooking { get; set; }

        public int? CourtBookingId { get; set; }
        public CourtBooking? CourtBooking { get; set; }

        [Required]
        public NotificationType Type { get; set; }
    }

    public enum NotificationType
    {
        BookingReminder,
        BookingApproved,
        BookingRejected,
        BookingCancelled
    }
}
