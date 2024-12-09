namespace FamilyRecipes.Models
{
    public class RecipeIngredient
    {
        // RecipeIngredient: [Ingredient], Unit, Amount, Calories
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public Ingredient Ingredient { get; set; }
        public int IngredientId { get; set; }
        public Unit Unit { get; set; }
        public int UnitId { get; set; }
        public int Amount { get; set; }
        public int TotalCalories { get; set; }

        public RecipeIngredient()
        {

        }
    }
}