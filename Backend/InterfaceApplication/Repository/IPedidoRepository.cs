using Domain.PedidoRoot.Dto;
using Domain.PedidoRoot.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceApplication.Repository
{
    public interface IPedidoRepository
    {
        Task<List<PedidoDto>> GetPedidosAsync();
        Task<Pedido> GetPedidoByIdAsync(int id);
        Task CreatePedidoAsync(Pedido pedido);
        Task UpdatePedidoAsync(Pedido pedido);
        Task DeletePedidoAsync(int id);
    }
}
