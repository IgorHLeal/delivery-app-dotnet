using DeliveryApp.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheRepository _lacheRepository;

        public LancheController(ILancheRepository lacheRepository)
        {
            _lacheRepository = lacheRepository;
        }

        public IActionResult List()
        {
            var lanches = _lacheRepository.Lanches;
            return View(lanches);
        }
    }
}
