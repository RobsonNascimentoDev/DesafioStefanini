using Domain.ItensPedidoRoot.Dto;
using Domain.PedidoRoot.Dto;
using Domain.PedidoRoot.Entity;
using InterfaceApplication.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repository
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _context;

        public PedidoRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<PedidoDto>> GetPedidosAsync()
        {
            var query = await _context.Pedidos.Include(p => p.Itens)
                                                .ThenInclude(i => i.Produto)
                                                .ToListAsync();


            return query.Select(p => new PedidoDto
            {
                Id = p.Id,
                NomeCliente = p.NomeCliente,
                EmailCliente = p.EmailCliente,
                Pago = p.Pago,
                ValorTotal = p.Itens.Sum(i => i.Quantidade * i.Produto.Valor),
                Itens = p.Itens.Select(i => new ItensPedidoDto
                {
                    Id = i.Id,
                    IdProduto = i.IdProduto,
                    NomeProduo = i.Produto.NomeProduto,
                    ValorUnitario = i.Produto.Valor,
                    Quantidade = i.Quantidade
                }).ToList()
            }).ToList();
        }

        public async Task<Pedido> GetPedidoByIdAsync(int id)
        {
            return await _context.Pedidos.Include(p => p.Itens)
                                            .ThenInclude(i => i.Produto)
                                            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task CreatePedidoAsync(Pedido pedido)
        {
            foreach (var item in pedido.Itens)
            {
                var productExists = await _context.Produtos.FirstOrDefaultAsync(p => p.NomeProduto.Equals(item.Produto.NomeProduto) &&
                                                                                     p.Valor == item.Produto.Valor);

                if (productExists is null)
                {
                    _context.Produtos.Add(item.Produto);
                    await _context.SaveChangesAsync();
                }   
                else
                    item.Produto = productExists;
            }

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePedidoAsync(Pedido pedido)
        {
            _context.Pedidos.Update(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePedidoAsync(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido != null)
            {
                _context.Pedidos.Remove(pedido);
                await _context.SaveChangesAsync();
            }
        }
    }
}
