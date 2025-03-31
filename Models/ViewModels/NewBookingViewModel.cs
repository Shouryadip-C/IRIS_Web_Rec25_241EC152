using System.ComponentModel.DataAnnotations;

namespace IRIS_Web_Rec25_241EC152.Models.ViewModels
{
    public class NewBookingViewModel
    {
        public string BookingType { get; set; }
        public string SportType { get; set; }

        [DataType(DataType.Date)]
        public DateTime SelectedDate { get; set; }

        // We'll store sport types in a simple array
        public string[] SportTypes = ["Football", "Basketball", "Fitness", "Tennis", "Table Tennis", "Kabaddi", "Squash", "Badminton", "Volleyball", "Cricket"];

        // Step 2: Fetched data to display as cards
        public List<Court> AvailableCourts { get; set; }
        public List<Equipment> AvailableEquipment { get; set; }

        // Step 3: User’s final selection
        public int? SelectedCourtId { get; set; }
        public int? SelectedEquipmentId { get; set; }

        // For Court
        public string SelectedTimeSlot { get; set; }

        // For Equipment
        [Range(1, 10, ErrorMessage = "Quantity must be 1-10.")]
        public int SelectedQuantity { get; set; } = 1;

        [Range(1, 4, ErrorMessage = "Duration must be 1-4 hours.")]
        public int SelectedDurationHours { get; set; } = 1;
    }
}
