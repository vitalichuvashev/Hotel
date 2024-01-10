using Hotell.Models;
using Hotell.Services;
using Hotell.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hotell.Controllers
{
    [Admin]
    public class AdminController : Controller
    {
        private readonly IAdminService adminService;
        public AdminController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        public async Task<IActionResult> Index()
        {
            var dataTransfer = await adminService.GetGuestBookings();
            if (dataTransfer.IsSuccess)
            {
                
                return View("Index", dataTransfer.Data);
            }
            else
                return RedirectToAction("Error", "Home", new Error() { Message = dataTransfer.ErrorMessage });
        }
    }
}
