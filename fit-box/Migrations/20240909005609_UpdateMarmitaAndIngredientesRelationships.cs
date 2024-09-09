using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fit_box.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMarmitaAndIngredientesRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredientes_Marmitas_MarmitaId",
                table: "Ingredientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Marmitas_Logins_LoginId",
                table: "Marmitas");

            migrationBuilder.DropIndex(
                name: "IX_Ingredientes_MarmitaId",
                table: "Ingredientes");

            migrationBuilder.DropColumn(
                name: "MarmitaId",
                table: "Ingredientes");

            migrationBuilder.AlterColumn<string>(
                name: "NameMarmita",
                table: "Marmitas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "NameIngrediente",
                table: "Ingredientes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateTable(
                name: "MarmitaIngredientes",
                columns: table => new
                {
                    IngredientesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MarmitasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarmitaIngredientes", x => new { x.IngredientesId, x.MarmitasId });
                    table.ForeignKey(
                        name: "FK_MarmitaIngredientes_Ingredientes_IngredientesId",
                        column: x => x.IngredientesId,
                        principalTable: "Ingredientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MarmitaIngredientes_Marmitas_MarmitasId",
                        column: x => x.MarmitasId,
                        principalTable: "Marmitas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MarmitaIngredientes_MarmitasId",
                table: "MarmitaIngredientes",
                column: "MarmitasId");

            migrationBuilder.AddForeignKey(
                name: "FK_Marmitas_Logins_LoginId",
                table: "Marmitas",
                column: "LoginId",
                principalTable: "Logins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Marmitas_Logins_LoginId",
                table: "Marmitas");

            migrationBuilder.DropTable(
                name: "MarmitaIngredientes");

            migrationBuilder.AlterColumn<string>(
                name: "NameMarmita",
                table: "Marmitas",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "NameIngrediente",
                table: "Ingredientes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "MarmitaId",
                table: "Ingredientes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Ingredientes_MarmitaId",
                table: "Ingredientes",
                column: "MarmitaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredientes_Marmitas_MarmitaId",
                table: "Ingredientes",
                column: "MarmitaId",
                principalTable: "Marmitas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Marmitas_Logins_LoginId",
                table: "Marmitas",
                column: "LoginId",
                principalTable: "Logins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
