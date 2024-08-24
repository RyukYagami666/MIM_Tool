using System.Windows.Forms;
using MIM_Tool.Helpers;

namespace MIM_Tool.Funktions
{
    internal class Funktion1Initialisieren
    {
        // Methode zur Initialisierung der wichtigen Funktionen
        public void Initialisieren()
        {
            Log.inf("Start der Installierung.");
            Properties.Settings.Default.InizialisierungAktiv = true;                                                                                                          // Initialisierung ist aktiv
            Properties.Settings.Default.InizialisierungsFehler = false;                                                                                                       // Initialisierung fehler zurücksetzen
            Properties.Settings.Default.Save();
            Log.inf("Initialisierungsstatus auf Run gestellt.");

            var defaultPath = new FunktionDefaultPath();                                                                                                                      // Initialisiert den Standardpfad
            defaultPath.DefaultPath();
            if (Properties.Settings.Default.InizialisierungsFehler) { Properties.Settings.Default.InizialisierungAktiv = false; Properties.Settings.Default.Save(); return; } // Wenn ein Fehler auftritt, wird die Methode beendet
            Log.inf("Kontrolle und setzen das Standardpfades abgeschlossen. ");

            var dodStatus = new FunktionDesktopOK();                                                                                                                          // Überprüft den Desktop-Status
            dodStatus.DODKontrolle();
            if (Properties.Settings.Default.InizialisierungsFehler) { Properties.Settings.Default.InizialisierungAktiv = false; Properties.Settings.Default.Save(); return; } // Wenn ein Fehler auftritt, wird die Methode beendet
            Log.inf("Bedingungsabfrage für Download von DesktopOk erfüllt ");

            var dodStart = new FunktionDesktopOK();                                                                                                                           // Startet die Desktop-Überwachung
            dodStart.DODStart();
            if (Properties.Settings.Default.InizialisierungsFehler) { Properties.Settings.Default.InizialisierungAktiv = false; Properties.Settings.Default.Save(); return; } // Wenn ein Fehler auftritt, wird die Methode beendet
            Log.inf("Download von DesktopOk erfolgreich ");

            var mmdStatus = new FunktionMultiMonitor();                                                                                                                       // Überprüft den Multi-Monitor-Status
            mmdStatus.MMDKontrolle();
            if (Properties.Settings.Default.InizialisierungsFehler) { Properties.Settings.Default.InizialisierungAktiv = false; Properties.Settings.Default.Save(); return; } // Wenn ein Fehler auftritt, wird die Methode beendet
            Log.inf("Bedingungsabfrage für Download von MultiMonitorTool erfüllt ");

            var mmdStart = new FunktionMultiMonitor();                                                                                                                        // Startet die Multi-Monitor-Überwachung
            mmdStart.MMDStart();
            if (Properties.Settings.Default.InizialisierungsFehler) { Properties.Settings.Default.InizialisierungAktiv = false; Properties.Settings.Default.Save(); return; } // Wenn ein Fehler auftritt, wird die Methode beendet
            Log.inf("Download von MultiMonitorTool erfolgreich ");

            Properties.Settings.Default.InizialisierungAktiv = false;                 //Initialisierung nicht mehr aktiv
            Properties.Settings.Default.Save();
            Log.inf("Initialisierungsstatus umstellen auf Datenladen ");
            var datenLesen = new Funktion2DatenLesen();                               // Liest die Daten ein
            datenLesen.DatenLesen();
        }

                                                                                      // Methode zur Abfrage, ob die Initialisierung erneut durchgeführt werden soll
        public void InitialisierenAbfrage()
        {
            var result = MessageBox.Show("Initialisieren erneut durchführen, Gespeichertes wird überschrieben", "Initialisieren", MessageBoxButtons.YesNo, MessageBoxIcon.Question); // Zeigt eine Nachricht an, um zu fragen, ob die Initialisierung erneut durchgeführt werden soll

            if (result == DialogResult.Yes)                                                                                                                                          // Wenn der Benutzer "Ja" wählt, wird die Initialisierung erneut durchgeführt
            {
                Log.inf("Abfrage ob Initialisierung gestartet werden soll akzeptiert.");
                Initialisieren();
            }
        }
    }
}

