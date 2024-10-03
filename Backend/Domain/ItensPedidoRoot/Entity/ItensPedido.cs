using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.PedidoRoot.Entity;
using Domain.ProdutoRoot.Entity;

namespace Domain.ItensPedidoRoot.Entity
{
    public class ItensPedido
    {
        public int Id { get; set; }
        public int IdPedido { get; set; }
        public Pedido Pedido { get; set; }
        public int IdProduto { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
    }
}
