using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinimalAPI_Multas.Migrations
{
    public partial class ALTER_TABLE_MULTA : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Multa_Precio_idPrecio",
                table: "Multa");

            migrationBuilder.DropIndex(
                name: "IX_Multa_idPrecio",
                table: "Multa");

            migrationBuilder.DropColumn(
                name: "idPrecio",
                table: "Multa");

            migrationBuilder.AlterColumn<double>(
                name: "Monto",
                table: "Precio",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<double>(
                name: "Monto",
                table: "Multa",
                type: "float",
                maxLength: 10,
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Monto",
                table: "Multa");

            migrationBuilder.AlterColumn<decimal>(
                name: "Monto",
                table: "Precio",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<int>(
                name: "idPrecio",
                table: "Multa",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Multa_idPrecio",
                table: "Multa",
                column: "idPrecio",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Multa_Precio_idPrecio",
                table: "Multa",
                column: "idPrecio",
                principalTable: "Precio",
                principalColumn: "idPrecio",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
