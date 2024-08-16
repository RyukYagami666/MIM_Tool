using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using MIM_Tool.Funktions;
using MIM_Tool.Services;
using static MIM_Tool.Funktions.FunktionIconListe;                                                                                     

namespace MIM_Tool.Views;//--------------------------------------------------------------------------------------Inizialiesieren-----------------------------------------------------------------------------

public partial class IconSavePage : Page, INotifyPropertyChanged
{
    public IconSavePage()
    {
        InitializeComponent();                                                                                // Initialisiert die Komponenten der Seite
        DataContext = this;                                                                                   // Setzt den Datenkontext auf die aktuelle Instanz
        FunktionIconListe.Execute();                                                                          // Ruft die Execute-Methode von FunktionIconListe auf
        IconListView.ItemsSource = MIM_Tool.Funktions.FunktionIconListe.LastExecutedFiles;                    // Setzt die Datenquelle der ListView
        this.Loaded += IconSavePage_Loaded;                                                                   // Abonniert das Loaded-Ereignis
        var dodStatus = new FunktionDesktopOK();
        dodStatus.DODKontrolle();                                                                             // Führt die DOD-Kontrolle durch
    }

    public event PropertyChangedEventHandler PropertyChanged;                                                 // Ereignis für Eigenschaftsänderungen

    private void IconSavePage_Loaded(object sender, RoutedEventArgs e)
    {
                                                                                                              // Setzt die Sichtbarkeit des Speichern-Buttons basierend auf dem Zustand von ISPSaveState
        btnListeSpeichern.Visibility = ISPSaveState.IsReady ? Visibility.Visible : Visibility.Collapsed;
    }

    private void Set<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
    {
        if (Equals(storage, value))
        {
            return;                                                                                                                     // Beendet die Methode, wenn der Wert gleich ist
        }

        storage = value;                                                                                                                // Setzt den neuen Wert
        OnPropertyChanged(propertyName);                                                                                                // Benachrichtigt über die Eigenschaftsänderung
    }

    private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));   // Methode zur Benachrichtigung über Eigenschaftsänderungen

    //------------------------------------------------------------------------------------------------------------------------Ereignishandler-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    private void btnListeSpeichern_Click(object sender, RoutedEventArgs e)
    {
        // Holt die ausgewählten Pfade aus der ListView
        var selectedPaths = IconListView.SelectedItems.Cast<FileIconInfo>().Select(item => item.Path).ToArray();
        string selectedItemsString = string.Join(";", selectedPaths);                                                                   // Verbindet die Pfade zu einem String
        Properties.Settings.Default.DeskIconPfadMTemp = selectedItemsString;                                                            // Speichert die Pfade in den Einstellungen
        Properties.Settings.Default.eMonitorIconsCountTemp = selectedPaths.Length;                                                      // Speichert die Anzahl der ausgewählten Icons
        Properties.Settings.Default.Save();                                                                                             // Speichert die Einstellungen
        MessageBox.Show($"Anzahl der ausgewählten Elemente: {selectedPaths.Length}\nPfad: {Properties.Settings.Default.DeskIconPfadMTemp}", "Information", MessageBoxButton.OK, MessageBoxImage.Information); // Zeigt eine Informationsnachricht an

        if (selectedPaths.Length > 0)
        {
            // Überprüft den ausgewählten Monitor und führt entsprechende Aktionen aus
            if (Properties.Settings.Default.SelectetMonitor == 10)
            {
                var auswahlFürISP = new AuswahlFürISP();
                auswahlFürISP.Show();                                                 // Zeigt das AuswahlFürISP-Fenster an
            }
            else if (Properties.Settings.Default.SelectetMonitor == 0)
            {
                FunktionAuswahlZiel auswahlFunk = new FunktionAuswahlZiel();
                auswahlFunk.MonitorGewählt1();                                        // Führt die MonitorGewählt1-Methode aus

                this.NavigationService.Navigate(new HauptseitePage());                // Navigiert zur Hauptseite
            }
            else if (Properties.Settings.Default.SelectetMonitor == 1)
            {
                FunktionAuswahlZiel auswahlFunk = new FunktionAuswahlZiel();
                auswahlFunk.MonitorGewählt2();                                        // Führt die MonitorGewählt2-Methode aus

                this.NavigationService.Navigate(new HauptseitePage());                // Navigiert zur Hauptseite
            }
            else if (Properties.Settings.Default.SelectetMonitor == 2)
            {
                FunktionAuswahlZiel auswahlFunk = new FunktionAuswahlZiel();
                auswahlFunk.MonitorGewählt3();                                        // Führt die MonitorGewählt3-Methode aus

                this.NavigationService.Navigate(new HauptseitePage());                // Navigiert zur Hauptseite
            }
            else if (Properties.Settings.Default.SelectetMonitor == 3)
            {
                FunktionAuswahlZiel auswahlFunk = new FunktionAuswahlZiel();
                auswahlFunk.MonitorGewählt4();                                        // Führt die MonitorGewählt4-Methode aus

                this.NavigationService.Navigate(new HauptseitePage());                // Navigiert zur Hauptseite
            }
        }
        else
        {
            // Zeigt eine Informationsnachricht an, wenn keine Icons ausgewählt wurden
            MessageBox.Show("Bitte wähle Icons aus um zu speichern", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}