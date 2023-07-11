using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class Add_Fundraising_Report_ReportTag_TextType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Username",
                schema: "public",
                table: "User",
                type: "text",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(120)");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                schema: "public",
                table: "User",
                type: "text",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(120)");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                schema: "public",
                table: "Tag",
                type: "text",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(128)");

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                schema: "public",
                table: "RefreshToken",
                type: "text",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(256)");

            migrationBuilder.CreateTable(
                name: "Fundraising",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Capture = table.Column<string>(type: "text", maxLength: 256, nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ActivityStatus = table.Column<int>(type: "integer", nullable: false),
                    VisabilityStatus = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fundraising", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Report",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Capture = table.Column<string>(type: "text", maxLength: 256, nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    VisabilityStatus = table.Column<int>(type: "integer", nullable: false),
                    FundraisingId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Report_Fundraising_FundraisingId",
                        column: x => x.FundraisingId,
                        principalSchema: "public",
                        principalTable: "Fundraising",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportTag",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReportId = table.Column<long>(type: "bigint", nullable: false),
                    TagId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportTag_Report_ReportId",
                        column: x => x.ReportId,
                        principalSchema: "public",
                        principalTable: "Report",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportTag_Tag_TagId",
                        column: x => x.TagId,
                        principalSchema: "public",
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Report_FundraisingId",
                schema: "public",
                table: "Report",
                column: "FundraisingId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTag_ReportId_TagId",
                schema: "public",
                table: "ReportTag",
                columns: new[] { "ReportId", "TagId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReportTag_TagId",
                schema: "public",
                table: "ReportTag",
                column: "TagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportTag",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Report",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Fundraising",
                schema: "public");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                schema: "public",
                table: "User",
                type: "varchar(120)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                schema: "public",
                table: "User",
                type: "varchar(120)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                schema: "public",
                table: "Tag",
                type: "varchar(128)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                schema: "public",
                table: "RefreshToken",
                type: "varchar(256)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldMaxLength: 256);
        }
    }
}
