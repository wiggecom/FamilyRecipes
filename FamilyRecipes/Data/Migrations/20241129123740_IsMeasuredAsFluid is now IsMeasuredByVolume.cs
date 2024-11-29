using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyRecipes.Data.Migrations
{
    /// <inheritdoc />
    public partial class IsMeasuredAsFluidisnowIsMeasuredByVolume : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsMeasuredAsFluid",
                table: "Ingredients",
                newName: "IsMeasuredByVolume");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsMeasuredByVolume",
                table: "Ingredients",
                newName: "IsMeasuredAsFluid");
        }
    }
}
