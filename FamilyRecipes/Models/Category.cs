namespace FamilyRecipes.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string MainCategory { get; set; }
        public string SubCategory { get; set; }

        public Category()
        {
            
        }
        public static List<Category> GetCategories() 
        {
            List<Category> categories = new List<Category>{
                new Category { MainCategory = "World", SubCategory = "Swedish" },
                new Category { MainCategory = "World", SubCategory = "North American" },
                new Category { MainCategory = "World", SubCategory = "Caribbean" },
                new Category { MainCategory = "World", SubCategory = "South American" },
                new Category { MainCategory = "World", SubCategory = "Chinese" },
                new Category { MainCategory = "World", SubCategory = "Thai" },
                new Category { MainCategory = "World", SubCategory = "Japanese" },
                new Category { MainCategory = "World", SubCategory = "Filipino" },
                new Category { MainCategory = "World", SubCategory = "Brazilian" },
                new Category { MainCategory = "World", SubCategory = "Italian" },
                new Category { MainCategory = "World", SubCategory = "African" },
                new Category { MainCategory = "World", SubCategory = "Mexican" },
                new Category { MainCategory = "World", SubCategory = "French" },
                new Category { MainCategory = "World", SubCategory = "Spanish" },
                new Category { MainCategory = "World", SubCategory = "Indian" },
                new Category { MainCategory = "World", SubCategory = "Greek" },
                new Category { MainCategory = "World", SubCategory = "Arabic" },
                new Category { MainCategory = "World", SubCategory = "English" },
                new Category { MainCategory = "Holidays", SubCategory = "Christmas" },
                new Category { MainCategory = "Holidays", SubCategory = "Easter" },
                new Category { MainCategory = "Holidays", SubCategory = "Thanksgiving" },
                new Category { MainCategory = "Pizza", SubCategory = "Dough" },
                new Category { MainCategory = "Pizza", SubCategory = "Sauce" },
                new Category { MainCategory = "Pizza", SubCategory = "Pizza Topping" },
                new Category { MainCategory = "BBQ", SubCategory = "Meat BBQ" },
                new Category { MainCategory = "BBQ", SubCategory = "Vegetable BBQ" },
                new Category { MainCategory = "Stews and Soups", SubCategory = "Meat Stew" },
                new Category { MainCategory = "Stews and Soups", SubCategory = "Fish Stew" },
                new Category { MainCategory = "Stews and Soups", SubCategory = "Other Stew" },
                new Category { MainCategory = "Stews and Soups", SubCategory = "Soup (Warm)" },
                new Category { MainCategory = "Stews and Soups", SubCategory = "Soup (Cold)" },
                new Category { MainCategory = "Meat", SubCategory = "Pork" },
                new Category { MainCategory = "Meat", SubCategory = "Beef" },
                new Category { MainCategory = "Meat", SubCategory = "Game" },
                new Category { MainCategory = "Meat", SubCategory = "Other Meat" },
                new Category { MainCategory = "Meat", SubCategory = "Poultry" },
                new Category { MainCategory = "Oceanic", SubCategory = "Fish" },
                new Category { MainCategory = "Oceanic", SubCategory = "Seafood" },
                new Category { MainCategory = "Vegetarian", SubCategory = "Soup" },
                new Category { MainCategory = "Vegetarian", SubCategory = "Stew" },
                new Category { MainCategory = "Vegetarian", SubCategory = "Baked" },
                new Category { MainCategory = "Vegetarian", SubCategory = "Fresh Veg" },
                new Category { MainCategory = "Vegetarian", SubCategory = "Legume" },
                new Category { MainCategory = "Salad", SubCategory = "Creamy Salads" },
                new Category { MainCategory = "Salad", SubCategory = "Fresh Salads" },
                new Category { MainCategory = "Salad", SubCategory = "Warm Salads" },
                new Category { MainCategory = "Sandwich", SubCategory = "Subs" },
                new Category { MainCategory = "Sandwich", SubCategory = "Warm Sandwiches" },
                new Category { MainCategory = "Egg", SubCategory = "Omelettes" },
                new Category { MainCategory = "Egg", SubCategory = "Styles" },
                new Category { MainCategory = "Egg", SubCategory = "Pancake" },
                new Category { MainCategory = "Dessert", SubCategory = "Ice-Cream" },
                new Category { MainCategory = "Dessert", SubCategory = "Puddings and Jelly" },
                new Category { MainCategory = "Dessert", SubCategory = "Dessert Toppings" },
                new Category { MainCategory = "Dessert", SubCategory = "Candies" },
                new Category { MainCategory = "Baking", SubCategory = "Breads" },
                new Category { MainCategory = "Baking", SubCategory = "Cakes" },
                new Category { MainCategory = "Baking", SubCategory = "Cookies" },
                new Category { MainCategory = "Side", SubCategory = "Appetizer" },
                new Category { MainCategory = "Side", SubCategory = "Snack" },
                new Category { MainCategory = "Side", SubCategory = "Entry" },
                new Category { MainCategory = "Side", SubCategory = "Cold" },
                new Category { MainCategory = "Beverage (Non-Alcoholic)", SubCategory = "Soda" },
                new Category { MainCategory = "Beverage (Non-Alcoholic)", SubCategory = "Juice" },
                new Category { MainCategory = "Beverage (Non-Alcoholic)", SubCategory = "Warm Drinks" },
                new Category { MainCategory = "Beverage (Non-Alcoholic)", SubCategory = "Milkshake" },
                new Category { MainCategory = "Beverage (Non-Alcoholic)", SubCategory = "Punch" },
                new Category { MainCategory = "Beverage (Alcoholic)", SubCategory = "Spritzer" },
                new Category { MainCategory = "Beverage (Alcoholic)", SubCategory = "Milkshake (Alcohol)" },
                new Category { MainCategory = "Beverage (Alcoholic)", SubCategory = "Rum" },
                new Category { MainCategory = "Beverage (Alcoholic)", SubCategory = "Whiskey" },
                new Category { MainCategory = "Beverage (Alcoholic)", SubCategory = "Vodka" },
                new Category { MainCategory = "Beverage (Alcoholic)", SubCategory = "Gin" },
                new Category { MainCategory = "Beverage (Alcoholic)", SubCategory = "Cocktail" },
                new Category { MainCategory = "Beverage (Alcoholic)", SubCategory = "Long Drink" },
                new Category { MainCategory = "Beverage (Alcoholic)", SubCategory = "Liqueur" },
                new Category { MainCategory = "Beverage (Alcoholic)", SubCategory = "Warm (Alcohol)" },
                new Category { MainCategory = "Beverage (Alcoholic)", SubCategory = "Caloric Punch" },
                new Category { MainCategory = "Flavoring", SubCategory = "Condiment" },
                new Category { MainCategory = "Flavoring", SubCategory = "Spice" }
            };
            return categories.OrderBy(c => c.MainCategory).ThenBy(c => c.SubCategory).ToList();
        }
    }
}
