using System.Collections.Generic;
using System.Linq;
using UserManagement.Data;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.Services.Domain.Implementations;

public class UserLogService : IUserLogService
{
    private readonly IDataContext _dataAccess;
    public UserLogService(IDataContext dataAccess) => _dataAccess = dataAccess;

    public IEnumerable<UserLog> GetForUser(long userId) => _dataAccess.GetAll<UserLog>().Where(l => l.UserId == userId);

    IEnumerable<UserLog> IUserLogService.GetAll() => _dataAccess.GetAll<UserLog>();
}
