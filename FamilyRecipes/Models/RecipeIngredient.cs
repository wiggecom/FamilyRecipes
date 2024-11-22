namespace FamilyRecipes.Models
{
    public class RecipeIngredient
    {
        // RecipeIngredient: [Ingredient], Unit, Amount, Calories
        public int Id { get; set; }
        public Ingredient Ingredient { get; set; }
        public string Unit {  get; set; }
        public bool IsMetrical { get; set; }
        public int Amount { get; set; }
        public int Calories { get; set; }

        //public RecipeIngredient(Ingredient ingredient, string unit, bool isMetrical, int amount)
        //{
        //    Ingredient = ingredient;
        //    Unit = unit;
        //    IsMetrical = isMetrical;
        //    Amount = amount;
        //    Calories = CalculateCalories(amount, ingredient.Calories);
        //}

        public RecipeIngredient()
        {
            //Ingredient = ingredient;
            //Unit = unit;
            //IsMetrical = isMetrical;
            //Amount = amount;
            //Calories = CalculateCalories(amount, ingredient.Calories);
        }

        public static int CalculateCalories(int calcAmount, int calcCalories)
        {
            if (calcCalories > 0) return (calcCalories / 100) * calcAmount; // Early return
            return 0;
        }
    }
}
