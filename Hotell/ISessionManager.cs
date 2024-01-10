using Hotell.Models;

namespace Hotell
{
    public interface ISessionManager
    {
        User? GetUser();
        void SetUser(User user);
        void RemoveUser();

        string GetDisplayName();

        bool IsAdministrator();

        bool IsAuthenticated { get; }
    }
}
