using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class FinalizeItensPedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the existing ItensPedidos table if it exists
            migrationBuilder.DropTable(name: "ItensPedidos");

            // Recreate ItensPedidos table with Id as Identity
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

            // Create an index on IdPedido if needed
            migrationBuilder.CreateIndex(
                name: "IX_ItensPedidos_IdPedido",
                table: "ItensPedidos",
                column: "IdPedido");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "ItensPedidos");
        }
    }
}
