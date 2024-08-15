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



namespace App3.Views
{
    public partial class FunktionPage : Page, INotifyPropertyChanged
    {
        public FunktionPage()
        {
            InitializeComponent();
            DataContext = this;
            this.Loaded += FunktionPage_Loaded;
    
        }
    
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
            Properties.Settings.Default.SelectetMonitor = 10;
            Properties.Settings.Default.Save();
    
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
            FunktionIconListe.Execute(); // Rufen Sie die Execute-Methode von Funktion1 auf
            //ISPSaveState.IsReady = true;
            this.Loaded += FunktionPage_Loaded;
        }
    
        private void btnMoniScann_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var mmrStatus = new FunktionMultiMonitor();
            mmrStatus.MMRKontrolle();
            var mmrData = new FunktionDeskScen();
            mmrData.MonitorSaveData();
    
            var mmDataRead = new FunktionDeskScen();
            mmDataRead.DataRead(Properties.Settings.Default.pfadDeskOK + "\\MonitorDaten.txt");
    
            var mmDataTrim = new FunktionVergleich();
            mmDataTrim.MultiMonDataTrim();
            this.Loaded += FunktionPage_Loaded;
        }
    
        private void btnIniziStart_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var defaultPath = new Funktion1Initialisieren();
            defaultPath.InitialisierenAbfrage();
    
            var openPath = new FunktionDefaultPath();
            openPath.OpenConfig();
            this.Loaded += FunktionPage_Loaded;
        }
    
        private void btnDeskOkDownload_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var dodStart = new FunktionDesktopOK();
            dodStart.DODStart();
            var mmdStart = new FunktionMultiMonitor();
            mmdStart.MMDStart();
    
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
    
        private void btnDOBearbeiten_Click(object sender, RoutedEventArgs e)
        {
            var doConvert = new FunktionVergleich();
            doConvert.AbwandelnDerData();
        }
    
        private void btnVerschieben_Click(object sender, RoutedEventArgs e)
        {
            var iconMove = new FunktionVerschieben();
            iconMove.Verschieben1Control();
    
        }
    
        private void btnMoniOff_Click(object sender, RoutedEventArgs e)
        {
            string pathExe = $"{Properties.Settings.Default.pfadDeskOK}\\MultiMonitorTool.exe";
            
            FunktionMultiMonitor.MonitorDeaktivieren(pathExe, "2752",0);
        }
    }
}