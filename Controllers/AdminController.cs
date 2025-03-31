using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using IRIS_Web_Rec25_241EC152.Models;
using IRIS_Web_Rec25_241EC152.Models.ViewModels;
using IRIS_Web_Rec25_241EC152.Data;
using Microsoft.EntityFrameworkCore;
using System.Globalization;


namespace IRIS_Web_Rec25_241EC152.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AdminController(ApplicationDbContext db)
        {
            _db = db;
        }

        // Analytics Dashboard ---------------------------------------------------------------------------------------------------- //
        public async Task<IActionResult> Dashboard()
        {
            var startDate =  DateTime.Today;
            startDate = startDate.AddDays(-10);
            var endDate = startDate.AddDays(30);

            var model = new AnalyticsViewModel
            {
                MostBookedEquipment = await GetMostBookedEquipment(startDate, endDate),
                MostBookedCourts = await GetMostBookedCourts(startDate, endDate),
                EquipmentPeakHours = await GetEquipmentPeakHours(startDate, endDate),
                CourtPeakHours = await GetCourtPeakHours(startDate, endDate),
                DailyTrends = await GetCombinedTrends("daily", startDate, endDate),
                WeeklyTrends = await GetCombinedTrends("weekly", startDate, endDate),
                MonthlyTrends = await GetCombinedTrends("monthly", startDate, endDate)
            };

            return View(model);
        }

        private async Task<List<EquipmentBookingStats>> GetMostBookedEquipment(DateTime start, DateTime end)
        {
            return await _db.EquipmentBookings
                .Include(b => b.Equipment)
                .Where(b => b.BookingDate >= start.Date && b.BookingDate <= end.Date)
                .GroupBy(b => b.Equipment.Name)
                .Select(g => new EquipmentBookingStats
                {
                    EquipmentName = g.Key,
                    TotalBookings = g.Count(),
                    TotalHoursBooked = g.Count()
                })
                .OrderByDescending(x => x.TotalBookings)
                .Take(10)
                .ToListAsync();
        }

        private async Task<List<CourtBookingStats>> GetMostBookedCourts(DateTime start, DateTime end)
        {
            return await _db.CourtBookings
                .Include(b => b.Court)
                .Where(b => b.BookingDate >= start.Date && b.BookingDate <= end.Date)
                .GroupBy(b => b.Court.Name)
                .Select(g => new CourtBookingStats
                {
                    CourtName = g.Key,
                    TotalBookings = g.Count(),
                    TotalHoursBooked = g.Count()
                })
                .OrderByDescending(x => x.TotalBookings)
                .Take(10)
                .ToListAsync();
        }

        private async Task<List<PeakHourStats>> GetEquipmentPeakHours(DateTime start, DateTime end)
        {
            return await _db.EquipmentBookings
                .Where(b => b.BookingDate >= start.Date && b.BookingDate <= end.Date)
                .GroupBy(b => b.HourSlot)
                .Select(g => new PeakHourStats
                {
                    Hour = g.Key,
                    BookingCount = g.Count(),
                    TotalHours = g.Count()
                })
                .OrderByDescending(x => x.BookingCount)
                .ToListAsync();
        }

        private async Task<List<PeakHourStats>> GetCourtPeakHours(DateTime start, DateTime end)
        {
            return await _db.CourtBookings
                .Where(b => b.BookingDate >= start.Date && b.BookingDate <= end.Date)
                .GroupBy(b => b.HourSlot)
                .Select(g => new PeakHourStats
                {
                    Hour = g.Key,
                    BookingCount = g.Count(),
                    TotalHours = g.Count()
                })
                .OrderByDescending(x => x.BookingCount)
                .ToListAsync();
        }

        private async Task<Dictionary<string, int>> GetCombinedTrends(string period, DateTime start, DateTime end)
        {
            var equipmentData = await GetEquipmentTrends(period, start, end);
            var courtData = await GetCourtTrends(period, start, end);

            return equipmentData
                .Concat(courtData)
                .GroupBy(x => x.Key)
                .ToDictionary(
                    g => g.Key,
                    g => g.Sum(x => x.Value)
                );
        }


        private async Task<Dictionary<string, int>> GetEquipmentTrends(string period, DateTime start, DateTime end)
        {
            return period.ToLower() switch
            {
                "daily" => await GetDailyTrends(_db.EquipmentBookings, start, end),
                "weekly" => await GetWeeklyTrends(_db.EquipmentBookings, start, end),
                "monthly" => await GetMonthlyTrends(_db.EquipmentBookings, start, end),
                _ => []
            };
        }

        private async Task<Dictionary<string, int>> GetCourtTrends(string period, DateTime start, DateTime end)
        {
            return period.ToLower() switch
            {
                "daily" => await GetDailyTrends(_db.CourtBookings, start, end),
                "weekly" => await GetWeeklyTrends(_db.CourtBookings, start, end),
                "monthly" => await GetMonthlyTrends(_db.CourtBookings, start, end),
                _ => []
            };
        }

        private static async Task<Dictionary<string, int>> GetDailyTrends<T>(DbSet<T> dbSet, DateTime start, DateTime end) where T : class
        {
            var results = await dbSet
                .Where(b => EF.Property<DateTime>(b, "BookingDate") >= start.Date &&
                            EF.Property<DateTime>(b, "BookingDate") <= end.Date)
                .GroupBy(b => new {
                    EF.Property<DateTime>(b, "BookingDate").Year,
                    EF.Property<DateTime>(b, "BookingDate").Month,
                    EF.Property<DateTime>(b, "BookingDate").Day
                })
                .Select(g => new {
                    Date = new DateTime(g.Key.Year, g.Key.Month, g.Key.Day),
                    Count = g.Count()
                })
                .ToListAsync();

            return results
                .OrderBy(x => x.Date)
                .ToDictionary(x => x.Date.ToString("dd MMM"), x => x.Count);
        }

        private static async Task<Dictionary<string, int>> GetWeeklyTrends<T>(DbSet<T> dbSet, DateTime start, DateTime end) where T : class
        {
            var results = await dbSet
                .Where(b => EF.Property<DateTime>(b, "BookingDate") >= start.Date &&
                            EF.Property<DateTime>(b, "BookingDate") <= end.Date)
                .Select(b => EF.Property<DateTime>(b, "BookingDate"))
                .ToListAsync();

            return results
                .GroupBy(d => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                    d,
                    CalendarWeekRule.FirstDay,
                    DayOfWeek.Monday
                ))
                .OrderBy(g => g.Key)
                .ToDictionary(
                    g => $"Week {g.Key}",
                    g => g.Count()
                );
        }

        private static async Task<Dictionary<string, int>> GetMonthlyTrends<T>(DbSet<T> dbSet, DateTime start, DateTime end) where T : class
        {
            var results = await dbSet
                .Where(b => EF.Property<DateTime>(b, "BookingDate") >= start.Date &&
                            EF.Property<DateTime>(b, "BookingDate") <= end.Date)
                .GroupBy(b => new {
                    EF.Property<DateTime>(b, "BookingDate").Year,
                    EF.Property<DateTime>(b, "BookingDate").Month
                })
                .Select(g => new {
                    g.Key.Year,
                    g.Key.Month,
                    Count = g.Count()
                })
                .ToListAsync();

            return results
                .OrderBy(x => x.Year)
                .ThenBy(x => x.Month)
                .ToDictionary(
                    x => $"{x.Year}-{x.Month:00}",
                    x => x.Count
                );
        }

        // Manage Equipment ---------------------------------------------------------------------------------------------------- //
        public async Task<IActionResult> ManageEquipment()
        {
            var equipmentList = await _db.Equipments
                .OrderBy(e => e.Name)
                .ToListAsync();
            return View(equipmentList);
        }

        public async Task<IActionResult> EquipmentTablePartial()
        {
            var equipmentList = await _db.Equipments
                .OrderBy(e => e.Name)
                .ToListAsync();
            return PartialView("_EquipmentTablePartial", equipmentList);
        }

        // Create Equipment
        public IActionResult CreateEquipment()
        {
            return PartialView("_CreateEquipmentPartial");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEquipment(Equipment equipment)
        {
            if (ModelState.IsValid)
            {
                _db.Add(equipment);
                await _db.SaveChangesAsync();

                return Json(new { success = true, redirectUrl = Url.Action("ManageEquipment") });
            }

            return PartialView("_CreateEquipmentPartial", equipment);
        }

        // Update Equipment
        public async Task<IActionResult> EditEquipment(int? id)
        {
            if (id == null) return NotFound();

            var equipment = await _db.Equipments.FindAsync(id);
            if (equipment == null) return NotFound();

            return PartialView("_EditEquipmentPartial", equipment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEquipment(int id, Equipment equipment)
        {
            if (id != equipment.Id) return NotFound();

            var originalEquipment = await _db.Equipments
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);

            if (ModelState.IsValid)
            {
                try
                {
                    // Preserve other uneditable fields
                    equipment.Name = originalEquipment.Name;
                    equipment.Type = originalEquipment.Type;

                    _db.Update(equipment);
                    await _db.SaveChangesAsync();
                    return Json(new { success = true });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipmentExists(equipment.Id)) return NotFound();
                    throw;
                }
            }

            return PartialView("_EditEquipmentPartial", equipment);
        }

        private bool EquipmentExists(int id)
        {
            return _db.Equipments.Any(e => e.Id == id);
        }

        // Delete Equipment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEquipment(int id)
        {
            var equipment = await _db.Equipments.FindAsync(id);
            if (equipment == null) return NotFound();

            _db.Equipments.Remove(equipment);
            await _db.SaveChangesAsync();
            return Ok();
        }


        // Manage Court  --------------------------------------------------------------------------------------------------------- //
        public async Task<IActionResult> ManageCourt()
        {
            var courtList = await _db.Courts
                .OrderBy(e => e.Name)
                .ToListAsync();
            return View(courtList);
        }

        public async Task<IActionResult> CourtTablePartial()
        {
            var courtList = await _db.Courts
                .OrderBy(e => e.Name)
                .ToListAsync();
            return PartialView("_CourtTablePartial", courtList);
        }

        // Create Court
        public IActionResult CreateCourt()
        {
            return PartialView("_CreateCourtPartial");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCourt(Court court)
        {
            if (ModelState.IsValid)
            {
                _db.Add(court);
                await _db.SaveChangesAsync();

                return Json(new { success = true, redirectUrl = Url.Action("ManageCourt") });
            }

            return PartialView("_CreateCourtPartial", court);
        }

        // Update Court
        public async Task<IActionResult> EditCourt(int? id)
        {
            if (id == null) return NotFound();

            var court = await _db.Courts.FindAsync(id);
            if (court == null) return NotFound();

            return PartialView("_EditCourtPartial", court);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCourt(int id, Court court)
        {
            if (id != court.Id) return NotFound();

            var originalCourt = await _db.Courts
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(court);
                    await _db.SaveChangesAsync();
                    return Json(new { success = true });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourtExists(court.Id)) return NotFound();
                    throw;
                }
            }

            return PartialView("_EditCourtPartial", court);
        }

        private bool CourtExists(int id)
        {
            return _db.Courts.Any(e => e.Id == id);
        }

        // Delete Court
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCourt(int id)
        {
            var court = await _db.Courts.FindAsync(id);
            if (court == null) return NotFound();

            _db.Courts.Remove(court);
            await _db.SaveChangesAsync();
            return Ok();
        }


        // Manage Request ------------------------------------------------------------------------------------------------ //
        public async Task<IActionResult> ManageRequests()
        {
            var model = new ManageRequestsViewModel
            {
                CourtBookings = await _db.CourtBookings
                    .Include(b => b.Court)
                    .Include(b => b.User)
                    .Where(b => b.Status == BookingStatus.Pending || b.Status == BookingStatus.CancellationRequested)
                    .ToListAsync(),

                EquipmentBookings = await _db.EquipmentBookings
                    .Include(b => b.Equipment)
                    .Include(b => b.User)
                    .Where(b => b.Status == BookingStatus.Pending || b.Status == BookingStatus.CancellationRequested)
                    .ToListAsync()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveCourtBooking(int id)
        {
            var booking = await _db.CourtBookings
                .Include(b => b.Court)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null)
            {
                return NotFound();
            }

            // Check court availability
            var isAvailable = await IsCourtAvailable(booking.CourtId);
            if (!isAvailable)
            {
                TempData["ErrorMessage"] = "Court is no longer available for the selected slot";
                return RedirectToAction(nameof(ManageRequests));
            }

            booking.Status = BookingStatus.Approved;
            _db.Update(booking);

            // Create notification
            var notification = new Notification
            {
                UserId = booking.UserId,
                Message = $"Your court booking for {booking.Court.Name} on {booking.BookingDate:dd MMM} at {booking.HourSlot}:00 has been approved",
                Type = NotificationType.BookingApproved,
                CourtBookingId = booking.Id
            };
            _db.Add(notification);

            await _db.SaveChangesAsync();

            TempData["SuccessMessage"] = "Court booking approved";
            return RedirectToAction(nameof(ManageRequests));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveCourtCancellation(int id)
        {
            var booking = await _db.CourtBookings
                .Include(b => b.Court)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null)
            {
                return NotFound();
            }

            // Check court availability
            var isAvailable = await IsCourtAvailable(booking.CourtId);
            if (!isAvailable)
            {
                TempData["ErrorMessage"] = "Court is no longer available for the selected slot";
                return RedirectToAction(nameof(ManageRequests));
            }

            booking.Status = BookingStatus.Cancelled;
            _db.Update(booking);

            // Create notification
            var notification = new Notification
            {
                UserId = booking.UserId,
                Message = $"Your court booking for {booking.Court.Name} on {booking.BookingDate:dd MMM} at {booking.HourSlot}:00 has been cancelled",
                Type = NotificationType.BookingCancelled,
                CourtBookingId = booking.Id
            };
            _db.Add(notification);

            await _db.SaveChangesAsync();

            TempData["SuccessMessage"] = "Court cancellation approved";
            return RedirectToAction(nameof(ManageRequests));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectCourtBooking(int id, string reason)
        {
            var booking = await _db.CourtBookings
                .Include(b => b.Court)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null)
            {
                return NotFound();
            }

            booking.Status = BookingStatus.Rejected;
            booking.RejectionReason = reason;
            _db.Update(booking);

            // Create notification
            var notification = new Notification
            {
                UserId = booking.UserId,
                Message = $"Your court booking for {booking.Court.Name} on {booking.BookingDate:dd MMM} at {booking.HourSlot}:00 has been rejected. Reason: {reason}",
                Type = NotificationType.BookingRejected,
                CourtBookingId = booking.Id
            };
            _db.Add(notification);

            await _db.SaveChangesAsync();

            TempData["SuccessMessage"] = "Court booking rejected";
            return RedirectToAction(nameof(ManageRequests));
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectCourtCancellation(int id, string reason)
        {
            var booking = await _db.CourtBookings
                .Include(b => b.Court)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null)
            {
                return NotFound();
            }

            booking.Status = BookingStatus.Approved;
            booking.RejectionReason = reason;
            _db.Update(booking);

            // Create notification
            var notification = new Notification
            {
                UserId = booking.UserId,
                Message = $"Your court cancellation request for {booking.Court.Name} on {booking.BookingDate:dd MMM} at {booking.HourSlot}:00 has been rejected. Reason: {reason}",
                Type = NotificationType.BookingApproved,
                CourtBookingId = booking.Id
            };
            _db.Add(notification);

            await _db.SaveChangesAsync();

            TempData["SuccessMessage"] = "Court cancellation rejected";
            return RedirectToAction(nameof(ManageRequests));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveEquipmentBooking(int id)
        {
            var booking = await _db.EquipmentBookings
                .Include(b => b.Equipment)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null)
            {
                return NotFound();
            }

            // Check equipment availability
            if (booking.Equipment.Quantity < booking.Quantity)
            {
                TempData["ErrorMessage"] = "Insufficient equipment quantity available";
                return RedirectToAction(nameof(ManageRequests));
            }

            booking.Status = BookingStatus.Approved;
            _db.Update(booking);

            // Create notification
            var notification = new Notification
            {
                UserId = booking.UserId,
                Message = $"Your equipment booking for {booking.Quantity}x {booking.Equipment.Name} has been approved",
                Type = NotificationType.BookingApproved,
                EquipmentBookingId = booking.Id
            };
            _db.Add(notification);

            await _db.SaveChangesAsync();

            TempData["SuccessMessage"] = "Equipment booking approved successfully";
            return RedirectToAction(nameof(ManageRequests));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveEquipmentCancellation(int id)
        {
            var booking = await _db.EquipmentBookings
                .Include(b => b.Equipment)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null)
            {
                return NotFound();
            }

            // Check equipment availability
            var isAvailable = await IsEquipmentAvailable(booking.EquipmentId);
            if (!isAvailable)
            {
                TempData["ErrorMessage"] = "Equipment is no longer available for the selected slot";
                return RedirectToAction(nameof(ManageRequests));
            }

            booking.Status = BookingStatus.Cancelled;
            _db.Update(booking);

            // Create notification
            var notification = new Notification
            {
                UserId = booking.UserId,
                Message = $"Your equipment booking for {booking.Equipment.Name} on {booking.BookingDate:dd MMM} at {booking.HourSlot}:00 has been cancelled",
                Type = NotificationType.BookingCancelled,
                EquipmentBookingId = booking.Id
            };
            _db.Add(notification);

            await _db.SaveChangesAsync();

            TempData["SuccessMessage"] = "Equipment cancellation approved";
            return RedirectToAction(nameof(ManageRequests));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectEquipmentBooking(int id, string reason)
        {
            var booking = await _db.EquipmentBookings
                .Include(b => b.Equipment)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null)
            {
                return NotFound();
            }

            booking.Status = BookingStatus.Rejected;
            booking.RejectionReason = reason;
            _db.Update(booking);

            // Create notification
            var notification = new Notification
            {
                UserId = booking.UserId,
                Message = $"Your equipment booking for {booking.Quantity}x {booking.Equipment.Name} has been rejected. Reason: {reason}",
                Type = NotificationType.BookingRejected,
                EquipmentBookingId = booking.Id
            };
            _db.Add(notification);

            await _db.SaveChangesAsync();

            TempData["SuccessMessage"] = "Equipment booking rejected successfully";
            return RedirectToAction(nameof(ManageRequests));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectEquipmentCancellation(int id, string reason)
        {
            var booking = await _db.EquipmentBookings
                .Include(b => b.Equipment)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null)
            {
                return NotFound();
            }

            booking.Status = BookingStatus.Approved;
            booking.RejectionReason = reason;
            _db.Update(booking);

            // Create notification
            var notification = new Notification
            {
                UserId = booking.UserId,
                Message = $"Your equipment cancellation request for {booking.Equipment.Name} on {booking.BookingDate:dd MMM} at {booking.HourSlot}:00 has been rejected. Reason: {reason}",
                Type = NotificationType.BookingApproved,
                EquipmentBookingId = booking.Id
            };
            _db.Add(notification);

            await _db.SaveChangesAsync();

            TempData["SuccessMessage"] = "Equipment cancellation rejected";
            return RedirectToAction(nameof(ManageRequests));
        }

        private async Task<bool> IsCourtAvailable(int courtId)
        {
            return await _db.Courts
                .AnyAsync(c => c.Id == courtId);
        }
        
        private async Task<bool> IsEquipmentAvailable(int equipmentId)
        {
            return await _db.Equipments
                .AnyAsync(c => c.Id == equipmentId);
        }
    }
}
