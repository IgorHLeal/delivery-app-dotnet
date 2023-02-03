using DeliveryApp.Models;

namespace DeliveryApp.Repositories.Interfaces;
public interface ICategoryRepository
{
    IEnumerable<Categoria> Categorias { get; }
}
