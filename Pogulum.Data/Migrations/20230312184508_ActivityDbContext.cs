using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pogulum.Data.Migrations
{
    /// <inheritdoc />
    public partial class ActivityDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActorId = table.Column<int>(type: "int", nullable: false),
                    GameSubjectId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ClipSubjectId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BroadcasterSubjectId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activities_Broadcasters_BroadcasterSubjectId",
                        column: x => x.BroadcasterSubjectId,
                        principalTable: "Broadcasters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Activities_Clips_ClipSubjectId",
                        column: x => x.ClipSubjectId,
                        principalTable: "Clips",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Activities_Games_GameSubjectId",
                        column: x => x.GameSubjectId,
                        principalTable: "Games",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Activities_Users_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_ActorId",
                table: "Activities",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_BroadcasterSubjectId",
                table: "Activities",
                column: "BroadcasterSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_ClipSubjectId",
                table: "Activities",
                column: "ClipSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_GameSubjectId",
                table: "Activities",
                column: "GameSubjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activities");
        }
    }
}
