using FamilyRecipes.Helpers;
using FamilyRecipes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FamilyRecipes.Pages
{
    public class addrecipeModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public addrecipeModel(Data.ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        public List<Unit> Units { get; set; } = Models.Unit.GetUnits();
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public Recipe MyRecipe { get; set; } = new Recipe();
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
            MyRecipe = _context.Recipes
                    .Include(r => r.RecipeIngredients)
                    .ThenInclude(ri => ri.Ingredient) // Include Ingredient
                    .Include(r => r.RecipeIngredients)
                    .ThenInclude(ri => ri.Unit)      // Include Unit
                    .FirstOrDefault();


            Ingredients = _context.Ingredients.ToList();
            if (MyRecipe != null)
            {
                Thread.Sleep(1000);
            }

        }
    }
}

