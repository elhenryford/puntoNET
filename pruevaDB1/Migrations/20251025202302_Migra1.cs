using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pruevaDB1.Migrations
{
    /// <inheritdoc />
    public partial class Migra1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Atleta",
                columns: table => new
                {
                    idAtleta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    edad = table.Column<int>(type: "int", nullable: false),
                    discapacidades = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atleta", x => x.idAtleta);
                });

            migrationBuilder.CreateTable(
                name: "Carrera",
                columns: table => new
                {
                    idCarrera = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    cantidadPuntosControl = table.Column<int>(type: "int", nullable: false),
                    cuposDisponibles = table.Column<int>(type: "int", nullable: false),
                    mapa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carrera", x => x.idCarrera);
                });

            migrationBuilder.CreateTable(
                name: "Participacion",
                columns: table => new
                {
                    idParticipacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    atletaidAtleta = table.Column<int>(type: "int", nullable: false),
                    carreraidCarrera = table.Column<int>(type: "int", nullable: false),
                    tiempos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tiempoFinal = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participacion", x => x.idParticipacion);
                    table.ForeignKey(
                        name: "FK_Participacion_Atleta_atletaidAtleta",
                        column: x => x.atletaidAtleta,
                        principalTable: "Atleta",
                        principalColumn: "idAtleta",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Participacion_Carrera_carreraidCarrera",
                        column: x => x.carreraidCarrera,
                        principalTable: "Carrera",
                        principalColumn: "idCarrera",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Participacion_atletaidAtleta",
                table: "Participacion",
                column: "atletaidAtleta");

            migrationBuilder.CreateIndex(
                name: "IX_Participacion_carreraidCarrera",
                table: "Participacion",
                column: "carreraidCarrera");
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
        }
    }
}
