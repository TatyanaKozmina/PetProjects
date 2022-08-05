using Microsoft.EntityFrameworkCore;

namespace SchoolJournal.Data.Repos
{
    public class StreamRepository : IStreamRepository
    {
        private readonly SchoolJournalDBContext _context;

        public StreamRepository(SchoolJournalDBContext context)
        {
            _context = context;
        }
        public async Task Create(Models.DB.Stream stream)
        {
            var entry = _context.Entry(stream);
            if (entry.State == EntityState.Detached)
            {
                stream.Id = Guid.NewGuid();
                await _context.Streams.AddAsync(stream);
            }
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var stream = _context.Streams.Find(id);
            if (stream == null) return;
            _context.Streams.Remove(stream);
            await _context.SaveChangesAsync();
        }

        public async Task<Models.DB.Stream> GetById(Guid? id)
        {
            return await _context.Streams.Where(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Models.DB.Stream>> GetAll()
        {
            return await _context.Streams.OrderByDescending(s => s.Started).ToListAsync();
        }

        public async Task Update(Models.DB.Stream stream)
        {
            var updateStream = _context.Streams.Find(stream.Id);
            if (updateStream == null) return;
            _context.Entry(updateStream).CurrentValues.SetValues(stream);
            await _context.SaveChangesAsync();            
        }
    }
}
