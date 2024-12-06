using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FamilyRecipes.Models;
using FamilyRecipes.Helpers;

namespace FamilyRecipes.Pages
{
    public class IndexModel : PageModel
    {
        //private readonly ILogger<IndexModel> _logger;

        //public IndexModel(ILogger<IndexModel> logger)
        //{
        //    _logger = logger;
        //}
        private readonly Data.ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public IndexModel(Data.ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        [BindProperty] public List<Unit> Units { get; set; } = Models.Unit.GetUnits();
        [BindProperty] public List<Category> Categories { get; set; } = new List<Category>();
        [BindProperty] public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        [BindProperty] public Recipe MyRecipe { get; set; } = new Recipe();
        public string success = "Boo!";
        public async void OnGet()
        {
            foreach (var unit in Units)
            {
                System.Diagnostics.Debug.Write(unit.Name);
                if (unit.IsMetrical) System.Diagnostics.Debug.WriteLine(" is a metrical measure");
                if (!unit.IsMetrical) System.Diagnostics.Debug.WriteLine(" is an imperial measure");
            }

            if (!_context.Categories.Any() || !_context.Units.Any() || !_context.Ingredients.Any() || !_context.Recipes.Any())
            {
                var seeds = new DbSeeds(_context);
                await seeds.SeedingDataAsync();
            }

            // Example of IsNullOrWhiteSpace
            //if (!string.IsNullOrWhiteSpace(MyRecipe.Title)) 
            //{
            //    System.Diagnostics.Debug.WriteLine(MyRecipe.Title);
            //}

            //if (string.IsNullOrEmpty(success)) { } //will trigger on null and "" but not " "
            //if (string.IsNullOrWhiteSpace(success)) { } //will trigger on null, "" and " "

            // Used?
            Categories = _context.Categories.ToList();
            Ingredients = _context.Ingredients.ToList();
            MyRecipe = _context.Recipes.FirstOrDefault();
            if (MyRecipe != null) { success = "Great Succses!"; }
            //

        }
    }
}

