using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutriCare.Migrations
{
    /// <inheritdoc />
    public partial class DiabetesRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Diabetes_DiabetesId",
                table: "Accounts");

            migrationBuilder.DropTable(
                name: "Diabetes");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_DiabetesId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "DiabetesId",
                table: "Accounts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DiabetesId",
                table: "Accounts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Diabetes",
                columns: table => new
                {
                    DiabetesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diabetes", x => x.DiabetesId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_DiabetesId",
                table: "Accounts",
                column: "DiabetesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Diabetes_DiabetesId",
                table: "Accounts",
                column: "DiabetesId",
                principalTable: "Diabetes",
                principalColumn: "DiabetesId");
        }
    }
}
