using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trabalho_POO.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conta_Clientes_Cliente_Id",
                table: "Conta");

            migrationBuilder.DropIndex(
                name: "IX_Conta_Cliente_Id",
                table: "Conta");

            migrationBuilder.DropColumn(
                name: "Cliente_Id",
                table: "Conta");

            migrationBuilder.AddColumn<string>(
                name: "clienteId",
                table: "Conta",
                type: "varchar(14)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Conta_clienteId",
                table: "Conta",
                column: "clienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Conta_Clientes_clienteId",
                table: "Conta",
                column: "clienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conta_Clientes_clienteId",
                table: "Conta");

            migrationBuilder.DropIndex(
                name: "IX_Conta_clienteId",
                table: "Conta");

            migrationBuilder.DropColumn(
                name: "clienteId",
                table: "Conta");

            migrationBuilder.AddColumn<string>(
                name: "Cliente_Id",
                table: "Conta",
                type: "varchar(14)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Conta_Cliente_Id",
                table: "Conta",
                column: "Cliente_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Conta_Clientes_Cliente_Id",
                table: "Conta",
                column: "Cliente_Id",
                principalTable: "Clientes",
                principalColumn: "Id");
        }
    }
}
