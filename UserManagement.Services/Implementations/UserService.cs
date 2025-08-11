using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
        => _dataAccess.GetAll<User>().Where(x => x.IsActive == isActive);

    public async Task<IEnumerable<User>> FilterByActiveAsync(bool isActive)
        => await _dataAccess.GetAll<User>().Where(x => x.IsActive == isActive).ToListAsync();

    public IEnumerable<User> GetAll()
        => _dataAccess.GetAll<User>();

    public async Task<IEnumerable<User>> GetAllAsync()
        => await _dataAccess.GetAll<User>().ToListAsync();

    public User? GetById(long id)
        => _dataAccess.GetAll<User>().SingleOrDefault(x => x.Id == id);

    public Task<User?> GetByIdAsync(long id)
        => _dataAccess.GetAll<User>().SingleOrDefaultAsync(x => x.Id == id);

    public bool Exists(long id)
        => _dataAccess.GetAll<User>().Any(u => u.Id == id);

    public Task<bool> ExistsAsync(long id)
        => _dataAccess.GetAll<User>().AnyAsync(u => u.Id == id);

    /// <inheritdoc />
    public long Create(User user)
    {
        _dataAccess.Create(user);
        return user.Id;
    }

    public async Task<long> CreateAsync(User user)
    {
        await _dataAccess.CreateAsync(user);
        return user.Id;
    }

    /// <inheritdoc />
    public void Edit(User user)
        => _dataAccess.Update(user);

    public Task EditAsync(User user)
        => _dataAccess.UpdateAsync(user);

    /// <inheritdoc />
    public void Delete(User user)
        => _dataAccess.Delete(user);

    public Task DeleteAsync(User user)
        => _dataAccess.DeleteAsync(user);
}
