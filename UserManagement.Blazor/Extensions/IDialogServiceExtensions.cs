using MudBlazor;
using UserManagement.Blazor.Components.Dialogs;
using UserManagement.Models;

namespace UserManagement.Blazor.Extensions;

public static class IDialogServiceExtensions
{
    public static Task<IDialogReference> ShowUserViewAsync(this IDialogService dialogService, User user)
        => dialogService.ShowUserDialog<UserViewDialog>(user);

    public static Task<IDialogReference> ShowUserDeleteAsync(this IDialogService dialogService, User user)
        => dialogService.ShowUserDialog<UserDeleteDialog>(user);

    public static Task<IDialogReference> ShowUserDialog<TDialog>(this IDialogService dialogService, User user) where TDialog : UserDialog
    {
        var parameters = new DialogParameters<TDialog>
        {
            { x => x.User, user}
        };
        return dialogService.ShowAsync<TDialog>("User", parameters);
    }

}
