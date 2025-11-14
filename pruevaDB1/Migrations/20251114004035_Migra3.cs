using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pruevaDB1.Migrations
{
    /// <inheritdoc />
    public partial class Migra3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cupos",
                table: "Carreras",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cupos",
                table: "Carreras");
        }
    }
}
