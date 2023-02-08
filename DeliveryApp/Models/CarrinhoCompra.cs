using DeliveryApp.Context;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Models;
public class CarrinhoCompra
{
    private readonly AppDbContext _context;

    // Injeta ma instância do contexto no construtor
    public CarrinhoCompra(AppDbContext context)
    {
        _context = context;
    }

    public string CarrinhoCompraId { get; set; }

    public List<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }

    // Esse método é definido como estático para que possa ser injetado sem criar uma instância da classe em outros arquivos
    public static CarrinhoCompra GetCarrinho(IServiceProvider services)
    {
        // define uma sessão
        // ? = Elvis operator
        ISession session =
            services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

        // obtem um serviço do tipo do nosso contexto
        var context = services.GetService<AppDbContext>();

        // obtém ou gera o Id do carrinho
        // ?? = Operador de coalescência nula
        string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

        // atribui o id do carrinho na sessão
        session.SetString("CarrinhoId", carrinhoId);

        // retorna ao carrinho com o contexto e o Id atribuído ou obtido
        return new CarrinhoCompra(context)
        {
            CarrinhoCompraId = carrinhoId
        };
    }

    public void AdicionarAoCarrinho(Lanche lanche)
    {
        var carrinhoCompraitem =
            _context.CarrinhoCompraItens.SingleOrDefault(
                item => item.Lanche.LancheId == lanche.LancheId &&
                        item.CarrinhoCompraId == CarrinhoCompraId);

        if (carrinhoCompraitem == null)
        {
            carrinhoCompraitem = new CarrinhoCompraItem
            {
                CarrinhoCompraId = CarrinhoCompraId,
                Lanche = lanche,
                Quantidade = 1
            };
            _context.CarrinhoCompraItens.Add(carrinhoCompraitem);
        }
        else
        {
            carrinhoCompraitem.Quantidade += 1;
        }

        // persiste as informações no bd caso haja alterações no carrinho
        _context.SaveChanges();
    }

    public int RemoverDoCarrinho(Lanche lanche)
    {
        var carrinhoCompraitem =
            _context.CarrinhoCompraItens.SingleOrDefault(
                item => item.Lanche.LancheId == lanche.LancheId &&
                        item.CarrinhoCompraId == CarrinhoCompraId);

        var quantidadeLocal = 0;

        if(carrinhoCompraitem != null)
        {
            if(carrinhoCompraitem.Quantidade > 1)
            {
                carrinhoCompraitem.Quantidade -= 1;
                quantidadeLocal = carrinhoCompraitem.Quantidade;
            }
            else
            {
                _context.CarrinhoCompraItens.Remove(carrinhoCompraitem);
            }            
        }

        _context.SaveChanges();
        return quantidadeLocal;
    }

    public List<CarrinhoCompraItem> GetCarrinhoCompraItems()
    {
        return CarrinhoCompraItens ?? (CarrinhoCompraItens =
            _context.CarrinhoCompraItens
            .Where(item => item.CarrinhoCompraId == CarrinhoCompraId)
            .Include(element => element.Lanche)
            .ToList());
    }

    public void LimparCarrinho()
    {
        var carrinhoItens = _context.CarrinhoCompraItens
            .Where(carrinho => carrinho.CarrinhoCompraId == CarrinhoCompraId);

        _context.CarrinhoCompraItens.RemoveRange(carrinhoItens);
        _context.SaveChanges();
    }

    public decimal GetCarrinhoCompraTotal()
    {
        var total = _context.CarrinhoCompraItens
            .Where(carrinho => CarrinhoCompraId == CarrinhoCompraId)
            .Select(carrinho => carrinho.Lanche.Preco * carrinho.Quantidade).Sum();

        return total;
    }
}
