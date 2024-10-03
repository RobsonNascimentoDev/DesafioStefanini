using Domain.ItensPedidoRoot.Entity;
using Domain.PedidoRoot.Entity;
using Domain.ProdutoRoot.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Infra
{
    public class AppDbContext : DbContext
    {
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<ItensPedido> ItensPedidos { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItensPedido>()
                .HasKey(ip => new { ip.IdPedido, ip.IdProduto });

            // Relationships
            modelBuilder.Entity<ItensPedido>()
                .HasOne(ip => ip.Pedido)
                .WithMany(p => p.Itens)
                .HasForeignKey(ip => ip.IdPedido);

            modelBuilder.Entity<ItensPedido>()
                .HasOne(ip => ip.Produto)
                .WithMany()
                .HasForeignKey(ip => ip.IdProduto);
        }

    }
}
