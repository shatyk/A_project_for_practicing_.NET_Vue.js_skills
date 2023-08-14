using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class OMG_Genius_Migrate_RenameCaptureToCaption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Capture",
                schema: "public",
                table: "ReportContent",
                newName: "Caption");

            migrationBuilder.RenameColumn(
                name: "Capture",
                schema: "public",
                table: "FundraisingContent",
                newName: "Caption");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Caption",
                schema: "public",
                table: "ReportContent",
                newName: "Capture");

            migrationBuilder.RenameColumn(
                name: "Caption",
                schema: "public",
                table: "FundraisingContent",
                newName: "Capture");
        }
    }
}
