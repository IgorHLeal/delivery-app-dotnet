using DeliveryApp.Models;

namespace DeliveryApp.Repositories.Interfaces
{
    public interface IPedidoRepository
    {
        void CriarPedido(Pedido pedido);
    }
}
