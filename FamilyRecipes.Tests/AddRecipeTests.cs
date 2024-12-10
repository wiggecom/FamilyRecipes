using FamilyRecipes.Data;
using FamilyRecipes.Models;
using FamilyRecipes.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace FamilyRecipes.Tests
{

  
    public class AddRecipeTests
    {

        [Fact]
        public async Task OnPostAddRecipe_Should_Return_BadRequest_When_No_Ingredients_Provided()
        {
            // Arrange: Skapa en in-memory databas
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                // Lägg till exempeldata för kategorier
                context.Categories.AddRange(
                    new Category { Id = 1, MainCategory = "Main Category 1", SubCategory = "SubCategory 1" },
                    new Category { Id = 2, MainCategory = "Main Category 2", SubCategory = "SubCategory 2" }
                );
                context.SaveChanges();

                var ingredientsJson = "[\"Ingredient 1\", \"Ingredient 2\"]";


                // Skapa modellen och använd in-memory databas
                var addRecipeModel = new AddRecipeModel(context, null); // Mocka IWebHostEnvironment om det behövs



                // Act: Utför den asynkrona metoden
                var result = await addRecipeModel.OnPostAddRecipe(null);

                // Assert: Kontrollera om resultatet är BadRequest
                Assert.IsType<BadRequestObjectResult>(result);
            }
        }



       


        [Fact]
        public async Task OnPostAddRecipe_Should_Return_A_Redirection_When_Ingredients_Are_Provided()
        {
            // Arrange: Skapa en in-memory databas
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase_Ingredients_Provided")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                // Lägg till exempeldata för kategorier, ingredienser och enheter
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

                // Ingredienslista i JSON-format
                var ingredientsJson =
                    "[{ \"IngredientName\": \"Ingredient 1\", \"UnitName\": \"grams\", \"Amount\": \"2\"}]";

                // Skapa modellen och sätt obligatoriska egenskaper
                var addRecipeModel = new AddRecipeModel(context, null)
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

                // Act: Kör den asynkrona metoden
                var result = await addRecipeModel.OnPostAddRecipe(ingredientsJson);

                // Assert: Kontrollera att resultatet är RedirectToActionResult
                Assert.IsType<RedirectToActionResult>(result);

                // Kontrollera att receptet har sparats i databasen
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
        }






        [Fact]
        public void OnGetGetSubcategories_Should_Return_Correct_Subcategories_For_Valid_MainCategory()
        {
            // Arrange
            //var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            //    .UseInMemoryDatabase(databaseName: "TestDatabase")
            //    .Options;

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Isolerad databas för detta test
           .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Categories.AddRange(
                    new Category { Id = 1, MainCategory = "Main", SubCategory = "Sub1" },
                    new Category { Id = 2, MainCategory = "Main", SubCategory = "Sub2" },
                    new Category { Id = 3, MainCategory = "Other", SubCategory = "Sub3" }
                );
                context.SaveChanges();

                var addRecipeModel = new AddRecipeModel(context, null);

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
                var model = new AddRecipeModel(context, null);
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
                var model = new AddRecipeModel(context, null);

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
                var model = new AddRecipeModel(context, null);
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




        //Commented out for later check to make it work

        //[Fact]
        //public void OnGetGetUnitsByType_Should_Return_Correct_Units_For_Valid_Ingredient()
        //{
        //    // Arrange
        //    var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        //        .UseInMemoryDatabase("TestDatabase_GetUnitsByType")
        //        .Options;

        //    using (var context = new ApplicationDbContext(options))
        //    {
        //        // Setup test data
        //        var ingredient = new Ingredient
        //        {
        //            Name = "Milk",
        //            IsMeasuredByVolume = true,
        //            Type = ""
        //        };
        //        context.Ingredients.Add(ingredient);

        //        var unit1 = new Unit { Name = "Liter", IsVolume = true, IsMetrical = true, InMl = 1000 };
        //        var unit2 = new Unit { Name = "Cup", IsVolume = true, IsMetrical = false, InMl = 240 };
        //        context.Units.AddRange(unit1, unit2);

        //        context.SaveChanges();

        //        var model = new AddRecipeModel(context, null);

        //        // Act
        //        var result = model.OnGetGetUnitsByType("Milk") as JsonResult;
        //        var units = result.Value as List<string>;

        //        // Assert
        //        Assert.NotNull(result);
        //        Assert.IsType<JsonResult>(result);
        //        Assert.Contains("Liter", units);
        //        Assert.Contains("Cup", units);
        //    }
        //}





        // Tests For later check

        //public static class MockDbSetExtensions
        //{
        //    public static Mock<DbSet<T>> BuildMockDbSet<T>(this IQueryable<T> source) where T : class
        //    {
        //        var mockSet = new Mock<DbSet<T>>();
        //        mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(source.Provider);
        //        mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(source.Expression);
        //        mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(source.ElementType);
        //        mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(source.GetEnumerator());
        //        return mockSet;
        //    }
        //}

        //public class AddRecipeTests
        //{
        //    private readonly Mock<ApplicationDbContext> _mockContext;
        //    private readonly AddRecipeModel _addRecipeModel;

        //    public AddRecipeTests()
        //    {
        //        // Mocka databasen
        //        _mockContext = new Mock<ApplicationDbContext>();
        //        _addRecipeModel = new AddRecipeModel(_mockContext.Object);

        //        // Lägg till mockade data i contexten
        //        var mockCategories = new List<Category>
        //    {
        //        new Category { Id = 1, SubCategory = "Dessert" },
        //        new Category { Id = 2, SubCategory = "Main Dish" }
        //    }.AsQueryable();

        //        var mockIngredients = new List<Ingredient>
        //    {
        //        new Ingredient { Id = 1, Name = "Sugar", Calories = 400 },
        //        new Ingredient { Id = 2, Name = "Flour", Calories = 500 }
        //    }.AsQueryable();

        //        var mockUnits = new List<Unit>
        //    {
        //        new Unit { Id = 1, Name = "Cup" },
        //        new Unit { Id = 2, Name = "Gram" }
        //    }.AsQueryable();

        //        var mockRecipes = new List<Recipe>().AsQueryable();

        //        // Mocka DbSets
        //        _mockContext.Setup(c => c.Categories).Returns(mockCategories.BuildMockDbSet().Object);
        //        _mockContext.Setup(c => c.Ingredients).Returns(mockIngredients.BuildMockDbSet().Object);
        //        _mockContext.Setup(c => c.Units).Returns(mockUnits.BuildMockDbSet().Object);
        //        _mockContext.Setup(c => c.Recipes).Returns(mockRecipes.BuildMockDbSet().Object);
        //    }

        //    [Fact]
        //    public async Task OnPostAddRecipe_Should_Return_BadRequest_When_No_Ingredients_Provided()
        //    {
        //        // Arrange
        //        string ingredientList = ""; // Ingen ingredienslista

        //        // Act
        //        var result = await _addRecipeModel.OnPostAddRecipe(ingredientList);

        //        // Assert
        //        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        //        Assert.Equal("No ingredients provided.", badRequestResult.Value);
        //    }

        //    [Fact]
        //    public async Task OnPostAddRecipe_Should_Return_RedirectToAction_When_Ingredients_Provided()
        //    {
        //        // Arrange
        //        var ingredientList = "[{\"IngredientName\": \"Sugar\", \"UnitName\": \"Cup\", \"Amount\": \"2\"}]"; // Korrekt JSON
        //        _addRecipeModel.AddTitle = "Chocolate Cake";
        //        _addRecipeModel.AddSubCategory = "Dessert";
        //        _addRecipeModel.AddTimeRequired = 30;
        //        _addRecipeModel.AddServings = 8;
        //        _addRecipeModel.AddDescription = "Delicious cake";
        //        _addRecipeModel.AddImage = "chocolate_cake.jpg";
        //        _addRecipeModel.AddAdultsOnly = false;
        //        _addRecipeModel.AddSteps = new List<string> { "Mix ingredients", "Bake at 180°C for 30 minutes" };

        //        // Act
        //        var result = await _addRecipeModel.OnPostAddRecipe(ingredientList);

        //        // Assert
        //        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        //        Assert.Equal("/AddRecipe", redirectResult.ActionName);

        //        // Kontrollera att ingredienserna har lagts till i RecipeIngredients
        //        var recipeIngredients = _addRecipeModel.thisRecipe.RecipeIngredients;
        //        Assert.Single(recipeIngredients);
        //        Assert.Equal("Sugar", recipeIngredients[0].Ingredient.Name);
        //        Assert.Equal("Cup", recipeIngredients[0].Unit.Name);
        //        Assert.Equal(2, recipeIngredients[0].Amount);
        //    }
        //}















        //public static class MockDbSetExtensions
        //{
        //    public static Mock<DbSet<T>> BuildMockDbSet<T>(this IQueryable<T> source) where T : class
        //    {
        //        var mockSet = new Mock<DbSet<T>>();
        //        mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(source.Provider);
        //        mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(source.Expression);
        //        mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(source.ElementType);
        //        mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(source.GetEnumerator());
        //        return mockSet;
        //    }
        //}



        //public class AddRecipeTests
        //{
        //    private readonly Mock<ApplicationDbContext> _mockContext;
        //    private readonly AddRecipeModel _addRecipeModel;

        //    public AddRecipeTests()
        //    {
        //        // Mocka databasen
        //        _mockContext = new Mock<ApplicationDbContext>();
        //        _addRecipeModel = new AddRecipeModel(_mockContext.Object);

        //        // Lägg till mockade data i contexten
        //        var mockCategories = new List<Category>
        //    {
        //        new Category { Id = 1, SubCategory = "Dessert" },
        //        new Category { Id = 2, SubCategory = "Main Dish" }
        //    }.AsQueryable();

        //        var mockIngredients = new List<Ingredient>
        //    {
        //        new Ingredient { Id = 1, Name = "Sugar", Calories = 400 },
        //        new Ingredient { Id = 2, Name = "Flour", Calories = 500 }
        //    }.AsQueryable();

        //        var mockUnits = new List<Unit>
        //    {
        //        new Unit { Id = 1, Name = "Cup" },
        //        new Unit { Id = 2, Name = "Gram" }
        //    }.AsQueryable();

        //        var mockRecipes = new List<Recipe>().AsQueryable();

        //        // Mocka DbSets
        //        _mockContext.Setup(c => c.Categories).Returns(mockCategories.BuildMockDbSet().Object);
        //        _mockContext.Setup(c => c.Ingredients).Returns(mockIngredients.BuildMockDbSet().Object);
        //        _mockContext.Setup(c => c.Units).Returns(mockUnits.BuildMockDbSet().Object);
        //        _mockContext.Setup(c => c.Recipes).Returns(mockRecipes.BuildMockDbSet().Object);
        //    }

        //    [Fact]
        //    public async Task OnPostAddRecipe_Should_Return_BadRequest_When_No_Ingredients_Provided()
        //    {
        //        // Arrange
        //        string ingredientList = ""; // Ingen ingredienslista

        //        // Act
        //        var result = await _addRecipeModel.OnPostAddRecipe(ingredientList);

        //        // Assert
        //        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        //        Assert.Equal("No ingredients provided.", badRequestResult.Value);
        //    }

        //    [Fact]
        //    public async Task OnPostAddRecipe_Should_Return_RedirectToAction_When_Ingredients_Provided()
        //    {
        //        // Arrange
        //        var ingredientList = "[{\"IngredientName\": \"Sugar\", \"UnitName\": \"Cup\", \"Amount\": \"2\"}]"; // Korrekt JSON
        //        _addRecipeModel.AddTitle = "Chocolate Cake";
        //        _addRecipeModel.AddSubCategory = "Dessert";
        //        _addRecipeModel.AddTimeRequired = 30;
        //        _addRecipeModel.AddServings = 8;
        //        _addRecipeModel.AddDescription = "Delicious cake";
        //        _addRecipeModel.AddImage = "chocolate_cake.jpg";
        //        _addRecipeModel.AddAdultsOnly = false;
        //        _addRecipeModel.AddSteps = new List<string> { "Mix ingredients", "Bake at 180°C for 30 minutes" };

        //        // Act
        //        var result = await _addRecipeModel.OnPostAddRecipe(ingredientList);

        //        // Assert
        //        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        //        Assert.Equal("/AddRecipe", redirectResult.RouteName);
        //    }
        //}










        //public class AddRecipeTests
        //{
        //    private readonly Mock<ApplicationDbContext> _mockContext;
        //    private readonly Mock<IWebHostEnvironment> _mockWebHostEnvironment;

        //    public AddRecipeTests()
        //    {
        //        // Mocka databaskontexten och webbmiljön
        //        _mockContext = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());
        //        _mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
        //    }


        //    [Fact]
        //    public async Task OnPostAddRecipe_EmptyIngredientList_ReturnsBadRequest()
        //    {
        //        // Arrange
        //        var pageModel = new AddRecipeModel(_mockContext.Object, _mockWebHostEnvironment.Object);
        //        string ingredientList = null; // Simulera att ingen ingredienslista skickas

        //        // Act
        //        var result = await pageModel.OnPostAddRecipe(ingredientList);

        //        // Assert
        //        Assert.IsType<BadRequestObjectResult>(result);
        //        var badRequestResult = result as BadRequestObjectResult;
        //        Assert.Equal("No ingredients provided.", badRequestResult?.Value);
        //    }


        //    //[Fact]
        //    //public async Task OnPostAddRecipe_ValidData_CreatesRecipe()
        //    //{
        //    //    // Arrange
        //    //    var pageModel = new AddRecipeModel(_mockContext.Object, _mockWebHostEnvironment.Object);

        //    //    // Mocka databasinnehåll
        //    //    _mockContext.Setup(x => x.Categories).ReturnsDbSet(new List<Category>
        //    //    {
        //    //        new Category { SubCategory = "Dessert", MainCategory = "Sweets" }
        //    //    });

        //    //    _mockContext.Setup(x => x.Ingredients).ReturnsDbSet(new List<Ingredient>
        //    //    {
        //    //       new Ingredient { Name = "Sugar", Calories = 400, Type = "Sweetener" }
        //    //    });

        //    //    _mockContext.Setup(x => x.Units).ReturnsDbSet(new List<Unit>
        //    //    {
        //    //       new Unit { Name = "Grams", IsVolume = false }
        //    //    });

        //    //    string ingredientList = "[{\"IngredientName\":\"Sugar\",\"UnitName\":\"Grams\",\"Amount\":\"100\"}]";

        //    //    // Bindningsdata
        //    //    pageModel.AddTitle = "Chocolate Cake";
        //    //    pageModel.AddSubCategory = "Dessert";
        //    //    pageModel.AddTimeRequired = 60;
        //    //    pageModel.AddServings = 4;
        //    //    pageModel.AddDescription = "Delicious cake.";
        //    //    pageModel.AddSteps = new List<string> { "Mix", "Bake", "Cool" };
        //    //    pageModel.AddImage = "cake.jpg";
        //    //    pageModel.AddAdultsOnly = false;

        //    //    // Act
        //    //    var result = await pageModel.OnPostAddRecipe(ingredientList);

        //    //    // Assert
        //    //    _mockContext.Verify(x => x.Recipes.Add(It.IsAny<Recipe>()), Times.Once);
        //    //    _mockContext.Verify(x => x.SaveChangesAsync(default), Times.Once);
        //    //    Assert.IsType<RedirectToActionResult>(result);
        //    //}



        //    //[Fact]
        //    //public void OnPostAddRecipe_Should_Return_BadRequest_When_No_Ingredients_Provided()
        //    //{
        //    //    // Arrange
        //    //    var mockContext = new Mock<ApplicationDbContext>();

        //    //    // Mocka DbSet<Category> för att returnera en lista av kategorier
        //    //    var mockCategorySet = new Mock<DbSet<Category>>();
        //    //    mockCategorySet.As<IQueryable<Category>>().Setup(m => m.Provider).Returns(new List<Category>
        //    //{
        //    //    new Category { Id = 1, MainCategory = "Main Category 1", SubCategory = "SubCategory 1" },
        //    //    new Category { Id = 2, MainCategory = "Main Category 2", SubCategory = "SubCategory 2" }
        //    //}.AsQueryable().Provider);

        //    //    mockCategorySet.As<IQueryable<Category>>().Setup(m => m.Expression).Returns(new List<Category>
        //    //{
        //    //    new Category { Id = 1, MainCategory = "Main Category 1", SubCategory = "SubCategory 1" },
        //    //    new Category { Id = 2, MainCategory = "Main Category 2", SubCategory = "SubCategory 2" }
        //    //}.AsQueryable().Expression);

        //    //    mockCategorySet.As<IQueryable<Category>>().Setup(m => m.ElementType).Returns(typeof(Category));
        //    //    mockCategorySet.As<IQueryable<Category>>().Setup(m => m.GetEnumerator()).Returns(new List<Category>
        //    //{
        //    //    new Category { Id = 1, MainCategory = "Main Category 1", SubCategory = "SubCategory 1" },
        //    //    new Category { Id = 2, MainCategory = "Main Category 2", SubCategory = "SubCategory 2" }
        //    //}.GetEnumerator());

        //    //    mockContext.Setup(c => c.Categories).Returns(mockCategorySet.Object);

        //    //    // Mocka ytterligare data om det behövs, t.ex. Ingredients, Units, osv.

        //    //    var addRecipeModel = new AddRecipeModel(mockContext.Object, null); // Om du behöver mocka IWebHostEnvironment, gör det här också

        //    //    // Act
        //    //    var result = addRecipeModel.OnPostAddRecipe("some ingredient list");

        //    //    // Assert
        //    //    Assert.IsType<BadRequestResult>(result);
        //    //}


        //    //[Fact]
        //    //public void OnPostAddRecipe_Should_Return_BadRequest_When_No_Ingredients_Provided()
        //    //{
        //    //    // Arrange
        //    //    var mockCategorySet = new Mock<DbSet<Category>>();

        //    //    // Mocka IQueryable<Category> för DbSet
        //    //    var categories = new List<Category>
        //    //{
        //    //    new Category { Id = 1, MainCategory = "Main Category 1", SubCategory = "SubCategory 1" },
        //    //    new Category { Id = 2, MainCategory = "Main Category 2", SubCategory = "SubCategory 2" }
        //    //}.AsQueryable();

        //    //    mockCategorySet.As<IQueryable<Category>>()
        //    //        .Setup(m => m.Provider).Returns(categories.Provider);
        //    //    mockCategorySet.As<IQueryable<Category>>()
        //    //        .Setup(m => m.Expression).Returns(categories.Expression);
        //    //    mockCategorySet.As<IQueryable<Category>>()
        //    //        .Setup(m => m.ElementType).Returns(categories.ElementType);
        //    //    mockCategorySet.As<IQueryable<Category>>()
        //    //        .Setup(m => m.GetEnumerator()).Returns(categories.GetEnumerator());

        //    //    var mockContext = new Mock<ApplicationDbContext>();
        //    //    mockContext.Setup(c => c.Categories).Returns(mockCategorySet.Object);

        //    //    // Mocka ytterligare databasoperationer om det behövs, t.ex. Ingredients, Units, osv.

        //    //    var addRecipeModel = new AddRecipeModel(mockContext.Object, null); // Om du behöver mocka IWebHostEnvironment, gör det här också

        //    //    // Act
        //    //    var result = addRecipeModel.OnPostAddRecipe("some ingredient list");

        //    //    // Assert
        //    //    Assert.IsType<BadRequestResult>(result);
        //    //}







        //        [Fact]
        //        public async Task OnPostAddRecipe_Should_Return_A_Redirection_When_Ingredients_Are_Provided()
        //        {
        //            // Arrange: Skapa en in-memory databas
        //            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        //                .UseInMemoryDatabase(databaseName: "TestDatabase")
        //                .Options;

        //            using (var context = new ApplicationDbContext(options))
        //            {
        //                // Lägg till exempeldata för kategorier, ingredienser och enheter
        //                context.Categories.Add(new Category { Id = 1, MainCategory = "Desserts", SubCategory = "Dessert" });
        //                //context.Ingredients.Add(new Ingredient { Id = 1, Name = "Sugar", Calories = 400 });
        //                context.Ingredients.AddRange(
        //                   new Ingredient
        //                   {
        //                       Id = 1,
        //                       Name = "Sugar",
        //                       Type = "Sweetener", // Ange ett värde här
        //                       Calories = 400
        //                   },
        //                   new Ingredient
        //                   {
        //                       Id = 2,
        //                       Name = "Flour",
        //                       Type = "Grain", // Ange ett värde här
        //                       Calories = 500
        //                   }
        //                   );

        //                //context.Units.Add(new Unit { Id = 1, Name = "Cup" });
        //                context.Units.AddRange(
        //    new Unit
        //    {
        //        Id = 1,
        //        Name = "Cup",
        //        Short = "C" // Ange ett värde här
        //    },
        //    new Unit
        //    {
        //        Id = 2,
        //        Name = "Gram",
        //        Short = "g" // Ange ett värde här
        //    }
        //);

        //                context.SaveChanges();

        //                var ingredientsJson = "[{ \"IngredientName\": \"Sugar\", \"UnitName\": \"Cup\", \"Amount\": \"2\" }]";

        //                // Skapa modellen och använd in-memory databas
        //                var addRecipeModel = new AddRecipeModel(context);

        //                addRecipeModel.AddTitle = "Chocolate Cake";
        //                addRecipeModel.AddSubCategory = "Dessert";
        //                addRecipeModel.AddTimeRequired = 30;
        //                addRecipeModel.AddServings = 8;
        //                addRecipeModel.AddDescription = "Delicious cake";
        //                addRecipeModel.AddImage = "cake.jpg";
        //                addRecipeModel.AddAdultsOnly = false;
        //                addRecipeModel.AddSteps = new List<string> { "Mix ingredients", "Bake" };

        //                // Act: Utför den asynkrona metoden
        //                var result = await addRecipeModel.OnPostAddRecipe(ingredientsJson);

        //                // Assert: Kontrollera att det är en RedirectToActionResult
        //                var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        //                Assert.Equal("/AddRecipe", redirectResult.ActionName);

        //                // Verifiera att receptet faktiskt lades till i databasen
        //                var createdRecipe = context.Recipes.FirstOrDefault();
        //                Assert.NotNull(createdRecipe);
        //                Assert.Equal("Chocolate Cake", createdRecipe.Title);
        //            }
        //        }






        //Commeted out to try a new test

        //[Fact]
        //public async Task OnPostAddRecipe_Should_Return_A_Redirecttion_When_Ingredients_Are_Provided()
        //{
        //    // Arrange: Skapa en in-memory databas
        //    var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        //        .UseInMemoryDatabase(databaseName: "TestDatabase")
        //        .Options;

        //    using (var context = new ApplicationDbContext(options))
        //    {
        //        // Lägg till exempeldata för kategorier
        //        context.Categories.AddRange(
        //            new Category { Id = 1, MainCategory = "Main Category 1", SubCategory = "SubCategory 1" },
        //            new Category { Id = 2, MainCategory = "Main Category 2", SubCategory = "SubCategory 2" }
        //        );
        //        context.SaveChanges();

        //        var ingredientsJson =
        //        "[{ \"IngredientName\": \"Ingredient 1\", \"UnitName\": \"grams\", \"Amount\": \"2\"}]";


        //        // Skapa modellen och använd in-memory databas
        //        var addRecipeModel = new AddRecipeModel(context, null); // Mocka IWebHostEnvironment om det behövs


        //        var createdRecipe = context.Recipes.FirstOrDefault();
        //        Assert.NotNull(createdRecipe);
        //        Assert.Equal("Expected Title", createdRecipe.Title); // Kontrollera fält som är viktiga




        //        // Act: Utför den asynkrona metoden
        //        var result = await addRecipeModel.OnPostAddRecipe(ingredientsJson);

        //        // Assert: Kontrollera om resultatet är BadRequest
        //        Assert.IsType<RedirectToActionResult>(result);
        //    }
        //}



        //[Fact]
        //public async Task OnPostAddRecipe_ValidData_CreatesRecipe()
        //{
        //    // Arrange
        //    var pageModel = new AddRecipeModel(_mockContext.Object, _mockWebHostEnvironment.Object);

        //    // Mocka databasinnehåll
        //    _mockContext.Setup(x => x.Categories).ReturnsDbSet(new List<Category>
        //    {
        //        new Category { SubCategory = "Dessert", MainCategory = "Sweets" }
        //    });

        //    _mockContext.Setup(x => x.Ingredients).ReturnsDbSet(new List<Ingredient>
        //    {
        //       new Ingredient { Name = "Sugar", Calories = 400, Type = "Sweetener" }
        //    });

        //    _mockContext.Setup(x => x.Units).ReturnsDbSet(new List<Unit>
        //    {
        //       new Unit { Name = "Grams", IsVolume = false }
        //    });

        //    string ingredientList = "[{\"IngredientName\":\"Sugar\",\"UnitName\":\"Grams\",\"Amount\":\"100\"}]";

        //    // Bindningsdata
        //    pageModel.AddTitle = "Chocolate Cake";
        //    pageModel.AddSubCategory = "Dessert";
        //    pageModel.AddTimeRequired = 60;
        //    pageModel.AddServings = 4;
        //    pageModel.AddDescription = "Delicious cake.";
        //    pageModel.AddSteps = new List<string> { "Mix", "Bake", "Cool" };
        //    pageModel.AddImage = "cake.jpg";
        //    pageModel.AddAdultsOnly = false;

        //    // Act
        //    var result = await pageModel.OnPostAddRecipe(ingredientList);

        //    // Assert
        //    _mockContext.Verify(x => x.Recipes.Add(It.IsAny<Recipe>()), Times.Once);
        //    _mockContext.Verify(x => x.SaveChangesAsync(default), Times.Once);
        //    Assert.IsType<RedirectToActionResult>(result);
        //}




    }
}

