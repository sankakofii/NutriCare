using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutriCare.Migrations
{
    /// <inheritdoc />
    public partial class NewScanAndProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ScanHistoryId",
                table: "ScanHistories",
                newName: "ScanId");

            migrationBuilder.AddColumn<string>(
                name: "Allergens",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AllergensFromIngredients",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageFrontUrl",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageNutritionUrl",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Product_name",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Allergens",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "AllergensFromIngredients",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ImageFrontUrl",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ImageNutritionUrl",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Product_name",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "ScanId",
                table: "ScanHistories",
                newName: "ScanHistoryId");
        }
    }
}
