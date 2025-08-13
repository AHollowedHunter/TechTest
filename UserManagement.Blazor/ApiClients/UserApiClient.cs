using System.Net.Http.Json;
using UserManagement.Models;

namespace UserManagement.Blazor;

public class UserApiClient(HttpClient httpClient) : IUserApiClient
{
    public async Task<List<User>> GetUsersAsync(bool? filterActive = null, CancellationToken cancellationToken = default)
    {
        List<User> users = [];

        var uri = "/api/users";
        if (filterActive is { } filter)
        {
            uri += $"?isActive={filter}";
        }

        await foreach (var user in httpClient.GetFromJsonAsAsyncEnumerable<User>(uri, cancellationToken))
        {
            if (user is not null)
            {
                users.Add(user);
            }
        }

        return users;
    }

    public async Task<bool> DeleteUserAsync(long userId)
    {
        return (await httpClient.DeleteAsync($"/api/users/{userId}")).IsSuccessStatusCode;
    }
}
