using Hotell.Models;
using Hotell.Services;
using Hotell.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Hotell.Controllers
{
    [Authorize]
    public class ReceptionController : Controller
    {
        private readonly IRoomService roomService;
        public ReceptionController(IRoomService roomService)
        {
            this.roomService = roomService;
        }

        [HttpPost]
        public async Task<IActionResult> Search(Reception form)
        {
            if (ModelState.IsValid)
            {
                var rooms = new List<Room>();
                if (form.SearchStart.HasValue && form.SearchEnd.HasValue)
                {
                    var dataTransfer = await this.roomService.GetFreeRooms(form.SearchStart.Value, form.SearchEnd.Value);
                    if(dataTransfer.IsSuccess)
                    {
                        rooms.AddRange(dataTransfer.Data);

                        if (form.IsCheaperFirst)
                        {
                            rooms = rooms.OrderBy(room => room.Price).ToList();
                        }
                        Reception model = new()
                        {
                            SearchStart = form.SearchStart,
                            SearchEnd = form.SearchEnd,
                            IsCheaperFirst = form.IsCheaperFirst,
                            Rooms = rooms
                        };
                        return View("Index", model);
                    }
                    else
                        return RedirectToAction("Error", "Home", new Error() { Message = dataTransfer.ErrorMessage });
                }
                else
                {
                    return RedirectToAction("Error", "Home", new Error() { Message = "Internal system error, please try later..." });
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
            
        }
        public IActionResult Index()
        {
            Reception booking = new() { Rooms= Enumerable.Empty<Room>(), IsCheaperFirst=true };

            return View(booking);
        }


    }
}
