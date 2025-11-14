using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pruevaDB1.Migrations
{
    /// <inheritdoc />
    public partial class Migra2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "posicion",
                table: "Inscripcion",
                newName: "Posicion");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "HoraPaso",
                table: "TiempoParcial",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TiempoTotal",
                table: "Inscripcion",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<DateTime>(
                name: "HoraInicio",
                table: "Carreras",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TiempoTotal",
                table: "Inscripcion");

            migrationBuilder.DropColumn(
                name: "HoraInicio",
                table: "Carreras");

            migrationBuilder.RenameColumn(
                name: "Posicion",
                table: "Inscripcion",
                newName: "posicion");

            migrationBuilder.AlterColumn<DateTime>(
                name: "HoraPaso",
                table: "TiempoParcial",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");
        }
    }
}
