using Hotel.Repository;
using Hotel.Repository.Interfaces;
using Entity = Hotel.Repository.Entity;
using DTO = Hotel.WebApi.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository roomRepository;
        public RoomController(IRoomRepository roomRepository) => this.roomRepository = roomRepository;
       

        [HttpGet()]
        public async Task<DTO.DataTransfer<IEnumerable<Entity.Room>>> Get(string startBooking, string endBooking)
        {
            if (DateTime.TryParse(startBooking, out var startDate))
            {
                if (DateTime.TryParse(endBooking, out var endDate))
                {
                    var rooms = await this.roomRepository.GetFreeRoms(startDate, endDate);

                    var data = new DTO.DataTransfer<IEnumerable<Entity.Room>>(rooms);
                    return data;
                }
            }

            return DTO.DataTransfer<IEnumerable<Entity.Room>>.Empty();
        }

        
        [HttpGet("{userID}")]
        public async Task<DTO.DataTransfer<IEnumerable<Entity.Room>>> Get(int userID)
        {
            var rooms = await this.roomRepository.GetRooms(userID);

            var data = new DTO.DataTransfer<IEnumerable<Entity.Room>>(rooms);
            return data;
        }


    }
}
