using Hotell.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Text;
using Hotell.Services.Interfaces;

namespace Hotell.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISessionManager sessionManager;
        public HomeController(ISessionManager sessionManager)
        {
            this.sessionManager = sessionManager;
        }

        public IActionResult Index()
        {
            if (this.sessionManager.IsAuthenticated)
            {

                return RedirectToAction("Index", "Reception");
            }
            
            return RedirectToAction("Login", "Account");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(Error error)
        {
            return View(error);
        }
    }
}
