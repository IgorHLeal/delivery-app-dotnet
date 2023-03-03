using DeliveryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Context
{
    // A classe AppDbContext carrega informações de configurações do DbContext
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        // Os DbSets definem quais classes serão mapeadas para quais tabelas
        // As propriedades definem os nomes das tabelas que serão criadas pelo EntityFramework
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Lanche> Lanches { get; set; }
        public DbSet<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoDetalhe> PedidoDetalhes { get; set; }
    }
}
