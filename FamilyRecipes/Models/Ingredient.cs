namespace FamilyRecipes.Models
{
    public class Ingredient
    {
        // Name, Type, Calories, Description
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int Calories { get; set; } // per 100g
        public bool IsMeasuredByVolume { get; set; } // Helps selecting available measures
        public Ingredient() { }

        public static List<string> GetIngredientTypes()
        {
            var ingredientTypes = new List<string> {
                // Animal products
                "Meat",
                "Poultry",
                "Fish", 
                "Seafood",
                "Egg",
                "Dairy",
                // Grown products
                "Nut/Seed",
                "Bean",
                "Fruit/Berry",
                "Vegetable",
                "Wheat",
                "Legume",
                "Grain",
                // Misc
                "Snack",
                "Beverage",
                "Condiment",
                "Alcohol",
                "Other",
                // Basics
                "Salt",
                "Sugar",
                "Water",
                "Oil",
                "Condiment",
                // Additives and Spices
                "Herb",
                "Bullion",
                "Chemical",
                "Coloring",
                "Flavor",
                "Essential Oil",
                "Extract",
                "Concentrate"
            };
            return ingredientTypes;
        }
    }
}
