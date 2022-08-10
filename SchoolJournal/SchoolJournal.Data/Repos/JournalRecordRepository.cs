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
            var pupil = await _context.Pupils.Where(p => p.Id == pupilId).FirstOrDefaultAsync();
            if (pupil == null)
                throw new Exception("Journal record without assigned pupil could not be created");                
            journalRecord.PupilId = pupilId;
            journalRecord.Pupil = pupil;
            journalRecord.Id = Guid.NewGuid();
            await _context.JournalRecords.AddAsync(journalRecord);
            await _context.SaveChangesAsync();
        }        
    }
}
