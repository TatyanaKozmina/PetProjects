using Microsoft.EntityFrameworkCore;
using SchoolJournal.Models.DB;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace SchoolJournal.Data
{
    public sealed class SchoolJournalDBContext : DbContext
    {
        public DbSet<Models.DB.Stream> Streams { get; set; }
        public DbSet<Pupil> Pupils { get; set; }
        public DbSet<JournalRecord> JournalRecords { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public SchoolJournalDBContext(DbContextOptions<SchoolJournalDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
