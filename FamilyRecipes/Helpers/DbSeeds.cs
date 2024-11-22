using FamilyRecipes.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FamilyRecipes.Helpers
{
    public class DbSeeds
    {
        private readonly Data.ApplicationDbContext _context;
        public DbSeeds(Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SeedingDataAsync()
        {
            if (!_context.Categories.Any())
            {
                List<Category> categories = CategorySeed();
                _context.Categories.AddRange(categories);
                await _context.SaveChangesAsync();
            };

            if (!_context.Units.Any())
            {
                List<Unit> units = Unit.GetUnits().ToList();
                _context.Units.AddRange(units);
                await _context.SaveChangesAsync();
            };
            
            if (!_context.Ingredients.Any())
            {
                List<Ingredient> ingredients = IngredientSeed();
                _context.Ingredients.AddRange(ingredients);
                await _context.SaveChangesAsync();
            };

            if (!_context.Recipes.Any())
            {
                Recipe recipe = DemoRecipe();
                _context.Recipes.Add(recipe);
                await _context.SaveChangesAsync();
            };
        }

        public static List<Category> CategorySeed()
        {
            // Categories
            List<Category> categories = new List<Category>{
                new Category { CategoryType = "Food", CategoryName = "Soup" },
                new Category { CategoryType = "Food", CategoryName = "Chinese" },
                new Category { CategoryType = "Food", CategoryName = "BBQ" },
                new Category { CategoryType = "Dessert", CategoryName = "Baking" },
                new Category { CategoryType = "Dessert", CategoryName = "Ice Cream and Milkshakes" },
                new Category { CategoryType = "Dessert", CategoryName = "Mousses, Puddings and Jelly" },
                new Category { CategoryType = "Drinks", CategoryName = "Sodas" },
                new Category { CategoryType = "Drinks", CategoryName = "Warm Drinks" },
                new Category { CategoryType = "Alcoholic Drinks", CategoryName = "Rum" },
                new Category { CategoryType = "Food", CategoryName = "Chicken" },
            };

            return categories.OrderBy(c => c.CategoryType).ThenBy(c => c.CategoryName).ToList();
        }
        public List<Ingredient> IngredientSeed()
        {
            // Ingredients
            List<Ingredient> ingredients = new List<Ingredient>
            {
                new Ingredient { Name="Potato", Calories=77, Description="Plant-based thing", Type="Vegetable", IsMeasuredAsFluid=false},
                new Ingredient { Name="Milk", Calories=42, Description="Moo", Type="Dairy-product", IsMeasuredAsFluid=true},
                new Ingredient { Name="Cocoa Powder", Calories=228, Description="Chocolate", Type="Fruit", IsMeasuredAsFluid=true},
                new Ingredient { Name="Chicken", Calories=239, Description="Without feathers", Type="Poultry", IsMeasuredAsFluid=false },
                new Ingredient { Name="Mixed Vegetables", Calories=60, Description="Frozen mix", Type="Vegetable", IsMeasuredAsFluid=false },
                new Ingredient { Name="Cream", Calories=196, Description="It's creamy!", Type="Dairy-product", IsMeasuredAsFluid=true }
            };

            return ingredients.OrderBy(c => c.Type).ThenBy(c => c.Name).ToList();
        }

        public Recipe DemoRecipe()
        {
            List<Unit> myUnits = _context.Units.ToList();
            List<Ingredient> myIngredients = _context.Ingredients.ToList();
            List<Category> myCategories = _context.Categories.ToList();

            Recipe recipe = new Recipe
            {
                Title = "Easy Chicken Curry",
                UserName = "Mr Yamamoto",
                CreatedDate = DateTime.Now,
                Category = myCategories.Where(i => i.CategoryType == "Food" && i.CategoryName == "Chicken").FirstOrDefault(),
                TimeRequired = 45,
                Description = "This is the best recipe ever!",
                Steps = new List<string> { "1. Chop Chicken", "2. Fry mixed vegetables and chicken", "3. Add cream and spices" },
                RecipeIngredients = new List<RecipeIngredient>
                {
                    new RecipeIngredient
                    {
                                Ingredient = myIngredients.Where(i => i.Name=="Chicken").FirstOrDefault(),
                                Unit = myUnits.Where(u => u.Name=="Gram").FirstOrDefault(),
                                IsMetrical = true,
                                Amount = 500,
                    },
                    new RecipeIngredient
                    {
                                Ingredient = myIngredients.Where(i => i.Name=="Mixed Vegetables").FirstOrDefault(),
                                Unit = myUnits.Where(u => u.Name=="Gram").FirstOrDefault(),
                                IsMetrical = true,
                                Amount = 750,
                    },
                    new RecipeIngredient
                    {
                                Ingredient = myIngredients.Where(i => i.Name=="Cream").FirstOrDefault(),
                                Unit = myUnits.Where(u => u.Name=="Millilitre").FirstOrDefault(),
                                IsMetrical = true,
                                Amount = 500,
                    }
                },
                UserRatings = new List<UserRating>(),
                AdultsOnly = false,
                Image = "",
            };
            int chickenTotal = RecipeIngredient.CalculateTotalCalories(recipe.RecipeIngredients[0].Amount, recipe.RecipeIngredients[0].Ingredient.Calories);
            recipe.RecipeIngredients[0].TotalCalories = chickenTotal;
            int veggiesTotal = RecipeIngredient.CalculateTotalCalories(recipe.RecipeIngredients[1].Amount, recipe.RecipeIngredients[1].Ingredient.Calories);
            recipe.RecipeIngredients[1].TotalCalories = veggiesTotal;
            int creamTotal = RecipeIngredient.CalculateTotalCalories(recipe.RecipeIngredients[2].Amount, recipe.RecipeIngredients[2].Ingredient.Calories);
            recipe.RecipeIngredients[2].TotalCalories = creamTotal;

            return recipe;
        }
    }
}
