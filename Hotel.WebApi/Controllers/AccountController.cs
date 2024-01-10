using Hotel.Repository;
using Hotel.Repository.Interfaces;
using Entity = Hotel.Repository.Entity;
using DTO = Hotel.WebApi.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.WebApi.Controllers
{ 
    

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        public AccountController(IUserRepository userRepository) => this.userRepository = userRepository;
        
        [HttpGet("{personalCode}/{email}")]
        public async Task<DTO.DataTransfer<Entity.User>> Get(string personalCode, string email)
        {
            var user = await this.userRepository.GetUser(personalCode, email);
            if (user != null)
            {
                var dataTransfer = new DTO.DataTransfer<Entity.User>(user);
                return dataTransfer;
            }
            else
            {
                var dataTransfer = new DTO.DataTransfer<Entity.User>(default!);
                dataTransfer.ErrorMessage = $"Wrong email or personal code";
                return dataTransfer;
            }
            
        }

        [HttpPost]
        public async Task<DTO.DataTransfer<Entity.User>> Post(DTO.DataTransfer<Entity.User> newUserData)
        {
            var user = await this.userRepository.GetUser(newUserData.Data.PersonalCode, newUserData.Data.Email);
            if(user == null)
            {
                this.userRepository.AddNew(newUserData.Data);
                var dataTransfer = new DTO.DataTransfer<Entity.User>(newUserData.Data);
                return dataTransfer;
            }
            else
            {
                var dataTransfer = new DTO.DataTransfer<Entity.User>(default!);
                dataTransfer.ErrorMessage = $"User with personal code or email already exist";
                return dataTransfer;
            }
        }
    }
}
