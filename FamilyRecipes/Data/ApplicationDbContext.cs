using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FamilyRecipes.Models;

namespace FamilyRecipes.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<UserRating> UserRatings { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<FamilyUser> FamilyUsers { get; set; }
    }
}
