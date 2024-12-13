using FamilyRecipes.Data;
using FamilyRecipes.Interfaces;
using FamilyRecipes.Models;
using FamilyRecipes.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using static FamilyRecipes.Pages.AddRecipeModel;


namespace FamilyRecipes.Tests
{

    public class AddRecipeTests
    {

        [Fact]
        public async Task OnPostAddRecipe_Should_Return_BadRequest_When_No_Ingredients_Provided()
        {
            
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                
                context.Categories.AddRange(
                    new Category { Id = 1, MainCategory = "Main Category 1", SubCategory = "SubCategory 1" },
                    new Category { Id = 2, MainCategory = "Main Category 2", SubCategory = "SubCategory 2" }
                );
                context.SaveChanges();

               
                var calculationsMock = new Mock<ICalculations>();

                
                var addRecipeModel = new AddRecipeModel(context, null, calculationsMock.Object);

                
                var result = await addRecipeModel.OnPostAddRecipe(null);

                
                Assert.IsType<BadRequestObjectResult>(result);
            }
        }

        //-------------------------------------------------------------------------

        [Fact]
        public async Task OnPostAddRecipe_Should_Return_Redirection_And_Save_Recipe_When_Ingredients_Are_Provided()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase_Ingredients_Provided")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                // Lägg till testdata i databasen
                context.Categories.AddRange(
                    new Category { Id = 1, MainCategory = "Main Category 1", SubCategory = "SubCategory 1" },
                    new Category { Id = 2, MainCategory = "Main Category 2", SubCategory = "SubCategory 2" }
                );

                context.Ingredients.AddRange(
                    new Ingredient { Id = 1, Name = "Ingredient 1", Type = "Vegetable" },
                    new Ingredient { Id = 2, Name = "Ingredient 2", Type = "Spice" }
                );

                context.Units.AddRange(
                    new Unit { Id = 1, Name = "grams", Short = "g" },
                    new Unit { Id = 2, Name = "liters", Short = "l" }
                );

                context.SaveChanges();

                // Mocka ICalculations
                var mockCalculations = new Mock<ICalculations>();
                mockCalculations
                    .Setup(c => c.CalculateTotalCalories(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                    .Returns(100); // Returnera ett testvärde för kalorier

                // Skapa AddRecipeModel med mock och in-memory-databasen
                var addRecipeModel = new AddRecipeModel(context, null, mockCalculations.Object)
                {
                    AddTitle = "Test Recipe",
                    AddSubCategory = "SubCategory 1",
                    AddTimeRequired = 30,
                    AddServings = 4,
                    AddDescription = "A delicious test recipe",
                    AddImage = "",
                    AddAdultsOnly = false,
                    AddSteps = new List<string> { "Step 1", "Step 2" }
                };

                var ingredientsJson =
                    "[{ \"IngredientName\": \"Ingredient 1\", \"UnitName\": \"grams\", \"Amount\": \"2\"}]";

                // Act: metoden som ska testas
                var result = await addRecipeModel.OnPostAddRecipe(ingredientsJson);

                // Assert: Kontrollera resultat och data
                Assert.IsType<RedirectToActionResult>(result);

                // Kontrollera att receptet sparades korrekt
                var createdRecipe = context.Recipes.Include(r => r.RecipeIngredients).FirstOrDefault();
                Assert.NotNull(createdRecipe);
                Assert.Equal("Test Recipe", createdRecipe.Title);
                Assert.Equal("SubCategory 1", createdRecipe.Category.SubCategory);
                Assert.Equal(4, createdRecipe.Servings);
                Assert.Equal("A delicious test recipe", createdRecipe.Description);

                // Kontrollera ingrediensinformationen
                Assert.Single(createdRecipe.RecipeIngredients);
                var ingredient = createdRecipe.RecipeIngredients.First();
                Assert.Equal("Ingredient 1", ingredient.Ingredient.Name);
                Assert.Equal("grams", ingredient.Unit.Name);
                Assert.Equal(2, ingredient.Amount);
            }

            // Cleanup: Databasen återställs automatiskt när using-blocket avslutas
        }

        [Fact]
        public void OnGetGetSubcategories_Should_Return_Correct_Subcategories_For_Valid_MainCategory()
        {
            // Arrange

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Isolerad databas för detta test bara för att prova annorlunda
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
               
                context.Categories.AddRange(
                    new Category { Id = 1, MainCategory = "Main", SubCategory = "Sub1" },
                    new Category { Id = 2, MainCategory = "Main", SubCategory = "Sub2" },
                    new Category { Id = 3, MainCategory = "Other", SubCategory = "Sub3" }
                );
                context.SaveChanges();

                // Mocka ICalculations (används inte direkt här, men behövs för konstruktorn)
                var mockCalculations = new Mock<ICalculations>();

                
                var addRecipeModel = new AddRecipeModel(context, null, mockCalculations.Object);
                

                // Act
                var result = addRecipeModel.OnGetGetSubcategories("Main") as JsonResult;

                // Assert
                Assert.NotNull(result);
                var subcategories = result.Value as List<string>;
                Assert.NotNull(subcategories);
                Assert.Contains("Sub1", subcategories);
                Assert.Contains("Sub2", subcategories);
                Assert.DoesNotContain("Sub3", subcategories);
            }
        }

        [Fact]
        public void OnPostAddRecipeIngredientList_Should_Add_Valid_Ingredients_To_RecipeIngredientsMapSource()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDatabase_AddRecipeIngredientList")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                
                var mockCalculations = new Mock<ICalculations>();

                // Skapa instansen av AddRecipeModel
                var model = new AddRecipeModel(context, null, mockCalculations.Object);

                // Skapa en giltig lista med ingredienser
                var validIngredients = new AddRecipeModel.AddRecipeIngredientListModel
                {
                    Ingredients = new List<RecipeIngredientMapSource>
                    {
                        new RecipeIngredientMapSource { IngredientName = "Sugar", UnitName = "grams", Amount = "100" },
                        new RecipeIngredientMapSource { IngredientName = "Flour", UnitName = "grams", Amount = "200" }
                    }
                };

                // Act
                var result = model.OnPostAddRecipeIngredientList(validIngredients) as JsonResult;

                // Assert
                Assert.NotNull(result);
                Assert.IsType<JsonResult>(result);
                Assert.Equal(2, model.RecipeIngredientsMapSource.Count); // Kontrollera att två ingredienser lagts till
                Assert.Contains(model.RecipeIngredientsMapSource, i => i.IngredientName == "Sugar");
                Assert.Contains(model.RecipeIngredientsMapSource, i => i.IngredientName == "Flour");
            }
        }

        [Fact]
        public void OnPostAddRecipeIngredientList_Should_Return_Error_When_Model_Is_Null()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDatabase_AddRecipeIngredientList_NullModel")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                
                var mockCalculations = new Mock<ICalculations>();

                // Skapa instansen av AddRecipeModel
                var model = new AddRecipeModel(context, null, mockCalculations.Object);

                // Act
                var result = model.OnPostAddRecipeIngredientList(null) as JsonResult;

                // Assert
                Assert.NotNull(result);
                Assert.IsType<JsonResult>(result);
                Assert.Equal("Model is null", result.Value);
            }
        }

        [Fact]
        public void OnPostAddRecipeIngredientList_Should_Return_Error_When_IngredientList_Is_Null()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDatabase_AddRecipeIngredientList_NullIngredients")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                // Mocka ICalculations
                var mockCalculations = new Mock<ICalculations>();

                // Skapa instansen av AddRecipeModel
                var model = new AddRecipeModel(context, null, mockCalculations.Object);

                // Skapa en modell med null-ingredienser
                var invalidIngredients = new AddRecipeModel.AddRecipeIngredientListModel
                {
                    Ingredients = null
                };

                // Act
                var result = model.OnPostAddRecipeIngredientList(invalidIngredients) as JsonResult;

                // Assert
                Assert.NotNull(result);
                Assert.IsType<JsonResult>(result);
                Assert.Equal("Ingredients list is null", result.Value);
            }
        }

        [Fact]
        public void OnGetGetUnitsByType_Should_Return_Correct_Units_For_Valid_Ingredient()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDatabase_GetUnitsByType")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                // Mocka ICalculations
                var mockCalculations = new Mock<ICalculations>();

                // Setup test data
                var ingredient = new Ingredient
                {
                    Name = "Milk",
                    IsMeasuredByVolume = true,
                    Type = ""
                };
                context.Ingredients.Add(ingredient);

                var unit1 = new Unit { Name = "Litre", Short = "l", IsVolume = true, IsMetrical = true, InMl = 1000 };
                var unit2 = new Unit { Name = "Cup", Short = "cup", IsVolume = true, IsMetrical = true, InMl = 240 };
                context.Units.AddRange(unit1, unit2);

                context.SaveChanges();

                // Skapa modellen
                var model = new AddRecipeModel(context, null, mockCalculations.Object);

                // Act
                var result = model.OnGetGetUnitsByType("Milk") as JsonResult;
                var units = result.Value as List<string>;

                // Assert
                Assert.NotNull(result);
                Assert.IsType<JsonResult>(result);
                Assert.NotNull(units);
                Assert.Contains("Litre", units);
                Assert.Contains("Cup", units);

                if (!units.Contains("Cup"))
                {
                    // Logga vad som faktiskt finns i listan för felsökning
                    Console.WriteLine($"Units found: {string.Join(", ", units)}");
                }
            }
        }

        [Fact]
        public void IngredientList_Should_Be_Null_By_Default()
        {
            // Act
            var model = new RecipeIngredientListModel();

            // Assert
            Assert.Null(model.IngredientList);
        }
    }
}

