using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using MIM_Tool.Funktions;
using MIM_Tool.Services;
using static MIM_Tool.Funktions.FunktionIconListe;
using MIM_Tool.Helpers;

namespace MIM_Tool.Views;//--------------------------------------------------------------------------------------Inizialiesieren-----------------------------------------------------------------------------

public partial class IconSavePage : Page, INotifyPropertyChanged
{
    public IconSavePage()
    {
        InitializeComponent();                                                                                                          // Initialisiert die Komponenten der Seite
        DataContext = this;                                                                                                             // Setzt den Datenkontext auf die aktuelle Instanz
        Log.inf("IconSavePage initialisiert. Komponenten und Datenkontext gesetzt.");
        FunktionIconListe.Execute();                                                                                                    // Ruft die Execute-Methode von FunktionIconListe auf
        Log.inf("FunktionIconListe.Execute() aufgerufen.");
        IconListView.ItemsSource = MIM_Tool.Funktions.FunktionIconListe.LastExecutedFiles;                                              // Setzt die Datenquelle der ListView
        Log.inf("Datenquelle der ListView gesetzt.");
        this.Loaded += IconSavePage_Loaded;                                                                                             // Abonniert das Loaded-Ereignis
        Log.inf("Loaded-Ereignis abonniert.");
        var dodStatus = new FunktionDesktopOK();
        dodStatus.DODKontrolle();                                                                                                       // Führt die DOD-Kontrolle durch
        Log.inf("DOD-Kontrolle durchgeführt.");
    }

    public event PropertyChangedEventHandler PropertyChanged;                                                                           // Ereignis für Eigenschaftsänderungen

    private void IconSavePage_Loaded(object sender, RoutedEventArgs e)
    {
                                                                                                                                        // Setzt die Sichtbarkeit des Speichern-Buttons basierend auf dem Zustand von ISPSaveState
        btnListeSpeichern.Visibility = ISPSaveState.IsReady ? Visibility.Visible : Visibility.Collapsed;
        Log.inf($"IconSavePage geladen. Sichtbarkeit des Speichern-Buttons gesetzt: {btnListeSpeichern.Visibility}");
    }

    private void Set<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
    {
        if (Equals(storage, value))
        {
            return;                                                                                                                     // Beendet die Methode, wenn der Wert gleich ist
        }

        storage = value;                                                                                                                // Setzt den neuen Wert
        OnPropertyChanged(propertyName);                                                                                                // Benachrichtigt über die Eigenschaftsänderung
        Log.inf($"Eigenschaft {propertyName} geändert auf {value}.");
    }

    private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));   // Methode zur Benachrichtigung über Eigenschaftsänderungen

    private void btnListeSpeichern_Click(object sender, RoutedEventArgs e)
    {
        Log.inf("Button 'Liste Speichern' geklickt.");
                                                                                                                                        // Holt die ausgewählten Pfade aus der ListView
        var selectedPaths = IconListView.SelectedItems.Cast<FileIconInfo>().Select(item => item.Path).ToArray();
        string selectedItemsString = string.Join(";", selectedPaths);                                                                   // Verbindet die Pfade zu einem String
        Properties.Settings.Default.DeskIconPfadMTemp = selectedItemsString;                                                            // Speichert die Pfade in den Einstellungen
        Properties.Settings.Default.eMonitorIconsCountTemp = selectedPaths.Length;                                                      // Speichert die Anzahl der ausgewählten Icons
        Properties.Settings.Default.Save();                                                                                             // Speichert die Einstellungen
        Log.inf($"Anzahl der ausgewählten Elemente: {selectedPaths.Length}, Pfade: {selectedItemsString}");
       
        if (selectedPaths.Length > 0)
        {
            Log.inf("Ausgewählte Pfade vorhanden. Überprüfe den ausgewählten Monitor.");
            // Überprüft den ausgewählten Monitor und führt entsprechende Aktionen aus
            if (Properties.Settings.Default.SelectetMonitor == 10)
            {
                var auswahlFürISP = new AuswahlFürISP();
                auswahlFürISP.Show();                                                 // Zeigt das AuswahlFürISP-Fenster an
                Log.inf("AuswahlFürISP-Fenster angezeigt.");
            }
            else if (Properties.Settings.Default.SelectetMonitor == 0)
            {
                FunktionAuswahlZiel auswahlFunk = new FunktionAuswahlZiel();
                auswahlFunk.MonitorGewählt1();                                        // Führt die MonitorGewählt1-Methode aus
                Log.inf("Monitor 1 ausgewählt. MonitorGewählt1-Methode ausgeführt.");

                this.NavigationService.Navigate(new HauptseitePage());                // Navigiert zur Hauptseite
                Log.inf("Zur Hauptseite navigiert.");
            }
            else if (Properties.Settings.Default.SelectetMonitor == 1)
            {
                FunktionAuswahlZiel auswahlFunk = new FunktionAuswahlZiel();
                auswahlFunk.MonitorGewählt2();                                        // Führt die MonitorGewählt2-Methode aus
                Log.inf("Monitor 2 ausgewählt. MonitorGewählt2-Methode ausgeführt.");

                this.NavigationService.Navigate(new HauptseitePage());                // Navigiert zur Hauptseite
                Log.inf("Zur Hauptseite navigiert.");
            }
            else if (Properties.Settings.Default.SelectetMonitor == 2)
            {
                FunktionAuswahlZiel auswahlFunk = new FunktionAuswahlZiel();
                auswahlFunk.MonitorGewählt3();                                        // Führt die MonitorGewählt3-Methode aus
                Log.inf("Monitor 3 ausgewählt. MonitorGewählt3-Methode ausgeführt.");

                this.NavigationService.Navigate(new HauptseitePage());                // Navigiert zur Hauptseite
                Log.inf("Zur Hauptseite navigiert.");
            }
            else if (Properties.Settings.Default.SelectetMonitor == 3)
            {
                FunktionAuswahlZiel auswahlFunk = new FunktionAuswahlZiel();
                auswahlFunk.MonitorGewählt4();                                        // Führt die MonitorGewählt4-Methode aus
                Log.inf("Monitor 4 ausgewählt. MonitorGewählt4-Methode ausgeführt.");

                this.NavigationService.Navigate(new HauptseitePage());                // Navigiert zur Hauptseite
                Log.inf("Zur Hauptseite navigiert.");
            }
        }
        else
        {
            // Zeigt eine Informationsnachricht an, wenn keine Icons ausgewählt wurden
            Log.war("Bitte wähle Icons aus um zu speichern");
        }
    }
}
