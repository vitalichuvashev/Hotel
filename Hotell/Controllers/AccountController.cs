using Microsoft.AspNetCore.Mvc;
using Hotell.Models;
using Hotell.Services.Interfaces;

namespace Hotell.Controllers
{
    public class AccountController : Controller
    {
        private readonly ISessionManager sessionManager;
        private readonly IAccountService accountService;
        public AccountController(ISessionManager sessionManager, IAccountService accountService)
        {
            this.sessionManager = sessionManager;
            this.accountService = accountService;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(User login)
        {
            if (ModelState.IsValid)
            {
                var userData = await this.accountService.GetUser(login);
                if (userData.IsSuccess)
                {
                    this.sessionManager.SetUser(userData.Data);
                    if (userData.Data.IsAdmin)
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                        return RedirectToAction("Index", "Reception");
                }
                else
                    ModelState.AddModelError("Error", userData.ErrorMessage);
            }
            return View();
        }
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registration(Registration newUser)
        {
            if (ModelState.IsValid)
            {
                var result = await this.accountService.AddUser(newUser);
                if (result.IsSuccess)
                {
                    this.sessionManager.SetUser(result.Data);
                    return RedirectToAction("Index", "Reception");
                }
                else
                    ModelState.AddModelError("Error", result.ErrorMessage);

            }
            return View();
        }
        public IActionResult Logout()
        {
            this.sessionManager.RemoveUser();
            return RedirectToAction("Login", "Account");
        }
    }
}
