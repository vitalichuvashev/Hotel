using Hotel.Repository.Entity;

namespace Hotel.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUser(string personalCode, string email);

        void AddNew(User user);

        Task<List<User>> GetUsers(IEnumerable<int> userIds);
    }
}
