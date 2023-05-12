using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutriCare.Migrations
{
    /// <inheritdoc />
    public partial class IntoleranceIngredientsCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IntoleranceIngredient",
                columns: table => new
                {
                    IntoleranceIngredientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IntoleranceIngredientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IntoleranceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntoleranceIngredient", x => x.IntoleranceIngredientId);
                    table.ForeignKey(
                        name: "FK_IntoleranceIngredient_Intolerances_IntoleranceId",
                        column: x => x.IntoleranceId,
                        principalTable: "Intolerances",
                        principalColumn: "IntoleranceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IntoleranceIngredient_IntoleranceId",
                table: "IntoleranceIngredient",
                column: "IntoleranceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IntoleranceIngredient");
        }
    }
}
