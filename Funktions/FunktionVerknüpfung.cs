using System.Windows.Forms;
using IWshRuntimeLibrary;
using MIM_Tool.Helpers;

namespace MIM_Tool.Funktions
{
    internal class FunktionVerknüpfung
    {
        public void VerknüpfungStart()                                                                // Methode zum Starten der Verknüpfungserstellung.
        {
            Log.inf("VerknüpfungStart gestartet.");
            // Überprüft, ob Monitor 1 vorhanden ist.
            if (Properties.Settings.Default.eMonitorVorhanden1)
            {
                Log.inf("Monitor 1 vorhanden.");
                string data = Properties.Settings.Default.InfoMonitor1;                               // Holt die Monitor-Informationen aus den Einstellungen.
                Log.inf($"Monitor 1 Daten: {data}");
                string[] splitData = data.Split(';');                                                 // Teilt die Daten in ein Array auf.
                string monitorID = "MIM_" + splitData[1] + "_" + splitData[17];                       // Erstellt die Monitor-ID.
                Log.inf($"Monitor 1 ID: {monitorID}");
                ErstelleVerknüpfung(monitorID, "0");                                                  // Erstellt die Verknüpfung für Monitor 1.
            }
            // Überprüft, ob Monitor 2 vorhanden ist.
            if(Properties.Settings.Default.eMonitorVorhanden2)
    {
                Log.inf("Monitor 2 vorhanden.");
                string data = Properties.Settings.Default.InfoMonitor2;                               // Holt die Monitor-Informationen aus den Einstellungen.
                Log.inf($"Monitor 2 Daten: {data}");
                string[] splitData = data.Split(';');                                                 // Teilt die Daten in ein Array auf.
                string monitorID = "MIM_" + splitData[1] + "_" + splitData[17];                       // Erstellt die Monitor-ID.
                Log.inf($"Monitor 2 ID: {monitorID}");
                ErstelleVerknüpfung(monitorID, "1");                                                  // Erstellt die Verknüpfung für Monitor 2.
            }
            // Überprüft, ob Monitor 3 vorhanden ist.
            if (Properties.Settings.Default.eMonitorVorhanden3)
            {
                Log.inf("Monitor 3 vorhanden.");
                string data = Properties.Settings.Default.InfoMonitor3;                               // Holt die Monitor-Informationen aus den Einstellungen.
                Log.inf($"Monitor 3 Daten: {data}");
                string[] splitData = data.Split(';');                                                 // Teilt die Daten in ein Array auf.
                string monitorID = "MIM_" + splitData[1] + "_" + splitData[17];                       // Erstellt die Monitor-ID.
                Log.inf($"Monitor 3 ID: {monitorID}");
                ErstelleVerknüpfung(monitorID, "2");                                                  // Erstellt die Verknüpfung für Monitor 3.
            }
            // Überprüft, ob Monitor 4 vorhanden ist.
            if (Properties.Settings.Default.eMonitorVorhanden4)
            {
                Log.inf("Monitor 4 vorhanden.");
                string data = Properties.Settings.Default.InfoMonitor4;                               // Holt die Monitor-Informationen aus den Einstellungen.
                Log.inf($"Monitor 4 Daten: {data}");
                string[] splitData = data.Split(';');                                                 // Teilt die Daten in ein Array auf.
                string monitorID = "MIM_" + splitData[1] + "_" + splitData[17];                       // Erstellt die Monitor-ID.
                Log.inf($"Monitor 4 ID: {monitorID}");
                ErstelleVerknüpfung(monitorID, "3");                                                  // Erstellt die Verknüpfung für Monitor 4.
            }
            Log.inf("VerknüpfungStart abgeschlossen.");
        }

        public void ErstelleVerknüpfung(string verknüpfungsName, string argumente)                    // Methode zum Erstellen einer Verknüpfung.
        {
            Log.inf($"ErstelleVerknüpfung gestartet für {verknüpfungsName} mit Argumenten {argumente}.");

            string desktopPfad = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);        // Holt den Pfad zum Desktop.
            Log.inf($"Desktop-Pfad: {desktopPfad}");

            string verknüpfungsPfad = System.IO.Path.Combine(desktopPfad, verknüpfungsName + ".lnk"); // Erstellt den Pfad für die Verknüpfung.
            Log.inf($"Verknüpfungs-Pfad: {verknüpfungsPfad}");

            string zielPfad = Application.ExecutablePath;                                             // Holt den Pfad zur ausführbaren Datei der Anwendung.
            Log.inf($"Ziel-Pfad: {zielPfad}");

            WshShell shell = new WshShell();                                                          // Erstellt eine neue Instanz von WshShell.
            IWshShortcut verknüpfung = (IWshShortcut)shell.CreateShortcut(verknüpfungsPfad);          // Erstellt die Verknüpfung.
            verknüpfung.TargetPath = zielPfad;                                                        // Setzt den Zielpfad der Verknüpfung.
            verknüpfung.Arguments = argumente;                                                        // Setzt die Argumente der Verknüpfung.
            verknüpfung.Save();                                                                       // Speichert die Verknüpfung.

            Log.inf("Verknüpfung erstellt und gespeichert.");
            Log.inf("ErstelleVerknüpfung abgeschlossen.");
        }
    }
}

