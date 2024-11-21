using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FamilyRecipes.Models;

namespace FamilyRecipes.Pages
{
    public class IndexModel : PageModel
    {
        //private readonly ILogger<IndexModel> _logger;

        //public IndexModel(ILogger<IndexModel> logger)
        //{
        //    _logger = logger;
        //}

        [BindProperty] public List<Unit> Units { get; set; } = Models.Unit.GetUnits();
        public void OnGet()
        {
            foreach (var unit in Units)
            {
                System.Diagnostics.Debug.Write(unit.Name);
                if (unit.IsMetrical) System.Diagnostics.Debug.WriteLine(" is a metrical measure");
                if (!unit.IsMetrical) System.Diagnostics.Debug.WriteLine(" is an imperial measure");
            }


        }
    }
}

