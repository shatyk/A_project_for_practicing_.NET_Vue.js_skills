using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class NoCascadeFundraisingToReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Report_Fundraising_FundraisingId",
                schema: "public",
                table: "Report");

            migrationBuilder.AlterColumn<long>(
                name: "FundraisingId",
                schema: "public",
                table: "Report",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Report_Fundraising_FundraisingId",
                schema: "public",
                table: "Report",
                column: "FundraisingId",
                principalSchema: "public",
                principalTable: "Fundraising",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Report_Fundraising_FundraisingId",
                schema: "public",
                table: "Report");

            migrationBuilder.AlterColumn<long>(
                name: "FundraisingId",
                schema: "public",
                table: "Report",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Report_Fundraising_FundraisingId",
                schema: "public",
                table: "Report",
                column: "FundraisingId",
                principalSchema: "public",
                principalTable: "Fundraising",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
