using SchoolJournal.Models.DB;

namespace SchoolJournal.Data.Repos
{
    public interface IJournalRecordRepository
    {
        Task Create(Guid pupilId, JournalRecord jornalRecord);
    }
}
