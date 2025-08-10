using System.Collections.Generic;
using UserManagement.Models;

namespace UserManagement.Services.Domain.Interfaces;

public interface IUserService
{
    /// <summary>
    /// Return users by active state
    /// </summary>
    /// <param name="isActive"></param>
    /// <returns></returns>
    IEnumerable<User> FilterByActive(bool isActive);
    IEnumerable<User> GetAll();

    User? GetById(long id);

    /// <summary>
    /// Create a new User
    /// </summary>
    /// <param name="user"></param>
    /// <returns>The new User's ID</returns>
    long Create(User user);

    /// <summary>
    /// Edit an existing User
    /// </summary>
    /// <param name="user"></param>
    void Edit(User user);

    /// <summary>
    /// Delete an existing User
    /// </summary>
    /// <param name="user"></param>
    void Delete(User user);

    bool Exists(long id);
}
