namespace FamilyRecipes.Models
{
    public class RecipeIngredient
    {
        // RecipeIngredient: [Ingredient], Unit, Amount, Calories
        public int Id { get; set; }
        public Ingredient Ingredient { get; set; }
        public string Unit { get; set; }
        public bool IsMetrical { get; set; }
        public int Amount { get; set; }
        public int TotalCalories { get; set; }

        public RecipeIngredient()
        {

        }

        public static int CalculateTotalCalories(int calcAmount, int calcIngredientCalories)
        {
            if (calcIngredientCalories > 0) return (calcIngredientCalories / 100) * calcAmount; // Early return
            return 0;
        }
    }
}