using FamilyRecipes.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FamilyRecipes.Tests.Helpers
/*

        CalculateTotalCalories(string ingredientName, string unitName, int recipeAmount = 0, int ingredientCalories = 0)
    
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

 */
{
    public class CalculationsTests
    {
        //[Theory]
        //[InlineData(200, 150, 300)] // Exempeldata: 200g av en ingrediens med 150 kcal/100g ger 300 kcal
        //[InlineData(100, 50, 50)]   // 100g av en ingrediens med 50 kcal/100g ger 50 kcal
        //[InlineData(0, 100, 0)]     // 0g av en ingrediens ger 0 kcal
        //[InlineData(100, 0, 0)]     // 100g av en ingrediens med 0 kcal/100g ger 0 kcal
        //[InlineData(100, -50, 0)]   // Negativa kalorier ska ge 0 kcal

        //[InlineData("", "", 200, 150, 300)] // Exempeldata: 200g av en ingrediens med 150 kcal/100g ger 300 kcal
        //[InlineData("", "", 100, 50, 50)]   // 100g av en ingrediens med 50 kcal/100g ger 50 kcal
        //[InlineData("", "", 0, 100, 0)]     // 0g av en ingrediens ger 0 kcal
        //[InlineData("", "", 100, 0, 0)]     // 100g av en ingrediens med 0 kcal/100g ger 0 kcal
        //[InlineData("", "", 100, -50, 0)]   // Negativa kalorier ska ge 0 kcal
        //public void CalculateTotalCalories_ValidInputs_ReturnsExpectedResult(string ingredientName, string unitName, int recipeAmount, int ingredientCalories, int expectedCalories)
        //{
        //    //Arrange 
        //    //Behövs inte eftersom CalculateTotalCalories är en isolerad metod och har inte några beroenden :)


        //    // Act
        //    int result = Calculations.CalculateTotalCalories(ingredientName, unitName, recipeAmount, ingredientCalories);

        //    // Assert
        //    Assert.Equal(expectedCalories, result);
        //}

    }
}
