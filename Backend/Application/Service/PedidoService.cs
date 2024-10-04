using Domain.PedidoRoot.Dto;
using Domain.PedidoRoot.Entity;
using InterfaceApplication.Repository;
using InterfaceApplication.Service;

namespace Application.Service
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoService(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<List<PedidoDto>> GetPedidosAsync()
        {
            return await _pedidoRepository.GetPedidosAsync();
        }

        public async Task<Pedido> GetPedidoByIdAsync(int id)
        {
            return await _pedidoRepository.GetPedidoByIdAsync(id);
        }

        public async Task<Pedido> CreatePedidoAsync(Pedido pedido)
        {
            await _pedidoRepository.CreatePedidoAsync(pedido);
            return pedido;
        }

        public async Task UpdatePedidoAsync(Pedido pedido)
        {
            await _pedidoRepository.UpdatePedidoAsync(pedido);
        }

        public async Task DeletePedidoAsync(int id)
        {
            await _pedidoRepository.DeletePedidoAsync(id);
        }
    }
}
