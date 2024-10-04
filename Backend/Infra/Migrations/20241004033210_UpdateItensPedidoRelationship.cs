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
            migrationBuilder.DropPrimaryKey(
          name: "PK_ItensPedidos",
          table: "ItensPedidos");

            // Drop the existing Id column if necessary
            migrationBuilder.DropColumn(
                name: "Id",
                table: "ItensPedidos");

            // Add the Id column back with identity
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ItensPedidos",
                type: "int",
                nullable: false);

            // Set the identity property
            migrationBuilder.Sql("ALTER TABLE ItensPedidos ADD CONSTRAINT [PK_ItensPedidos] PRIMARY KEY (Id)");
            migrationBuilder.Sql("ALTER TABLE ItensPedidos ALTER COLUMN Id INT IDENTITY(1,1)");

            // Re-establish the primary key
            migrationBuilder.AddPrimaryKey(
                name: "PK_ItensPedidos",
                table: "ItensPedidos",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItensPedidos_IdPedido",
                table: "ItensPedidos",
                column: "IdPedido");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
       name: "PK_ItensPedidos",
       table: "ItensPedidos");

            migrationBuilder.DropIndex(
                name: "IX_ItensPedidos_IdPedido",
                table: "ItensPedidos");

            // Drop the Id column
            migrationBuilder.DropColumn(
                name: "Id",
                table: "ItensPedidos");

            // Add the Id column back without identity
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ItensPedidos",
                type: "int",
                nullable: false);

            // Re-establish the primary key
            migrationBuilder.AddPrimaryKey(
                name: "PK_ItensPedidos",
                table: "ItensPedidos",
                columns: new[] { "IdPedido", "IdProduto" });
        }
    }
}
