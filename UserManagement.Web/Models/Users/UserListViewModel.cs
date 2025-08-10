using System;
using System.ComponentModel.DataAnnotations;
using UserManagement.Models;

namespace UserManagement.Web.Models.Users;

public class UserListViewModel
{
    public List<UserListItemViewModel> Items { get; set; } = new();
}

public class UserListItemViewModel
{
    public long Id { get; set; }

    [Required, StringLength(50)]
    public string? Forename { get; set; }

    [Required, StringLength(50)]
    public string? Surname { get; set; }

    [Required, StringLength(254)]
    public string? Email { get; set; }

    public bool     IsActive    { get; set; }
    public DateOnly DateOfBirth { get; set; }

    public static UserListItemViewModel FromUser(User user)
        => new()
        {
            Id          = user.Id,
            Forename    = user.Forename,
            Surname     = user.Surname,
            Email       = user.Email,
            IsActive    = user.IsActive,
            DateOfBirth = user.DateOfBirth,
        };

    public User ToUser()
        => new()
        {
            Id          = Id,
            Forename    = Forename!,
            Surname     = Surname!,
            Email       = Email!,
            DateOfBirth = DateOfBirth,
            IsActive    = IsActive,
        };
}
