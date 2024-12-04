namespace FamilyRecipes.Models
{
    public class Ingredient
    {
        // Name, Type, Calories, Description
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Calories { get; set; } // per 100g
        public bool IsMeasuredByVolume { get; set; } // Helps selecting available measures
        public Ingredient() { }

        // Ingredient types are 
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
                // Additives and Spices
                "Herb",
                "Bullion",
                "Chemical",
                "Coloring",
                "Sweetener",
                "Flavor",
                "Essential Oil",
                "Extract",
                "Concentrate"
            };
            return ingredientTypes.OrderBy(s => s).ToList();
        }

        public static List<Ingredient> GetIngredients()
        {
            List<Ingredient> ingredients = new List<Ingredient>
            {
                new Ingredient { Name="Potato", Calories=77, Type="Vegetable", IsMeasuredByVolume=false},
                new Ingredient { Name="Milk", Calories=42, Type="Dairy", IsMeasuredByVolume=true},
                new Ingredient { Name="Cocoa Powder", Calories=228, Type="Flavor", IsMeasuredByVolume=true},
                new Ingredient { Name="Chicken", Calories=239, Type="Poultry", IsMeasuredByVolume=false },
                new Ingredient { Name="Mixed Vegetables", Calories=60, Type="Vegetable", IsMeasuredByVolume=false },
                new Ingredient { Name="Cream", Calories=196, Type="Dairy", IsMeasuredByVolume=true }
            };

            return ingredients.OrderBy(c => c.Type).ThenBy(c => c.Name).ToList();
        }
    }
}
