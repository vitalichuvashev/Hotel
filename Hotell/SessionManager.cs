using System.Text.Json;
using Hotell.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Hotell
{
    public class SessionManager : ISessionManager
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public SessionManager(IHttpContextAccessor httpContextAccessor)=> this.httpContextAccessor = httpContextAccessor;

        private ISession? Session { get => this.httpContextAccessor.HttpContext?.Session; }

        public bool IsAuthenticated { get=> GetUser() != null; }
        
        public User? GetUser()
        {
            var data = this.Session?.GetString("User");

            if (!string.IsNullOrEmpty(data))
            {
                var user = JsonSerializer.Deserialize<User>(data);
                return user;
            }
            return null;
        }
        public void SetUser(User user)
        {
            var json = JsonSerializer.Serialize<User>(user) ?? string.Empty;
            this.Session?.SetString("User", json);
        }
        public void RemoveUser()
        {
            this.Session?.Remove("User");
        }
        public string GetDisplayName()
        {
            var user = GetUser();
            if(user != null)
            {
                return $"{user.Firstname} {user.Lastname}"; 
            }
            return string.Empty;
        }
        public bool IsAdministrator()
        {
            var user = GetUser();
            if (user != null)
            {
                return user.IsAdmin;
            }
            return false;
        }
    }
}
