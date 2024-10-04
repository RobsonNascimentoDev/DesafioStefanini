using Application.Service;
using Domain.ItensPedidoRoot.Entity;
using Domain.PedidoRoot.Entity;
using Domain.ProdutoRoot.Entity;
using Infra;
using Infra.Repository;
using InterfaceApplication.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using Xunit;

namespace Test
{
    public class PedidoRepositoryTests
    {
        private readonly PedidoService _pedidoService;
        private readonly Mock<IPedidoRepository> _pedidoRepositoryMock;

        public PedidoRepositoryTests()
        {
            _pedidoRepositoryMock = new Mock<IPedidoRepository>();
            _pedidoService = new PedidoService(_pedidoRepositoryMock.Object);
        }

        [Fact]
        public async Task GetPedidosAsync_ReturnsListOfPedidos()
        {
            // Arrange
            var pedidos = new List<Pedido>
            {
                CreatePopulatedPedido(),
                CreatePopulatedPedido()
            };

            _pedidoRepositoryMock.Setup(repo => repo.GetPedidosAsync()).ReturnsAsync(pedidos);

            // Act
            var result = await _pedidoService.GetPedidosAsync();

            // Assert
            Assert.Equal(pedidos.Count, result.Count);
        }

        [Fact]
        public async Task GetPedidoByIdAsync_ReturnsPedido()
        {
            // Arrange
            var pedido = new Pedido { Id = 1 };
            _pedidoRepositoryMock.Setup(repo => repo.GetPedidoByIdAsync(pedido.Id)).ReturnsAsync(pedido);

            // Act
            var result = await _pedidoService.GetPedidoByIdAsync(pedido.Id);

            // Assert
            Assert.Equal(pedido, result);
        }

        [Fact]
        public async Task CreatePedidoAsync_CreatesPedidoAndReturnsIt()
        {
            // Arrange
            var pedido = new Pedido { Id = 1 };

            // Act
            await _pedidoService.CreatePedidoAsync(pedido);

            // Assert
            _pedidoRepositoryMock.Verify(repo => repo.CreatePedidoAsync(pedido), Times.Once);
        }

        [Fact]
        public async Task UpdatePedidoAsync_UpdatesPedido()
        {
            // Arrange
            var pedido = new Pedido { Id = 1 };

            // Act
            await _pedidoService.UpdatePedidoAsync(pedido);

            // Assert
            _pedidoRepositoryMock.Verify(repo => repo.UpdatePedidoAsync(pedido), Times.Once);
        }

        [Fact]
        public async Task DeletePedidoAsync_DeletesPedido()
        {
            // Arrange
            var pedidoId = 1;

            // Act
            await _pedidoService.DeletePedidoAsync(pedidoId);

            // Assert
            _pedidoRepositoryMock.Verify(repo => repo.DeletePedidoAsync(pedidoId), Times.Once);
        }


        private Pedido CreatePopulatedPedido()
        {
            Random random = new Random();

            var produto1 = new Produto
            {
                Id = random.Next(1, 1000),
                NomeProduto = $"Produto {random.Next(1, 100)}",
                Valor = random.NextDouble() * 500 + 1
            };

            var produto2 = new Produto
            {
                Id = random.Next(1, 1000),
                NomeProduto = $"Produto {random.Next(1, 100)}",
                Valor = random.NextDouble() * 500 + 1
            };

            var itensPedido1 = new ItensPedido
            {
                Id = random.Next(1, 1000),
                IdPedido = 1,
                IdProduto = produto1.Id,
                Quantidade = random.Next(1, 10),
                Produto = produto1
            };

            var itensPedido2 = new ItensPedido
            {
                Id = random.Next(1, 1000),
                IdPedido = 1,
                IdProduto = produto2.Id,
                Quantidade = random.Next(1, 10),
                Produto = produto2
            };

            var pedido = new Pedido
            {
                Id = random.Next(1, 1000),
                NomeCliente = $"Cliente {random.Next(1, 1000)}",
                EmailCliente = $"cliente{random.Next(1, 1000)}@example.com",
                DataCriacao = DateTime.Now.AddDays(-random.Next(0, 30)),
                Pago = random.Next(0, 2) == 1,
                Itens = new List<ItensPedido> { itensPedido1, itensPedido2 }
            };

            itensPedido1.Pedido = pedido;
            itensPedido2.Pedido = pedido;

            return pedido;
        }
    }
}
