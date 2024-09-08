using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fit_box.Migrations
{
    /// <inheritdoc />
    public partial class ajuste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredientes_Logins_LoginId",
                table: "Ingredientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredientes_Marmitas_MarmitaId",
                table: "Ingredientes");

            migrationBuilder.DropColumn(
                name: "QuantidadeEmEstoque",
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

            migrationBuilder.AlterColumn<Guid>(
                name: "LoginId",
                table: "Ingredientes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredientes_Logins_LoginId",
                table: "Ingredientes",
                column: "LoginId",
                principalTable: "Logins",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredientes_Marmitas_MarmitaId",
                table: "Ingredientes",
                column: "MarmitaId",
                principalTable: "Marmitas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredientes_Logins_LoginId",
                table: "Ingredientes");

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

            migrationBuilder.AlterColumn<Guid>(
                name: "LoginId",
                table: "Ingredientes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuantidadeEmEstoque",
                table: "Ingredientes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredientes_Logins_LoginId",
                table: "Ingredientes",
                column: "LoginId",
                principalTable: "Logins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredientes_Marmitas_MarmitaId",
                table: "Ingredientes",
                column: "MarmitaId",
                principalTable: "Marmitas",
                principalColumn: "Id");
        }
    }
}
