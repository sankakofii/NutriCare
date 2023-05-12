using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutriCare.Migrations
{
    /// <inheritdoc />
    public partial class UpdateScan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScanHistories_Accounts_AccountId",
                table: "ScanHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_ScanHistories_Product_ProductId",
                table: "ScanHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScanHistories",
                table: "ScanHistories");

            migrationBuilder.RenameTable(
                name: "ScanHistories",
                newName: "Scans");

            migrationBuilder.RenameIndex(
                name: "IX_ScanHistories_ProductId",
                table: "Scans",
                newName: "IX_Scans_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ScanHistories_AccountId",
                table: "Scans",
                newName: "IX_Scans_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Scans",
                table: "Scans",
                column: "ScanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Scans_Accounts_AccountId",
                table: "Scans",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Scans_Product_ProductId",
                table: "Scans",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scans_Accounts_AccountId",
                table: "Scans");

            migrationBuilder.DropForeignKey(
                name: "FK_Scans_Product_ProductId",
                table: "Scans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Scans",
                table: "Scans");

            migrationBuilder.RenameTable(
                name: "Scans",
                newName: "ScanHistories");

            migrationBuilder.RenameIndex(
                name: "IX_Scans_ProductId",
                table: "ScanHistories",
                newName: "IX_ScanHistories_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Scans_AccountId",
                table: "ScanHistories",
                newName: "IX_ScanHistories_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScanHistories",
                table: "ScanHistories",
                column: "ScanId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScanHistories_Accounts_AccountId",
                table: "ScanHistories",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScanHistories_Product_ProductId",
                table: "ScanHistories",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
