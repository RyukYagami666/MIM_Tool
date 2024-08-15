using MIM_Tool.Contracts.Services;
using MIM_Tool.Contracts.Views;
using MIM_Tool.Models;
using MIM_Tool.Services;
using MIM_Tool.Funktions;
using Microsoft.Extensions.Options;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.IO;

namespace MIM_Tool.Views;

public partial class SettingsPage : Page, INotifyPropertyChanged, INavigationAware
{
    private readonly AppConfig _appConfig;
    private readonly IThemeSelectorService _themeSelectorService;
    private readonly ISystemService _systemService;
    private readonly IApplicationInfoService _applicationInfoService;
    private bool _isInitialized;
    private AppTheme _theme;
    private string _versionDescription;

    public AppTheme Theme
    {
        get { return _theme; }
        set { Set(ref _theme, value); }
    }

    public string VersionDescription
    {
        get { return _versionDescription; }
        set { Set(ref _versionDescription, value); }
    }

    public SettingsPage(IOptions<AppConfig> appConfig, IThemeSelectorService themeSelectorService, ISystemService systemService, IApplicationInfoService applicationInfoService)
    {
        _appConfig = appConfig.Value;
        _themeSelectorService = themeSelectorService;
        _systemService = systemService;
        _applicationInfoService = applicationInfoService;
        InitializeComponent();
        DataContext = this;
        btnSettingPath.Content = Properties.Settings.Default.pfadDeskOK;
        Properties.Settings.Default.SelectetMonitor = 10;
        Properties.Settings.Default.Save();
    }

    public void OnNavigatedTo(object parameter)
    {
        VersionDescription = $"{Properties.Resources.AppDisplayName} - {_applicationInfoService.GetVersion()}";
        Theme = _themeSelectorService.GetCurrentTheme();
        _isInitialized = true;
    }

    public void OnNavigatedFrom()
    {
    }

    private void OnLightChecked(object sender, RoutedEventArgs e)
    {
        if (_isInitialized)
        {
            _themeSelectorService.SetTheme(AppTheme.Light);
        }
    }

    private void OnDarkChecked(object sender, RoutedEventArgs e)
    {
        if (_isInitialized)
        {
            _themeSelectorService.SetTheme(AppTheme.Dark);
        }
    }

    private void OnDefaultChecked(object sender, RoutedEventArgs e)
    {
        if (_isInitialized)
        {
            _themeSelectorService.SetTheme(AppTheme.Default);
        }
    }

    private void OnPrivacyStatementClick(object sender, RoutedEventArgs e)
        => _systemService.OpenInWebBrowser(_appConfig.PrivacyStatement);

    public event PropertyChangedEventHandler PropertyChanged;

    private void Set<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
    {
        if (Equals(storage, value))
        {
            return;
        }

        storage = value;
        OnPropertyChanged(propertyName);
    }

    private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

   

    public void btnSettingPath_Click(object sender, RoutedEventArgs e)
    {

        // Erstellen einer Instanz von FunktionSavePath
        FunktionSavePath savePath = new FunktionSavePath();
        // Aufrufen der Methode, um den neuen Pfad zu erhalten
        string newPath = savePath.GetNewSavePath();
        // Überprüfen, ob ein Pfad ausgewählt wurde
        if (!string.IsNullOrEmpty(newPath))
        {
            Directory.Delete(Properties.Settings.Default.pfadDeskOK,true);
            Properties.Settings.Default.Reset();
            Properties.Settings.Default.pfadDeskOK = newPath;
            Properties.Settings.Default.Save();
        }
        btnSettingPath.Content = newPath;
        var iniziStart = new Funktion1Initialisieren();
        iniziStart.Initialisieren();

    }

    private void btnReset_Click(object sender, RoutedEventArgs e)
    {
        FunktionDefaultPath resetPath = new FunktionDefaultPath();
        resetPath.ResetPath();
        btnSettingPath.Content = Properties.Settings.Default.pfadDeskOK;
        var iniziStart = new Funktion1Initialisieren();
        iniziStart.Initialisieren();
    }

    private void btnAdmin_Click(object sender, RoutedEventArgs e)
    {
        if (Properties.Settings.Default.AdminMode) Properties.Settings.Default.AdminMode = false;
        else Properties.Settings.Default.AdminMode = true;
        
        Properties.Settings.Default.Save();
    }
}
