using MIM_Tool.Contracts.Services;
using MIM_Tool.Contracts.Views;
using MIM_Tool.Models;
using MIM_Tool.Funktions;
using Microsoft.Extensions.Options;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.IO;

namespace MIM_Tool.Views;//--------------------------------------------------------------------------------------Inizialiesieren-----------------------------------------------------------------------------

public partial class SettingsPage : Page, INotifyPropertyChanged, INavigationAware
{
    private readonly AppConfig _appConfig;                                  // App-Konfiguration
    private readonly IThemeSelectorService _themeSelectorService;           // Dienst zur Auswahl des Themas
    private readonly ISystemService _systemService;                         // Systemdienst
    private readonly IApplicationInfoService _applicationInfoService;       // Dienst zur Bereitstellung von Anwendungsinformationen
    private bool _isInitialized;                                            // Gibt an, ob die Seite initialisiert wurde
    private AppTheme _theme;                                                // Aktuelles Thema
    private string _versionDescription;                                     // Versionsbeschreibung

    public AppTheme Theme
    {
        get { return _theme; }                                              // Gibt das aktuelle Thema zurück
        set { Set(ref _theme, value); }                                     // Setzt das aktuelle Thema
    }

    public string VersionDescription
    {
        get { return _versionDescription; }                                 // Gibt die Versionsbeschreibung zurück
        set { Set(ref _versionDescription, value); }                        // Setzt die Versionsbeschreibung
    }

    public SettingsPage(IOptions<AppConfig> appConfig, IThemeSelectorService themeSelectorService, ISystemService systemService, IApplicationInfoService applicationInfoService)
    {
        _appConfig = appConfig.Value;                                        // Initialisiert die App-Konfiguration
        _themeSelectorService = themeSelectorService;                        // Initialisiert den ThemeSelectorService
        _systemService = systemService;                                      // Initialisiert den SystemService
        _applicationInfoService = applicationInfoService;                    // Initialisiert den ApplicationInfoService
        InitializeComponent();                                               // Initialisiert die Komponenten der Seite
        DataContext = this;                                                  // Setzt den Datenkontext auf die aktuelle Instanz
        btnSettingPath.Content = Properties.Settings.Default.pfadDeskOK;     // Setzt den Inhalt des Buttons auf den gespeicherten Pfad
        Properties.Settings.Default.SelectetMonitor = 10;                    // Setzt den ausgewählten Monitor auf 10
        Properties.Settings.Default.Save();                                  // Speichert die Einstellungen
    }

    public void OnNavigatedTo(object parameter)
    {
        // Setzt die Versionsbeschreibung und das aktuelle Thema, wenn die Seite navigiert wird
        VersionDescription = $"{Properties.Resources.AppDisplayName} - {_applicationInfoService.GetVersion()}";
        Theme = _themeSelectorService.GetCurrentTheme();
        _isInitialized = true;                                             // Markiert die Seite als initialisiert
    }

    public void OnNavigatedFrom()
    {
                                                                           // Methode wird aufgerufen, wenn von der Seite weg navigiert wird
    }
     private void OnLightChecked(object sender, RoutedEventArgs e)
    {
        if (_isInitialized)
        {
            _themeSelectorService.SetTheme(AppTheme.Light);                // Setzt das Thema auf "Light", wenn die Seite initialisiert ist
        }
    }

    private void OnDarkChecked(object sender, RoutedEventArgs e)
    {
        if (_isInitialized)
        {
            _themeSelectorService.SetTheme(AppTheme.Dark);                 // Setzt das Thema auf "Dark", wenn die Seite initialisiert ist
        }
    }

    private void OnDefaultChecked(object sender, RoutedEventArgs e)
    {
        if (_isInitialized)
        {
            _themeSelectorService.SetTheme(AppTheme.Default);              // Setzt das Thema auf "Default", wenn die Seite initialisiert ist
        }
    }

    private void OnPrivacyStatementClick(object sender, RoutedEventArgs e)
        => _systemService.OpenInWebBrowser(_appConfig.PrivacyStatement);                           // Öffnet die Datenschutzrichtlinie im Webbrowser

    public event PropertyChangedEventHandler PropertyChanged;                                      // Ereignis für Eigenschaftsänderungen

    private void Set<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
    {
        if (Equals(storage, value))
        {
            return;                                                                                // Beendet die Methode, wenn der Wert gleich ist
        }

        storage = value;                                                                           // Setzt den neuen Wert
        OnPropertyChanged(propertyName);                                                           // Benachrichtigt über die Eigenschaftsänderung
    }

    private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); // Methode zur Benachrichtigung über Eigenschaftsänderungen


    //------------------------------------------------------------------------------------------------------------------------Ereignishandler-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    public void btnSettingPath_Click(object sender, RoutedEventArgs e)
    {
                                                                                   // Erstellen einer Instanz von FunktionSavePath
        FunktionSavePath savePath = new FunktionSavePath();
                                                                                   // Aufrufen der Methode, um den neuen Pfad zu erhalten
        string newPath = savePath.GetNewSavePath();
                                                                                   // Überprüfen, ob ein Pfad ausgewählt wurde
        if (!string.IsNullOrEmpty(newPath))
        {
            Directory.Delete(Properties.Settings.Default.pfadDeskOK, true);        // Löscht das alte Verzeichnis
            Properties.Settings.Default.Reset();                                   // Setzt die Einstellungen zurück
            Properties.Settings.Default.pfadDeskOK = newPath;                      // Setzt den neuen Pfad
            Properties.Settings.Default.Save();                                    // Speichert die Einstellungen
        }
        btnSettingPath.Content = newPath;                                          // Setzt den Button-Inhalt auf den neuen Pfad
        var iniziStart = new Funktion1Initialisieren();
        iniziStart.Initialisieren();                                               // Initialisiert das Programm
    }

    private void btnReset_Click(object sender, RoutedEventArgs e)
    {
        FunktionDefaultPath resetPath = new FunktionDefaultPath();
        resetPath.ResetPath();                                                     // Setzt den Pfad auf den Standardpfad zurück
        btnSettingPath.Content = Properties.Settings.Default.pfadDeskOK;           // Setzt den Button-Inhalt auf den Standardpfad
        var iniziStart = new Funktion1Initialisieren();
        iniziStart.Initialisieren();                                               // Initialisiert das Programm
    }

    private void btnAdmin_Click(object sender, RoutedEventArgs e)
    {
        // Schaltet den Admin-Modus um
        if (Properties.Settings.Default.AdminMode) Properties.Settings.Default.AdminMode = false;
        else Properties.Settings.Default.AdminMode = true;

        Properties.Settings.Default.Save();                                        // Speichert die Einstellungen
    }
}
