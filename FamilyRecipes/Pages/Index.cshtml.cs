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
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        [BindProperty] public List<ElectricityPrice> priceList { get; set; } = new List<ElectricityPrice>();
        [BindProperty] public List<ElectricityPrice> priceListTomorrow { get; set; } = new List<ElectricityPrice>();

        public async Task OnGetAsync()
        {
            if (!_context.Categories.Any() || !_context.Units.Any() || !_context.Ingredients.Any() || !_context.Recipes.Any())
            {
                var seeds = new DbSeeds(_context, _calculations);
                await seeds.SeedingDataAsync();
            }

            priceList = await Electricity.GetPrice(0);
            if (DateTime.Now.Hour >= 14) 
            {
                priceListTomorrow = await Electricity.GetPrice(1);
            }
        }
    }
}


