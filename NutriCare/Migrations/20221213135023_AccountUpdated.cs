using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutriCare.Migrations
{
    /// <inheritdoc />
    public partial class AccountUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Password",
                table: "Accounts",
                type: "varbinary(96)",
                maxLength: 96,
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "Password",
                table: "Accounts",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(96)",
                oldMaxLength: 96);
        }
    }
}
