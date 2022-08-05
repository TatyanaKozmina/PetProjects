using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolJournal.Data.Repos;

namespace SchoolJournal.Controllers
{
    public class StreamsController : Controller
    {
        private IStreamRepository _streamRepository;

        public StreamsController(IStreamRepository streamRepository)
        {
            _streamRepository = streamRepository;
        }

        // GET: Streams
        public async Task<IActionResult> Index()
        {
            return View(await _streamRepository.GetAll());              
        }

        // GET: Streams/Create Метод для отображения формы создания нового потока
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Streams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Started,CurrentClass,IsCompleted")] Models.DB.Stream stream)        
        {
            if (ModelState.IsValid)
            {
                await _streamRepository.Create(stream);
                return RedirectToAction(nameof(Index));
            }
            return View(stream);
        }

        // GET: Streams/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var stream = await _streamRepository.GetById(id);
            if (stream == null) return NotFound();
            return View(stream);            
        }

        // POST: Streams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Started,CurrentClass,IsCompleted")] Models.DB.Stream stream)
        {
            if (ModelState.IsValid)
            {
                await _streamRepository.Update(stream);
                return RedirectToAction(nameof(Index));
            }
            return View(stream);
        }

        // GET: Streams/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var stream = await _streamRepository.GetById(id);
            if (stream == null) return NotFound();
            return View(stream);
        }

        // POST: Streams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _streamRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        
        // GET: Streams/Details/5 Показываем список учеников в потоке
        public async Task<IActionResult> Details(Guid? id)
        {
            return RedirectToAction("Index", "Pupils", new { streamId = id });            
        }
    }
}
