using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryApp.Models;

// Esse mapeamento não é necessário pois na classe de contexto já definimos o nome da tabela a ser criada 
[Table("CarrinhoCompraItens")]  
public class CarrinhoCompraItem
{
    public int CarrinhoCompraItemId { get; set; }

    public Lanche Lanche { get; set; }

    public int Quantidade { get; set; }

    [StringLength(200)]
    public string CarrinhoCompraId { get; set; }
}
