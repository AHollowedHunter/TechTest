using System;
using System.Collections.Generic;
using System.Linq;
using UserManagement.Data;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.Services.Domain.Implementations;

public class UserService : IUserService
{
    private readonly IDataContext _dataAccess;
    public UserService(IDataContext dataAccess) => _dataAccess = dataAccess;

    /// <summary>
    /// Return users by active state
    /// </summary>
    /// <param name="isActive"></param>
    /// <returns></returns>
    public IEnumerable<User> FilterByActive(bool isActive)
        => _dataAccess.GetAll<User>().Where(x => x.IsActive == isActive).ToArray();

    public IEnumerable<User> GetAll()
        => _dataAccess.GetAll<User>().ToArray();

    public User? GetById(long id)
        => _dataAccess.GetAll<User>().SingleOrDefault(x => x.Id == id);

    public bool Exists(long id)
        => _dataAccess.GetAll<User>().Any(u => u.Id == id);

    /// <inheritdoc />
    public long Create(User user)
    {
        _dataAccess.Create(user);
        return user.Id;
    }

    /// <inheritdoc />
    public void Edit(User user)
        => _dataAccess.Update(user);

    /// <inheritdoc />
    public void Delete(User user)
        => _dataAccess.Delete(user);
}
