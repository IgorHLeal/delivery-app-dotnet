using DeliveryApp.Context;
using DeliveryApp.Models;
using DeliveryApp.Repositories.Interfaces;

namespace DeliveryApp.Repositories
{
    public class CategoriaRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Categoria> Categorias => _context.Categorias;
    }
}
