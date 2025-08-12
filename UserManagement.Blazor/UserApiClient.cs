using System.Diagnostics;
using System.Net.Http.Json;
using UserManagement.Models;

namespace UserManagement.Blazor;

public class UserApiClient(HttpClient httpClient)
{
    public async Task<List<User>> GetUsersAsync(bool? filterActive = null, CancellationToken cancellationToken = default)
    {
        List<User> users = [];
        try
        {
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
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            Debugger.Break();
        }

        return users;
    }
}
