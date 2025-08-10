using System;
using System.ComponentModel.DataAnnotations;
using UserManagement.Models;

namespace UserManagement.Web.Models.UserLogs;

public class UserLogListViewModel
{
    public List<UserLogListItemViewModel> Items { get; set; } = [];
}

public class UserLogListItemViewModel
{
    public long Id { get; set; }

    public DateTimeOffset TimeStamp { get; set; }

    public long UserId { get; set; }

    public UserLogType LogType { get; set; }

    public static UserLogListItemViewModel FromUserLog(UserLog userLog)
        => new()
        {
            Id = userLog.Id,
            TimeStamp = userLog.Timestamp,
            UserId = userLog.UserId,
            LogType = userLog.LogType,
        };
}
