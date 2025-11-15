using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlHidayahPro.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNavigationAndGamification : Migration
    {
        private static readonly string[] BookmarksUserIdContentTypeIndexColumns = new[] { "UserId", "ContentType" };
        private static readonly string[] BookmarksUserIdIsFavoriteIndexColumns = new[] { "UserId", "IsFavorite" };
        private static readonly string[] ChallengesIsActiveStartDateEndDateIndexColumns = new[] { "IsActive", "StartDate", "EndDate" };
        private static readonly string[] ReadingHistoriesUserIdContentTypeContentIdIndexColumns = new[] { "UserId", "ContentType", "ContentId" };
        private static readonly string[] ReadingHistoriesUserIdLastReadAtIndexColumns = new[] { "UserId", "LastReadAt" };
        private static readonly string[] UserChallengesUserIdChallengeIdIndexColumns = new[] { "UserId", "ChallengeId" };
        private static readonly string[] UserChallengesUserIdIsCompletedIndexColumns = new[] { "UserId", "IsCompleted" };

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bookmarks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    ContentType = table.Column<string>(type: "TEXT", nullable: false),
                    ContentId = table.Column<int>(type: "INTEGER", nullable: false),
                    Reference = table.Column<string>(type: "TEXT", nullable: false),
                    Tags = table.Column<string>(type: "TEXT", nullable: true),
                    Notes = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsFavorite = table.Column<bool>(type: "INTEGER", nullable: false),
                    ColorLabel = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookmarks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookmarks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Challenges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    ChallengeType = table.Column<string>(type: "TEXT", nullable: false),
                    TargetValue = table.Column<int>(type: "INTEGER", nullable: false),
                    RewardPoints = table.Column<int>(type: "INTEGER", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    DifficultyLevel = table.Column<string>(type: "TEXT", nullable: false),
                    IconName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Challenges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReadingHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    ContentType = table.Column<string>(type: "TEXT", nullable: false),
                    ContentId = table.Column<int>(type: "INTEGER", nullable: false),
                    Position = table.Column<int>(type: "INTEGER", nullable: false),
                    LastReadAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ProgressPercentage = table.Column<int>(type: "INTEGER", nullable: false),
                    Metadata = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReadingHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReadingHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserChallenges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    ChallengeId = table.Column<int>(type: "INTEGER", nullable: false),
                    CurrentValue = table.Column<int>(type: "INTEGER", nullable: false),
                    ProgressPercentage = table.Column<int>(type: "INTEGER", nullable: false),
                    JoinedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsCompleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    LastActivityAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChallenges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserChallenges_Challenges_ChallengeId",
                        column: x => x.ChallengeId,
                        principalTable: "Challenges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserChallenges_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookmarks_UserId_ContentType",
                table: "Bookmarks",
                columns: BookmarksUserIdContentTypeIndexColumns);

            migrationBuilder.CreateIndex(
                name: "IX_Bookmarks_UserId_IsFavorite",
                table: "Bookmarks",
                columns: BookmarksUserIdIsFavoriteIndexColumns);

            migrationBuilder.CreateIndex(
                name: "IX_Challenges_IsActive_StartDate_EndDate",
                table: "Challenges",
                columns: ChallengesIsActiveStartDateEndDateIndexColumns);

            migrationBuilder.CreateIndex(
                name: "IX_ReadingHistories_UserId_ContentType_ContentId",
                table: "ReadingHistories",
                columns: ReadingHistoriesUserIdContentTypeContentIdIndexColumns,
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReadingHistories_UserId_LastReadAt",
                table: "ReadingHistories",
                columns: ReadingHistoriesUserIdLastReadAtIndexColumns);

            migrationBuilder.CreateIndex(
                name: "IX_UserChallenges_ChallengeId",
                table: "UserChallenges",
                column: "ChallengeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChallenges_UserId_ChallengeId",
                table: "UserChallenges",
                columns: UserChallengesUserIdChallengeIdIndexColumns,
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserChallenges_UserId_IsCompleted",
                table: "UserChallenges",
                columns: UserChallengesUserIdIsCompletedIndexColumns);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookmarks");

            migrationBuilder.DropTable(
                name: "ReadingHistories");

            migrationBuilder.DropTable(
                name: "UserChallenges");

            migrationBuilder.DropTable(
                name: "Challenges");
        }
    }
}
