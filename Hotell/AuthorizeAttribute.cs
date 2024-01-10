using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Hotell.Models;

namespace Hotell
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var json = context.HttpContext.Session?.GetString("User");
            if (!string.IsNullOrEmpty(json))
            {
                try
                {
                    var user = System.Text.Json.JsonSerializer.Deserialize<User>(json);
                    if (user == null)
                    {
                        context.Result = new RedirectToActionResult("Login", "Account", null);
                    }
                    else if(user.IsAdmin)
                    {
                        context.Result = new RedirectToActionResult("Error", "Home", new Error() { Message = "Unauthorized, for users only" });
                    }
                }
                catch(Exception e)
                {
                    context.Result = new RedirectToActionResult("Login", "Account", null);
                }
            }
            else
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
            }
        }
    }
}
