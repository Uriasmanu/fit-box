using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fit_box.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Logins",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NameIngrediente = table.Column<string>(type: "text", nullable: false),
                    QuantidadeEmGramas = table.Column<int>(type: "integer", nullable: false),
                    LoginId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingredientes_Logins_LoginId",
                        column: x => x.LoginId,
                        principalTable: "Logins",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Marmitas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NameMarmita = table.Column<string>(type: "text", nullable: false),
                    TamanhoMarmita = table.Column<int>(type: "integer", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LoginId = table.Column<Guid>(type: "uuid", nullable: false),
                    Favorito = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marmitas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Marmitas_Logins_LoginId",
                        column: x => x.LoginId,
                        principalTable: "Logins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MarmitaIngredientes",
                columns: table => new
                {
                    IngredientesId = table.Column<Guid>(type: "uuid", nullable: false),
                    MarmitasId = table.Column<Guid>(type: "uuid", nullable: false)
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
                name: "IX_Ingredientes_LoginId",
                table: "Ingredientes",
                column: "LoginId");

            migrationBuilder.CreateIndex(
                name: "IX_MarmitaIngredientes_MarmitasId",
                table: "MarmitaIngredientes",
                column: "MarmitasId");

            migrationBuilder.CreateIndex(
                name: "IX_Marmitas_LoginId",
                table: "Marmitas",
                column: "LoginId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MarmitaIngredientes");

            migrationBuilder.DropTable(
                name: "Ingredientes");

            migrationBuilder.DropTable(
                name: "Marmitas");

            migrationBuilder.DropTable(
                name: "Logins");
        }
    }
}
