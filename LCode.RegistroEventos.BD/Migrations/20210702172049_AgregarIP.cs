using Microsoft.EntityFrameworkCore.Migrations;

namespace LCode.RegistroEventos.BD.Migrations
{
    public partial class AgregarIP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IPOrigen",
                schema: "lcode",
                table: "Eventos",
                type: "varchar(25)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IPOrigen",
                schema: "lcode",
                table: "Eventos");
        }
    }
}
