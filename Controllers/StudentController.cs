using IRIS_Web_Rec25_241EC152.Models;
using IRIS_Web_Rec25_241EC152.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using IRIS_Web_Rec25_241EC152.Data;
using Microsoft.EntityFrameworkCore;


namespace IRIS_Web_Rec25_241EC152.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public StudentController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _db = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Dashboard()
        {
            // Get the currently logged in user
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Retrieve the user's court bookings including related Court details.
            var courtBookings = await _db.CourtBookings
                .Where(cb => cb.UserId == user.Id )
                .Include(cb => cb.Court)
                .ToListAsync();

            // Retrieve the user's equipment bookings including related Equipment details.
            var equipmentBookings = await _db.EquipmentBookings
                .Where(eb => eb.UserId == user.Id)
                .Include(eb => eb.Equipment)
                .ToListAsync();

            // Populate the view model.
            var viewModel = new StudentDashboardViewModel
            {
                CourtBookings = courtBookings,
                EquipmentBookings = equipmentBookings
            };

            return View(viewModel);
        }

        // Delete Booking -------------------------------------------------------------------------------------------- //

        // Delete Court Booking
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCourtBooking(int id)
        {
            var booking = await _db.CourtBookings.FindAsync(id);
            if (booking == null) return NotFound();

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var notifications = _db.Notifications.Where(n => n.CourtBookingId == id);

            switch (booking.Status)
            {
                case BookingStatus.Cancelled:
                case BookingStatus.Rejected:
                case BookingStatus.Pending:
                    _db.Notifications.RemoveRange(notifications);
                    _db.CourtBookings.Remove(booking);
                    break;
                    
                case BookingStatus.Approved:
                    if (notifications != null)
                    {
                        _db.Notifications.RemoveRange(notifications);
                    }

                    booking.Status = BookingStatus.CancellationRequested;
                    _db.Update(booking);

                    TempData["Success"] = "Court booking cancellation request raised successfully!";
                    break;
            }

            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEquipmentBooking(int id)
        {
            var booking = await _db.EquipmentBookings.FindAsync(id);
            if (booking == null) return NotFound();


            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var notifications = _db.Notifications.Where(n => n.EquipmentBookingId == id);

            switch (booking.Status)
            {
                case BookingStatus.Cancelled:
                case BookingStatus.Rejected:
                case BookingStatus.Pending:
                    _db.Notifications.RemoveRange(notifications);
                    _db.EquipmentBookings.Remove(booking);
                    break;

                case BookingStatus.Approved:
                    if (notifications != null)
                    {
                        _db.Notifications.RemoveRange(notifications);
                    }

                    booking.Status = BookingStatus.CancellationRequested;
                    _db.Update(booking);

                    TempData["Success"] = "Equipment booking cancellation request raised successfully!";
                    break;
            }

            await _db.SaveChangesAsync();
            return Ok();
        }

        // New Booking -------------------------------------------------------------------------------------------- //
        public IActionResult NewBooking(string bookingType, string sportType, DateTime? selectedDate)
        {
            // Default to tomorrow if no date is provided.
            var defaultDate = selectedDate ?? DateTime.Today.AddDays(1);
            var model = new NewBookingViewModel
            {
                BookingType = bookingType,
                SportType = sportType,
                SelectedDate = defaultDate
            };

            // If both booking type and sport type are provided, fetch available items.
            if (!string.IsNullOrEmpty(bookingType) && !string.IsNullOrEmpty(sportType))
            {
                if (bookingType == "Court")
                {
                    model.AvailableCourts = _db.Courts
                        .Where(c => c.Type == sportType && c.Availability == CourtAvailability.Available)
                        .ToList();
                }
                else if (bookingType == "Equipment")
                {
                    model.AvailableEquipment = _db.Equipments
                        .Where(e => e.Type == sportType && e.Status == Status.Available && e.Quantity > 0)
                        .ToList();
                }
            }

            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewBooking(NewBookingViewModel model)
        {
            // Remove errors for search fields that are not required on booking confirmation.
            ModelState.Remove("AvailableCourts");
            ModelState.Remove("AvailableEquipment");

            // Enforce booking must be made exactly one day prior.
            var tomorrow = DateTime.Today.AddDays(1);
            if (model.SelectedDate.Date != tomorrow)
            {
                ModelState.AddModelError("SelectedDate", "Bookings must be made exactly one day prior to the booking date.");
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (!ModelState.IsValid)
            {
                // Re-populate the sport types and available items before re-displaying.
              
                if (model.BookingType == "Court")
                {
                    model.AvailableCourts = _db.Courts
                        .Where(c => c.Type == model.SportType && c.Availability == CourtAvailability.Available)
                        .ToList();
                }
                else if (model.BookingType == "Equipment")
                {
                    model.AvailableEquipment = _db.Equipments
                        .Where(e => e.Type == model.SportType && e.Status == Status.Available && e.Quantity > 0)
                        .ToList();
                }
                return View(model);
            }

            if (!int.TryParse(model.SelectedTimeSlot, out int hourSlot))
            {
                ModelState.AddModelError("SelectedTimeSlot", "Invalid time slot.");

                model.AvailableCourts = _db.Courts
                    .Where(c => c.Type == model.SportType && c.Availability == CourtAvailability.Available)
                    .ToList();
                return View(model);
            }

            if (model.BookingType == "Court" && model.SelectedCourtId.HasValue)
            {
                var courtBooking = new CourtBooking
                {
                    CourtId = model.SelectedCourtId.Value,
                    UserId = user.Id,
                    BookingDate = model.SelectedDate,
                    HourSlot = hourSlot,
                    Status = BookingStatus.Pending
                };

                _db.CourtBookings.Add(courtBooking);
                await _db.SaveChangesAsync();

                // Optional: Create a notification.
                var notification = new Notification
                {
                    UserId = user.Id,
                    Message = "Your court booking has been created and is pending approval.",
                    CourtBookingId = courtBooking.Id,
                    Type = NotificationType.BookingReminder,
                    IsRead = false
                };
                _db.Notifications.Add(notification);
                await _db.SaveChangesAsync();

                TempData["Success"] = "Court booking created successfully!";
            }
            else if (model.BookingType == "Equipment" && model.SelectedEquipmentId.HasValue)
            {
                if (model.SelectedQuantity < 1)
                {
                    ModelState.AddModelError("SelectedQuantity", "Please select a valid quantity.");
                  
                    model.AvailableEquipment = _db.Equipments
                        .Where(e => e.Type == model.SportType && e.Status == Status.Available && e.Quantity > 0)
                        .ToList();
                    return View(model);
                }

                var equipment = _db.Equipments.Find(model.SelectedEquipmentId.Value);
                if (equipment == null || equipment.Quantity < model.SelectedQuantity)
                {
                    ModelState.AddModelError("SelectedQuantity", "Not enough equipment available.");
                  
                    model.AvailableEquipment = _db.Equipments
                        .Where(e => e.Type == model.SportType && e.Status == Status.Available && e.Quantity > 0)
                        .ToList();
                    return View(model);
                }

                var equipmentBooking = new EquipmentBooking
                {
                    EquipmentId = equipment.Id,
                    UserId = user.Id,
                    Quantity = model.SelectedQuantity,
                    BookingDate = model.SelectedDate,
                    HourSlot = hourSlot,
                    Status = BookingStatus.Pending
                };

                _db.EquipmentBookings.Add(equipmentBooking);
                await _db.SaveChangesAsync();

                var notification = new Notification
                {
                    UserId = user.Id,
                    Message = "Your equipment booking has been created and is pending approval.",
                    EquipmentBookingId = equipmentBooking.Id,
                    Type = NotificationType.BookingReminder,
                    IsRead = false
                };
                _db.Notifications.Add(notification);
                await _db.SaveChangesAsync();

                TempData["Success"] = "Equipment booking created successfully!";
            }

            return RedirectToAction("NewBooking", "Student");
        }

        [HttpGet]
        public IActionResult GetAvailableCourtSlots(int id, DateTime bookingDate)
        {
            var availableSlots = new List<object>();
            // Find the court by ID.
            var court = _db.Courts.Find(id);
            if (court == null)
            {
                return Json(new { success = false, message = "Court not found." });
            }

            int openingHour = court.OpeningTime.Hours;
            int closingHour = court.ClosingTime.Hours;

            // Get already booked slots for this court on the given date.
            var bookedSlots = _db.CourtBookings
                .Where(cb => cb.CourtId == id && cb.BookingDate.Date == bookingDate.Date)
                .Select(cb => cb.HourSlot)
                .ToList();

            for (int hour = openingHour; hour < closingHour; hour++)
            {
                if (!bookedSlots.Contains(hour))
                {
                    availableSlots.Add(new
                    {
                        hour,
                        displayText = $"{hour}:00 - {hour + 1}:00"
                    });
                }
            }

            return Json(new { success = true, slots = availableSlots });
        }

        [HttpGet]
        public IActionResult GetEquipmentAvailability(int equipmentId, DateTime bookingDate, int hourSlot)
        {
            // Get the equipment details
            var equipment = _db.Equipments.Find(equipmentId);
            if (equipment == null)
            {
                return Json(new { success = false, message = "Equipment not found." });
            }

            // Query all equipment bookings for this equipment on the same date
            var overlappingBookings = _db.EquipmentBookings
                .Where(b => b.EquipmentId == equipmentId &&
                        b.BookingDate.Date == bookingDate.Date &&
                        b.HourSlot == hourSlot &&
                        (b.Status == BookingStatus.Approved || b.Status == BookingStatus.CancellationRequested ||
                         b.Status == BookingStatus.Pending))
                .ToList();

            int bookedQuantity = 0;
            foreach (var booking in overlappingBookings)
            {
                bookedQuantity += booking.Quantity;
            }

            int availableQuantity = equipment.Quantity - bookedQuantity;
            if (availableQuantity < 0)
                availableQuantity = 0;

            return Json(new { success = true, availableQuantity });
        }

        // Notifications -------------------------------------------------------------------------------------------- //
        public async Task<IActionResult> Notifications()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Get all notifications for the logged in user
            var notifications = _db.Notifications
                .Where(n => n.UserId == user.Id)
                .OrderByDescending(n => n.CreatedAt)
                .ToList();

            return View(notifications);
        }

        [HttpPost]
        public async Task<IActionResult> MarkNotificationAsRead(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Json(new { success = false });

            var notification = _db.Notifications.FirstOrDefault(n => n.Id == id && n.UserId == user.Id);
            if (notification == null) return Json(new { success = false });

            notification.IsRead = true;
            _db.SaveChanges();

            return Json(new { success = true });
        }
    }
}
