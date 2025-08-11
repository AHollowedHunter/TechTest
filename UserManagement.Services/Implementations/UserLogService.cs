using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserManagement.Data;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.Services.Domain.Implementations;

public class UserLogService : IUserLogService
{
    private readonly IDataContext _dataAccess;
    public UserLogService(IDataContext dataAccess) => _dataAccess = dataAccess;

    public IEnumerable<UserLog> GetForUser(long userId)
        => _dataAccess.GetAll<UserLog>().Where(l => l.UserId == userId);

    public async Task<IEnumerable<UserLog>> GetForUserAsync(long userId)
        => await _dataAccess.GetAll<UserLog>().Where(l => l.UserId == userId).ToListAsync();

    public IEnumerable<UserLog> GetAll()
        => _dataAccess.GetAll<UserLog>();

    public async Task<IEnumerable<UserLog>> GetAllAsync()
        => await _dataAccess.GetAll<UserLog>().ToListAsync();
}
