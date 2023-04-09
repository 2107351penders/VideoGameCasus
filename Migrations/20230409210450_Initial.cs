using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoGameCasus.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "gameLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gameLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_gameLists_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Summary = table.Column<string>(type: "TEXT", nullable: false),
                    Publisher = table.Column<string>(type: "TEXT", nullable: false),
                    ReleaseDate = table.Column<string>(type: "TEXT", nullable: false),
                    Genre = table.Column<string>(type: "TEXT", nullable: false),
                    Platform = table.Column<string>(type: "TEXT", nullable: false),
                    Cover = table.Column<string>(type: "TEXT", nullable: false),
                    Finished = table.Column<bool>(type: "INTEGER", nullable: false),
                    GameListId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_gameLists_GameListId",
                        column: x => x.GameListId,
                        principalTable: "gameLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "Password" },
                values: new object[] { -1, "Tim", "password" });

            migrationBuilder.InsertData(
                table: "gameLists",
                columns: new[] { "Id", "UserId" },
                values: new object[] { -1, -1 });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Cover", "Finished", "GameListId", "Genre", "Name", "Platform", "Publisher", "ReleaseDate", "Summary" },
                values: new object[] { -1, "https://images.igdb.com/igdb/image/upload/t_cover_big/co1txv.png", false, -1, "Fake", "Seed Game", "None", "Nobody", "Never", "Example game from the database seed" });

            migrationBuilder.CreateIndex(
                name: "IX_gameLists_UserId",
                table: "gameLists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_GameListId",
                table: "Games",
                column: "GameListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "gameLists");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
