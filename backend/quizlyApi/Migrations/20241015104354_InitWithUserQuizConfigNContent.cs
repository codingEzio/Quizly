using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace quizlyApi.Migrations
{
    /// <inheritdoc />
    public partial class InitWithUserQuizConfigNContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "quiz_config",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    context = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    difficulty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quiz_config", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "quiz_content",
                columns: table => new
                {
                    quizId = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false),
                    lang = table.Column<int>(type: "int", nullable: false),
                    rawContent = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    postProcessedContent = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quiz_content", x => new { x.quizId, x.userId });
                    table.ForeignKey(
                        name: "FK_quiz_content_quiz_config_quizId",
                        column: x => x.quizId,
                        principalTable: "quiz_config",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_quiz_content_user_userId",
                        column: x => x.userId,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_quiz_content_userId",
                table: "quiz_content",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "quiz_content");

            migrationBuilder.DropTable(
                name: "quiz_config");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
