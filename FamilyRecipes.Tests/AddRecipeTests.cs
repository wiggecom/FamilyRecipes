using FamilyRecipes.Data;
using FamilyRecipes.Models;
using FamilyRecipes.Pages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace FamilyRecipes.Tests
{










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
                //var addRecipeModel = new AddRecipeModel(context, null); // Mocka IWebHostEnvironment om det behövs



                // Act: Utför den asynkrona metoden
                //var result = await addRecipeModel.OnPostAddRecipe(null);

                // Assert: Kontrollera om resultatet är BadRequest
                //Assert.IsType<BadRequestObjectResult>(result);
            }
        }

        [Fact]
        public async Task OnPostAddRecipe_Should_Return_A_Redirecttion_When_Ingredients_Are_Provided()
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

                var ingredientsJson =
                "[{ \"IngredientName\": \"Ingredient 1\", \"UnitName\": \"grams\", \"Amount\": \"2\"}]";


                // Skapa modellen och använd in-memory databas
                //var addRecipeModel = new AddRecipeModel(context, null); // Mocka IWebHostEnvironment om det behövs



                // Act: Utför den asynkrona metoden
                //var result = await addRecipeModel.OnPostAddRecipe(ingredientsJson);

                // Assert: Kontrollera om resultatet är BadRequest
                //Assert.IsType<RedirectResult>(result);
            }
        }


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

