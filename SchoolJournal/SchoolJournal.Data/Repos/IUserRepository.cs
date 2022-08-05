using SchoolJournal.Models.DB;

namespace SchoolJournal.Data.Repos
{
    public interface IUserRepository
    {
        Task<User> GetByEmailPassword(string email, string password);
        Task<User> GetByEmail(string email);
        Task Create(User user);
    }
}
