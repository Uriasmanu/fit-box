using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fit_box.Migrations
{
    /// <inheritdoc />
    public partial class Atualizar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredientes_Marmitas_MarmitaId",
                table: "Ingredientes");

            migrationBuilder.AlterColumn<Guid>(
                name: "MarmitaId",
                table: "Ingredientes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

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

            migrationBuilder.AlterColumn<Guid>(
                name: "MarmitaId",
                table: "Ingredientes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredientes_Marmitas_MarmitaId",
                table: "Ingredientes",
                column: "MarmitaId",
                principalTable: "Marmitas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
