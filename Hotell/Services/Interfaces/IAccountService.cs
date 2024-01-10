using Hotell.Models;
using Hotell.Services.DTO;

namespace Hotell.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<DataTransfer<User>> GetUser(User user);
        Task<DataTransfer<User>> AddUser(Registration newUser);
    }
}
