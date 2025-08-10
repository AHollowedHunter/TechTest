using System.Linq;
using UserManagement.Models;

namespace UserManagement.Data.Tests;

public class DataContextTests
{
    [Fact]
    public void GetAll_WhenNewEntityAdded_MustIncludeNewEntity()
    {
        // Arrange: Initializes objects and sets the value of the data that is passed to the method under test.
        using var context = CreateContext();

        var entity = new User { Forename = "Brand New", Surname = "User", Email = "brandnewuser@example.com" };
        context.Create(entity);

        // Act: Invokes the method under test with the arranged parameters.
        var result = context.GetAll<User>();

        // Assert: Verifies that the action of the method under test behaves as expected.
        result
            .Should()
            .Contain(s => s.Email == entity.Email)
            .Which.Should()
            .BeEquivalentTo(entity);
    }

    [Fact]
    public void GetAll_WhenDeleted_MustNotIncludeDeletedEntity()
    {
        // Arrange: Initializes objects and sets the value of the data that is passed to the method under test.
        using var context = CreateContext();

        var entity = context.GetAll<User>().First();
        context.Delete(entity);

        // Act: Invokes the method under test with the arranged parameters.
        var result = context.GetAll<User>();

        // Assert: Verifies that the action of the method under test behaves as expected.
        result.Should().NotContain(s => s.Email == entity.Email);
    }

    [Fact]
    public void Get_WhenUsingExistingId_ShouldReturnEntity()
    {
        using var context = CreateContext();

        var id     = 42;
        var entity = new User { Id = id, Forename = "Brand New", Surname = "User", Email = "brandnewuser@example.com" };
        context.Create(entity);

        var result = context.Get<User>(id);

        result.Should().BeEquivalentTo(entity);
    }

    [Fact]
    public void Get_WhenUsingNonExistentId_ShouldReturnNull()
    {
        using var context = CreateContext();

        var id = 42;

        var result = context.Get<User>(id);

        result.Should().BeNull();
    }

    private DataContext CreateContext()
    {
        var context = new DataContext();
        // Ensure the In-Memory database is cleared between tests.
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        return context;
    }
}
