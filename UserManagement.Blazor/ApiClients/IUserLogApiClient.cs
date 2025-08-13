using UserManagement.Models;

namespace UserManagement.Blazor;
public interface IUserLogApiClient
{
    Task<List<UserLog>> GetAsync(CancellationToken cancellationToken = default);
    Task<List<UserLog>> GetUserLogsAsync(long userId, CancellationToken cancellationToken = default);
}
