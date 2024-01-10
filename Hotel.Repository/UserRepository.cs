using Hotel.Repository.Entity;
using Hotel.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly Database Database;
        public UserRepository(Database database)=> this.Database = database;

        public async Task<User?> GetUser(string personalCode, string email)
        {
            return await this.Database.Users.FirstOrDefaultAsync(user => user.PersonalCode == personalCode && user.Email == email);
        }
        public void AddNew(User user)
        {
            this.Database.Users.Add(user);
            this.Database.SaveChanges();
        }

        public async Task<List<User>> GetUsers(IEnumerable<int> userIds)
        {
            var users = await (from user in this.Database.Users
                               where userIds.Contains(user.Id)
                               select user).ToListAsync();

            return users;
        }
    }
}
