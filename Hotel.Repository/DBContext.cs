using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using Hotel.Repository.Entity;

namespace Hotel.Repository
{
    public class Database : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public Database()
        {

        }
        public Database(DbContextOptions options) : base(options)
        {
           
            var brk = "";


        }
        /// <summary>
        /// ONLY USED FOR database creation(Entity framework designer), not by WebApi
        /// </summary>
        /// <param name="options"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if(options.IsConfigured == false)
            {
                options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Hotel;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;");
            }
            
        }
      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedUsers(modelBuilder);
            SeedRooms(modelBuilder);
        }
        private void SeedUsers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Firstname = "John", Lastname = "Deree", Email = "admin@admin.ee", PersonalCode = "12345678910", IsAdmin=true },
                new User { Id = 2, Firstname = "Petja", Lastname = "Petrov", Email = "petja.petrov@gmail.com", PersonalCode = "10987654321",  IsAdmin = false },
                new User { Id = 3, Firstname = "Dasha", Lastname = "Semjonova", Email = "dasha.semjonova@gmail.com", PersonalCode = "48711270533", IsAdmin = false },
                new User { Id = 4, Firstname = "Kolja", Lastname = "Mashkov", Email = "kolja.mashkov@gmail.com", PersonalCode = "39012219342", IsAdmin = false }

                );
            
        }
        private void SeedRooms(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>().HasData(
                new Room { Id = 1, RoomNumber = "100", Beds = 1, Price = 60 },
                new Room { Id = 2, RoomNumber = "101", Beds = 2, Price = 80 },
                new Room { Id = 3, RoomNumber = "102", Beds = 3, Price = 100 },
                new Room { Id = 4, RoomNumber = "200", Beds = 1, Price = 80 },
                new Room { Id = 5, RoomNumber = "201", Beds = 3, Price = 120 },
                new Room { Id = 6, RoomNumber = "202", Beds = 2, Price = 90 },
                new Room { Id = 7, RoomNumber = "300", Beds = 1, Price = 75 },
                new Room { Id = 8, RoomNumber = "301", Beds = 2, Price = 110 },
                new Room { Id = 10, RoomNumber = "302", Beds = 3, Price = 160 },
                new Room { Id = 11, RoomNumber = "400", Beds = 2, Price = 90 },
                new Room { Id = 12, RoomNumber = "401", Beds = 3, Price = 140 },
                new Room { Id = 13, RoomNumber = "402", Beds = 1, Price = 100 },
                new Room { Id = 14, RoomNumber = "500", Beds = 3, Price = 140 },
                new Room { Id = 15, RoomNumber = "501", Beds = 2, Price = 130 },
                new Room { Id = 16, RoomNumber = "502", Beds = 1, Price = 150 }

            );
        }
        
    }
}
