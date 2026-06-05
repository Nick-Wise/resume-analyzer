using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumeAnalyzerAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnalysisRecord",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    jobDescription = table.Column<string>(type: "TEXT", nullable: false),
                    skills = table.Column<string>(type: "Text", nullable: false),
                    matchedSkills = table.Column<string>(type: "Text", nullable: false),
                    unmatchedSkills = table.Column<string>(type: "Text", nullable: false),
                    matchedSkillsPercentage = table.Column<decimal>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalysisRecord", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnalysisRecord");
        }
    }
}
