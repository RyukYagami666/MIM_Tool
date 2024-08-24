using MahApps.Metro.Controls;
using System.ComponentModel;
using System.Windows;
using MIM_Tool.Funktions;
using MIM_Tool.Helpers;

namespace MIM_Tool.Views
{
    public partial class AuswahlFürISP : MetroWindow                                     // Erbt von MetroWindow.
    {
        public event PropertyChangedEventHandler PropertyChanged;                        // Ereignis für Eigenschaftsänderungen.

        public AuswahlFürISP()
        {
            InitializeComponent();                                                       // Initialisiert die Komponenten.
            DataContext = this;                                                          // Setzt den Datenkontext auf die aktuelle Instanz.
            Log.inf("AuswahlFürISP Fenster initialisiert.");
            this.Loaded += AuswahlFürISP_Loaded;                                         // Abonniert das Loaded-Ereignis.
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); // Methode zur Benachrichtigung über Eigenschaftsänderungen.

        public void CloseWindow()
         => Close();                                                                     // Methode zum Schließen des Fensters.
                                                                                         
        public void AuswahlFürISP_Loaded(object sender, RoutedEventArgs e)               // Ereignishandler für das Loaded-Ereignis.
        {
            Log.inf("AuswahlFürISP Fenster geladen.");
            // Setzt die Sichtbarkeit der Monitor-Auswahl-Buttons basierend auf den Einstellungen.
            btnMonitorAuswahl1.Visibility = Properties.Settings.Default.eMonitorVorhanden1 ? Visibility.Visible : Visibility.Collapsed;
            btnMonitorAuswahl2.Visibility = Properties.Settings.Default.eMonitorVorhanden2 ? Visibility.Visible : Visibility.Collapsed;
            btnMonitorAuswahl3.Visibility = Properties.Settings.Default.eMonitorVorhanden3 ? Visibility.Visible : Visibility.Collapsed;
            btnMonitorAuswahl4.Visibility = Properties.Settings.Default.eMonitorVorhanden4 ? Visibility.Visible : Visibility.Collapsed;
            Log.inf("Sichtbarkeit der Monitor-Auswahl-Buttons gesetzt.");

            // Setzt den Inhalt der Monitor-Auswahl-Buttons basierend auf den gespeicherten Informationen.
            if (Properties.Settings.Default.eMonitorVorhanden1)
            {
                string infoMonitor1str = Properties.Settings.Default.InfoMonitor1;                                  // Holt die gespeicherten Informationen für Monitor 1.
                string[] infoMonitor1 = infoMonitor1str.Split(';');                                                 // Teilt die Informationen in ein Array.
                btnMonitorAuswahl1.Content = "Monitor: " + infoMonitor1[1] + "\nNummer: " + infoMonitor1[3];        // Setzt den Inhalt des Buttons.
                Log.inf("Inhalt des Buttons für Monitor 1 gesetzt.");
            }
            if (Properties.Settings.Default.eMonitorVorhanden2)
            {
                string infoMonitor2str = Properties.Settings.Default.InfoMonitor2;                                  // Holt die gespeicherten Informationen für Monitor 2.
                string[] infoMonitor2 = infoMonitor2str.Split(';');                                                 // Teilt die Informationen in ein Array.
                btnMonitorAuswahl2.Content = "Monitor: " + infoMonitor2[1] + "\nNummer: " + infoMonitor2[3];        // Setzt den Inhalt des Buttons.
                Log.inf("Inhalt des Buttons für Monitor 2 gesetzt.");
            }
            if (Properties.Settings.Default.eMonitorVorhanden3)
            {
                string infoMonitor3str = Properties.Settings.Default.InfoMonitor3;                                  // Holt die gespeicherten Informationen für Monitor 3.
                string[] infoMonitor3 = infoMonitor3str.Split(';');                                                 // Teilt die Informationen in ein Array.
                btnMonitorAuswahl3.Content = "Monitor: " + infoMonitor3[1] + "\nNummer: " + infoMonitor3[3];        // Setzt den Inhalt des Buttons.
                Log.inf("Inhalt des Buttons für Monitor 3 gesetzt.");
            }
            if (Properties.Settings.Default.eMonitorVorhanden4)
            {
                string infoMonitor4str = Properties.Settings.Default.InfoMonitor4;                                  // Holt die gespeicherten Informationen für Monitor 4.
                string[] infoMonitor4 = infoMonitor4str.Split(';');                                                 // Teilt die Informationen in ein Array.
                btnMonitorAuswahl4.Content = "Monitor: " + infoMonitor4[1] + "\nNummer: " + infoMonitor4[3];        // Setzt den Inhalt des Buttons.
                Log.inf("Inhalt des Buttons für Monitor 4 gesetzt.");
            }
        }

        // Ereignishandler für die Klick-Ereignisse der Monitor-Auswahl-Buttons.
        private void btnMonitorAuswahl1_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Log.inf("Button für Monitor 1 ausgewählt. Starte FunktionAuswahlZiel.MonitorGewählt4 und schließe das Fenster.");
            FunktionAuswahlZiel auswahlFunkFürISP1 = new FunktionAuswahlZiel();                                     // Erstellt eine neue Instanz von FunktionAuswahlZiel.
            auswahlFunkFürISP1.MonitorGewählt1();                                                                   // Ruft die Methode MonitorGewählt1 auf.
            CloseWindow();                                                                                          // Schließt das Fenster.
        }

        private void btnMonitorAuswahl2_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Log.inf("Button für Monitor 2 ausgewählt. Starte FunktionAuswahlZiel.MonitorGewählt4 und schließe das Fenster.");
            FunktionAuswahlZiel auswahlFunkFürISP2 = new FunktionAuswahlZiel();                                     // Erstellt eine neue Instanz von FunktionAuswahlZiel.
            auswahlFunkFürISP2.MonitorGewählt2();                                                                   // Ruft die Methode MonitorGewählt2 auf.
            CloseWindow();                                                                                          // Schließt das Fenster.
        }

        private void btnMonitorAuswahl3_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Log.inf("Button für Monitor 3 ausgewählt. Starte FunktionAuswahlZiel.MonitorGewählt4 und schließe das Fenster.");
            FunktionAuswahlZiel auswahlFunkFürISP3 = new FunktionAuswahlZiel();                                     // Erstellt eine neue Instanz von FunktionAuswahlZiel.
            auswahlFunkFürISP3.MonitorGewählt3();                                                                   // Ruft die Methode MonitorGewählt3 auf.
            CloseWindow();                                                                                          // Schließt das Fenster.
        }

        private void btnMonitorAuswahl4_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Log.inf("Button für Monitor 4 ausgewählt. Starte FunktionAuswahlZiel.MonitorGewählt4 und schließe das Fenster.");
            FunktionAuswahlZiel auswahlFunkFürISP4 = new FunktionAuswahlZiel();                                     // Erstellt eine neue Instanz von FunktionAuswahlZiel.
            auswahlFunkFürISP4.MonitorGewählt4();                                                                   // Ruft die Methode MonitorGewählt4 auf.
            CloseWindow();                                                                                          // Schließt das Fenster.
        }
    }
}