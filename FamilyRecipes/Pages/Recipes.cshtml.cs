using FamilyRecipes.Helpers;
using FamilyRecipes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.Json;

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
        public List<Recipe> AllRecipes { get; set; } = new List<Recipe>() { };
        public Recipe MyRecipe { get; set; } = new Recipe();
        public bool HasCaloriesAny { get; set; } = false;
        [BindProperty ]public FamilyUser CurrentUser { get; set; } = new FamilyUser();
        public Family Family { get; set; } = new Family();
        public string? UserId { get; set; }

        public void OnGet()
        {
            // Demo Family //
            Family.Members.Add(FamilyUser.GetFakeAdmin()); // Admin is adult, admin
            Family.Members.Add(FamilyUser.GetFakeUser("Lisa", true)); // Lisa is adult, not admin, likes metrical
            Family.Members.Add(FamilyUser.GetFakeChild("Leifi")); // Leifi is not adult or admin
            Family.Members[1].IsLoggedIn = true; // Logging in Lisa

            if (User?.Identity.IsAuthenticated == true)
            {
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            else
            {
                UserId = "";
            }

            // Check which FamilyUser IsLoggedIn
            foreach (var u in Family.Members) 
            { 
                if (u.IsLoggedIn) { CurrentUser = u; break; }
            }

            Categories = _context.Categories.ToList();
            Ingredients = _context.Ingredients.ToList();
            AllRecipes = _context.Recipes.ToList();
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
            if (!string.IsNullOrWhiteSpace(CurrentUser.Name))
            {
                if (CurrentUser.IsAdult)
                {
                    return new JsonResult(recipes);
                }
                else
                {
                    // Refill list with recipes != AdultsOnly
                    recipes.Clear();
                    recipes = _context.Recipes
                    .Include(c => c.Category)
                    .Where(c => c.Category.SubCategory == subCategory)
                    .Where(c => c.AdultsOnly == false)
                    .Select(c => c.Title)
                    .ToList();
                    return new JsonResult(recipes);
                }
            }
            else 
            { 
            return new JsonResult(recipes);
            }

        }

        public JsonResult OnGetGetSelectRecipe(string selectRecipe)
        {
            Recipe recipe = new Recipe();
                recipe = _context.Recipes
                .Where(c => c.Title == selectRecipe)
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
