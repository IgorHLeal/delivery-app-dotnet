﻿using DeliveryApp.Context;

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
        }
    }
}
