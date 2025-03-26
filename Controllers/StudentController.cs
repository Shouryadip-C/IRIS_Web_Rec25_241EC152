using IRIS_Web_Rec25_241EC152.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IRIS_Web_Rec25_241EC152.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentController : Controller
    {
        public IActionResult Dashboard()
        {
            ViewBag.UserEmail = User.Identity.Name;
            ViewBag.Branch = User.FindFirst("Branch")?.Value;
            return View();
        }

        // Bookings
        /*public IActionResult MyBookings() { ... }
        public IActionResult NewBooking() { ... }
        [HttpPost]
        public IActionResult SubmitBooking(BookingRequest model) { ... }
        [HttpPost]
        public IActionResult CancelBooking(int bookingId) { ... }*/
    }
}
