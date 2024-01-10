using Hotell.Models;
using Hotell.Services;
using Hotell.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hotell.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly IBookingService bookingService;
        private readonly ISessionManager sessionManager;
        public BookingController(IBookingService bookingService, ISessionManager sessionManager)
        {
            this.bookingService = bookingService;
            this.sessionManager = sessionManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = this.sessionManager.GetUser();
            if (user != null)
            {
                var dataTransfer = await bookingService.GetBookings(user.Id);
                if (dataTransfer.IsSuccess)
                {
                    ViewData["Message"] = TempData["Message"];
                    ViewData["Alert"] = TempData["Alert"];
                    return View("Index", dataTransfer.Data);
                }
                else
                    return RedirectToAction("Error", "Home", new Error() { Message = dataTransfer.ErrorMessage });
            }
            else
                return RedirectToAction("Login", "Account");

        }
        [HttpPost]
        public async Task<IActionResult> Booking(Booking booking)
        {
            var user = this.sessionManager.GetUser();
            if (user != null)
            {
                booking.UserId = user.Id;
                var dataTransfer = await bookingService.AddNew(booking);
                if (dataTransfer.IsSuccess)
                {
                    TempData["Message"] = $"New booking from {booking.Start.ToShortDateString()}-{booking.End.ToShortDateString()} for room Nr{booking.RoomNumber} is created";

                    return RedirectToAction("Index");
                }
                else
                    return RedirectToAction("Error", "Home", new Error() { Message = dataTransfer.ErrorMessage });

            }
            else
                return RedirectToAction("Login", "Account");
        }
        public async Task<IActionResult> Remove(Booking booking)
        {
            DateTime bookingStartDate = booking.Start.Date.AddDays(-3);
            if(DateTime.Now.Date >= bookingStartDate && DateTime.Now.Date <= booking.End.Date)
            {
                TempData["Alert"] = $"Booking cancellation from {booking.Start.ToShortDateString()}-{booking.End.ToShortDateString()} for room Nr{booking.RoomNumber} is not allowed";
                return RedirectToAction("Index");
            }
            else
            {
                var dataTransfer = await bookingService.RemoveBooking(booking.Id);
                if (dataTransfer.IsSuccess)
                {
                    TempData["Message"] = $"Booking from {booking.Start.ToShortDateString()}-{booking.End.ToShortDateString()} for room Nr{booking.RoomNumber} is removed";
                    return RedirectToAction("Index");
                }
                else
                    return RedirectToAction("Error", "Home", new Error() { Message = dataTransfer.ErrorMessage });
            }
        }
    }
}
