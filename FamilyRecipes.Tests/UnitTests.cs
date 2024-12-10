﻿using FamilyRecipes.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyRecipes.Tests
{
    public class UnitTests
    {
        [Theory]
        [InlineData("", "", 200, 150, 300)] // Exempeldata: 200g av en ingrediens med 150 kcal/100g ger 300 kcal
        [InlineData("", "", 100, 50, 50)]   // 100g av en ingrediens med 50 kcal/100g ger 50 kcal
        [InlineData("", "", 0, 100, 0)]     // 0g av en ingrediens ger 0 kcal
        [InlineData("", "", 100, 0, 0)]     // 100g av en ingrediens med 0 kcal/100g ger 0 kcal
        [InlineData("", "", 100, -50, 0)]   // Negativa kalorier ska ge 0 kcal
        public void CalculateTotalCalories_ValidInputs_ReturnsExpectedResult(string ingredientName, string unitName, int recipeAmount, int ingredientCalories, int expectedCalories)
        {
            //Arrange 
            //Behövs inte eftersom CalculateTotalCalories är en isolerad metod och har inte några beroenden :)


            // Act
            //int result = Calculations.CalculateTotalCalories(ingredientName, unitName, recipeAmount, ingredientCalories);

            // Assert
            //Assert.Equal(expectedCalories, result);
        }
    }

    public class Calculations : ICalculations
    {
        public int CalculateTotalCalories(string ingredientName, string unitName, int recipeAmount, int ingredientCalories = 0)
        {
            throw new NotImplementedException();
        }
    }
}