using SchoolJournal.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace SchoolJournal.Data.Repos
{
    public class UserRepository : IUserRepository
    {
        private readonly SchoolJournalDBContext _context;
        public UserRepository(SchoolJournalDBContext context)
        {
            _context = context;
        }

        public async Task Create(User user)
        {
            user.Role = _context.Roles.Where(r => r.Name == "user").FirstOrDefault();
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByEmailPassword(string email, string password)
        {
            return await _context.Users
                .Where(u => u.Email == email && u.Password == password)
                .Include(u => u.Role).FirstOrDefaultAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
        }
    }
}
