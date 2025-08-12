using MudBlazor;

namespace UserManagement.Blazor.Services;

public interface IThemeService
{
    event EventHandler ThemeUpdated;

    bool IsDarkMode { get; }

    MudTheme Theme { get; }

    ThemeMode ThemeMode { get; set; }

    void UpdateDarkModeState(bool? autoIsDark = null);
}
