using System.Text.Json.Serialization;

namespace FamilyRecipes.Models
{
    public class RecipeIngredientMapSource
    {

        [JsonPropertyName("IngredientName")] public string IngredientName { get; set; }

        [JsonPropertyName("UnitName")] public string UnitName { get; set; }

        [JsonPropertyName("Amount")] public string Amount { get; set; }
    }
}
