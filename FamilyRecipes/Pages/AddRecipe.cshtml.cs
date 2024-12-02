using FamilyRecipes.Helpers;
using FamilyRecipes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace FamilyRecipes.Pages
{
    public class AddRecipeModel : PageModel

    {
        private readonly Data.ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public AddRecipeModel(Data.ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        public List<Unit> Units { get; set; } = Models.Unit.GetUnits();
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Category> AllCategories { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public Recipe? MyRecipe { get; set; } = new Recipe();
        public Recipe? AddRecipe { get; set; } = new Recipe();
        public Category AddCategory { get; set; }

        [BindProperty] public string AddTitle { get; set; }
        [BindProperty] public string AddMainCategory { get; set; }
        [BindProperty] public string AddSubCategory { get; set; }
        [BindProperty] public int AddTimeRequired { get; set; }
        [BindProperty] public int AddServings { get; set; }
        [BindProperty] public string AddDescription { get; set; }
        [BindProperty] public List<string> AddSteps { get; set; } = new List<string>();
        [BindProperty] public List<RecipeIngredient> AddRecipeIngredients { get; set; } = new List<RecipeIngredient>();
        [BindProperty] public bool AddAdultsOnly { get; set; }
        [BindProperty] public string AddImage { get; set; }

        // public string UserName { get; set; } // FamilyUser.Name
        // public DateTime CreatedDate { get; set; }
        // public List<UserRating> UserRatings { get; set; } = new List<UserRating>();

        public void OnGet()
        {
            Categories = _context.Categories.ToList();
            AllCategories = _context.Categories.ToList();
            Ingredients = _context.Ingredients.ToList();

            if (true)
            {
                Thread.Sleep(1000);
                return;
            }

        }

        public void OnPostAddRecipe()
        {
            //Categories = _context.Categories.ToList();
            //AllCategories = _context.Categories.ToList();
            //Ingredients = _context.Ingredients.ToList();

            if (true)
            {
                Thread.Sleep(1000);
                return;
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

        public JsonResult OnGetGetIngredientByType(string ingredientType)
        {
            // Use _context to fetch subcategories for the selected main category
            var ingredientsOfType = _context.Ingredients
                .Where(i => i.Type == ingredientType)
                .ToList();

            return new JsonResult(ingredientsOfType);
        }
    }
}
