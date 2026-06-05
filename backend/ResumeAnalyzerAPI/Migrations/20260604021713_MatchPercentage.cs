using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumeAnalyzerAPI.Migrations
{
    /// <inheritdoc />
    public partial class MatchPercentage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "unmatchedSkills",
                table: "AnalysisRecord",
                newName: "UnmatchedSkills");

            migrationBuilder.RenameColumn(
                name: "skills",
                table: "AnalysisRecord",
                newName: "Skills");

            migrationBuilder.RenameColumn(
                name: "matchedSkills",
                table: "AnalysisRecord",
                newName: "MatchedSkills");

            migrationBuilder.RenameColumn(
                name: "jobDescription",
                table: "AnalysisRecord",
                newName: "JobDescription");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "AnalysisRecord",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "matchedSkillsPercentage",
                table: "AnalysisRecord",
                newName: "RunDate");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "AnalysisRecord",
                newName: "MatchPercentage");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnmatchedSkills",
                table: "AnalysisRecord",
                newName: "unmatchedSkills");

            migrationBuilder.RenameColumn(
                name: "Skills",
                table: "AnalysisRecord",
                newName: "skills");

            migrationBuilder.RenameColumn(
                name: "MatchedSkills",
                table: "AnalysisRecord",
                newName: "matchedSkills");

            migrationBuilder.RenameColumn(
                name: "JobDescription",
                table: "AnalysisRecord",
                newName: "jobDescription");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AnalysisRecord",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "RunDate",
                table: "AnalysisRecord",
                newName: "matchedSkillsPercentage");

            migrationBuilder.RenameColumn(
                name: "MatchPercentage",
                table: "AnalysisRecord",
                newName: "CreatedAt");
        }
    }
}
