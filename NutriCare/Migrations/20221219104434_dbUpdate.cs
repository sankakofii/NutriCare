using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutriCare.Migrations
{
    /// <inheritdoc />
    public partial class dbUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Allergy_AllergiesAllergyId",
                table: "Accounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Allergy",
                table: "Allergy");

            migrationBuilder.RenameTable(
                name: "Allergy",
                newName: "Allergies");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Allergies",
                table: "Allergies",
                column: "AllergyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Allergies_AllergiesAllergyId",
                table: "Accounts",
                column: "AllergiesAllergyId",
                principalTable: "Allergies",
                principalColumn: "AllergyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Allergies_AllergiesAllergyId",
                table: "Accounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Allergies",
                table: "Allergies");

            migrationBuilder.RenameTable(
                name: "Allergies",
                newName: "Allergy");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Allergy",
                table: "Allergy",
                column: "AllergyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Allergy_AllergiesAllergyId",
                table: "Accounts",
                column: "AllergiesAllergyId",
                principalTable: "Allergy",
                principalColumn: "AllergyId");
        }
    }
}
