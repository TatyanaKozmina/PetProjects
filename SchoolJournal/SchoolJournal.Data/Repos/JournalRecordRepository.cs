using Microsoft.EntityFrameworkCore;
using SchoolJournal.Models.DB;

namespace SchoolJournal.Data.Repos
{
    public class JournalRecordRepository : IJournalRecordRepository
    {
        private readonly SchoolJournalDBContext _context;

        public JournalRecordRepository(SchoolJournalDBContext context)
        {
            _context = context;
        }

        public async Task Create(Guid pupilId, JournalRecord journalRecord)
        {
            journalRecord.PupilId = pupilId;
            journalRecord.Pupil = await _context.Pupils.Where(p => p.Id == pupilId).FirstOrDefaultAsync();
            journalRecord.Id = Guid.NewGuid();
            await _context.JournalRecords.AddAsync(journalRecord);
            await _context.SaveChangesAsync();
        }        
    }
}
