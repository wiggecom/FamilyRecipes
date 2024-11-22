namespace FamilyRecipes.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string User { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Category { get; set; } // Add Category-object
        public int TimeRequired { get; set; }
        public string Description { get; set; }
        public List<string> Steps { get; set; }
        public List<RecipeIngredient> Ingredients { get; set; } = new List<RecipeIngredient>();
        public List<UserRating> UserRatings { get; set; } = new List<UserRating>();
        public int AgeCheck { get; set; }
        public string Image { get; set; }

        public Recipe()
        {
            
        }
    }
}
