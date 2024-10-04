using Domain.PedidoRoot.Entity;

namespace InterfaceApplication.Service
{
    public interface IPedidoService
    {
        Task<List<Pedido>> GetPedidosAsync();
        Task<Pedido> GetPedidoByIdAsync(int id);
        Task<Pedido> CreatePedidoAsync(Pedido pedido);
        Task UpdatePedidoAsync(Pedido pedido);
        Task DeletePedidoAsync(int id);
    }
}
