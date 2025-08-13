using System.Net.Http.Json;
using UserManagement.Models;

namespace UserManagement.Blazor;

public class UserLogApiClient(HttpClient httpClient) : IUserLogApiClient
{
    public async Task<List<UserLog>> GetAsync(CancellationToken cancellationToken = default)
    {
        List<UserLog> logs = [];

        await foreach (var log in httpClient.GetFromJsonAsAsyncEnumerable<UserLog>("/api/logs/user", cancellationToken))
        {
            if (log is not null)
            {
                logs.Add(log);
            }
        }

        return logs;
    }

    public async Task<List<UserLog>> GetUserLogsAsync(long userId, CancellationToken cancellationToken = default)
    {
        List<UserLog> logs = [];

        await foreach (var log in httpClient.GetFromJsonAsAsyncEnumerable<UserLog>($"/api/logs/user/{userId}", cancellationToken))
        {
            if (log is not null)
            {
                logs.Add(log);
            }
        }

        return logs;
    }
}
