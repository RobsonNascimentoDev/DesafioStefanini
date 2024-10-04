using Domain.ProdutoRoot.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ItensPedidoRoot.Dto
{
    public class ItensPedidoDto
    {
        public int Id { get; set; }
        public int IdProduto { get; set; }
        public string NomeProduo { get; set; }
        public double ValorUnitario { get; set; }
        public int Quantidade { get; set; }
    }
}
