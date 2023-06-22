using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pogulum.Data.Migrations
{
    /// <inheritdoc />
    public partial class Addactivityschema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Popularity",
                table: "Broadcasters",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Popularity",
                table: "Broadcasters");
        }
    }
}
