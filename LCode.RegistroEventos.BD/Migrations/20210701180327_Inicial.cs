using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LCode.RegistroEventos.BD.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "lcode");

            migrationBuilder.CreateTable(
                name: "Eventos",
                schema: "lcode",
                columns: table => new
                {
                    IdRegistroEvento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoEvento = table.Column<int>(type: "int", nullable: false),
                    NombreComponente = table.Column<string>(type: "varchar(200)", nullable: false),
                    NombreComponenteCompleto = table.Column<string>(type: "varchar(500)", nullable: false),
                    Version = table.Column<string>(type: "varchar(20)", nullable: false),
                    EsDocker = table.Column<bool>(type: "bit", nullable: false),
                    IdActividad = table.Column<string>(type: "varchar(150)", nullable: false),
                    NombreClase = table.Column<string>(type: "varchar(50)", nullable: false),
                    NombreMetodo = table.Column<string>(type: "varchar(50)", nullable: false),
                    NumeroLinea = table.Column<int>(type: "int", nullable: false),
                    NumeroColumna = table.Column<int>(type: "int", nullable: false),
                    Mensaje = table.Column<string>(type: "text", nullable: false),
                    MensajeDetallado = table.Column<string>(type: "text", nullable: true),
                    MensajeAdicional = table.Column<string>(type: "text", nullable: true),
                    FechaHoraEvento = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.IdRegistroEvento);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Eventos",
                schema: "lcode");
        }
    }
}
