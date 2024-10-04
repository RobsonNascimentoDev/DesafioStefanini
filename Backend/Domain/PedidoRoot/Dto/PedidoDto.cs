using Domain.ItensPedidoRoot.Dto;
using Domain.ItensPedidoRoot.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.PedidoRoot.Dto
{
    public class PedidoDto
    {
        public int Id { get; set; }
        public string NomeCliente { get; set; }
        public string EmailCliente { get; set; }
        public bool Pago { get; set; }
        public double ValorTotal { get; set; }
        public List<ItensPedidoDto> Itens { get; set; } = [];
    }
}
