using MIM_Tool.Helpers;
using System.Windows.Forms;

namespace MIM_Tool.Funktions
{
    internal class Funktion2DatenLesen
    {
        // Methode zum Lesen der Daten
        public void DatenLesen()
        {
            Log.inf("Start das Programmlaufs DatenLesen ");
            Properties.Settings.Default.DatenLesenFehler = false;
            Properties.Settings.Default.DatenLesenAktiv = true;                                 // Setzt die Initialisierungseinstellung auf true
            Properties.Settings.Default.Save();
            Log.inf("Setze Status auf Run");

            var dosStatus = new FunktionDesktopOK();
            dosStatus.DOSKontrolle();                                                           // Überprüft den Desktop-Status
            Log.inf("Bedingungsabfrage zum Benutzen von DesktopOk erfolgreich ");
            
            var doIconPos = new FunktionDesktopOK();
            doIconPos.IconSavePos();                                                            // Speichert die Position der Icons
            Log.inf("Das Speichern der Icon Positionen mit DesktopOk erfolgreich ");

            var mmsStatus = new FunktionMultiMonitor();
            mmsStatus.MMSKontrolle();                                                           // Überprüft den Multi-Monitor-Status
            Log.inf("Bedienungsabfrage zum Coonfig Speicher von MultiMonitorTool erfolgreich ");

            var monSaveConfig = new FunktionMultiMonitor();
            monSaveConfig.MonitorSaveConfig();                                                  // Speichert die Monitor-Konfiguration
            Log.inf("Das Speichern der Monitor Konfigurationen mit MultiMonitorTool erfolgreich ");

            var mmrStatus = new FunktionMultiMonitor();
            mmrStatus.MMRKontrolle();                                                           // Überprüft den Multi-Monitor-Status erneut
            Log.inf("Bedingungsabfrage zum Monitordaten Laden mit MultiMonitorTool erfolgreich ");

            var monSaveData = new FunktionDeskScen();
            monSaveData.MonitorSaveData();                                                      // Speichert die Monitor-Daten
            Log.inf("Monitordaten erfolgreich mit MultiMonitorTool geladen (noch nicht gelesen) ");

            var mmDataRead = new FunktionDeskScen();
            mmDataRead.DataRead(Properties.Settings.Default.pfadDeskOK + "\\MonitorDaten.txt"); // Liest die Monitor-Daten aus einer Datei
            Log.inf("Was die gespeicherte Liste von Monitordaten auslesen ");

            var mmDataTrim = new FunktionVergleich();
            mmDataTrim.MultiMonDataTrim();                                                      // Trimmt die Multi-Monitor-Daten
            Log.inf("Gelesene Monitordaten verwendbar gemacht ");

            var dorStatus = new FunktionDesktopOK();
            dorStatus.DORKontrolle();                                                           // Überprüft den Desktop-Status erneut
            Log.inf("Bedingungsabfrage zum Lesender Icon Liste von Desktop okay erfolgreich ");

            var doReadData = new FunktionDesktopOK();
            doReadData.DataRead(Properties.Settings.Default.eDeskOkLastSave);                   // Liest die letzten gespeicherten Desktop-Daten
            Log.inf("Lesen der gespeicherten Icon Position und dessen Pfade ");

            var doConvert = new FunktionVergleich();
            doConvert.AbwandelnDerData();                                                       // Konvertiert die Daten
            Log.inf("\"Gelesene IconListe verwendbar gemacht");

            if (Properties.Settings.Default.DatenLesenFehler)
            {
                Log.war("Fehler beim Lesen der Daten, \nauf der InfoSeite kannst du nochmal nachlesen wieso. \nBitte wiederhole das Lesen der Daten.",8000); // Zeigt eine Fehlermeldung an, wenn ein Fehler beim Lesen der Daten auftritt
                Properties.Settings.Default.DatenLesenAktiv = false; 
            } // Wenn ein Fehler auftritt.
            else
            {
                Log.inf("Der Prozess des Datenlesens ist erfolgreich beendet worden, Daten werden gespeichert ");
                Properties.Settings.Default.Inizialisiert = true;                                                                // Setzt die Initialisierungseinstellung auf true
                Properties.Settings.Default.DatenLesenAktiv = false;
                MessageBox.Show("Daten wurden geladen");                                                                         // Zeigt eine Nachricht an, dass die Daten neu geladen wurden
            }
            Properties.Settings.Default.Save();                                                                                  // Speichert die Einstellungen
        }

        public void DatenLesenAbfrage()
        {
            
            Properties.Settings.Default.FehlerPreReload = false;
            if (Properties.Settings.Default.eMonitorVorhanden1 && (!Properties.Settings.Default.eMonitorAktiv1 || Properties.Settings.Default.eMonitorIconsVerstaut1)) Properties.Settings.Default.FehlerPreReload = true;
            if (Properties.Settings.Default.eMonitorVorhanden2 && (!Properties.Settings.Default.eMonitorAktiv2 || Properties.Settings.Default.eMonitorIconsVerstaut2)) Properties.Settings.Default.FehlerPreReload = true;
            if (Properties.Settings.Default.eMonitorVorhanden3 && (!Properties.Settings.Default.eMonitorAktiv3 || Properties.Settings.Default.eMonitorIconsVerstaut3)) Properties.Settings.Default.FehlerPreReload = true;
            if (Properties.Settings.Default.eMonitorVorhanden4 && (!Properties.Settings.Default.eMonitorAktiv4 || Properties.Settings.Default.eMonitorIconsVerstaut4)) Properties.Settings.Default.FehlerPreReload = true;
            Properties.Settings.Default.Save();
            Log.inf("Abfrage ob der Prozessdaten lesen gestartet werden kann und soll ");
            
            if (Properties.Settings.Default.FehlerPreReload)
            {
                Log.err("Es sind noch nicht alle Monitore aktiviert oder die Icons sind noch nicht Zurück! \nFalls du Trotzdem die Daten Neu Laden willst, gehe in die Einstellung.", null ,true);// Zeigt eine Fehlermeldung an, wenn nicht alle Monitore aktiviert sind oder die Icons noch nicht verstaut sind
            }
            else
            {
                var result = MessageBox.Show("Monitordaten erneut Laden, Gespeichertes wird überschrieben", "Reload", MessageBoxButtons.YesNo, MessageBoxIcon.Question);                          // Zeigt eine Nachricht an, um zu fragen, ob die Monitor-Daten erneut geladen werden sollen

                if (result == DialogResult.Yes)
                {
                    Log.inf("Die Userabfrage und Bedienung zum Starten das Datenlesens war erfolgreich ");
                    DatenLesen();                                                                                                                                                                 // Ruft die Methode zum Lesen der Daten auf
                }
            }
        }
    }
}

