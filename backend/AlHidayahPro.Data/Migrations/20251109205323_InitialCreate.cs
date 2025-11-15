using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlHidayahPro.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AudioRecitations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReciterName = table.Column<string>(type: "TEXT", nullable: false),
                    SurahNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    AyahNumber = table.Column<int>(type: "INTEGER", nullable: true),
                    AudioUrl = table.Column<string>(type: "TEXT", nullable: false),
                    Format = table.Column<string>(type: "TEXT", nullable: false),
                    DurationSeconds = table.Column<int>(type: "INTEGER", nullable: true),
                    RecitationStyle = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AudioRecitations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hadiths",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Collection = table.Column<string>(type: "TEXT", nullable: false),
                    Book = table.Column<string>(type: "TEXT", nullable: false),
                    BookNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    HadithNumber = table.Column<string>(type: "TEXT", nullable: false),
                    ArabicText = table.Column<string>(type: "TEXT", nullable: true),
                    EnglishText = table.Column<string>(type: "TEXT", nullable: false),
                    Grade = table.Column<string>(type: "TEXT", nullable: false),
                    Narrator = table.Column<string>(type: "TEXT", nullable: true),
                    Chapter = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hadiths", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Surahs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Number = table.Column<int>(type: "INTEGER", nullable: false),
                    ArabicName = table.Column<string>(type: "TEXT", nullable: false),
                    EnglishName = table.Column<string>(type: "TEXT", nullable: false),
                    EnglishTranslation = table.Column<string>(type: "TEXT", nullable: true),
                    NumberOfAyahs = table.Column<int>(type: "INTEGER", nullable: false),
                    RevelationType = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surahs", x => x.Id);
                    table.UniqueConstraint("AK_Surahs_Number", x => x.Number);
                });

            migrationBuilder.CreateTable(
                name: "QuranVerses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SurahNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    AyahNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    ArabicText = table.Column<string>(type: "TEXT", nullable: false),
                    EnglishTranslation = table.Column<string>(type: "TEXT", nullable: true),
                    Transliteration = table.Column<string>(type: "TEXT", nullable: true),
                    AudioUrl = table.Column<string>(type: "TEXT", nullable: true),
                    JuzNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    PageNumber = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuranVerses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuranVerses_Surahs_SurahNumber",
                        column: x => x.SurahNumber,
                        principalTable: "Surahs",
                        principalColumn: "Number",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AudioRecitations_ReciterName_SurahNumber_AyahNumber",
                table: "AudioRecitations",
                columns: new[] { "ReciterName", "SurahNumber", "AyahNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_Hadiths_Collection_HadithNumber",
                table: "Hadiths",
                columns: new[] { "Collection", "HadithNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_QuranVerses_SurahNumber_AyahNumber",
                table: "QuranVerses",
                columns: new[] { "SurahNumber", "AyahNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Surahs_Number",
                table: "Surahs",
                column: "Number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AudioRecitations");

            migrationBuilder.DropTable(
                name: "Hadiths");

            migrationBuilder.DropTable(
                name: "QuranVerses");

            migrationBuilder.DropTable(
                name: "Surahs");
        }
    }
}
