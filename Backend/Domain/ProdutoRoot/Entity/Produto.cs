using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ProdutoRoot.Entity
{
    public class Produto
    {
        public int Id { get; set; }
        public string NomeProduto { get; set; }
        public double Valor { get; set; }
    }
}
