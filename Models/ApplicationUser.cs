using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace IRIS_Web_Rec25_241EC152.Models
{
    public class ApplicationUser: IdentityUser
    {
        [Required]
        [PersonalData]
        public string FullName { get; set; }

        [Required]
        public string Branch { get; set; }

        public bool IsAdmin { get; set; }

        // Navigation properties
        public List<EquipmentBooking> EquipmentBookings { get; set; } = new();
        public List<CourtBooking> CourtBookings { get; set; } = new();
        public List<Notification> Notifications { get; set; } = new();
    }
}

