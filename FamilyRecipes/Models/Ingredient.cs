namespace FamilyRecipes.Models
{
    public class Ingredient
    {
        // Name, Type, Calories, Description
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int Calories { get; set; } = 0; // per 100g
        public Ingredient() { }
    }
}
