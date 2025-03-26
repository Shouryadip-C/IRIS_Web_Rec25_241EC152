using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using IRIS_Web_Rec25_241EC152.Models;


namespace IRIS_Web_Rec25_241EC152.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            ViewBag.UserEmail = User.Identity.Name;
            return View();
        }

        // Equipment Management
        /*public IActionResult ManageEquipment() { ... }
        [HttpPost]
        public IActionResult UpdateEquipment(Equipment model) { ... }

        // Request Management
        public IActionResult PendingRequests() { ... }
        [HttpPost]
        public IActionResult ApproveRequest(int requestId) { ... }
        [HttpPost]
        public IActionResult RejectRequest(int requestId, string comments) { ... }*/
    }
}
