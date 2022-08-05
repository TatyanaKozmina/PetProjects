using Microsoft.AspNetCore.Mvc;
using SchoolJournal.Data.Repos;
using SchoolJournal.Models.DB;

namespace SchoolJournal.Controllers
{
    public class JournalRecordsController : Controller
    {
        private IJournalRecordRepository _journalRecordRepository;

        public JournalRecordsController(IJournalRecordRepository journalRecordRepository)
        {
            _journalRecordRepository = journalRecordRepository;
        }   

        // GET: JournalRecords/Create
        public async Task<IActionResult> Create(Guid id)
        {
            ViewData["PupilId"] = id;
            return View();
        }

        // POST: JournalRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid pupilId, [Bind("Id,Created,Subject,Mark,Comment,PupilId")] JournalRecord journalRecord)
        {
            await _journalRecordRepository.Create(pupilId, journalRecord);
            return RedirectToAction("Details", "Pupils", new { id = pupilId });
        }        
    }
}
