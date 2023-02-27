using DeliveryApp.Models;
using DeliveryApp.Repositories.Interfaces;
using DeliveryApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.Controllers;
public class CarrinhoCompraController : Controller
{
    private readonly ILancheRepository _lancheRepository;
    private readonly CarrinhoCompra _carrinhoCompra;

    public CarrinhoCompraController(ILancheRepository lancheRepository, CarrinhoCompra carrinhoCompra)
    {
        _lancheRepository = lancheRepository;
        _carrinhoCompra = carrinhoCompra;
    }

    public IActionResult Index()
    {
        // GetCarrinhoCompraItes retorna os itens de um carrinho de compras existente
        var itens = _carrinhoCompra.GetCarrinhoCompraItems();

        // CarrinhoCompraItens retorna uma lista de itens e estamos atribuindo os itens obtidos a ess método
        _carrinhoCompra.CarrinhoCompraItens = itens;

        var carrinhoCompraVM = new CarrinhoCompraViewModel
        {
            // Atribuir à propriedade da vieModel a instância do carrinho atual e do valor total
            CarrinhoCompra = _carrinhoCompra,
            CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal()
        };

        return View(carrinhoCompraVM);
    }

    // ReditectToActionResult herda de IActionResult, tanto um quanto o outro terrão o mesmo comportamento
    public RedirectToActionResult AdicionarItemNoCarrinhoCompra(int lancheId)
    {
        var lancheSelecionado = _lancheRepository.Lanches.FirstOrDefault(item => item.LancheId == lancheId);

        if(lancheSelecionado != null)
        {
            _carrinhoCompra.AdicionarAoCarrinho(lancheSelecionado);
        }

        return RedirectToAction("Index");
    }

    public RedirectToActionResult RemoverItemDoCarrinhoCompra(int lancheId)
    {
        var lancheSelecionado = _lancheRepository.Lanches.FirstOrDefault(item => item.LancheId == lancheId);

        if (lancheSelecionado != null)
        {
            _carrinhoCompra.RemoverDoCarrinho(lancheSelecionado);
        }

        return RedirectToAction("Index");
    }
}