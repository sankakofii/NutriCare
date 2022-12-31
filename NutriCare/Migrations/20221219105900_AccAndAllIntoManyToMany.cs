using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutriCare.Migrations
{
    /// <inheritdoc />
    public partial class AccAndAllIntoManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Allergies_AllergiesAllergyId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Intolerance_IntoleranceId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_AllergiesAllergyId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_IntoleranceId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "AllergiesAllergyId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "IntoleranceId",
                table: "Accounts");

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Intolerance",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Allergies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Intolerance_AccountId",
                table: "Intolerance",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Allergies_AccountId",
                table: "Allergies",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Allergies_Accounts_AccountId",
                table: "Allergies",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Intolerance_Accounts_AccountId",
                table: "Intolerance",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Allergies_Accounts_AccountId",
                table: "Allergies");

            migrationBuilder.DropForeignKey(
                name: "FK_Intolerance_Accounts_AccountId",
                table: "Intolerance");

            migrationBuilder.DropIndex(
                name: "IX_Intolerance_AccountId",
                table: "Intolerance");

            migrationBuilder.DropIndex(
                name: "IX_Allergies_AccountId",
                table: "Allergies");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Intolerance");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Allergies");

            migrationBuilder.AddColumn<int>(
                name: "AllergiesAllergyId",
                table: "Accounts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IntoleranceId",
                table: "Accounts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AllergiesAllergyId",
                table: "Accounts",
                column: "AllergiesAllergyId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_IntoleranceId",
                table: "Accounts",
                column: "IntoleranceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Allergies_AllergiesAllergyId",
                table: "Accounts",
                column: "AllergiesAllergyId",
                principalTable: "Allergies",
                principalColumn: "AllergyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Intolerance_IntoleranceId",
                table: "Accounts",
                column: "IntoleranceId",
                principalTable: "Intolerance",
                principalColumn: "IntoleranceId");
        }
    }
}
