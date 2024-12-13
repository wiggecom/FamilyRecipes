using FamilyRecipes.Models;
using FamilyRecipes.Interfaces;
using FamilyRecipes.Data;
using Microsoft.EntityFrameworkCore;


namespace FamilyRecipes.Helpers
{
    public class Calculations : ICalculations
    {
        private readonly Data.ApplicationDbContext _context;
        public Calculations(Data.ApplicationDbContext context)
        {
            _context = context;
        }
        // Add Calculating Functions Here

        public int CalculateTotalCalories(string ingredientName, string unitName, int recipeAmount = 0, int ingredientCalories = 0)
        {
            // ex. test: CalculateTotalCalories("", "", 100, 150)

            // test: ingredientName: if "" or " ", no ingredient calories will be used.
            //                       if "name", ingredient "name" calories will be used, 
            //                       overwrites optional ingredientCalories argument.

            // test: unitName:       if "" or " ", will use default factor of 1 (1 * amount * calories)
            //                       if "name", will use unit "name" factor and overwrite default
            //                       ((unit factor) * amount * calories)

            // test: recipeAmount:   if not included, amount is 0, should return 0 in total with early return
            //                       amount should always be included for calculations

            // test: ingredientCalories: if > 0, will be used as test calories per 100g
            //                           will be overwritten to ingredientName.calories
            //                           if ingredientName is valid.

            // ---------- Declarations START ---------- //
            float unitFactor = 1; // if no unit is sent, the factor is 1
            int thisIngredientCalories = ingredientCalories; // sent test value (or default to 0).
            int thisValue;

            // amount early return
            if (recipeAmount <= 0) return 0;

            // if we send a named Unit
            if (!string.IsNullOrWhiteSpace(unitName)) // 
            {
                Unit thisUnit = _context.Units.Where(u => u.Name == unitName).FirstOrDefault();
                if (thisUnit.IsVolume) unitFactor = thisUnit.InMl;
                if (!thisUnit.IsVolume) unitFactor = thisUnit.InGr;
            }

            // If we send a named ingredient, set calories from ingredient
            if (!string.IsNullOrWhiteSpace(ingredientName))
            {
                thisIngredientCalories = _context.Ingredients.Where(i => i.Name == ingredientName).Select(i => i.Calories).FirstOrDefault();
            }

            float thisCaloriesSingle = (float)thisIngredientCalories / 100; // set value per ml/g
            // ---------- Declarations END ---------- //

            // To get the total we need to use calories per g/ml
            // times the amount and the factor
            thisValue = (int)Math.Round((thisCaloriesSingle * (recipeAmount * unitFactor)), 0);
            return thisValue; // Early return surpasses the other return

        }
    }
}
