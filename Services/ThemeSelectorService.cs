using MIM_Tool.Contracts.Services;                           
using MIM_Tool.Models;                                       
using ControlzEx.Theming;                                    
using MahApps.Metro.Theming;                                 
using System.Windows;                                        

namespace MIM_Tool.Services
{
    public class ThemeSelectorService : IThemeSelectorService                                                          // Implementiert den IThemeSelectorService.
    {
        private const string HcDarkTheme = "pack://application:,,,/Styles/Themes/HC.Dark.Blue.xaml";                   // Pfad zum dunklen High-Contrast-Thema.
        private const string HcLightTheme = "pack://application:,,,/Styles/Themes/HC.Light.Blue.xaml";                 // Pfad zum hellen High-Contrast-Thema.

        public ThemeSelectorService()
        {
        }

        public void InitializeTheme()                                                                                  // Methode zur Initialisierung des Themas.
        {
            // Bitte vervollständigen Sie diese Themen gemäß den Anweisungen unter https://mahapps.com/docs/themes/thememanager#creating-custom-themes
            ThemeManager.Current.AddLibraryTheme(new LibraryTheme(new Uri(HcDarkTheme), MahAppsLibraryThemeProvider.DefaultInstance)); // Fügt das dunkle High-Contrast-Thema hinzu.
            ThemeManager.Current.AddLibraryTheme(new LibraryTheme(new Uri(HcLightTheme), MahAppsLibraryThemeProvider.DefaultInstance)); // Fügt das helle High-Contrast-Thema hinzu.

            var theme = GetCurrentTheme();                                                                             // Holt das aktuelle Thema.
            SetTheme(theme);                                                                                           // Setzt das aktuelle Thema.
        }

        public void SetTheme(AppTheme theme)                                                                           // Methode zum Setzen des Themas.
        {
            if (theme == AppTheme.Default)
            {
                ThemeManager.Current.ThemeSyncMode = ThemeSyncMode.SyncAll;                                            // Synchronisiert alle Themen.
                ThemeManager.Current.SyncTheme();                                                                      // Synchronisiert das Thema.
            }
            else
            {
                ThemeManager.Current.ThemeSyncMode = ThemeSyncMode.SyncWithHighContrast;                               // Synchronisiert nur mit High-Contrast-Themen.
                ThemeManager.Current.SyncTheme();                                                                      // Synchronisiert das Thema.
                ThemeManager.Current.ChangeTheme(Application.Current, $"{theme}.Blue", SystemParameters.HighContrast); // Ändert das Thema der Anwendung.
            }

            App.Current.Properties["Theme"] = theme.ToString();                                                        // Speichert das aktuelle Thema in den Anwendungseigenschaften.
        }

        public AppTheme GetCurrentTheme()                                                                              // Methode zum Abrufen des aktuellen Themas.
        {
            if (App.Current.Properties.Contains("Theme"))                                                              // Überprüft, ob das Thema in den Anwendungseigenschaften gespeichert ist.
            {
                var themeName = App.Current.Properties["Theme"].ToString();                                            // Holt den Namen des Themas.
                Enum.TryParse(themeName, out AppTheme theme);                                                          // Konvertiert den Namen in das AppTheme-Enum.
                return theme;                                                                                          // Gibt das aktuelle Thema zurück.
            }

            return AppTheme.Default;                                                                                   // Gibt das Standardthema zurück, wenn kein Thema gespeichert ist.
        }
    }
}


