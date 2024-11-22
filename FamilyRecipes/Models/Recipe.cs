namespace FamilyRecipes.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string UserName { get; set; } // FamilyUser.Name
        public DateTime CreatedDate { get; set; }
        public Category Category { get; set; }
        public int TimeRequired { get; set; }
        public string Description { get; set; }
        public List<string> Steps { get; set; }
        public List<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
        public List<UserRating> UserRatings { get; set; } = new List<UserRating>();
        public bool AdultsOnly { get; set; } = false;
        public string Image { get; set; }

        public Recipe()
        {
            
        }

        public static int CalculateTotalCalories(int calcAmount, int calcIngredientCalories)
        {
            if (calcIngredientCalories > 0) return (calcIngredientCalories / 100) * calcAmount; // Early return
            return 0;
        }
    }
}
