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
                    IdAtleta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Edad = table.Column<int>(type: "int", nullable: false),
                    Discapacidades = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ubicacion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carrera", x => x.IdCarrera);
                });

            migrationBuilder.CreateTable(
                name: "Participacion",
                columns: table => new
                {
                    IdInscripcion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroDorsal = table.Column<int>(type: "int", nullable: false),
                    Atleta = table.Column<int>(type: "int", nullable: false),
                    Carrera = table.Column<int>(type: "int", nullable: false),
                    AtletaIdAtleta = table.Column<int>(type: "int", nullable: true),
                    CarreraIdCarrera = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participacion", x => x.IdInscripcion);
                    table.ForeignKey(
                        name: "FK_Participacion_Atleta_AtletaIdAtleta",
                        column: x => x.AtletaIdAtleta,
                        principalTable: "Atleta",
                        principalColumn: "IdAtleta");
                    table.ForeignKey(
                        name: "FK_Participacion_Carrera_CarreraIdCarrera",
                        column: x => x.CarreraIdCarrera,
                        principalTable: "Carrera",
                        principalColumn: "IdCarrera");
                });

            migrationBuilder.CreateTable(
                name: "PuntosdeControl",
                columns: table => new
                {
                    IdPunto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: false),
                    CarreraIdIdCarrera = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PuntosdeControl", x => x.IdPunto);
                    table.ForeignKey(
                        name: "FK_PuntosdeControl_Carrera_CarreraIdIdCarrera",
                        column: x => x.CarreraIdIdCarrera,
                        principalTable: "Carrera",
                        principalColumn: "IdCarrera",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TiemposParciales",
                columns: table => new
                {
                    IdTiempo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoraPaso = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InscripcionIdIdInscripcion = table.Column<int>(type: "int", nullable: false),
                    PuntoControlIdIdPunto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiemposParciales", x => x.IdTiempo);
                    table.ForeignKey(
                        name: "FK_TiemposParciales_Participacion_InscripcionIdIdInscripcion",
                        column: x => x.InscripcionIdIdInscripcion,
                        principalTable: "Participacion",
                        principalColumn: "IdInscripcion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TiemposParciales_PuntosdeControl_PuntoControlIdIdPunto",
                        column: x => x.PuntoControlIdIdPunto,
                        principalTable: "PuntosdeControl",
                        principalColumn: "IdPunto",
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

            migrationBuilder.CreateIndex(
                name: "IX_PuntosdeControl_CarreraIdIdCarrera",
                table: "PuntosdeControl",
                column: "CarreraIdIdCarrera");

            migrationBuilder.CreateIndex(
                name: "IX_TiemposParciales_InscripcionIdIdInscripcion",
                table: "TiemposParciales",
                column: "InscripcionIdIdInscripcion");

            migrationBuilder.CreateIndex(
                name: "IX_TiemposParciales_PuntoControlIdIdPunto",
                table: "TiemposParciales",
                column: "PuntoControlIdIdPunto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TiemposParciales");

            migrationBuilder.DropTable(
                name: "Participacion");

            migrationBuilder.DropTable(
                name: "PuntosdeControl");

            migrationBuilder.DropTable(
                name: "Atleta");

            migrationBuilder.DropTable(
                name: "Carrera");
        }
    }
}
