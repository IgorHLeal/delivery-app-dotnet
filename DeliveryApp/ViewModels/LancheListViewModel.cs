using DeliveryApp.Models;

namespace DeliveryApp.ViewModels;
public class LancheListViewModel
{
    public IEnumerable<Lanche> Lanches { get; set; }

    public string CategoiriaAtual { get; set; }

}
