using System.Windows.Forms;
using IWshRuntimeLibrary;

namespace MIM_Tool.Funktions
{
    internal class FunktionVerknüpfung
    {
        public void VerknüpfungStart()                                                                // Methode zum Starten der Verknüpfungserstellung.
        {
            // Überprüft, ob Monitor 1 vorhanden ist.
            if (Properties.Settings.Default.eMonitorVorhanden1)
            {
                string data = Properties.Settings.Default.InfoMonitor1;                               // Holt die Monitor-Informationen aus den Einstellungen.
                string[] splitData = data.Split(';');                                                 // Teilt die Daten in ein Array auf.
                string monitorID = "MIM_" + splitData[1] + "_" + splitData[17];                       // Erstellt die Monitor-ID.
                ErstelleVerknüpfung(monitorID, "0");                                                  // Erstellt die Verknüpfung für Monitor 1.
            }
            // Überprüft, ob Monitor 2 vorhanden ist.
            if (Properties.Settings.Default.eMonitorVorhanden2)
            {
                string data = Properties.Settings.Default.InfoMonitor2;                               // Holt die Monitor-Informationen aus den Einstellungen.
                string[] splitData = data.Split(';');                                                 // Teilt die Daten in ein Array auf.
                string monitorID = "MIM_" + splitData[1] + "_" + splitData[17];                       // Erstellt die Monitor-ID.
                ErstelleVerknüpfung(monitorID, "1");                                                  // Erstellt die Verknüpfung für Monitor 2.
            }
            // Überprüft, ob Monitor 3 vorhanden ist.
            if (Properties.Settings.Default.eMonitorVorhanden3)
            {
                string data = Properties.Settings.Default.InfoMonitor3;                               // Holt die Monitor-Informationen aus den Einstellungen.
                string[] splitData = data.Split(';');                                                 // Teilt die Daten in ein Array auf.
                string monitorID = "MIM_" + splitData[1] + "_" + splitData[17];                       // Erstellt die Monitor-ID.
                ErstelleVerknüpfung(monitorID, "2");                                                  // Erstellt die Verknüpfung für Monitor 3.
            }
            // Überprüft, ob Monitor 4 vorhanden ist.
            if (Properties.Settings.Default.eMonitorVorhanden4)
            {
                string data = Properties.Settings.Default.InfoMonitor4;                               // Holt die Monitor-Informationen aus den Einstellungen.
                string[] splitData = data.Split(';');                                                 // Teilt die Daten in ein Array auf.
                string monitorID = "MIM_" + splitData[1] + "_" + splitData[17];                       // Erstellt die Monitor-ID.
                ErstelleVerknüpfung(monitorID, "3");                                                  // Erstellt die Verknüpfung für Monitor 4.
            }
        }

        public void ErstelleVerknüpfung(string verknüpfungsName, string argumente)                    // Methode zum Erstellen einer Verknüpfung.
        {
            string desktopPfad = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);        // Holt den Pfad zum Desktop.
            string verknüpfungsPfad = System.IO.Path.Combine(desktopPfad, verknüpfungsName + ".lnk"); // Erstellt den Pfad für die Verknüpfung.
            string zielPfad = Application.ExecutablePath;                                             // Holt den Pfad zur ausführbaren Datei der Anwendung.
            WshShell shell = new WshShell();                                                          // Erstellt eine neue Instanz von WshShell.
            IWshShortcut verknüpfung = (IWshShortcut)shell.CreateShortcut(verknüpfungsPfad);          // Erstellt die Verknüpfung.
            verknüpfung.TargetPath = zielPfad;                                                        // Setzt den Zielpfad der Verknüpfung.
            verknüpfung.Arguments = argumente;                                                        // Setzt die Argumente der Verknüpfung.
            verknüpfung.Save();                                                                       // Speichert die Verknüpfung.
        }
    }
}

