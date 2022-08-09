using Microsoft.EntityFrameworkCore;
using SchoolJournal.Data;
using SchoolJournal.Models.DB;

namespace SchoolJournal.Tests
{
    public class BaseTest
    {
        public readonly DbContextOptions<SchoolJournalDBContext> dbContextOptions;
        public BaseTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<SchoolJournalDBContext>()
                                    .UseInMemoryDatabase(databaseName: "Test")
                                    .Options;
        }

        public async Task AddRolesToDatabase(SchoolJournalDBContext ctx)
        {
            Role user = new Role { Id = 1, Name = "user" };
            Role admin = new Role { Id = 2, Name = "admin" };
            await ctx.Roles.AddRangeAsync(user, admin);
            await ctx.SaveChangesAsync();
        }

        public async Task CleanDatabase(SchoolJournalDBContext ctx)
        {
            
        }
    }
}
