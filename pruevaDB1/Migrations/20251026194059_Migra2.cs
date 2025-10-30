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
            migrationBuilder.DropForeignKey(
                name: "FK_Participacion_Atleta_atletaidAtleta",
                table: "Participacion");

            migrationBuilder.DropForeignKey(
                name: "FK_Participacion_Carrera_carreraidCarrera",
                table: "Participacion");

            migrationBuilder.RenameColumn(
                name: "tiempos",
                table: "Participacion",
                newName: "Tiempos");

            migrationBuilder.RenameColumn(
                name: "tiempoFinal",
                table: "Participacion",
                newName: "TiempoFinal");

            migrationBuilder.RenameColumn(
                name: "carreraidCarrera",
                table: "Participacion",
                newName: "CarreraIdCarrera");

            migrationBuilder.RenameColumn(
                name: "atletaidAtleta",
                table: "Participacion",
                newName: "AtletaIdAtleta");

            migrationBuilder.RenameColumn(
                name: "idParticipacion",
                table: "Participacion",
                newName: "IdParticipacion");

            migrationBuilder.RenameIndex(
                name: "IX_Participacion_carreraidCarrera",
                table: "Participacion",
                newName: "IX_Participacion_CarreraIdCarrera");

            migrationBuilder.RenameIndex(
                name: "IX_Participacion_atletaidAtleta",
                table: "Participacion",
                newName: "IX_Participacion_AtletaIdAtleta");

            migrationBuilder.RenameColumn(
                name: "nombre",
                table: "Carrera",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "mapa",
                table: "Carrera",
                newName: "Mapa");

            migrationBuilder.RenameColumn(
                name: "fecha",
                table: "Carrera",
                newName: "Fecha");

            migrationBuilder.RenameColumn(
                name: "cuposDisponibles",
                table: "Carrera",
                newName: "CuposDisponibles");

            migrationBuilder.RenameColumn(
                name: "cantidadPuntosControl",
                table: "Carrera",
                newName: "CantidadPuntosControl");

            migrationBuilder.RenameColumn(
                name: "idCarrera",
                table: "Carrera",
                newName: "IdCarrera");

            migrationBuilder.RenameColumn(
                name: "nombre",
                table: "Atleta",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "edad",
                table: "Atleta",
                newName: "Edad");

            migrationBuilder.RenameColumn(
                name: "discapacidades",
                table: "Atleta",
                newName: "Discapacidades");

            migrationBuilder.RenameColumn(
                name: "idAtleta",
                table: "Atleta",
                newName: "IdAtleta");

            migrationBuilder.AddForeignKey(
                name: "FK_Participacion_Atleta_AtletaIdAtleta",
                table: "Participacion",
                column: "AtletaIdAtleta",
                principalTable: "Atleta",
                principalColumn: "IdAtleta",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Participacion_Carrera_CarreraIdCarrera",
                table: "Participacion",
                column: "CarreraIdCarrera",
                principalTable: "Carrera",
                principalColumn: "IdCarrera",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participacion_Atleta_AtletaIdAtleta",
                table: "Participacion");

            migrationBuilder.DropForeignKey(
                name: "FK_Participacion_Carrera_CarreraIdCarrera",
                table: "Participacion");

            migrationBuilder.RenameColumn(
                name: "Tiempos",
                table: "Participacion",
                newName: "tiempos");

            migrationBuilder.RenameColumn(
                name: "TiempoFinal",
                table: "Participacion",
                newName: "tiempoFinal");

            migrationBuilder.RenameColumn(
                name: "CarreraIdCarrera",
                table: "Participacion",
                newName: "carreraidCarrera");

            migrationBuilder.RenameColumn(
                name: "AtletaIdAtleta",
                table: "Participacion",
                newName: "atletaidAtleta");

            migrationBuilder.RenameColumn(
                name: "IdParticipacion",
                table: "Participacion",
                newName: "idParticipacion");

            migrationBuilder.RenameIndex(
                name: "IX_Participacion_CarreraIdCarrera",
                table: "Participacion",
                newName: "IX_Participacion_carreraidCarrera");

            migrationBuilder.RenameIndex(
                name: "IX_Participacion_AtletaIdAtleta",
                table: "Participacion",
                newName: "IX_Participacion_atletaidAtleta");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Carrera",
                newName: "nombre");

            migrationBuilder.RenameColumn(
                name: "Mapa",
                table: "Carrera",
                newName: "mapa");

            migrationBuilder.RenameColumn(
                name: "Fecha",
                table: "Carrera",
                newName: "fecha");

            migrationBuilder.RenameColumn(
                name: "CuposDisponibles",
                table: "Carrera",
                newName: "cuposDisponibles");

            migrationBuilder.RenameColumn(
                name: "CantidadPuntosControl",
                table: "Carrera",
                newName: "cantidadPuntosControl");

            migrationBuilder.RenameColumn(
                name: "IdCarrera",
                table: "Carrera",
                newName: "idCarrera");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Atleta",
                newName: "nombre");

            migrationBuilder.RenameColumn(
                name: "Edad",
                table: "Atleta",
                newName: "edad");

            migrationBuilder.RenameColumn(
                name: "Discapacidades",
                table: "Atleta",
                newName: "discapacidades");

            migrationBuilder.RenameColumn(
                name: "IdAtleta",
                table: "Atleta",
                newName: "idAtleta");

            migrationBuilder.AddForeignKey(
                name: "FK_Participacion_Atleta_atletaidAtleta",
                table: "Participacion",
                column: "atletaidAtleta",
                principalTable: "Atleta",
                principalColumn: "idAtleta",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Participacion_Carrera_carreraidCarrera",
                table: "Participacion",
                column: "carreraidCarrera",
                principalTable: "Carrera",
                principalColumn: "idCarrera",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
