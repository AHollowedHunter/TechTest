using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Models;

namespace UserManagement.Services.Domain.Interfaces;

public interface IUserLogService
{
    IEnumerable<UserLog> GetAll();

    IEnumerable<UserLog> GetForUser(long userId);

    Task<IEnumerable<UserLog>> GetForUserAsync(long userId);

    Task<IEnumerable<UserLog>> GetAllAsync();
}
