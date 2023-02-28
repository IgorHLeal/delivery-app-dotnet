using DeliveryApp.Models;
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

        public IActionResult List(string categoria)
        {
            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;

            if(string.IsNullOrEmpty(categoria))
            {
                lanches = _lacheRepository.Lanches.OrderBy(lanche => lanche.LancheId);
                categoriaAtual = "Todos os lanches";
            }
            else
            {
                if(string.Equals("Normal", categoria, StringComparison.OrdinalIgnoreCase))
                {
                    lanches = _lacheRepository.Lanches
                        .Where(lanche => lanche.Categoria.CategoriaNome.Equals("Normal"))
                        .OrderBy(lanche => lanche.Nome);
                }
                else
                {
                    lanches = _lacheRepository.Lanches
                        .Where(lanche => lanche.Categoria.CategoriaNome.Equals("Natural"))
                        .OrderBy(lanche => lanche.Nome);
                }
                
                categoriaAtual = categoria;
            }

            var lancheListViewModel = new LancheListViewModel
            {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual,
            };

            return View(lancheListViewModel);

            //var lancheListViewModel = new LancheListViewModel();
            //lancheListViewModel.Lanches = _lacheRepository.Lanches;
            //lancheListViewModel.CategoriaAtual = "Categoria Atual";

            //return View(lancheListViewModel);


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
