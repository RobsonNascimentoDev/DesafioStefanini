using Application.Service;
using Domain.ItensPedidoRoot.Dto;
using Domain.ItensPedidoRoot.Entity;
using Domain.PedidoRoot.Dto;
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
    public class PedidoServiceTests
    {
        private readonly PedidoService _pedidoService;
        private readonly Mock<IPedidoRepository> _pedidoRepositoryMock;

        public PedidoServiceTests()
        {
            _pedidoRepositoryMock = new Mock<IPedidoRepository>();
            _pedidoService = new PedidoService(_pedidoRepositoryMock.Object);
        }

        [Fact]
        public async Task GetPedidosAsync_ReturnsListOfPedidos()
        {
            // Arrange
            var pedidos = new List<PedidoDto>
            {
                CreatePopulatedPedidoDto(),
                CreatePopulatedPedidoDto(),
                CreatePopulatedPedidoDto(),
                CreatePopulatedPedidoDto()
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
        public async Task UpdatePedidoAsync_ExistingPedido_UpdatesCorrectly()
        {
            // Arrange
            var existingPedido = new Pedido
            {
                Id = 1,
                NomeCliente = "Old Name",
                EmailCliente = "oldemail@example.com",
                DataCriacao = DateTime.Now,
                Pago = false,
                Itens =
                [
                    new() { Id = 1, Quantidade = 2, Produto = new Produto { Id = 1, NomeProduto = "Produto 1", Valor = 10 } }
                ]};

            var updatedPedido = new Pedido
            {
                Id = 1,
                NomeCliente = "New Name",
                EmailCliente = "newemail@example.com",
                DataCriacao = DateTime.Now,
                Pago = true,
                Itens =
                [
                    new() { Id = 1, Quantidade = 3, Produto = new Produto { Id = 1, NomeProduto = "Produto 1", Valor = 15 } },
                    new() { Id = 2, Quantidade = 1, Produto = new Produto { Id = 2, NomeProduto = "Produto 2", Valor = 20 } } // New item
                ]};

            _pedidoRepositoryMock.Setup(repo => repo.GetPedidoByIdAsync(existingPedido.Id)).ReturnsAsync(existingPedido);

            // Act
            await _pedidoService.UpdatePedidoAsync(updatedPedido);

            // Assert
            _pedidoRepositoryMock.Verify(repo => repo.UpdatePedidoAsync(It.Is<Pedido>(p =>
                p.Id == existingPedido.Id &&
                p.NomeCliente == "New Name" &&
                p.EmailCliente == "newemail@example.com" &&
                p.Pago == true &&
                p.Itens.Count == 2 &&
                p.Itens.First(i => i.Id == 1).Quantidade == 3 &&
                p.Itens.First(i => i.Id == 2).Quantidade == 1
            )), Times.Once);
        }

        public async Task UpdatePedidoAsync_NonExistingPedido_ThrowsException()
        {
            // Arrange
            var updatedPedido = new Pedido { Id = 1 };

            _pedidoRepositoryMock.Setup(repo => repo.GetPedidoByIdAsync(updatedPedido.Id)).ReturnsAsync((Pedido)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _pedidoService.UpdatePedidoAsync(updatedPedido));
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


        private PedidoDto CreatePopulatedPedidoDto()
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

            var itensPedido1 = new ItensPedidoDto
            {
                Id = random.Next(1, 1000),
                IdProduto = produto1.Id,
                NomeProduo = produto1.NomeProduto,
                ValorUnitario = produto1.Valor,
                Quantidade = random.Next(1, 10)
            };

            var itensPedido2 = new ItensPedidoDto
            {
                Id = random.Next(1, 1000),
                IdProduto = produto2.Id,
                NomeProduo = produto2.NomeProduto,
                ValorUnitario = produto2.Valor,
                Quantidade = random.Next(1, 10)
            };

            var pedidoDto = new PedidoDto
            {
                Id = random.Next(1, 1000),
                NomeCliente = $"Cliente {random.Next(1, 1000)}",
                EmailCliente = $"cliente{random.Next(1, 1000)}@example.com",
                Pago = random.Next(0, 2) == 1,
                ValorTotal = (itensPedido1.Quantidade * itensPedido1.ValorUnitario) + (itensPedido2.Quantidade * itensPedido2.ValorUnitario),
                Itens = new List<ItensPedidoDto> { itensPedido1, itensPedido2 }
            };

            return pedidoDto;
        }
    }
}
