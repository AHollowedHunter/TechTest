using MudBlazor;
using UserManagement.Blazor.Services;

namespace UserManagement.Blazor.Layout;
public partial class MainLayout
{
    private bool _drawerOpen = false;
    private string _themeIcon = Icons.Material.Filled.AutoMode;
    private MudThemeProvider _mudThemeProvider = default!;

    protected override Task OnInitializedAsync()
    {
        ThemeService.ThemeUpdated += ThemeUpdated;
        return base.OnInitializedAsync();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var systemIsDark = await _mudThemeProvider.GetSystemDarkModeAsync();
            ThemeService.UpdateDarkModeState(systemIsDark);
            await _mudThemeProvider.WatchSystemDarkModeAsync(SystemDarkModeChanged);
        }
    }

    private void ToggleTheme()
    {
        ThemeService.ThemeMode = ThemeService.ThemeMode switch
        {
            ThemeMode.Auto => ThemeMode.Light,
            ThemeMode.Light => ThemeMode.Dark,
            ThemeMode.Dark => ThemeMode.Auto,
            _ => ThemeMode.Auto,
        };
    }

    private void DrawerToggle()
        => _drawerOpen = !_drawerOpen;

    private void ThemeUpdated(object? sender, EventArgs eventArgs)
    {
        _themeIcon = ThemeService.ThemeMode switch
        {
            ThemeMode.Light => Icons.Material.Filled.LightMode,
            ThemeMode.Dark => Icons.Material.Filled.DarkMode,
            _ => Icons.Material.Filled.AutoMode,
        };
        StateHasChanged();
    }

    private Task SystemDarkModeChanged(bool isDarkMode)
    {
        if (ThemeService.ThemeMode is ThemeMode.Auto)
        {
            ThemeService.UpdateDarkModeState(isDarkMode);
            StateHasChanged();
        }

        return Task.CompletedTask;
    }

    #region IDisposable
    private bool _disposedValue;
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                ThemeService.ThemeUpdated -= ThemeUpdated;
            }

            _disposedValue = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put clean up code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
    #endregion
}
