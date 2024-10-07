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
            try
            {
                var existingPedido = await _pedidoRepository.GetPedidoByIdAsync(pedido.Id);

                existingPedido.NomeCliente = pedido.NomeCliente;
                existingPedido.EmailCliente = pedido.EmailCliente;
                existingPedido.DataCriacao = pedido.DataCriacao;
                existingPedido.Pago = pedido.Pago;

                foreach (var item in pedido.Itens)
                {
                    var existingItem = existingPedido.Itens.FirstOrDefault(i => i.Id == item.Id);

                    if (existingItem is null)
                    {
                        existingPedido.Itens.Add(item);
                        continue;
                    }

                    existingItem.Quantidade = item.Quantidade;
                    existingItem.Produto.Valor = item.Produto.Valor;
                    existingItem.Produto.NomeProduto = item.Produto.NomeProduto;
                }

                await _pedidoRepository.UpdatePedidoAsync(existingPedido);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeletePedidoAsync(int id)
        {
            await _pedidoRepository.DeletePedidoAsync(id);
        }
    }
}
