using System.Windows.Forms;

namespace MIM_Tool.Funktions
{
    internal class Funktion2DatenLesen
    {
        // Methode zum Lesen der Daten
        public void DatenLesen()
        {                                                                            
            var dosStatus = new FunktionDesktopOK();                         
            dosStatus.DOSKontrolle();                                        // Überprüft den Desktop-Status
                                                                             
            var doIconPos = new FunktionDesktopOK();                         
            doIconPos.IconSavePos();                                         // Speichert die Position der Icons
                                                                             
            var mmsStatus = new FunktionMultiMonitor();                      
            mmsStatus.MMSKontrolle();                                        // Überprüft den Multi-Monitor-Status
                                                                             
            var monSaveConfig = new FunktionMultiMonitor();                  
            monSaveConfig.MonitorSaveConfig();                               // Speichert die Monitor-Konfiguration
                                                                             
            var mmrStatus = new FunktionMultiMonitor();                      
            mmrStatus.MMRKontrolle();                                        // Überprüft den Multi-Monitor-Status erneut
                                                                             
            var monSaveData = new FunktionDeskScen();                        
            monSaveData.MonitorSaveData();                                   // Speichert die Monitor-Daten

            var mmDataRead = new FunktionDeskScen();
            mmDataRead.DataRead(Properties.Settings.Default.pfadDeskOK + "\\MonitorDaten.txt"); // Liest die Monitor-Daten aus einer Datei

            var mmDataTrim = new FunktionVergleich();
            mmDataTrim.MultiMonDataTrim();                                   // Trimmt die Multi-Monitor-Daten

            var dorStatus = new FunktionDesktopOK();
            dorStatus.DORKontrolle();                                        // Überprüft den Desktop-Status erneut

            var doReadData = new FunktionDesktopOK();
            doReadData.DataRead(Properties.Settings.Default.eDeskOkLastSave);// Liest die letzten gespeicherten Desktop-Daten

            var doConvert = new FunktionVergleich();
            doConvert.AbwandelnDerData();                                    // Konvertiert die Daten

            Properties.Settings.Default.Inizialisiert = true;                // Setzt die Initialisierungseinstellung auf true
            Properties.Settings.Default.Save();                              // Speichert die Einstellungen
        }

        public void DatenLesenAbfrage()
        {
            var result = MessageBox.Show("Monitordaten erneut Laden, Gespeichertes wird überschrieben", "Reload", MessageBoxButtons.YesNo, MessageBoxIcon.Question); // Zeigt eine Nachricht an, um zu fragen, ob die Monitor-Daten erneut geladen werden sollen

            if (result == DialogResult.Yes)
            {
                // Setze die Monitor-Informationen zurück
                Properties.Settings.Default.InfoMonitor1 = "";                  // Setzt die Informationen für Monitor 1 zurück
                Properties.Settings.Default.InfoMonitor2 = "";                  // Setzt die Informationen für Monitor 2 zurück
                Properties.Settings.Default.InfoMonitor3 = "";                  // Setzt die Informationen für Monitor 3 zurück
                Properties.Settings.Default.InfoMonitor4 = "";                  // Setzt die Informationen für Monitor 4 zurück

                // Setze die Icon-Zuweisungen und -Status zurück
                Properties.Settings.Default.eMonitorVorhanden1 = false;         // Setzt den Status für Monitor 1 zurück
                Properties.Settings.Default.eMonitorAktiv1 = false;             // Setzt den Aktivitätsstatus für Monitor 1 zurück
                Properties.Settings.Default.eMonitorVorhanden2 = false;         // Setzt den Status für Monitor 2 zurück
                Properties.Settings.Default.eMonitorAktiv2 = false;             // Setzt den Aktivitätsstatus für Monitor 2 zurück
                Properties.Settings.Default.eMonitorVorhanden3 = false;         // Setzt den Status für Monitor 3 zurück
                Properties.Settings.Default.eMonitorAktiv3 = false;             // Setzt den Aktivitätsstatus für Monitor 3 zurück
                Properties.Settings.Default.eMonitorVorhanden4 = false;         // Setzt den Status für Monitor 4 zurück
                Properties.Settings.Default.eMonitorAktiv4 = false;             // Setzt den Aktivitätsstatus für Monitor 4 zurück

                // Setze die Desktop-Informationen zurück
                Properties.Settings.Default.MultiMonData = null;                // Setzt die Multi-Monitor-Daten zurück
                Properties.Settings.Default.MultiMonDataTrim1 = null;           // Setzt die getrimmten Multi-Monitor-Daten für Monitor 1 zurück
                Properties.Settings.Default.MultiMonDataTrim2 = null;           // Setzt die getrimmten Multi-Monitor-Daten für Monitor 2 zurück
                Properties.Settings.Default.MultiMonDataTrim3 = null;           // Setzt die getrimmten Multi-Monitor-Daten für Monitor 3 zurück
                Properties.Settings.Default.MultiMonDataTrim4 = null;           // Setzt die getrimmten Multi-Monitor-Daten für Monitor 4 zurück

                Properties.Settings.Default.SelectetMonitor = 10;               // Setzt den ausgewählten Monitor zurück
                Properties.Settings.Default.Save();                             // Speichert die Einstellungen

                DatenLesen();                                                   // Ruft die Methode zum Lesen der Daten auf
            }
        }
    }
}

