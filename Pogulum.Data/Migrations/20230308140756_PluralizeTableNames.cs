using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pogulum.Data.Migrations
{
    /// <inheritdoc />
    public partial class PluralizeTableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Broadcaster_Users_UserId",
                table: "Broadcaster");

            migrationBuilder.DropForeignKey(
                name: "FK_Clip_Users_UserId",
                table: "Clip");

            migrationBuilder.DropForeignKey(
                name: "FK_Game_Users_UserId",
                table: "Game");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Game",
                table: "Game");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clip",
                table: "Clip");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Broadcaster",
                table: "Broadcaster");

            migrationBuilder.RenameTable(
                name: "Game",
                newName: "Games");

            migrationBuilder.RenameTable(
                name: "Clip",
                newName: "Clips");

            migrationBuilder.RenameTable(
                name: "Broadcaster",
                newName: "Broadcasters");

            migrationBuilder.RenameIndex(
                name: "IX_Game_UserId",
                table: "Games",
                newName: "IX_Games_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Clip_UserId",
                table: "Clips",
                newName: "IX_Clips_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Broadcaster_UserId",
                table: "Broadcasters",
                newName: "IX_Broadcasters_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Games",
                table: "Games",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clips",
                table: "Clips",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Broadcasters",
                table: "Broadcasters",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessages_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_UserId",
                table: "ChatMessages",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Broadcasters_Users_UserId",
                table: "Broadcasters",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clips_Users_UserId",
                table: "Clips",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Users_UserId",
                table: "Games",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Broadcasters_Users_UserId",
                table: "Broadcasters");

            migrationBuilder.DropForeignKey(
                name: "FK_Clips_Users_UserId",
                table: "Clips");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Users_UserId",
                table: "Games");

            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Games",
                table: "Games");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clips",
                table: "Clips");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Broadcasters",
                table: "Broadcasters");

            migrationBuilder.RenameTable(
                name: "Games",
                newName: "Game");

            migrationBuilder.RenameTable(
                name: "Clips",
                newName: "Clip");

            migrationBuilder.RenameTable(
                name: "Broadcasters",
                newName: "Broadcaster");

            migrationBuilder.RenameIndex(
                name: "IX_Games_UserId",
                table: "Game",
                newName: "IX_Game_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Clips_UserId",
                table: "Clip",
                newName: "IX_Clip_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Broadcasters_UserId",
                table: "Broadcaster",
                newName: "IX_Broadcaster_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Game",
                table: "Game",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clip",
                table: "Clip",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Broadcaster",
                table: "Broadcaster",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Broadcaster_Users_UserId",
                table: "Broadcaster",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clip_Users_UserId",
                table: "Clip",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Users_UserId",
                table: "Game",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
