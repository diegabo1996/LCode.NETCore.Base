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
                name: "AplicacionesComponentes",
                schema: "lcode",
                columns: table => new
                {
                    IdAplicativoComponente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreComponente = table.Column<string>(type: "varchar(200)", nullable: false),
                    NombreComponenteCompleto = table.Column<string>(type: "varchar(500)", nullable: false),
                    FechaHoraRegistro = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AplicacionesComponentes", x => x.IdAplicativoComponente);
                });

            migrationBuilder.CreateTable(
                name: "EventosOrigenes",
                schema: "lcode",
                columns: table => new
                {
                    IdEventoOrigen = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAplicativoComponente = table.Column<int>(type: "int", nullable: false),
                    IPOrigen = table.Column<string>(type: "varchar(25)", nullable: true),
                    NombreHost = table.Column<string>(type: "varchar(25)", nullable: true),
                    Version = table.Column<string>(type: "varchar(20)", nullable: false),
                    EsDocker = table.Column<bool>(type: "bit", nullable: false),
                    FechaHoraRegistro = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventosOrigenes", x => x.IdEventoOrigen);
                    table.ForeignKey(
                        name: "FK_EventosOrigenes_AplicacionesComponentes_IdAplicativoComponente",
                        column: x => x.IdAplicativoComponente,
                        principalSchema: "lcode",
                        principalTable: "AplicacionesComponentes",
                        principalColumn: "IdAplicativoComponente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Eventos",
                schema: "lcode",
                columns: table => new
                {
                    IdEvento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEventoOrigen = table.Column<int>(type: "int", nullable: false),
                    TipoEvento = table.Column<int>(type: "int", nullable: false),
                    TipoExcepcion = table.Column<string>(type: "varchar(100)", nullable: true),
                    CodigoExcepcion = table.Column<int>(type: "int", nullable: false),
                    Mensaje = table.Column<string>(type: "text", nullable: false),
                    MensajeDetallado = table.Column<string>(type: "text", nullable: true),
                    MensajeAdicional = table.Column<string>(type: "text", nullable: true),
                    IdActividad = table.Column<string>(type: "varchar(150)", nullable: true),
                    FechaHoraEvento = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.IdEvento);
                    table.ForeignKey(
                        name: "FK_Eventos_EventosOrigenes_IdEventoOrigen",
                        column: x => x.IdEventoOrigen,
                        principalSchema: "lcode",
                        principalTable: "EventosOrigenes",
                        principalColumn: "IdEventoOrigen",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RastrosEventos",
                schema: "lcode",
                columns: table => new
                {
                    IdRastro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEvento = table.Column<int>(type: "int", nullable: false),
                    NombreDll = table.Column<string>(type: "varchar(500)", nullable: false),
                    NombreArchivo = table.Column<string>(type: "varchar(500)", nullable: false),
                    NombreClase = table.Column<string>(type: "varchar(500)", nullable: false),
                    NombreMetodo = table.Column<string>(type: "varchar(50)", nullable: false),
                    NumeroLinea = table.Column<int>(type: "int", nullable: false),
                    NumeroColumna = table.Column<int>(type: "int", nullable: false),
                    FechaHoraRastro = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RastrosEventos", x => x.IdRastro);
                    table.ForeignKey(
                        name: "FK_RastrosEventos_Eventos_IdEvento",
                        column: x => x.IdEvento,
                        principalSchema: "lcode",
                        principalTable: "Eventos",
                        principalColumn: "IdEvento",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_IdEventoOrigen",
                schema: "lcode",
                table: "Eventos",
                column: "IdEventoOrigen");

            migrationBuilder.CreateIndex(
                name: "IX_EventosOrigenes_IdAplicativoComponente",
                schema: "lcode",
                table: "EventosOrigenes",
                column: "IdAplicativoComponente");

            migrationBuilder.CreateIndex(
                name: "IX_RastrosEventos_IdEvento",
                schema: "lcode",
                table: "RastrosEventos",
                column: "IdEvento");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RastrosEventos",
                schema: "lcode");

            migrationBuilder.DropTable(
                name: "Eventos",
                schema: "lcode");

            migrationBuilder.DropTable(
                name: "EventosOrigenes",
                schema: "lcode");

            migrationBuilder.DropTable(
                name: "AplicacionesComponentes",
                schema: "lcode");
        }
    }
}
