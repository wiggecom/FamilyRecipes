using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyRecipes.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovedIsMetricalfromRecipe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMetrical",
                table: "RecipeIngredients");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMetrical",
                table: "RecipeIngredients",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
