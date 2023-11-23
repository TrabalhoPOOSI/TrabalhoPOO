using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trabalho_POO.Migrations
{
    /// <inheritdoc />
    public partial class CriandoEndereco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConsumoMesAnterior",
                table: "Conta");

            migrationBuilder.DropColumn(
                name: "TipoConta",
                table: "Conta");

            migrationBuilder.DropColumn(
                name: "consumoEsgoto",
                table: "Conta");

            migrationBuilder.DropColumn(
                name: "tarifa",
                table: "Conta");

            migrationBuilder.DropColumn(
                name: "tarifaEsgoto",
                table: "Conta");

            migrationBuilder.UpdateData(
                table: "Conta",
                keyColumn: "Endereco",
                keyValue: null,
                column: "Endereco",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Endereco",
                table: "Conta",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<double>(
                name: "leitura",
                table: "Conta",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "leituraAnterior",
                table: "Conta",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "leitura",
                table: "Conta");

            migrationBuilder.DropColumn(
                name: "leituraAnterior",
                table: "Conta");

            migrationBuilder.AlterColumn<string>(
                name: "Endereco",
                table: "Conta",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<double>(
                name: "ConsumoMesAnterior",
                table: "Conta",
                type: "double",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoConta",
                table: "Conta",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "consumoEsgoto",
                table: "Conta",
                type: "double",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tarifa",
                table: "Conta",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "tarifaEsgoto",
                table: "Conta",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
