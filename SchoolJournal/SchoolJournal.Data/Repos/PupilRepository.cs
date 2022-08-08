using Microsoft.EntityFrameworkCore;
using SchoolJournal.Models.DB;

namespace SchoolJournal.Data.Repos
{
    public class PupilRepository : IPupilRepository
    {
        private readonly SchoolJournalDBContext _context;

        public PupilRepository(SchoolJournalDBContext context)
        {
            _context = context;
        }

        public async Task Create(Pupil pupil)
        {
            pupil.Stream = _context.Streams.Where(s => s.Id == pupil.StreamId).FirstOrDefault();
            pupil.Id = Guid.NewGuid();
            await _context.Pupils.AddAsync(pupil);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid? id)
        {
            var pupil = _context.Pupils.Find(id);
            if (pupil == null) return;
            _context.Pupils.Remove(pupil);
            await _context.SaveChangesAsync();
        }

        public async Task<Pupil?> GetById(Guid? id)
        {
            return await _context.Pupils
                .Where(p => p.Id == id)
                .Include(p => p.JournalRecords.OrderByDescending(j => j.Created))
                .FirstOrDefaultAsync();
        }

        public async Task<List<Pupil>> GetAll(Guid streamId)
        {
            return await _context.Pupils
                .Where(p => p.StreamId == streamId)
                .Include(p => p.Stream).ToListAsync();
        }

        public async Task Update(Pupil pupil)
        {
            var updatePupil = _context.Pupils.Find(pupil.Id);
            if (updatePupil == null) return;
            _context.Entry(updatePupil).CurrentValues.SetValues(pupil);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Pupil>> Search(string searchCriteria)
        {
            return await _context.Pupils
                .Where(p => p.FirstName.Contains(searchCriteria) || p.LastName.Contains(searchCriteria))
                .Include(p => p.Stream).ToListAsync();
        }
    }
}
