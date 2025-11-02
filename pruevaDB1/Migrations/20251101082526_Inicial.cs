using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pruevaDB1.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChipEvents");

            migrationBuilder.DropTable(
                name: "Atletas");

            migrationBuilder.DropTable(
                name: "Eventos");

            migrationBuilder.CreateTable(
                name: "Atleta",
                columns: table => new
                {
                    IdAtleta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Edad = table.Column<int>(type: "int", nullable: false),
                    Discapacidades = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atleta", x => x.IdAtleta);
                });

            migrationBuilder.CreateTable(
                name: "Carrera",
                columns: table => new
                {
                    IdCarrera = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    CantidadPuntosControl = table.Column<int>(type: "int", nullable: false),
                    CuposDisponibles = table.Column<int>(type: "int", nullable: false),
                    Mapa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carrera", x => x.IdCarrera);
                });

            migrationBuilder.CreateTable(
                name: "Participacion",
                columns: table => new
                {
                    IdParticipacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AtletaIdAtleta = table.Column<int>(type: "int", nullable: false),
                    CarreraIdCarrera = table.Column<int>(type: "int", nullable: false),
                    Tiempos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TiempoFinal = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participacion", x => x.IdParticipacion);
                    table.ForeignKey(
                        name: "FK_Participacion_Atleta_AtletaIdAtleta",
                        column: x => x.AtletaIdAtleta,
                        principalTable: "Atleta",
                        principalColumn: "IdAtleta",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Participacion_Carrera_CarreraIdCarrera",
                        column: x => x.CarreraIdCarrera,
                        principalTable: "Carrera",
                        principalColumn: "IdCarrera",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Participacion_AtletaIdAtleta",
                table: "Participacion",
                column: "AtletaIdAtleta");

            migrationBuilder.CreateIndex(
                name: "IX_Participacion_CarreraIdCarrera",
                table: "Participacion",
                column: "CarreraIdCarrera");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Participacion");

            migrationBuilder.DropTable(
                name: "Atleta");

            migrationBuilder.DropTable(
                name: "Carrera");

            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    EventoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.EventoId);
                });

            migrationBuilder.CreateTable(
                name: "Atletas",
                columns: table => new
                {
                    Numero = table.Column<int>(type: "INTEGER", nullable: false),
                    EventoId = table.Column<int>(type: "INTEGER", nullable: true),
                    Categoria = table.Column<string>(type: "TEXT", nullable: true),
                    ChipId = table.Column<string>(type: "TEXT", nullable: true),
                    Edad = table.Column<int>(type: "INTEGER", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atletas", x => x.Numero);
                    table.ForeignKey(
                        name: "FK_Atletas_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "EventoId");
                });

            migrationBuilder.CreateTable(
                name: "ChipEvents",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false),
                    AtletaNumero = table.Column<int>(type: "INTEGER", nullable: true),
                    AthleteId = table.Column<int>(type: "INTEGER", nullable: true),
                    Checkpoint = table.Column<string>(type: "TEXT", nullable: true),
                    ChipId = table.Column<string>(type: "TEXT", nullable: true),
                    Timestamp = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChipEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChipEvents_Atletas_AtletaNumero",
                        column: x => x.AtletaNumero,
                        principalTable: "Atletas",
                        principalColumn: "Numero");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Atletas_EventoId",
                table: "Atletas",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_ChipEvents_AtletaNumero",
                table: "ChipEvents",
                column: "AtletaNumero");
        }
    }
}
