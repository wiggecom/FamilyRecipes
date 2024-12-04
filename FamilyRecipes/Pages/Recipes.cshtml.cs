using FamilyRecipes.Helpers;
using FamilyRecipes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FamilyRecipes.Pages
{
    public class RecipesModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public RecipesModel(Data.ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        public List<Unit> Units { get; set; } = Models.Unit.GetUnits();
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public Recipe MyRecipe { get; set; } = new Recipe();
        public bool HasCaloriesAny { get; set; } = false;


        public string success = "Boo!";
        public async void OnGet()
        {
            foreach (var unit in Units)
            {
                System.Diagnostics.Debug.Write(unit.Name);
                if (unit.IsMetrical) System.Diagnostics.Debug.WriteLine(" is a metrical measure");
                if (!unit.IsMetrical) System.Diagnostics.Debug.WriteLine(" is an imperial measure");
            }

            if (!_context.Categories.Any())
            {
                var seeds = new DbSeeds(_context);
                await seeds.SeedingDataAsync();
            }

            Categories = _context.Categories.ToList();
            Recipe? firstRecipe = _context.Recipes
                    .Include(r => r.RecipeIngredients)
                    .ThenInclude(ri => ri.Ingredient) // Include Ingredient
                    .Include(r => r.RecipeIngredients)
                    .ThenInclude(ri => ri.Unit)      // Include Unit
                    .FirstOrDefault();
            if (firstRecipe != null)
            {
                MyRecipe = firstRecipe;
                foreach (var ri in MyRecipe.RecipeIngredients)
                {
                    if(ri.TotalCalories > 0) { HasCaloriesAny = true; break; }
                }
            }

            Ingredients = _context.Ingredients.ToList();
            if (MyRecipe != null)
            {
                Thread.Sleep(1000);
            }

        }
    }
}
