using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using UserManagement.Models;

namespace UserManagement.Data;

public class UserLogInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        if (eventData.Context is { } dataContext)
            Log(dataContext);

        return result;
    }

    private void Log(DbContext context)
    {
        List<UserLog> logs = [];

        foreach (var entityEntry in context.ChangeTracker.Entries())
        {
            if (entityEntry.Entity is not User user)
                continue;

            if (entityEntry.State is EntityState.Added or EntityState.Modified or EntityState.Deleted)
                logs.Add(new UserLog { UserId = user.Id, LogType = entityEntry.State.ToUserLogType(), });
        }

        if (logs.Count is > 0)
            context.Set<UserLog>().AddRange(logs);
    }
}
