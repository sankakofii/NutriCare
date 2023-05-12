using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutriCare.Migrations
{
    /// <inheritdoc />
    public partial class CreateInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Allergy",
                columns: table => new
                {
                    AllergyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allergy", x => x.AllergyId);
                });

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

            migrationBuilder.CreateTable(
                name: "Intolerance",
                columns: table => new
                {
                    IntoleranceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intolerance", x => x.IntoleranceId);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<byte>(type: "tinyint", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AllergiesAllergyId = table.Column<int>(type: "int", nullable: true),
                    DiabetesId = table.Column<int>(type: "int", nullable: true),
                    IntoleranceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_Accounts_Allergy_AllergiesAllergyId",
                        column: x => x.AllergiesAllergyId,
                        principalTable: "Allergy",
                        principalColumn: "AllergyId");
                    table.ForeignKey(
                        name: "FK_Accounts_Diabetes_DiabetesId",
                        column: x => x.DiabetesId,
                        principalTable: "Diabetes",
                        principalColumn: "DiabetesId");
                    table.ForeignKey(
                        name: "FK_Accounts_Intolerance_IntoleranceId",
                        column: x => x.IntoleranceId,
                        principalTable: "Intolerance",
                        principalColumn: "IntoleranceId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AllergiesAllergyId",
                table: "Accounts",
                column: "AllergiesAllergyId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_DiabetesId",
                table: "Accounts",
                column: "DiabetesId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_IntoleranceId",
                table: "Accounts",
                column: "IntoleranceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Allergy");

            migrationBuilder.DropTable(
                name: "Diabetes");

            migrationBuilder.DropTable(
                name: "Intolerance");
        }
    }
}
