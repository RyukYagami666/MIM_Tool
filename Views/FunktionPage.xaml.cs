using MIM_Tool.Funktions;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Drawing;
using System.Windows.Media.Imaging;
using MIM_Tool.Helpers;

namespace MIM_Tool.Views//-------------------------------------------------------------------------------------------Inizialisieren-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
{
    public partial class FunktionPage : Page, INotifyPropertyChanged                                     // Erbt von Page und implementiert INotifyPropertyChanged.
    {
        public FunktionPage()
        {
            InitializeComponent();                                                                       // Initialisiert die Komponenten.
            DataContext = this;                                                                          // Setzt den Datenkontext auf die aktuelle Instanz.
            this.Loaded += FunktionPage_Loaded;                                                          // Abonniert das Loaded-Ereignis.
            Log.inf("FunktionPage initialisiert.");
            LoadIcons();
        }

        public event PropertyChangedEventHandler PropertyChanged;                                        // Ereignis für Eigenschaftsänderungen.

        private void Set<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)       // Methode zum Setzen einer Eigenschaft.
        {
            if (Equals(storage, value))
            {
                return;                                                                                  // Beendet die Methode, wenn der Wert gleich ist.
            }

            storage = value;                                                                             // Setzt den neuen Wert.
            OnPropertyChanged(propertyName);                                                             // Benachrichtigt über die Eigenschaftsänderung.
            Log.inf($"Eigenschaft {propertyName} geändert.");
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); // Methode zur Benachrichtigung über Eigenschaftsänderungen.

        private void FunktionPage_Loaded(object sender, RoutedEventArgs e)                               // Ereignishandler für das Loaded-Ereignis.
        {
            Log.inf("FunktionPage geladen.");
            Properties.Settings.Default.SelectetMonitor = 10;                                            // Setzt den ausgewählten Monitor auf 10.
            Properties.Settings.Default.Save();                                                          // Speichert die Einstellungen.
            Log.inf("Ausgewählter Monitor auf 10 gesetzt und Einstellungen gespeichert.");

            var dodStatus = new FunktionDesktopOK();
            dodStatus.DODKontrolle();
            dodStatus.DOSKontrolle();
            dodStatus.DORKontrolle();
            Log.inf("DesktopOk-Kontrollen durchgeführt.");
            var mmdStatus = new FunktionMultiMonitor();
            mmdStatus.MMDKontrolle();                                                                    // Führt die DOR-Kontrolle durch.
            mmdStatus.MMSKontrolle();
            mmdStatus.MMRKontrolle();
            Log.inf("MultiMonitorTool-Kontrolle durchgeführt.");

            var transparentGreen = new SolidColorBrush(System.Windows.Media.Color.FromArgb(20, 0, 255, 0));                   // 50% Transparenz grünen.
            var transparentRed = new SolidColorBrush(System.Windows.Media.Color.FromArgb(20, 255, 0, 0));                     // 50% Transparenz roten.

            // Setzt die Sichtbarkeit der Buttons basierend auf dem Admin-Modus.
            btnGetIconList.Visibility = Properties.Settings.Default.AdminMode ? Visibility.Visible : Visibility.Collapsed;
            btnMoniScann.Visibility = Properties.Settings.Default.AdminMode ? Visibility.Visible : Visibility.Collapsed;
            btnIniziStart.Visibility = Properties.Settings.Default.AdminMode ? Visibility.Visible : Visibility.Collapsed;
            btnDeskOkDownload.Visibility = Properties.Settings.Default.AdminMode ? Visibility.Visible : Visibility.Collapsed;
            btnDOSavePos.Visibility = Properties.Settings.Default.AdminMode ? Visibility.Visible : Visibility.Collapsed;
            btnDOReadData.Visibility = Properties.Settings.Default.AdminMode ? Visibility.Visible : Visibility.Collapsed;
            btnDOBearbeiten.Visibility = Properties.Settings.Default.AdminMode ? Visibility.Visible : Visibility.Collapsed;
            btnVerschieben.Visibility = Properties.Settings.Default.AdminMode ? Visibility.Visible : Visibility.Collapsed;
            btnMoniOff.Visibility = Properties.Settings.Default.AdminMode ? Visibility.Visible : Visibility.Collapsed;
            Log.inf("Sichtbarkeit der Admin-Buttons gesetzt.");
            LoadIconStatus();
        }
        private void LoadIcons()
        {
            Log.inf("Lade Icons.");
            BitmapImage bitmapImage1 = LoadIcon(Properties.Settings.Default.pfadDeskOK + "\\DesktopOK.exe");
            BitmapImage bitmapImage2 = LoadIcon(Properties.Settings.Default.pfadDeskOK + "\\MultiMonitorTool.exe");

            if (bitmapImage1 != null) DOImage.Source = bitmapImage1;
            if (bitmapImage2 != null) MMImage.Source = bitmapImage2;
            Log.inf("Icons geladen und gesetzt.");
        }

        private BitmapImage LoadIcon(string iconPath)
        {
            if (System.IO.File.Exists(iconPath))
            {
                Icon icon = System.Drawing.Icon.ExtractAssociatedIcon(iconPath);
                Log.inf($"Icon von {iconPath} geladen.");
                return FunktionIconListe.ConvertIconToImageSource(icon);
            }
            Log.war($"Icon-Pfad {iconPath} existiert nicht.");
            return null;
        }
        private void LoadIconStatus()
        {
            Log.inf("Lade Icon-Status.");

            var transparentGreen = new SolidColorBrush(System.Windows.Media.Color.FromArgb(30, 0, 255, 0));                   // 50% Transparenz grünen.

            if (Properties.Settings.Default.eDeskOkDownloadDone)
            {
                icoDO1Status.Background = transparentGreen;
                icoDO1Status.ToolTip = $"Donwload fertig: zuletzt ausgefüh am: {Properties.Settings.Default.eDeskOkDownloadDate}";
            }
            else
            {
                icoDO1Status.ToolTip = "Donwload: nicht fertig.";
            }
            if (Properties.Settings.Default.eDeskOkSavePosDone)
            {
                icoDO2Status.Background = transparentGreen;
                icoDO2Status.ToolTip = $"Icon Pos Save: zuletzt ausgefüh am: {Properties.Settings.Default.eDeskOkSavePosDate}";
            }
            else
            {
                icoDO2Status.ToolTip = "Icon Pos: nicht Gespeichert.";
            }
            if (Properties.Settings.Default.eDeskOkDataReedDone)
            {
                icoDO3Status.Background = transparentGreen;
                icoDO3Status.ToolTip = $"Icon Daten Lesen: zuletzt ausgefüh am: {Properties.Settings.Default.eDeskOkDataReedDate}";
            }
            else
            {
                icoDO3Status.ToolTip = "Icon Daten: nicht Gespeichert.";
            }

            if (Properties.Settings.Default.eMultiMonDownloadDone)
            {
                icoMM1Status.Background = transparentGreen;
                icoMM1Status.ToolTip = $"Donwload fertig: zuletzt ausgefüh am: {Properties.Settings.Default.eMultiMonDownloadDate}";
            }
            else
            {
                icoMM1Status.ToolTip = "Donwload: nicht fertig.";
            }
            if (Properties.Settings.Default.eMultiMonSavePosDone)
            {
                icoMM2Status.Background = transparentGreen;
                icoMM2Status.ToolTip = $"Monitor Configs Save; zuletzt ausgefüh am: {Properties.Settings.Default.eMultiMonSavePosDate}";
            }
            else
            {
                icoMM2Status.ToolTip = "Icon Pos: nicht Gespeichert.";
            }
            if (Properties.Settings.Default.eMultiMonSaveDataDone)
            {
                icoMM3Status.Background = transparentGreen;
                icoMM3Status.ToolTip = $"Monitor Infos Save; zuletzt ausgefüh am: {Properties.Settings.Default.eMultiMonSaveDataDate}";
            }
            else
            {
                icoMM3Status.ToolTip = "Monitor Infos: nicht Gespeichert.";
            }
            string doImageToolTip = DOImage.ToolTip.ToString();
            string mmImageToolTip = MMImage.ToolTip.ToString();
            if (!doImageToolTip.StartsWith("DesktopOK")) DOImage.ToolTip = "DesktopOK v" + Properties.Settings.Default.eDeskOkVers + DOImage.ToolTip;
            if (!mmImageToolTip.StartsWith("MultiMonitorTool")) MMImage.ToolTip = "MultiMonitorTool v" + Properties.Settings.Default.eMultiMonVers + MMImage.ToolTip;
            StatusTB11.Text = Properties.Settings.Default.eUsername;
            if (Properties.Settings.Default.SelectetMonitor == 10) StatusTB12.Text = "Keiner";
            else StatusTB12.Text = Convert.ToString(Properties.Settings.Default.SelectetMonitor);
            StatusTB13.Text = Properties.Settings.Default.Inizialisiert ? "Ja" : "Nein";
            StatusTB14.Text = Properties.Settings.Default.AdminMode ? "Admin" : "Benutzer";
            StatusTB15.Text = Properties.Settings.Default.FehlerPreReload ? "Fehler" : "OK";
            if (!string.IsNullOrEmpty(Properties.Settings.Default.LetzterFehler)) btnFehlerReset.Content = Properties.Settings.Default.LetzterFehler;
            else btnFehlerReset.Content = "Keine fehler aufgetreten, Läuft!";
            Log.inf("Icon-Status geladen.");
        }
        //------------------------------------------------------------------------------------------------------------------------Ereignishandler-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        // Ereignishandler für die Klick-Ereignisse der Buttons.
        private void btnGetIconList_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Log.inf("Button 'Get Icon List' geklickt.");
            FunktionIconListe.Execute();                                                               // Ruft die Execute-Methode von FunktionIconListe auf.
            this.Loaded += FunktionPage_Loaded;                                                        // Abonniert das Loaded-Ereignis erneut.
        }

        private void btnMoniScann_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Log.inf("Button 'Moni Scann' geklickt.");
            var mmrStatus = new FunktionMultiMonitor();
            mmrStatus.MMRKontrolle();                                                                  // Führt die MMR-Kontrolle durch.
            var mmrData = new FunktionDeskScen();
            mmrData.MonitorSaveData();                                                                 // Speichert die Monitor-Daten.
            Log.inf("Monitor-Daten gespeichert.");

            var mmDataRead = new FunktionDeskScen();
            mmDataRead.DataRead(Properties.Settings.Default.pfadDeskOK + "\\MonitorDaten.txt");        // Liest die Monitor-Daten.
            Log.inf("Monitor-Daten gelesen.");

            var mmDataTrim = new FunktionVergleich();
            mmDataTrim.MultiMonDataTrim();                                                             // Trimmt die Monitor-Daten.
            Log.inf("Monitor-Daten getrimmt.");
            this.Loaded += FunktionPage_Loaded;                                                        // Abonniert das Loaded-Ereignis erneut.
        }

        private void btnIniziStart_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Log.inf("Button 'Inizi Start' geklickt.");
            var defaultPath = new Funktion1Initialisieren();
            defaultPath.InitialisierenAbfrage();                                                       // Führt die Initialisierungsabfrage durch.
            Log.inf("Initialisierungsabfrage durchgeführt.");

            var openPath = new FunktionDefaultPath();
            openPath.OpenConfig();                                                                     // Öffnet die Konfiguration.
            Log.inf("Konfiguration geöffnet.");
            this.Loaded += FunktionPage_Loaded;                                                        // Abonniert das Loaded-Ereignis erneut.
        }

        private void btnDeskOkDownload_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Log.inf("Button 'Desk OK Download' geklickt.");
            var dodStart = new FunktionDesktopOK();
            dodStart.DODStart();                                                                       // Startet die DOD-Funktion.
            Log.inf("DOD-Funktion gestartet.");
            var mmdStart = new FunktionMultiMonitor();
            mmdStart.MMDStart();                                                                       // Startet die MMD-Funktion.
            Log.inf("MMD-Funktion gestartet.");
        }

        private void btnDOSavePos_Click(object sender, RoutedEventArgs e)
        {
            Log.inf("Button 'DO Save Pos' geklickt.");
            var doIconPos = new FunktionDesktopOK();
            doIconPos.IconSavePos();                                                                   // Speichert die Position der Icons.
            Log.inf("Icon-Positionen gespeichert.");
        }

        private void btnDOReadData_Click(object sender, RoutedEventArgs e)
        {
            Log.inf("Button 'DO Read Data' geklickt.");
            var doReadData = new FunktionDesktopOK();
            doReadData.DataRead(Properties.Settings.Default.eDeskOkLastSave);                          // Liest die gespeicherten Daten.
            Log.inf("Daten gelesen.");
        }

        private void btnDOBearbeiten_Click(object sender, RoutedEventArgs e)
        {
            Log.inf("Button 'DO Bearbeiten' geklickt.");
            var doConvert = new FunktionVergleich();
            doConvert.AbwandelnDerData();                                                              // Wandelt die Daten ab.
            Log.inf("Daten abgewandelt.");
        }

        private void btnVerschieben_Click(object sender, RoutedEventArgs e)
        {
            Log.inf("Button 'Verschieben' geklickt.");
            MessageBox.Show("Ohne Rücksicht auf verluste, alles zurück auf Desktop");
            var iconMove = new FunktionVerschieben();
            iconMove.MovePathToDesk(0);
            iconMove.MovePathToDesk(1);
            iconMove.MovePathToDesk(2); 
            iconMove.MovePathToDesk(3);   // Verschiebt die Icons.
            Log.inf("Icons verschoben.");
            string pathExe = $"{Properties.Settings.Default.pfadDeskOK}\\DesktopOk.exe";
            FunktionDesktopOK.IconRestore(pathExe, Properties.Settings.Default.eDeskOkLastSave);
        }

        private void btnMoniOff_Click(object sender, RoutedEventArgs e)
        {
            Log.inf("Button 'Moni Off' geklickt.");
            // Erstelle und zeige das Monitor-Auswahlfenster
            var monitorSelectionWindow = new MonitorSelectionWindow();
            if (monitorSelectionWindow.ShowDialog() == true)
            {
                int selectedMonitor = monitorSelectionWindow.SelectedMonitor;
                string pathExe = $"{Properties.Settings.Default.pfadDeskOK}\\MultiMonitorTool.exe";

                // Deaktiviere den ausgewählten Monitor
                FunktionMultiMonitor.MonitorSwitch(pathExe, selectedMonitor);
                Log.inf($"Monitor {selectedMonitor + 1} deaktiviert.");
            }
            else
            {
                Log.inf("Keine Monitor-Auswahl getroffen.");
            }
        }

        private void btnFehlerReset_Click(object sender, RoutedEventArgs e)
        {
            Log.inf("Button 'Fehler Reset' geklickt.");
            Properties.Settings.Default.LetzterFehler = "";                                            // Setzt den letzten Fehler zurück.
            Properties.Settings.Default.Save();                                                        // Speichert die Einstellungen.

            Log.inf("Letzter Fehler zurückgesetzt und Einstellungen gespeichert.");
            LoadIconStatus();                                                                          // Abonniert das Loaded-Ereignis erneut.

        }

        private void btnTest1_Cilck(object sender, RoutedEventArgs e)
        {
           
            var monitorSelectionWindow = new MonitorSelectionWindow();
            if (monitorSelectionWindow.ShowDialog() == true)
            {
                int selectedMonitor = monitorSelectionWindow.SelectedMonitor;
              
                Log.inf($"Icons holen {selectedMonitor + 1} deaktiviert.");
            }
            else
            {
                Log.inf("Keine Monitor-Auswahl getroffen.");
            }
        }

        private void btnTest2_Cilck(object sender, RoutedEventArgs e)
        {
            FunktionMultiMonitor.MMTTest(1);
            MessageBox.Show("Test2");
        }

        private void btnTest3_Cilck(object sender, RoutedEventArgs e)
        {
            FunktionMultiMonitor.MMTTest(2);
            MessageBox.Show("Test3");
        }

        private void btnTest4_Cilck(object sender, RoutedEventArgs e)
        {
            var loadConfig = new FunktionMultiMonitor();
            loadConfig.MonitorLoadConfig();
            loadConfig.MonitorLoadConfig();
            MessageBox.Show("Test4");
        }
    }
}




