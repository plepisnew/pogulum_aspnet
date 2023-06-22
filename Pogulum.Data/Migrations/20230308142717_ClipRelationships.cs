using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pogulum.Data.Migrations
{
    /// <inheritdoc />
    public partial class ClipRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "GameId",
                table: "Clips",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "BroadcasterId",
                table: "Clips",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "SavedClipId",
                table: "Clips",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SavedClip",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClipDurations = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedClip", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SavedClip_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clips_BroadcasterId",
                table: "Clips",
                column: "BroadcasterId");

            migrationBuilder.CreateIndex(
                name: "IX_Clips_GameId",
                table: "Clips",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Clips_SavedClipId",
                table: "Clips",
                column: "SavedClipId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedClip_UserId",
                table: "SavedClip",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clips_Broadcasters_BroadcasterId",
                table: "Clips",
                column: "BroadcasterId",
                principalTable: "Broadcasters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clips_Games_GameId",
                table: "Clips",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clips_SavedClip_SavedClipId",
                table: "Clips",
                column: "SavedClipId",
                principalTable: "SavedClip",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clips_Broadcasters_BroadcasterId",
                table: "Clips");

            migrationBuilder.DropForeignKey(
                name: "FK_Clips_Games_GameId",
                table: "Clips");

            migrationBuilder.DropForeignKey(
                name: "FK_Clips_SavedClip_SavedClipId",
                table: "Clips");

            migrationBuilder.DropTable(
                name: "SavedClip");

            migrationBuilder.DropIndex(
                name: "IX_Clips_BroadcasterId",
                table: "Clips");

            migrationBuilder.DropIndex(
                name: "IX_Clips_GameId",
                table: "Clips");

            migrationBuilder.DropIndex(
                name: "IX_Clips_SavedClipId",
                table: "Clips");

            migrationBuilder.DropColumn(
                name: "SavedClipId",
                table: "Clips");

            migrationBuilder.AlterColumn<string>(
                name: "GameId",
                table: "Clips",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BroadcasterId",
                table: "Clips",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
