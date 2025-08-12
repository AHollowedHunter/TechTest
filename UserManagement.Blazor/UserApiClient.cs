using System.Diagnostics;
using System.Net.Http.Json;
using UserManagement.Models;

namespace UserManagement.Blazor;

public class UserApiClient(HttpClient httpClient)
{
    public async Task<List<User>> GetUsersAsync(CancellationToken cancellationToken = default)
    {
        List<User> users = [];
        try
        {
            await foreach (var user in httpClient.GetFromJsonAsAsyncEnumerable<User>("/api/users", cancellationToken))
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
