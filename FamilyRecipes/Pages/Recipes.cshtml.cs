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

        public List<Unit> Units { get; set; } = Unit.GetUnits();
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public Recipe MyRecipe { get; set; } = new Recipe();
        public bool HasCaloriesAny { get; set; } = false;


        public void OnGet()
        {
            Categories = _context.Categories.ToList();
            Ingredients = _context.Ingredients.ToList();

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
        }

        public JsonResult OnGetGetSubcategories(string mainCategory)
        {
            // Use _context to fetch subcategories for the selected main category
            var subcategories = _context.Categories
                .Where(c => c.MainCategory == mainCategory)
                .Select(c => c.SubCategory)
                .ToList();

            return new JsonResult(subcategories);
        }

        public JsonResult OnGetGetRecipes(string subCategory)
        {
            // Use _context to fetch subcategories for the selected main category
            var recipes = _context.Recipes
                .Include(c => c.Category)
                .Where(c => c.Category.SubCategory == subCategory)
                .Select(c => c.Title)
                .ToList();

            return new JsonResult(recipes);
        }

        public JsonResult OnGetGetSelectRecipe(string selectRecipe)
        {
            // Use _context to fetch subcategories for the selected main category
            var recipe = _context.Recipes
                .Include(c => c.Category)
                .Include(c => c.RecipeIngredients)
                .ThenInclude(c => c.Ingredient)
                .Include(c => c.RecipeIngredients)
                .ThenInclude(c => c.Unit)
                .FirstOrDefault();

            return new JsonResult(recipe);
        }
    }
}
