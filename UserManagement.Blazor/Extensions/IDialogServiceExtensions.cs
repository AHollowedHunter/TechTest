using MudBlazor;
using UserManagement.Blazor.Components.Dialogs;
using UserManagement.Models;

namespace UserManagement.Blazor.Extensions;

public static class IDialogServiceExtensions
{
    public static Task<IDialogReference> ShowUserAddAsync(this IDialogService dialogService)
        => dialogService.ShowUserDialog<UserEditDialog>(new User(), "Add User");

    public static Task<IDialogReference> ShowUserViewAsync(this IDialogService dialogService, User user)
        => dialogService.ShowUserDialog<UserViewDialog>(user);

    public static Task<IDialogReference> ShowUserEditAsync(this IDialogService dialogService, User user)
        => dialogService.ShowUserDialog<UserEditDialog>(user, "Edit User");

    public static Task<IDialogReference> ShowUserDeleteAsync(this IDialogService dialogService, User user)
        => dialogService.ShowUserDialog<UserDeleteDialog>(user, "Delete User");

    public static Task<IDialogReference> ShowUserDialog<TDialog>(this IDialogService dialogService, User user, string title = "User") where TDialog : UserDialog
    {
        var parameters = new DialogParameters<TDialog>
        {
            { x => x.User, user}
        };
        var options = new DialogOptions
        {
            BackdropClick = false
        };
        return dialogService.ShowAsync<TDialog>(title, parameters, options);
    }

}
