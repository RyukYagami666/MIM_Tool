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
using System.Windows.Forms;
using MIM_Tool.Helpers;

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
        Log.inf("SettingsPage initialisiert. Pfad und Monitor-Einstellungen gesetzt.");
    }

    public void OnNavigatedTo(object parameter)
    {
        // Setzt die Versionsbeschreibung und das aktuelle Thema, wenn die Seite navigiert wird
        VersionDescription = $"{Properties.Resources.AppDisplayName} - {_applicationInfoService.GetVersion()}";
        Theme = _themeSelectorService.GetCurrentTheme();
        _isInitialized = true;                                             // Markiert die Seite als initialisiert
        Log.inf($"Navigiert zu SettingsPage. Versionsbeschreibung: {VersionDescription}, aktuelles Thema: {Theme}.");
    }

    public void OnNavigatedFrom()
    {
        // Methode wird aufgerufen, wenn von der Seite weg navigiert wird
        Log.inf("Von SettingsPage weg navigiert.");
    }

    private void OnLightChecked(object sender, RoutedEventArgs e)
    {
        if (_isInitialized)
        {
            _themeSelectorService.SetTheme(AppTheme.Light);                // Setzt das Thema auf "Light", wenn die Seite initialisiert ist
            Log.inf("Thema auf 'Light' gesetzt.");
        }
    }

    private void OnDarkChecked(object sender, RoutedEventArgs e)
    {
        if (_isInitialized)
        {
            _themeSelectorService.SetTheme(AppTheme.Dark);                 // Setzt das Thema auf "Dark", wenn die Seite initialisiert ist
            Log.inf("Thema auf 'Dark' gesetzt.");
        }
    }

    private void OnDefaultChecked(object sender, RoutedEventArgs e)
    {
        if (_isInitialized)
        {
            _themeSelectorService.SetTheme(AppTheme.Default);              // Setzt das Thema auf "Default", wenn die Seite initialisiert ist
            Log.inf("Thema auf 'Default' gesetzt.");
        }
    }

    private void OnPrivacyStatementClick(object sender, RoutedEventArgs e)
    {
        _systemService.OpenInWebBrowser(_appConfig.PrivacyStatement);      // Öffnet die Datenschutzrichtlinie im Webbrowser
        Log.inf($"Datenschutzrichtlinie im Webbrowser geöffnet: {_appConfig.PrivacyStatement}");
    }

    public event PropertyChangedEventHandler PropertyChanged;              // Ereignis für Eigenschaftsänderungen

    private void Set<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
    {
        if (Equals(storage, value))
        {
            return;                                                        // Beendet die Methode, wenn der Wert gleich ist
        }

        storage = value;                                                   // Setzt den neuen Wert
        OnPropertyChanged(propertyName);                                   // Benachrichtigt über die Eigenschaftsänderung
        Log.inf($"Eigenschaft {propertyName} geändert auf {value}.");
    }

    private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); // Methode zur Benachrichtigung über Eigenschaftsänderungen

    public void btnSettingPath_Click(object sender, RoutedEventArgs e)
    {
        Log.inf("Button 'Setting Path' geklickt.");
        FunktionSavePath savePath = new FunktionSavePath();                // Erstellen einer Instanz von FunktionSavePath
        string newPath = savePath.GetNewSavePath();                        // Aufrufen der Methode, um den neuen Pfad zu erhalten

        if (!string.IsNullOrEmpty(newPath))                                // Überprüfen, ob ein Pfad ausgewählt wurde
        {
            Log.inf($"Neuer Pfad ausgewählt: {newPath}");
            if (Directory.Exists(Properties.Settings.Default.pfadDeskOK))
            {
                Directory.Delete(Properties.Settings.Default.pfadDeskOK, true); // Löscht das alte Verzeichnis
                Log.inf($"Altes Verzeichnis gelöscht: {Properties.Settings.Default.pfadDeskOK}");
            }
            Properties.Settings.Default.Reset();                           // Setzt die Einstellungen zurück
            Properties.Settings.Default.pfadDeskOK = newPath;              // Setzt den neuen Pfad
            Properties.Settings.Default.Save();                            // Speichert die Einstellungen
            Log.inf($"Einstellungen zurückgesetzt und neuer Pfad gespeichert: {newPath}");
        }

        btnSettingPath.Content = newPath;                                  // Setzt den Button-Inhalt auf den neuen Pfad
        var iniziStart = new Funktion1Initialisieren();
        iniziStart.Initialisieren();                                       // Initialisiert das Programm
        Log.inf("Programm initialisiert nach Pfadänderung.");
    }

    private void btnReset_Click(object sender, RoutedEventArgs e)
    {
        Log.inf("Button 'Reset' geklickt.");
        FunktionDefaultPath resetPath = new FunktionDefaultPath();
        resetPath.ResetPath();                                             // Setzt den Pfad auf den Standardpfad zurück
        Log.inf("Pfad auf Standardpfad zurückgesetzt.");
        btnSettingPath.Content = Properties.Settings.Default.pfadDeskOK;   // Setzt den Button-Inhalt auf den Standardpfad
        var iniziStart = new Funktion1Initialisieren();
        iniziStart.Initialisieren();                                       // Initialisiert das Programm
        Log.inf("Programm initialisiert nach Pfadrücksetzung.");
    }

    private void btnAdmin_Click(object sender, RoutedEventArgs e)
    {
        Log.inf("Button 'Admin' geklickt.");
        // Schaltet den Admin-Modus um
        if (Properties.Settings.Default.AdminMode)
        {
            Properties.Settings.Default.AdminMode = false;
            Log.inf("Admin-Modus deaktiviert.");
        }
        else
        {
            Properties.Settings.Default.AdminMode = true;
            Log.inf("Admin-Modus aktiviert.");
        }

        Properties.Settings.Default.Save();                                // Speichert die Einstellungen
        Log.inf("Admin-Modus-Einstellung gespeichert.");
    }

    private void btnReload_Click(object sender, RoutedEventArgs e)
    {
        Log.inf("Button 'Reload' geklickt.");
        var result = System.Windows.Forms.MessageBox.Show("Monitordaten erneut Laden, Gespeichertes wird überschrieben", "Reload", MessageBoxButtons.YesNo, MessageBoxIcon.Question); // Zeigt eine Nachricht an, um zu fragen, ob die Monitor-Daten erneut geladen werden sollen

        if (result == DialogResult.Yes)
        {
            Log.inf("Benutzer hat 'Ja' gewählt. Monitordaten werden erneut geladen.");
            var datenLesen = new Funktion2DatenLesen();
            datenLesen.DatenLesen();                                       // Ruft die Methode zum Lesen der Daten auf
            Log.inf("Monitordaten erfolgreich erneut geladen.");
        }
        else
        {
            Log.inf("Benutzer hat 'Nein' gewählt. Monitordaten werden nicht erneut geladen.");
        }
    }
}
