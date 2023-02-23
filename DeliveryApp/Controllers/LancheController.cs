using DeliveryApp.Repositories.Interfaces;
using DeliveryApp.ViewModels;
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
            var lancheListViewModel = new LancheListViewModel();
            lancheListViewModel.Lanches = _lacheRepository.Lanches;
            lancheListViewModel.CategoriaAtual = "Categoria Atual";

            return View(lancheListViewModel);
            //ViewData["Titulo"] = "Todos os Lanches";
            ////ViewData["Data"] = DateTime.Now;

            //var lanches = _lacheRepository.Lanches;
            ////var totalLanches = lanches.Count();

            ////ViewBag.Total = "Total de lanches: ";
            ////ViewBag.TotalLanches = totalLanches;
            //return View(lanches);
        }
    }
}
