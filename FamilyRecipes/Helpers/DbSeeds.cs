using FamilyRecipes.Models;
using FamilyRecipes.Data;
using FamilyRecipes.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using FamilyRecipes.Interfaces;

namespace FamilyRecipes.Helpers
{
    public class DbSeeds
    {
        private readonly ApplicationDbContext _context;
        private readonly ICalculations _calculations;
        public DbSeeds(ApplicationDbContext context, ICalculations calculations)
        {
            _context = context;
            _calculations = calculations;
        }

        public async Task SeedingDataAsync()
        {
            if (!_context.Categories.Any())
            {
                List<Category> categories = Category.GetCategories().ToList();
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
                List<Ingredient> ingredients = Ingredient.GetIngredients().ToList();
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

        public Recipe DemoRecipe()
        {
            List<Unit> myUnits = _context.Units.ToList();
            List<Ingredient> myIngredients = _context.Ingredients.ToList();
            List<Category> myCategories = _context.Categories.ToList();

            Recipe recipe = new Recipe
            {
                Title = "Easy Chicken Curry",
                UserName = "Mr Lee",
                CreatedDate = DateTime.Now,
                Servings = 4,
                Category = myCategories.Where(i => i.MainCategory == "World" && i.SubCategory == "Chinese").FirstOrDefault(),
                TimeRequired = 45,
                Description = "This is the best recipe ever!",
                Steps = new List<string> { "Chop Chicken", "Fry mixed vegetables and chicken", "Add cream and spices" },
                RecipeIngredients = new List<RecipeIngredient>
                {
                    new RecipeIngredient
                    {
                                Ingredient = myIngredients.Where(i => i.Name=="Chicken Breast").FirstOrDefault(),
                                Unit = myUnits.Where(u => u.Name=="Gram").FirstOrDefault(),
                                Amount = 500,
                    },
                    new RecipeIngredient
                    {
                                Ingredient = myIngredients.Where(i => i.Name=="Mixed Vegetables").FirstOrDefault(),
                                Unit = myUnits.Where(u => u.Name=="Gram").FirstOrDefault(),
                                Amount = 750,
                    },
                    new RecipeIngredient
                    {
                                Ingredient = myIngredients.Where(i => i.Name=="Cream (Heavy)").FirstOrDefault(),
                                Unit = myUnits.Where(u => u.Name=="Millilitre").FirstOrDefault(),
                                Amount = 500,
                    }
                },
                UserRatings = new List<UserRating>(),
                AdultsOnly = false,
                Image = "",
            };
            foreach (RecipeIngredient ri in recipe.RecipeIngredients)
            {
                ri.TotalCalories = _calculations.CalculateTotalCalories(ri.Ingredient.Name, ri.Unit.Name, ri.Amount);
            }

            return recipe;
        }
    }
}
