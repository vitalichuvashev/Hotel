using Hotell.Services.Interfaces;
using Hotell.Models;
using Hotell.Services.DTO;

namespace Hotell.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient httpClient;
        public AccountService(HttpClient httpClient)=>this.httpClient = httpClient;

        public async Task<DataTransfer<User>> GetUser(User login)
        {
            var user = await this.httpClient.GetFromJsonAsync<DataTransfer<User>>($"api/account/{login.PersonalCode}/{login.Email}");
            return user ?? DataTransfer<User>.Empty();
        }
        public async Task<DataTransfer<User>> AddUser(Registration newUser)
        {
            var dataTransfer = new DataTransfer<Registration>(newUser);
            var response = await this.httpClient.PostAsJsonAsync<DataTransfer<Registration>>("api/account", dataTransfer);

            var data = await response.Content.ReadFromJsonAsync<DataTransfer<User>>(); 
            return data ?? DataTransfer<User>.Empty(); 
        }
    }
}
