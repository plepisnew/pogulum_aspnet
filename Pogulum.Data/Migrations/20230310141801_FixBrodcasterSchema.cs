using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pogulum.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixBrodcasterSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavedClips_Users_UserId",
                table: "SavedClips");

            migrationBuilder.DropIndex(
                name: "IX_SavedClips_UserId",
                table: "SavedClips");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SavedClips");

            migrationBuilder.RenameColumn(
                name: "Language",
                table: "Broadcasters",
                newName: "ProfileImageUrl");

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "SavedClips",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Broadcasters",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Broadcasters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OfflineImageUrl",
                table: "Broadcasters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "Broadcasters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SavedClips_CreatorId",
                table: "SavedClips",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_SavedClips_Users_CreatorId",
                table: "SavedClips",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavedClips_Users_CreatorId",
                table: "SavedClips");

            migrationBuilder.DropIndex(
                name: "IX_SavedClips_CreatorId",
                table: "SavedClips");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "SavedClips");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Broadcasters");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Broadcasters");

            migrationBuilder.DropColumn(
                name: "OfflineImageUrl",
                table: "Broadcasters");

            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "Broadcasters");

            migrationBuilder.RenameColumn(
                name: "ProfileImageUrl",
                table: "Broadcasters",
                newName: "Language");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "SavedClips",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SavedClips_UserId",
                table: "SavedClips",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SavedClips_Users_UserId",
                table: "SavedClips",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
