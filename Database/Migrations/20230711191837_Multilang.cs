using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class Multilang : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ReportTag",
                schema: "public",
                table: "ReportTag");

            migrationBuilder.DropIndex(
                name: "IX_ReportTag_ReportId_TagId",
                schema: "public",
                table: "ReportTag");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "public",
                table: "ReportTag");

            migrationBuilder.DropColumn(
                name: "Capture",
                schema: "public",
                table: "Fundraising");

            migrationBuilder.DropColumn(
                name: "Text",
                schema: "public",
                table: "Fundraising");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReportTag",
                schema: "public",
                table: "ReportTag",
                columns: new[] { "ReportId", "TagId" });

            migrationBuilder.CreateTable(
                name: "Language",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FundraisingContent",
                schema: "public",
                columns: table => new
                {
                    FundraisingId = table.Column<long>(type: "bigint", nullable: false),
                    LanguageId = table.Column<int>(type: "integer", nullable: false),
                    Capture = table.Column<string>(type: "text", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundraisingContent", x => new { x.FundraisingId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_FundraisingContent_Fundraising_FundraisingId",
                        column: x => x.FundraisingId,
                        principalSchema: "public",
                        principalTable: "Fundraising",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FundraisingContent_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalSchema: "public",
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportContent",
                schema: "public",
                columns: table => new
                {
                    ReportId = table.Column<long>(type: "bigint", nullable: false),
                    LanguageId = table.Column<int>(type: "integer", nullable: false),
                    Capture = table.Column<string>(type: "text", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportContent", x => new { x.ReportId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_ReportContent_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalSchema: "public",
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportContent_Report_ReportId",
                        column: x => x.ReportId,
                        principalSchema: "public",
                        principalTable: "Report",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FundraisingContent_LanguageId",
                schema: "public",
                table: "FundraisingContent",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportContent_LanguageId",
                schema: "public",
                table: "ReportContent",
                column: "LanguageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FundraisingContent",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ReportContent",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Language",
                schema: "public");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReportTag",
                schema: "public",
                table: "ReportTag");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                schema: "public",
                table: "ReportTag",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "Capture",
                schema: "public",
                table: "Fundraising",
                type: "text",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Text",
                schema: "public",
                table: "Fundraising",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReportTag",
                schema: "public",
                table: "ReportTag",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTag_ReportId_TagId",
                schema: "public",
                table: "ReportTag",
                columns: new[] { "ReportId", "TagId" },
                unique: true);
        }
    }
}
