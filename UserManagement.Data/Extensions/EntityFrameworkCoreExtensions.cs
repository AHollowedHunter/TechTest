using System;
using Microsoft.EntityFrameworkCore;
using UserManagement.Models;

namespace UserManagement.Data;

internal static class EntityFrameworkCoreExtensions
{
    internal static UserLogType ToUserLogType(this EntityState entityState)
        => entityState switch
        {
            EntityState.Added     => UserLogType.Created,
            EntityState.Modified  => UserLogType.Updated,
            EntityState.Deleted   => UserLogType.Deleted,
            _                     => throw new NotImplementedException(),
        };
}
