using App3.Funktions;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Markup;
using App3.Services;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Windows;
using System.Windows.Media;
using System.Net;



namespace App3.Views;

public partial class FunktionPage : Page, INotifyPropertyChanged
{
    public FunktionPage()
    {
        InitializeComponent();
        DataContext = this;
        this.Loaded += FunktionPage_Loaded;

    }

    //  private void InitializeComponent()
    //  {
    //      throw new NotImplementedException();
    //  }

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

    private void FunktionPage_Loaded(object sender, RoutedEventArgs e)
    {
        var dodStatus = new FunktionDesktopOK();
        dodStatus.DODKontrolle();
        var dosStatus = new FunktionDesktopOK();
        dosStatus.DOSKontrolle();
        var dorStatus = new FunktionDesktopOK();
        dorStatus.DORKontrolle();

        var transparentGreen = new SolidColorBrush(Color.FromArgb(20, 0, 255, 0)); // 50% Transparenz grünen
        var transparentRed = new SolidColorBrush(Color.FromArgb(20, 255, 0, 0)); // 50% Transparenz roten

        btnDeskOkDownload.Background = Properties.Settings.Default.eDeskOkDownloadReady ? transparentGreen : transparentRed;
        btnDOSavePos.Background = Properties.Settings.Default.eDeskOkSavePosReady ? transparentGreen : transparentRed;
        btnDOReadData.Background = Properties.Settings.Default.eDeskOkDataReedReady ? transparentGreen : transparentRed;
    }

    private void btnGetIconList_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        Funktion1.Execute(); // Rufen Sie die Execute-Methode von Funktion1 auf
        ISPSaveState.IsReady = true;
        this.Loaded += FunktionPage_Loaded;
    }

    private void btnMoniScann_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        var deskScen = new FunktionDeskScen();
        deskScen.AuslesenMonitoreUndPositionen();
        this.Loaded += FunktionPage_Loaded;
    }

    private void btnIniziStart_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        var defaultPath = new FunktionDefaultPath();
        defaultPath.DefaultPath();

        var openPath = new FunktionDefaultPath();
        openPath.OpenConfig();
        this.Loaded += FunktionPage_Loaded;
    }

    private void btnDeskOkDownload_Click(object sender, System.Windows.RoutedEventArgs e)
    {

        var dodStart = new FunktionDesktopOK();
        dodStart.DODStart();
    }

    private void btnDOSavePos_Click(object sender, RoutedEventArgs e)
    {


        var doIconPos = new FunktionDesktopOK();
        doIconPos.IconSavePos();
    }

    private void btnDOReadData_Click(object sender, RoutedEventArgs e)
    {
        var doReadData = new FunktionDesktopOK();
        doReadData.DataRead(Properties.Settings.Default.eDeskOkLastSave);
    }
}