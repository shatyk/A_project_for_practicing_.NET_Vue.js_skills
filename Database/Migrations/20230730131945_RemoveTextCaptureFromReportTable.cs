using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTextCaptureFromReportTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capture",
                schema: "public",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "Text",
                schema: "public",
                table: "Report");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Capture",
                schema: "public",
                table: "Report",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Text",
                schema: "public",
                table: "Report",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
