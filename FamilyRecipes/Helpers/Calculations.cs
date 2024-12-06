namespace FamilyRecipes.Helpers
{
    public class Calculations
    {
        // Add Calculating Functions Here
        public static void ConvertToImperialUS()
        {
            // Functions to break down ml to American Imperial measures
        }

        //public static void ConvertToImperialUK()
        //{
        //    // Functions to break down ml to American Imperial measures
        //}

        //public static void ConvertToImperialAU()
        //{
        //    // Functions to break down ml to American Imperial measures
        //}

        public static void ChangeServings()
        {
            //GetRecipe: 
            //foreach RecipeIngredient
            //(amount / servings) * wanted servings
        }


        public static void ChangeIngredientAmounts()
        {
            //ChangeByIngredientAmount: 
            //Factor = NewAmount / RecipeIngredient(name).amount
            //foreach RecipeIngredient = RecipeIngredient* Factor        }

            //Recept:
            //Köttfärs 500g
            //Potatis  750g
            //Lök      150g
            //Grädde   300ml

            //Vi har:
            //Köttfärs 750g
            //Faktor = 1.5
            //Potatis: 1125g
            //Lök:     225
            //Grädde:  450
        }

        public static int CalculateTotalCalories(int calcAmount, int calcIngredientCalories)
        {
            float ingredientCaloriesFloat = (float)calcIngredientCalories / 100;
            if (calcIngredientCalories > 0)
            {
                int myNum = (int)Math.Round((ingredientCaloriesFloat * calcAmount), 0); // Early return

                return myNum;
            }
            return 0;
        }
    }
}
