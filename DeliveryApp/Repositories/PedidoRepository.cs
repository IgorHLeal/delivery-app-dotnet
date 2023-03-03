using AspNetCore;
using DeliveryApp.Context;
using DeliveryApp.Models;
using DeliveryApp.Repositories.Interfaces;

namespace DeliveryApp.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoRepository(AppDbContext appDbContext, CarrinhoCompra carrinhoCompra)
        {
            _appDbContext = appDbContext;
            _carrinhoCompra = carrinhoCompra;
        }

        public void CriarPedido(Pedido pedido)
        {
            pedido.PedidoEnviado = DateTime.Now;
            _appDbContext.Pedidos.Add(pedido);

            // Esse SaveChanges é necessário para persistir no BD e recuperar o id do pedido na linha 35
            _appDbContext.SaveChanges();

            var itensCarrinhoCompra = _carrinhoCompra.CarrinhoCompraItens;

            foreach (var carrinhoItem in itensCarrinhoCompra)
            {
                var pedidoDetail = new PedidoDetalhe()
                {
                    Quantidade = carrinhoItem.Quantidade,
                    LancheId = carrinhoItem.Lanche.LancheId,
                    PedidoId = pedido.PedidoId,
                    Preco = carrinhoItem.Lanche.Preco
                };
                _appDbContext.PedidoDetalhes.Add(pedidoDetail);
            }
            // Esse SaveChanges persiste no BD os detalhes
            _appDbContext.SaveChanges();
        }
    }
}
