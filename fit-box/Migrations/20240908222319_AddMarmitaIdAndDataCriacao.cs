using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fit_box.Migrations
{
    /// <inheritdoc />
    public partial class AddMarmitaIdAndDataCriacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MarmitaIngredientes");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Marmitas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "MarmitaId",
                table: "Ingredientes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ingredientes_MarmitaId",
                table: "Ingredientes",
                column: "MarmitaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredientes_Marmitas_MarmitaId",
                table: "Ingredientes",
                column: "MarmitaId",
                principalTable: "Marmitas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredientes_Marmitas_MarmitaId",
                table: "Ingredientes");

            migrationBuilder.DropIndex(
                name: "IX_Ingredientes_MarmitaId",
                table: "Ingredientes");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Marmitas");

            migrationBuilder.DropColumn(
                name: "MarmitaId",
                table: "Ingredientes");

            migrationBuilder.CreateTable(
                name: "MarmitaIngredientes",
                columns: table => new
                {
                    MarmitaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IngredienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuantidadeEmGramas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarmitaIngredientes", x => new { x.MarmitaId, x.IngredienteId });
                    table.ForeignKey(
                        name: "FK_MarmitaIngredientes_Ingredientes_IngredienteId",
                        column: x => x.IngredienteId,
                        principalTable: "Ingredientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MarmitaIngredientes_Marmitas_MarmitaId",
                        column: x => x.MarmitaId,
                        principalTable: "Marmitas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MarmitaIngredientes_IngredienteId",
                table: "MarmitaIngredientes",
                column: "IngredienteId");
        }
    }
}
