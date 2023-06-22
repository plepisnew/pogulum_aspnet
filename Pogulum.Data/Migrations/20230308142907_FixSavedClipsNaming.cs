using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pogulum.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixSavedClipsNaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clips_SavedClip_SavedClipId",
                table: "Clips");

            migrationBuilder.DropForeignKey(
                name: "FK_SavedClip_Users_UserId",
                table: "SavedClip");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SavedClip",
                table: "SavedClip");

            migrationBuilder.RenameTable(
                name: "SavedClip",
                newName: "SavedClips");

            migrationBuilder.RenameIndex(
                name: "IX_SavedClip_UserId",
                table: "SavedClips",
                newName: "IX_SavedClips_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SavedClips",
                table: "SavedClips",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clips_SavedClips_SavedClipId",
                table: "Clips",
                column: "SavedClipId",
                principalTable: "SavedClips",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SavedClips_Users_UserId",
                table: "SavedClips",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clips_SavedClips_SavedClipId",
                table: "Clips");

            migrationBuilder.DropForeignKey(
                name: "FK_SavedClips_Users_UserId",
                table: "SavedClips");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SavedClips",
                table: "SavedClips");

            migrationBuilder.RenameTable(
                name: "SavedClips",
                newName: "SavedClip");

            migrationBuilder.RenameIndex(
                name: "IX_SavedClips_UserId",
                table: "SavedClip",
                newName: "IX_SavedClip_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SavedClip",
                table: "SavedClip",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clips_SavedClip_SavedClipId",
                table: "Clips",
                column: "SavedClipId",
                principalTable: "SavedClip",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SavedClip_Users_UserId",
                table: "SavedClip",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
