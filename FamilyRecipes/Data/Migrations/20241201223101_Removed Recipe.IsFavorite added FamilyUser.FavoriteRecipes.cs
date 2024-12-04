using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyRecipes.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovedRecipeIsFavoriteaddedFamilyUserFavoriteRecipes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFavorite",
                table: "Recipes");

            migrationBuilder.AddColumn<string>(
                name: "FavoriteRecipes",
                table: "FamilyUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FavoriteRecipes",
                table: "FamilyUsers");

            migrationBuilder.AddColumn<bool>(
                name: "IsFavorite",
                table: "Recipes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
