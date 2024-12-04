using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyRecipes.Data.Migrations
{
    /// <inheritdoc />
    public partial class Unit_IsFluid__to__Unit_IsVolume : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsFluid",
                table: "Units",
                newName: "IsVolume");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsVolume",
                table: "Units",
                newName: "IsFluid");
        }
    }
}
