using UserManagement.Models;

namespace UserManagement.Blazor;
public interface IUserApiClient
{
    Task<bool> AddUserAsync(User user);
    Task<bool> DeleteUserAsync(long userId);
    Task<List<User>> GetUsersAsync(bool? filterActive = null, CancellationToken cancellationToken = default);
    Task<bool> UpdateUserAsync(User user);
}