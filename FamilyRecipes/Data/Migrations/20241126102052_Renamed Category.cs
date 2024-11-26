using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyRecipes.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenamedCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CategoryType",
                table: "Categories",
                newName: "SubCategory");

            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "Categories",
                newName: "MainCategory");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubCategory",
                table: "Categories",
                newName: "CategoryType");

            migrationBuilder.RenameColumn(
                name: "MainCategory",
                table: "Categories",
                newName: "CategoryName");
        }
    }
}
