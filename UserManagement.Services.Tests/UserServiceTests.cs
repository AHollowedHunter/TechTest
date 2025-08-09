using System.Linq;
using UserManagement.Models;
using UserManagement.Services.Domain.Implementations;

namespace UserManagement.Data.Tests;

public class UserServiceTests
{
    [Fact]
    public void GetAll_WhenContextReturnsEntities_MustReturnSameEntities()
    {
        // Arrange: Initializes objects and sets the value of the data that is passed to the method under test.
        var service = CreateService();
        var users = SetupSingleUser();

        // Act: Invokes the method under test with the arranged parameters.
        var result = service.GetAll();

        // Assert: Verifies that the action of the method under test behaves as expected.
        result.Should().BeEquivalentTo(users);
    }

    [Fact]
    public void FilterByActive_IsTrue_WhenContextReturnsEntities_MustReturnActiveEntities()
    {
        // Arrange: Initializes objects and sets the value of the data that is passed to the method under test.
        var service = CreateService();
        SetupMultipleUsers([..ActiveUsers, ..InactiveUsers]);

        // Act: Invokes the method under test with the arranged parameters.
        var result = service.FilterByActive(true);

        // Assert: Verifies that the action of the method under test behaves as expected.
        result.Should().BeEquivalentTo(ActiveUsers);
    }

    [Fact]
    public void FilterByActive_IsFalse_WhenContextReturnsEntities_MustReturnInactiveEntities()
    {
        // Arrange: Initializes objects and sets the value of the data that is passed to the method under test.
        var service = CreateService();
        SetupMultipleUsers([..ActiveUsers, ..InactiveUsers]);

        // Act: Invokes the method under test with the arranged parameters.
        var result = service.FilterByActive(false);

        // Assert: Verifies that the action of the method under test behaves as expected.
        result.Should().BeEquivalentTo(InactiveUsers);
    }

    private IQueryable<User> SetupSingleUser(string forename = "Johnny", string surname = "User", string email = "juser@example.com", bool isActive = true)
    {
        var users = new[]
        {
            new User
            {
                Forename = forename,
                Surname = surname,
                Email = email,
                IsActive = isActive
            }
        }.AsQueryable();

        _dataContext
            .Setup(s => s.GetAll<User>())
            .Returns(users);

        return users;
    }

    private IQueryable<User> SetupMultipleUsers(params User[] users)
    {
        var queryableUsers = users.AsQueryable();
        _dataContext
            .Setup(s => s.GetAll<User>())
            .Returns(queryableUsers);

        return queryableUsers;
    }

    private readonly Mock<IDataContext> _dataContext = new();
    private UserService CreateService() => new(_dataContext.Object);

    private static readonly User[] ActiveUsers =
    [
        new User { Forename = "Stuart", Surname = "Braithwaite", Email = "sBraithwaite@example.org", IsActive = true, },
        new User { Forename = "Hazel ", Surname = "Wilde", Email = "hWilde@example.org", IsActive = true, },
    ];

    private static readonly User[] InactiveUsers =
    [
        new User { Forename = "Kurt", Surname = "Cobain", Email = "kCobain@example.org", IsActive = false, },
    ];
}
