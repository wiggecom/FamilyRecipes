using FamilyRecipes.Data;
using FamilyRecipes.Helpers;
using FamilyRecipes.Models;
using FamilyRecipes.Interfaces;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text.Json;


namespace FamilyRecipes.Pages
{
    public class AddRecipeModel : PageModel

    {
        private readonly Data.ApplicationDbContext _context;
        private readonly ICalculations _calculations;
        private readonly IWebHostEnvironment webHostEnvironment;
        private ApplicationDbContext @object;
        private ApplicationDbContext context;

        public AddRecipeModel(Data.ApplicationDbContext context, IWebHostEnvironment webHostEnvironment,
            ICalculations calculations)
        {
            _context = context;
            _calculations = calculations;
            this.webHostEnvironment = webHostEnvironment;
        }

        public List<Unit> Units { get; set; } = Models.Unit.GetUnits();
        public List<string> DistinctMainCategories { get; set; } = new List<string>();
        public Category AddCategory { get; set; }
        public List<string> IngredientTypes { get; set; } = new List<string>();

        [BindProperty] public string AddTitle { get; set; }
        [BindProperty] public List<RecipeIngredientMapSource> RecipeIngredientsMapSource { get; set; } = new List<RecipeIngredientMapSource>();
        [BindProperty] public string AddMainCategory { get; set; }
        [BindProperty] public string AddSubCategory { get; set; }
        [BindProperty] public int AddTimeRequired { get; set; }
        [BindProperty] public int AddServings { get; set; }
        [BindProperty] public string AddDescription { get; set; }
        [BindProperty] public List<string> AddSteps { get; set; } = new List<string>();
        [BindProperty] public bool AddAdultsOnly { get; set; }
        [BindProperty] public string AddImage { get; set; }

        // RecipeIngredients
        public List<RecipeIngredient> AddRecipeIngredients { get; set; } = new List<RecipeIngredient>();
        public string AddRI_IngredientName { get; set; }
        public string AddRI_UnitName { get; set; }
        public int AddRI_Amount { get; set; }

        public void OnGet()
        {
            DistinctMainCategories = _context.Categories.Select(c => c.MainCategory).Distinct().ToList();
            IngredientTypes = Ingredient.GetIngredientTypes();
        }

        public async Task<IActionResult> OnPostAddRecipe(string ingredientList)
        {
            if (string.IsNullOrEmpty(ingredientList))
            {
                return BadRequest("No ingredients provided.");
            }

          
            

            List<Category> allCategories = _context.Categories.ToList();
            List<Ingredient> allIngredients = _context.Ingredients.ToList();
            List<Unit> allUnits = _context.Units.ToList();
            List<Recipe> allRecipes = _context.Recipes.ToList();
            Recipe thisRecipe = new Recipe();
            FamilyUser thisUser = FamilyUser.GetFakeUser("Greger Man", true);

            // Deserialize the ingredient list from JSON
            var ingredients = JsonSerializer.Deserialize<List<RecipeIngredientMapSource>>(ingredientList);
           
            thisRecipe.Title = AddTitle;
            thisRecipe.UserName = thisUser.Name;
            thisRecipe.CreatedDate = DateTime.Now;
            thisRecipe.Category = allCategories.Where(c => c.SubCategory == AddSubCategory).FirstOrDefault();
            thisRecipe.TimeRequired = AddTimeRequired;
            thisRecipe.Servings = AddServings;
            thisRecipe.Description = AddDescription;
            thisRecipe.Image = AddImage;
            thisRecipe.AdultsOnly = AddAdultsOnly;

            foreach(string s in AddSteps)
            {
                thisRecipe.Steps.Add(s);
            }

            foreach (RecipeIngredientMapSource i in ingredients)
            {
                RecipeIngredient r = new RecipeIngredient();
                r.Ingredient = allIngredients.Where(a => a.Name == i.IngredientName).FirstOrDefault();
                r.Unit = allUnits.Where(u => u.Name == i.UnitName).FirstOrDefault();
                r.Amount = int.Parse(i.Amount);
                thisRecipe.RecipeIngredients.Add(r);
                r.TotalCalories = _calculations.CalculateTotalCalories(r.Ingredient.Name, r.Unit.Name, r.Amount);
            }

            try
            {
                _context.Recipes.Add(thisRecipe);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                List<string> errors = new List<string>(); 
                foreach (string s in errors) { errors.Add(s); }
                throw;
            }
            

                return RedirectToAction("/AddRecipe");
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

        public JsonResult OnGetGetIngredientsByType(string ingredientType)
        {
            List<string> ingredientsOfType = _context.Ingredients
                .Where(i => i.Type == ingredientType).Select(i => i.Name)
                .ToList();

            return new JsonResult(ingredientsOfType);
        }

        public JsonResult OnGetGetUnitsByType(string ingredientName)
        {
            List<string> unitsByType = new List<string>();
            //FamilyUser currentUser = FamilyUser.GetFakeAdmin();
            //FamilyUser currentUser = FamilyUser.GetFakeUser("Ms Moneypenny", false);
            FamilyUser currentUser = FamilyUser.GetFakeUser("Mr Greger", true);
            Ingredient chosenIngredient = new Ingredient();
            chosenIngredient = _context.Ingredients.Where(i => i.Name == ingredientName).FirstOrDefault();

            if (chosenIngredient.IsMeasuredByVolume)
            {
                unitsByType = _context.Units
                .Where(x => x.IsVolume == true
                && x.IsMetrical == currentUser.PreferMetrical)
                .OrderBy(x => x.InMl)
                .Select(x => x.Name).ToList();
            }
            else
            {
                unitsByType = _context.Units
                .Where(x => x.IsVolume == false
                && x.IsMetrical == currentUser.PreferMetrical)
                .OrderBy(x => x.InGr)
                .Select(x => x.Name).ToList();
                if (!currentUser.PreferMetrical)
                {
                    unitsByType.Add(_context.Units.Where(y => y.Name == "Number").Select(y => y.Name).FirstOrDefault());
                }
            }
            return new JsonResult(unitsByType);
        }

        // AddRecipeIngredientList
        public JsonResult OnPostAddRecipeIngredientList([FromBody] AddRecipeIngredientListModel model)
        {
            if (model == null)
            {
                return new JsonResult("Model is null");
            }

            if (model.Ingredients == null)
            {
                return new JsonResult("Ingredients list is null");
            }


            // Process the received data
            foreach (var item in model.Ingredients)
            {
                // Guard-clause, Reversed If-Statement, Early Return
                if (string.IsNullOrWhiteSpace(item.IngredientName)) return new JsonResult(model.Ingredients);
                if (string.IsNullOrWhiteSpace(item.UnitName)) return new JsonResult(model.Ingredients);
                if (item.Amount == null || item.Amount == "0") return new JsonResult(model.Ingredients);

                RecipeIngredientMapSource thisIngredient = new RecipeIngredientMapSource();
                thisIngredient.IngredientName = item.IngredientName;
                thisIngredient.UnitName = item.UnitName;
                thisIngredient.Amount = item.Amount;
                RecipeIngredientsMapSource.Add(thisIngredient);

            }

            // Return the updated list
            return new JsonResult(RecipeIngredientsMapSource);
        }

        // Helper class for deserialization
        public class RecipeIngredientListModel
        {
            public List<RecipeIngredient> IngredientList { get; set; }
            public string UnitName { get; set; }
            public string IngredientName { get; set; }
        }

        public class AddRecipeIngredientListModel
        {
            public List<RecipeIngredientMapSource> Ingredients { get; set; }
        }

    }
}
