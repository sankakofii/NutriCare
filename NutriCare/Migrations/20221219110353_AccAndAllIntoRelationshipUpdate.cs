using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutriCare.Migrations
{
    /// <inheritdoc />
    public partial class AccAndAllIntoRelationshipUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Allergies_Accounts_AccountId",
                table: "Allergies");

            migrationBuilder.DropForeignKey(
                name: "FK_Intolerance_Accounts_AccountId",
                table: "Intolerance");

            migrationBuilder.DropIndex(
                name: "IX_Allergies_AccountId",
                table: "Allergies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Intolerance",
                table: "Intolerance");

            migrationBuilder.DropIndex(
                name: "IX_Intolerance_AccountId",
                table: "Intolerance");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Allergies");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Intolerance");

            migrationBuilder.RenameTable(
                name: "Intolerance",
                newName: "Intolerances");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Intolerances",
                table: "Intolerances",
                column: "IntoleranceId");

            migrationBuilder.CreateTable(
                name: "AccountAllergy",
                columns: table => new
                {
                    AccountsAccountId = table.Column<int>(type: "int", nullable: false),
                    AllergiesAllergyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountAllergy", x => new { x.AccountsAccountId, x.AllergiesAllergyId });
                    table.ForeignKey(
                        name: "FK_AccountAllergy_Accounts_AccountsAccountId",
                        column: x => x.AccountsAccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountAllergy_Allergies_AllergiesAllergyId",
                        column: x => x.AllergiesAllergyId,
                        principalTable: "Allergies",
                        principalColumn: "AllergyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountIntolerance",
                columns: table => new
                {
                    AccountsAccountId = table.Column<int>(type: "int", nullable: false),
                    IntolerancesIntoleranceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountIntolerance", x => new { x.AccountsAccountId, x.IntolerancesIntoleranceId });
                    table.ForeignKey(
                        name: "FK_AccountIntolerance_Accounts_AccountsAccountId",
                        column: x => x.AccountsAccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountIntolerance_Intolerances_IntolerancesIntoleranceId",
                        column: x => x.IntolerancesIntoleranceId,
                        principalTable: "Intolerances",
                        principalColumn: "IntoleranceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountAllergy_AllergiesAllergyId",
                table: "AccountAllergy",
                column: "AllergiesAllergyId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountIntolerance_IntolerancesIntoleranceId",
                table: "AccountIntolerance",
                column: "IntolerancesIntoleranceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountAllergy");

            migrationBuilder.DropTable(
                name: "AccountIntolerance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Intolerances",
                table: "Intolerances");

            migrationBuilder.RenameTable(
                name: "Intolerances",
                newName: "Intolerance");

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Allergies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Intolerance",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Intolerance",
                table: "Intolerance",
                column: "IntoleranceId");

            migrationBuilder.CreateIndex(
                name: "IX_Allergies_AccountId",
                table: "Allergies",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Intolerance_AccountId",
                table: "Intolerance",
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
    }
}
