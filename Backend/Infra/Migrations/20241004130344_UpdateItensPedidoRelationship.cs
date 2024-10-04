using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class UpdateItensPedidoRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the foreign key and index if they exist
            migrationBuilder.DropForeignKey(
                name: "FK_ItensPedidos_Produtos_IdProduto",
                table: "ItensPedidos");

            migrationBuilder.DropIndex(
                name: "IX_ItensPedidos_IdProduto",
                table: "ItensPedidos");

            // Drop the existing ItensPedido table
            migrationBuilder.DropTable(
                name: "ItensPedidos");

            // Recreate ItensPedido table with Id as Identity
            migrationBuilder.CreateTable(
                name: "ItensPedidos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"), // Set Id to auto-increment
                    IdPedido = table.Column<int>(nullable: false),
                    IdProduto = table.Column<int>(nullable: false),
                    Quantidade = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensPedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItensPedidos_Produtos_IdProduto",
                        column: x => x.IdProduto,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItensPedidos_IdProduto",
                table: "ItensPedidos",
                column: "IdProduto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop the ItensPedidos table
            migrationBuilder.DropTable(
                name: "ItensPedidos");

            // Recreate the original ItensPedidos structure
            migrationBuilder.CreateTable(
                name: "ItensPedidos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false), // Keep this as it was
                    IdPedido = table.Column<int>(nullable: false),
                    IdProduto = table.Column<int>(nullable: false),
                    Quantidade = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensPedidos", x => new { x.IdPedido, x.IdProduto }); // Original primary key
                });
        }
    }
}
