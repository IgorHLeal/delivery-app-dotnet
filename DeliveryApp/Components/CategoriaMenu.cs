using DeliveryApp.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.Components
{
    public class CategoriaMenu : ViewComponent
    {
        private readonly ICategoryRepository _categoriaRepository;

        public CategoriaMenu(ICategoryRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public IViewComponentResult Invoke()
        {
            var categorias = _categoriaRepository.Categorias.OrderBy(categoria => categoria.CategoriaNome);
            return View(categorias);
        }
    }
}
