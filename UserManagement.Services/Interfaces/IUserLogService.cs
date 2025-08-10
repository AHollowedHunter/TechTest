using System.Collections.Generic;
using UserManagement.Models;

namespace UserManagement.Services.Domain.Interfaces;

public interface IUserLogService
{
    IEnumerable<UserLog> GetAll();

    IEnumerable<UserLog> GetForUser(long userId);
}
