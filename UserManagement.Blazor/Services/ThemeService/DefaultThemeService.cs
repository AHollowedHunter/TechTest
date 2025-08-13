using MudBlazor;

namespace UserManagement.Blazor.Services;

public class DefaultThemeService : IThemeService
{
    private bool _autoIsDark;
    private bool _isDarkMode;
    private ThemeMode _themeMode;

    public event EventHandler? ThemeUpdated;

    public MudTheme Theme { get; } = new MudTheme();

    public ThemeMode ThemeMode
    {
        get => _themeMode;
        set
        {
            _themeMode = value;
            UpdateDarkModeState();
            ThemeUpdated?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool IsDarkMode
    {
        get => _isDarkMode;
        private set
        {
            if (_isDarkMode == value)
                return;

            _isDarkMode = value;
            ThemeUpdated?.Invoke(this, EventArgs.Empty);
        }
    }

    public void UpdateDarkModeState(bool? autoIsDark = null)
    {
        if (autoIsDark.HasValue)
            _autoIsDark = autoIsDark.Value;

        IsDarkMode = ThemeMode switch
        {
            ThemeMode.Dark => true,
            ThemeMode.Light => false,
            _ => _autoIsDark,
        };
    }
}
