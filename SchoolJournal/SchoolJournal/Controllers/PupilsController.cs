using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolJournal.Data.Repos;
using SchoolJournal.Models.DB;

namespace SchoolJournal.Controllers
{
    public class PupilsController : Controller
    {
        private IPupilRepository _pupilRepository;
        private IStreamRepository _streamRepository;
        public PupilsController(IPupilRepository pupilRepository, IStreamRepository streamRepository)
        {
            _pupilRepository = pupilRepository;
            _streamRepository = streamRepository;
        }

        // GET: Pupils        
        public async Task<IActionResult> Index(Guid streamId)
        {
            ViewData["StreamId"] = streamId;
            return View(await _pupilRepository.GetAll(streamId));
        }

        // GET: Pupils/Create
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create(Guid? id)
        {
            ViewBag.Streams = new SelectList(await _streamRepository.GetAll(), "Id", "Started", id);
            return View();
        }

        // POST: Pupils/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,StreamId")] Pupil pupil)
        {
            await _pupilRepository.Create(pupil);
            return RedirectToAction("Index", "Pupils", new { streamId = pupil.StreamId });
        }

        // GET: Pupils/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var pupil = await _pupilRepository.GetById(id);
            if (pupil == null) return NotFound();
            ViewBag.Streams = new SelectList(await _streamRepository.GetAll(), "Id", "Started", pupil.StreamId);
            return View(pupil);
        }

        // POST: Pupils/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,FirstName,LastName,Email,StreamId")] Pupil pupil)
        {
            try
            {
                await _pupilRepository.Update(pupil);
                return RedirectToAction("Index", "Pupils", new { streamId = pupil.StreamId });
            }
            catch
            {
                return View(pupil);
            }
        }

        // GET: Pupils/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var pupil = await _pupilRepository.GetById(id);
            if (pupil == null) return NotFound();
            return View(pupil);
        }

        // POST: Pupils/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id, Guid StreamId)
        {
            await _pupilRepository.Delete(id);
            return RedirectToAction("Index", "Pupils", new { streamId = StreamId });
        }

        //GET: Pupils/Details/5
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            var pupil = await _pupilRepository.GetById(id);
            if (pupil == null) return NotFound();
            return View(pupil);
        }

        // GET: Pupils/Create
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddMark(Guid? pupilId)
        {
            return RedirectToAction("Create", "JournalRecords", new { id = pupilId });
        }
    }
}
