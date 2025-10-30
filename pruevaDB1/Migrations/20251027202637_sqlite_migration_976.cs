using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoPuntoNET.Migrations
{
    /// <inheritdoc />
    public partial class sqlite_migration_976 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    EventoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
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
                    Numero = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Edad = table.Column<int>(type: "INTEGER", nullable: false),
                    ChipId = table.Column<string>(type: "TEXT", nullable: true),
                    Categoria = table.Column<string>(type: "TEXT", nullable: true),
                    EventoId = table.Column<int>(type: "INTEGER", nullable: true)
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
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ChipId = table.Column<string>(type: "TEXT", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AthleteId = table.Column<int>(type: "INTEGER", nullable: true),
                    AtletaNumero = table.Column<int>(type: "INTEGER", nullable: true),
                    Checkpoint = table.Column<string>(type: "TEXT", nullable: true)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChipEvents");

            migrationBuilder.DropTable(
                name: "Atletas");

            migrationBuilder.DropTable(
                name: "Eventos");
        }
    }
}
