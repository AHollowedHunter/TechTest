using Microsoft.AspNetCore.Components;
using MudBlazor;
using UserManagement.Models;

namespace UserManagement.Blazor.Components.Dialogs;

public abstract class UserDialog : ComponentBase
{
    [CascadingParameter]
    protected IMudDialogInstance MudDialog { get; set; } = default!;

    [Parameter, EditorRequired]
    public User User { get; set; }
}
