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

        public static int CalculateTotalCalories(int calcAmount, int calcIngredientCalories)
        {
            float ingredientCaloriesFloat = (float)calcIngredientCalories/100;
            //if (calcIngredientCalories > 0) return (int)Math.Round((ingredientCaloriesFloat * calcAmount),0); // Early return
            if (calcIngredientCalories > 0) 
            { 
                int myNum = (int)Math.Round((ingredientCaloriesFloat * calcAmount), 0); // Early return

                return myNum;
            }
            return 0;
        }
    }
}