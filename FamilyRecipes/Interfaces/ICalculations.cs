namespace FamilyRecipes.Interfaces
{
    public interface ICalculations
    {
        int CalculateTotalCalories(string ingredientName, string unitName, int recipeAmount, int ingredientCalories = 0);
    }
}
