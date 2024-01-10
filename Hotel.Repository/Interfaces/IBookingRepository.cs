using Hotel.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Repository.Interfaces
{
    public  interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetAll();

        Task<IEnumerable<Booking>> GetByUserID(int userID);

        void AddNew(Booking booking);

        Booking? Delete(int id);
    }
}
