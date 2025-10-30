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
            migrationBuilder.AddColumn<string>(
                name: "mail",
                table: "Atleta",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "Atleta",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "mail",
                table: "Atleta");

            migrationBuilder.DropColumn(
                name: "password",
                table: "Atleta");
        }
    }
}
