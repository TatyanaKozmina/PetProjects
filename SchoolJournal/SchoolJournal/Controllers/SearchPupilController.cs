using Microsoft.AspNetCore.Mvc;
using SchoolJournal.Data.Repos;

namespace SchoolJournal.Controllers
{
    public class SearchPupilController : Controller
    {
        private IPupilRepository _pupilRepository;

        public SearchPupilController(IPupilRepository pupilRepository)
        {
            _pupilRepository = pupilRepository;
        }

        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search(Models.SearchPupil model)
        {
            var pupils = await _pupilRepository.Search(model.SearchText);
            model.Pupils = pupils;
            return View(model);
        }
    }
}
