using App3.Contracts.Services;
using App3.Contracts.Views;
using App3.Models;
using App3.Services;
using App3.Funktions;
using Microsoft.Extensions.Options;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace App3.Views;

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
        var defaultPath = new FunktionDefaultPath();
        defaultPath.DefaultPath();
        var dodStatus = new FunktionDesktopOK();
        dodStatus.DODKontrolle();
        btnSettingPath.Content = Properties.Settings.Default.pfadDeskOK;
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
            // Erstellen einer Instanz von FunktionIniPars und Aktualisieren des Pfades
            Properties.Settings.Default.pfadDeskOK = newPath;
            if (!System.IO.Directory.Exists(newPath+"\\Icons") && !System.IO.Directory.Exists(newPath+ "\\BackUps"))
            {
                System.IO.Directory.CreateDirectory(newPath + "\\Icons");
                System.IO.Directory.CreateDirectory(newPath + "\\BackUps");
            }
            else if (!System.IO.Directory.Exists(newPath + "\\Icons") && System.IO.Directory.Exists(newPath + "\\BackUps"))
            {
                System.IO.Directory.CreateDirectory(newPath + "\\Icons");
            }
            else if (System.IO.Directory.Exists(newPath + "\\Icons") && !System.IO.Directory.Exists(newPath + "\\BackUps"))
            {
                System.IO.Directory.CreateDirectory(newPath + "\\BackUps");
            }
            
            Properties.Settings.Default.Save();
        }
        btnSettingPath.Content = newPath;

    }

    private void btnReset_Click(object sender, RoutedEventArgs e)
    {
        FunktionDefaultPath resetPath = new FunktionDefaultPath();
        resetPath.ResetPath();
        btnSettingPath.Content = Properties.Settings.Default.pfadDeskOK;

    }
}
