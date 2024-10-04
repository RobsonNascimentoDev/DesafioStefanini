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

            modelBuilder.Entity<ItensPedido>(entity =>
            {
                entity.HasKey(ip => ip.Id);

                entity.Property(ip => ip.Id)
                      .ValueGeneratedOnAdd();

                // Relationships
                entity.HasOne(ip => ip.Produto)
                      .WithMany()
                      .HasForeignKey(ip => ip.IdProduto);

                entity.HasOne<Pedido>()
                      .WithMany(p => p.Itens)
                      .HasForeignKey(ip => ip.IdPedido);
            });

            modelBuilder.Entity<Pedido>()
                        .HasMany(p => p.Itens)
                        .WithOne()
                        .HasForeignKey(ip => ip.IdPedido);
        }

    }
}
