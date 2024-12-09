using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FamilyRecipes.Models;
using FamilyRecipes.Helpers;
using FamilyRecipes.Interfaces;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;
using static System.Net.WebRequestMethods;
using System.Net.Http;
using System.Text.Json;

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
        private readonly ICalculations _calculations;
        private readonly HttpClient _httpClient;

        public IndexModel(Data.ApplicationDbContext context, IWebHostEnvironment webHostEnvironment,
            ICalculations calculations, HttpClient httpClient)
        {
            _context = context;
            _calculations = calculations;
            _httpClient = httpClient;
            this.webHostEnvironment = webHostEnvironment;
            _httpClient.Timeout = TimeSpan.FromSeconds(120);
        }

        [BindProperty] public List<Unit> Units { get; set; } = Models.Unit.GetUnits();
        [BindProperty] public List<Category> Categories { get; set; } = new List<Category>();
        [BindProperty] public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        [BindProperty] public Recipe MyRecipe { get; set; } = new Recipe();
        public string success = "Boo!";

        public dynamic ApiData { get; private set; } // GetPrice()

        [BindProperty] public List<ElectricityPrice> priceList { get; set; } = new List<ElectricityPrice>();
        [BindProperty] public List<ElectricityPrice> priceListTomorrow { get; set; } = new List<ElectricityPrice>();

        public async Task OnGetAsync()
        {
            if (!_context.Categories.Any() || !_context.Units.Any() || !_context.Ingredients.Any() || !_context.Recipes.Any())
            {
                var seeds = new DbSeeds(_context, _calculations);
                await seeds.SeedingDataAsync();
            }

            // Used?
            Categories = _context.Categories.ToList();
            Ingredients = _context.Ingredients.ToList();
            MyRecipe = _context.Recipes.FirstOrDefault();
            if (MyRecipe != null) { success = "Great Succses!"; }

            //await GetPriceAsync();
            priceList = await Electricity.GetPrice(0);
            if (DateTime.Now.Hour >= 14) 
            {
                priceListTomorrow = await Electricity.GetPrice(1);
            }
            foreach (ElectricityPrice price in priceList)
            {
                System.Diagnostics.Debug.WriteLine(price.time_start.ToString("HH") + " - " + price.SEK_per_kWh);
            }

            if (true)
            {
                Thread.Sleep(10);

            }

        }
    }
}


// Example of IsNullOrWhiteSpace
//if (!string.IsNullOrWhiteSpace(MyRecipe.Title)) 
//{
//    System.Diagnostics.Debug.WriteLine(MyRecipe.Title);
//}

//if (string.IsNullOrEmpty(success)) { } //will trigger on null and "" but not " "
//if (string.IsNullOrWhiteSpace(success)) { } //will trigger on null, "" and " "

