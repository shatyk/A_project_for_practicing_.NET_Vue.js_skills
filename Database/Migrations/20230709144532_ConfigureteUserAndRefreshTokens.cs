using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureteUserAndRefreshTokens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Username",
                schema: "public",
                table: "User",
                type: "varchar(120)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                schema: "public",
                table: "User",
                type: "varchar(120)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Username",
                schema: "public",
                table: "User",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(120)");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                schema: "public",
                table: "User",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(120)");
        }
    }
}
