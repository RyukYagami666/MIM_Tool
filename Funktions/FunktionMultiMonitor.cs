using System.Net;
using System.Windows.Forms;
using System.IO.Compression;
using System.Diagnostics;
using System.IO;
using System.Net.NetworkInformation;
using System.Threading;
using MIM_Tool.Helpers;

namespace MIM_Tool.Funktions
{
    internal class FunktionMultiMonitor//------------------------------------------------------------------------Abfragen-----------------------------------------------------------------------------------------
    {
        string pathFolder = Properties.Settings.Default.pfadDeskOK;                                                       // Pfad zum DeskOK-Ordner.
        string pathFile = $"{Properties.Settings.Default.pfadDeskOK}\\multimonitortool-x64.zip";                          // Pfad zur ZIP-Datei.
        string pathExe = $"{Properties.Settings.Default.pfadDeskOK}\\MultiMonitorTool.exe";                               // Pfad zur ausführbaren Datei.
        string pathBackUP = $"{Properties.Settings.Default.pfadDeskOK}\\BackUps";                                         // Pfad zum Backup-Ordner.
        string pathBackUpFile;                                                                                            // Variable für den Pfad der Backup-Datei.
        string pathLastData = Properties.Settings.Default.eMultiMonLastSave;                                              // Pfad zur letzten gespeicherten Datei.

        public void MMDKontrolle()                                                                                        // Kontrolle, ob DesktopOK bereit ist zum Downloaden.
        {
            if (System.IO.Directory.Exists(pathFolder) && NetworkInterface.GetIsNetworkAvailable()) Properties.Settings.Default.eMultiMonDownloadReady = true; // Setzt den Download-Status auf bereit.
            else Properties.Settings.Default.eMultiMonDownloadReady = false;                                              // Setzt den Download-Status auf nicht bereit.
            Properties.Settings.Default.Save();                                                                           // Speichert die Einstellungen.
        }

        public void MMSKontrolle()                                                                                        // Kontrolle, ob DesktopOK bereit ist zum Speichern der Icon-Positionen.
        {
            if (System.IO.Directory.Exists(pathFolder) && System.IO.File.Exists(pathExe) && Properties.Settings.Default.eMultiMonDownloadDone) Properties.Settings.Default.eMultiMonSavePosReady = true;   // Setzt den Speicher-Status auf bereit.
            else Properties.Settings.Default.eMultiMonSavePosReady = false;                                               // Setzt den Speicher-Status auf nicht bereit.
            Properties.Settings.Default.Save();                                                                           // Speichert die Einstellungen.
        }

        public void MMRKontrolle()                                                                                        // Kontrolle, ob DesktopOK bereit ist zum Lesen und Vergleichen.
        {
            if (System.IO.Directory.Exists(pathFolder) && System.IO.File.Exists(Properties.Settings.Default.eMultiMonLastSave) && Properties.Settings.Default.eMultiMonSavePosDone) Properties.Settings.Default.eMultiMonSaveDataReady = true;   // Setzt den Daten-Status auf bereit.
            else Properties.Settings.Default.eMultiMonSaveDataReady = false;                                              // Setzt den Daten-Status auf nicht bereit.
            Properties.Settings.Default.Save();                                                                           // Speichert die Einstellungen.
        }

        public void GEVersion()                                                                                           // Methode zum Generieren des Backup-Dateipfads.
        {
            pathBackUpFile = $"{Properties.Settings.Default.pfadDeskOK}\\BackUps\\MultiMonitorTool{Properties.Settings.Default.eMultiMonVers}.zip"; // Setzt den Pfad der Backup-Datei.
        }
        //--------------------------------------------------------------------Inizial Funktionen DesktopOK-------------------------------------------------------------------------------------------------------------------

        public void MMDStart()                                                                                                                    // Download von MultiMonitorTool.
        {
            Log.inf("MMDStart gestartet.");
            if (Properties.Settings.Default.eMultiMonDownloadReady)
            {
                Log.inf("eMultiMonitorDownloadReady ist wahr.");
                if (System.IO.File.Exists(pathFile))
                {
                    Log.inf($"Datei {pathFile} existiert.");
                    var result = MessageBox.Show("Datei Erneut Herrunterladen?", "Downloaden", MessageBoxButtons.YesNo, MessageBoxIcon.Question); // Fragt den Benutzer, ob die Datei erneut heruntergeladen werden soll.
                    if (result == DialogResult.Yes)
                    {
                        Log.inf("Benutzer hat 'Ja' zum erneuten Herunterladen gewählt.");
                        try
                        {
                            GEVersion();                                                                                                          // Generiert den Backup-Dateipfad.
                            if (System.IO.Directory.Exists(pathBackUP)) System.IO.Directory.CreateDirectory(pathBackUP);                          // Erstellt den Backup-Ordner, falls er nicht existiert.
                            if (!System.IO.File.Exists(pathBackUpFile)) System.IO.File.Move(pathFile, pathBackUpFile);                            // Verschiebt die Datei ins Backup.
                            if (System.IO.File.Exists(pathBackUpFile)) System.IO.File.Delete(pathFile);                                           // Löscht die ursprüngliche Datei.
                            Log.inf("Starte Download der Datei.");
                            WebClient client = new WebClient();
                            client.DownloadFile("https:                                                                                           //www.nirsoft.net/utils/multimonitortool-x64.zip", pathFile);     // Lädt die Datei herunter.
                            Thread.Sleep(1000);                                                                                                   // Wartet eine Sekunde.
                            ZipFile.ExtractToDirectory(pathFile, pathFolder, true);                                                               // Entpackt die ZIP-Datei.
                            Log.inf("Download und Entpacken abgeschlossen.");
                            Properties.Settings.Default.eMultiMonDownloadDone = true;                                                             // Setzt den Download-Status auf abgeschlossen.
                            Properties.Settings.Default.eMultiMonDownloadReady = false;                                                           // Setzt den Download-Status auf nicht bereit.
                            Properties.Settings.Default.eMultiMonDownloadDate = Convert.ToString(DateTime.Now);                                   // Speichert das Download-Datum.
                            Properties.Settings.Default.eMultiMonVers = $"{GetExeVersion()}";                                                     // Speichert die Version des Programms.
                            Properties.Settings.Default.Save();                                                                                   // Speichert die Einstellungen.
                            Log.inf("Herunterladen von MultiMonitorTool war erfolgreich, Einstellungen gespeichert.");
                        }
                        catch (Exception ex)
                        {
                            Log.err($"Fehler beim erneuten Herunterladen der Datei", ex, true);                                                   // Speichert den Fehler in den Einstellungen und zeigt ihn an.
                            if (Properties.Settings.Default.InizialisierungAktiv) Properties.Settings.Default.InizialisierungsFehler = true;      // Setzt den Initialisierungsfehler auf true.
                            Properties.Settings.Default.Save();                                                                                   // Speichert die Einstellungen.                                                                                          // Speichert die Einstellungen.
                        }
                    }
                }
                else if (System.IO.Directory.Exists(pathFolder))
                {
                    Log.inf($"Verzeichnis {pathFolder} existiert.");
                    try
                    {
                        WebClient client = new WebClient();
                        client.DownloadFile("https:                                                                                               //www.nirsoft.net/utils/multimonitortool-x64.zip", pathFile);     // Lädt die Datei herunter.
                        Thread.Sleep(1000);                                                                                                       // Wartet eine Sekunde.
                        ZipFile.ExtractToDirectory(pathFile, pathFolder, true);                                                                   // Entpackt die ZIP-Datei.
                        Log.inf("Entpacke die heruntergeladene MultiMonitorTool.");
                        Properties.Settings.Default.eMultiMonDownloadDone = true;                                                                 // Setzt den Download-Status auf abgeschlossen.
                        Properties.Settings.Default.eMultiMonDownloadReady = false;                                                               // Setzt den Download-Status auf nicht bereit.
                        Properties.Settings.Default.eMultiMonDownloadDate = Convert.ToString(DateTime.Now);                                       // Speichert das Download-Datum.
                        Properties.Settings.Default.eMultiMonVers = $"{GetExeVersion()}";                                                         // Speichert die Version des Programms.
                        Properties.Settings.Default.Save();
                        Log.inf("Herunterladen von MultiMonitorTool war erfolgreich, Einstellungen gespeichert.");
                    }
                    catch (Exception ex)
                    {
                        Log.err($"Fehler beim Herunterladen der Datei", ex, true);                                                                // Speichert den Fehler in den Einstellungen und zeigt ihn an.
                        if (Properties.Settings.Default.InizialisierungAktiv) Properties.Settings.Default.InizialisierungsFehler = true;          // Setzt den Initialisierungsfehler auf true.
                        Properties.Settings.Default.Save();                                                                                       // Speichert die Einstellungen.
                    }
                }
                else
                {
                    Log.err($"Fehler: Unterordner {pathFolder} nicht vorhanden.", null, true);                                                    // Speichert den Fehler in den Einstellungen und zeigt ihn an.
                    if (Properties.Settings.Default.InizialisierungAktiv) Properties.Settings.Default.InizialisierungsFehler = true;              // Setzt den Initialisierungsfehler auf true.
                    Properties.Settings.Default.Save();                                                                                           // Speichert die Einstellungen.
                }
            }
            else
            {
                Log.err("Fehler: eMultiMonitorDownloadReady ist falsch oder keine Internetverbindung.", null, true);                              // Speichert den Fehler in den Einstellungen und zeigt ihn an.
                if (Properties.Settings.Default.InizialisierungAktiv) Properties.Settings.Default.InizialisierungsFehler = true;                  // Setzt den Initialisierungsfehler auf true.
                Properties.Settings.Default.Save();                                                                                               // Speichert die Einstellungen.
            }
        }

        public void MonitorSaveConfig()                                                                                                                                 // Mit MultiMonitorTool Monitor-Konfiguration speichern.
        {
            string pathDeskOkDate = Properties.Settings.Default.eDeskOkLastSave;                                                                                        // Pfad zur letzten DeskOK-Speicherung.
            string pathDate = pathDeskOkDate.Replace($"{pathFolder}\\", "");                                                                                            // Entfernt den Ordnerpfad.
            pathDate = pathDate.Replace($"DeskOk", "");                                                                                                                 // Entfernt "DeskOk".
            pathDate = pathDate.Replace($".dok", "");                                                                                                                   // Entfernt die Dateiendung.
            if (Properties.Settings.Default.eMultiMonSavePosReady)
            {
                Log.inf("MultiMonitorTool erfolgreich runtergeladen und bereit ");
                try
                {
                    if (!string.IsNullOrEmpty(Properties.Settings.Default.eMultiMonLastSave) && System.IO.File.Exists(Properties.Settings.Default.eMultiMonLastSave))
                    {
                        Log.inf("Letzte gespeicherte Datei existiert.");
                        string fileToMove = pathLastData.Replace($"{pathFolder}", "");                                                                                  // Entfernt den Ordnerpfad.
                        System.IO.Directory.CreateDirectory(pathBackUP);                                                                                                // Erstellt den Backup-Ordner.
                        System.IO.File.Move(Properties.Settings.Default.eMultiMonLastSave, $"{pathBackUP}{fileToMove}");                                                // Verschiebt die Datei ins Backup.
                         Log.inf("Alte Datei ins Backup-Verzeichnis verschoben.");
                    }
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = pathExe;                                                                                                                       // Pfad zur Anwendung.
                    startInfo.Arguments = $"/SaveConfig {pathFolder}\\MultiMon{pathDate}.cfg";                                                                          // Argumente für das Speichern der Konfiguration.
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;                                                                                                  // Fenster nicht anzeigen.

                    Log.inf("Starte MultiMonitorTool Prozess zum Speichern der Monitor-Konfiguration.");
                    Process process = Process.Start(startInfo);
                    process.WaitForExit();                                                                                                                              // Warten, bis der Prozess abgeschlossen ist.

                    Log.inf("MultiMonitorTool Prozess abgeschlossen.");
                    var directoryInfo = new DirectoryInfo(pathFolder);
                    var myFile = directoryInfo.GetFiles()
                                              .OrderByDescending(f => f.LastWriteTime)
                                              .FirstOrDefault();                                                                                                        // Holt die zuletzt erstellte Datei
                    Log.inf("Zuletzt erstellte Datei ermittelt.");
                    if (myFile != null)
                    {
                        Properties.Settings.Default.eMultiMonLastSave = myFile.FullName;                                                                                // Speichert den Pfad der zuletzt erstellten Datei.
                        Properties.Settings.Default.eMultiMonSavePosDone = true;                                                                                        // Setzt den Speicher-Status auf abgeschlossen.
                        Properties.Settings.Default.eMultiMonSavePosReady = false;                                                                                      // Setzt den Speicher-Status auf nicht bereit.
                        Properties.Settings.Default.eMultiMonSavePosDate = Convert.ToString(DateTime.Now);                                                              // Speichert das Speicher-Datum.
                        Properties.Settings.Default.Save();                                                                                                             // Speichert die Einstellungen.
                        Log.inf("Monitor-Konfiguration erfolgreich gespeichert und Einstellungen aktualisiert.");
                    }
                    else
                    {
                        Log.err("MultiMonitorTool hat keine Daten geschrieben, möglicherweise ein Zugriffsfehler.", null, true);
                        if (Properties.Settings.Default.DatenLesenAktiv) Properties.Settings.Default.DatenLesenFehler = true;                                           // Setzt den Initialisierungsfehler auf true.
                        Properties.Settings.Default.Save();
                    }
                }
                catch (Exception ex)
                {
                    Log.err("beim Ausführen des Programms MultiMonitorTool. Überprüfen Sie, ob das Programm im Hintergrund-Ordner vorhanden ist.", ex, true);
                    if (Properties.Settings.Default.DatenLesenAktiv) Properties.Settings.Default.DatenLesenFehler = true;                                               // Setzt den DatenLesen Ablauf auf true.
                    Properties.Settings.Default.Save();                                                                                                                 // Speichert die Einstellungen.
                }
                Thread.Sleep(1000);                                                                                                                                     // Wartet eine Sekunde.
            }
        }

        //---------------------------------------------------------------------------

        public void MonitorLoadConfig()                                                                                                                                 // Mit MultiMonitorTool Monitor-Daten laden.
        {
            if (!string.IsNullOrEmpty(Properties.Settings.Default.eMultiMonLastSave))
            {
                try
                {
                    Log.inf("Beginne mit dem Laden der Monitor-Konfiguration.");
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = pathExe;                                                                                                                       // Pfad zur Anwendung.
                    startInfo.Arguments = "/LoadConfig " + Properties.Settings.Default.eMultiMonLastSave;                                                               // Argumente für das Laden der Konfiguration.
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;                                                                                                  // Fenster nicht anzeigen.
                    Log.inf($"Starte Prozess: {startInfo.FileName} mit Argumenten: {startInfo.Arguments}");
                    Process process = Process.Start(startInfo);
                    process.WaitForExit();                                                                                                                              // Warten, bis der Prozess abgeschlossen ist.
                    Log.inf("Monitor-Konfiguration erfolgreich geladen.");
                }
                catch (Exception ex)
                {
                    Log.err("Ladehemmung von MultiMonitorTool,Schau mal nach zugriffsberechtigung des Programms nach.", ex,true);                                       //Fehler Text festlegen                                                                                                                                                          // Speichert die Einstellungen.
                }
            }
            else
            {   
                Log.err($"Speicherdatei ({Properties.Settings.Default.eMultiMonLastSave}) nicht gefunden, \nkontrolliere bitte den Hintergrund Ordner.", null, true);//Fehler Text festlegen                                                                                                                                                          // Speichert die Einstellungen.
            }
        }
        //--------------------------------------------------------------------Haupt Funktion Icon-Positionen-------------------------------------------------------------------------------------------------------------------

        public static void MonitorDeaktivieren(string pathExe, string moniAuswhal, int moniNr)            // Monitor deaktivieren.
        {
            Log.inf("Beginne mit dem Deaktivieren des Monitors.");
            bool[] aktiv =
            {
                Properties.Settings.Default.eMonitorAktiv1,                                               // Status von Monitor 1.
                Properties.Settings.Default.eMonitorAktiv2,                                               // Status von Monitor 2.
                Properties.Settings.Default.eMonitorAktiv3,                                               // Status von Monitor 3.
                Properties.Settings.Default.eMonitorAktiv4                                                // Status von Monitor 4.
            };
            if (!string.IsNullOrEmpty(Properties.Settings.Default.eMultiMonLastSave) && aktiv[moniNr])
            {;
                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = pathExe;                                                         // Pfad zur Anwendung.
                    startInfo.Arguments = $"/disable {moniAuswhal}";                                      // Argumente für das Deaktivieren des Monitors.
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;                                    // Fenster nicht anzeigen.
                    Log.inf($"Starte Prozess: {startInfo.FileName} mit Argumenten: {startInfo.Arguments}");
                    Process process = Process.Start(startInfo);
                    process.WaitForExit();                                                                // Warten, bis der Prozess abgeschlossen ist.
                    Log.inf("Monitor erfolgreich deaktiviert.");
                    if (moniNr == 0) Properties.Settings.Default.eMonitorAktiv1 = false;                  // Setzt den Status von Monitor 1 auf inaktiv.
                    if (moniNr == 1) Properties.Settings.Default.eMonitorAktiv2 = false;                  // Setzt den Status von Monitor 2 auf inaktiv.
                    if (moniNr == 2) Properties.Settings.Default.eMonitorAktiv3 = false;                  // Setzt den Status von Monitor 3 auf inaktiv.
                    if (moniNr == 3) Properties.Settings.Default.eMonitorAktiv4 = false;                  // Setzt den Status von Monitor 4 auf inaktiv.
                    Properties.Settings.Default.Save();                                                   // Speichert die Einstellungen.
                    Thread.Sleep(1000);
                    Log.inf("Einstellungen gespeichert und eine Sekunde gewartet.");
                }
                catch (Exception ex)
                {                               
                    Log.err("Ausführungs missgeschick, beiManueller Steuerung korekte steuerung achten, oder schaum mal nach dem Programm. ", ex, true); //Fehler Text festlegen
                }
            }
            else
            {                           
                Log.err("Der gewünschten Monitor hat den Status inaktiv oder die Speicherdatei fehlt, \nDas Neu Laden der Daten könnte helfen.", null, true);//Fehler Text festlegen 
            }
        }

        public static void MonitorAktivieren(string pathExe, string moniAuswhal, int moniNr)              // Monitor aktivieren.
        {
            if (!string.IsNullOrEmpty(Properties.Settings.Default.eMultiMonLastSave))
            {
                Log.inf("Beginne mit dem Aktivieren des Monitors.");
                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = pathExe;                                                         // Pfad zur Anwendung.
                    startInfo.Arguments = $"/enable {moniAuswhal}";                                       // Argumente für das Aktivieren des Monitors.
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;                                    // Fenster nicht anzeigen.
                    Log.inf($"Starte Prozess: {startInfo.FileName} mit Argumenten: {startInfo.Arguments}");
                    Process process = Process.Start(startInfo);
                    process.WaitForExit();                                                                // Warten, bis der Prozess abgeschlossen ist.
                    Log.inf("Monitor erfolgreich aktiviert.");
                    var loadConfig = new FunktionMultiMonitor();
                    loadConfig.MonitorLoadConfig();                                                       // Lädt die Monitor-Konfiguration.
                    Log.inf("Monitor-Konfiguration geladen.");
                    if (moniNr == 0) Properties.Settings.Default.eMonitorAktiv1 = true;                   // Setzt den Status von Monitor 1 auf aktiv.
                    if (moniNr == 1) Properties.Settings.Default.eMonitorAktiv2 = true;                   // Setzt den Status von Monitor 2 auf aktiv.
                    if (moniNr == 2) Properties.Settings.Default.eMonitorAktiv3 = true;                   // Setzt den Status von Monitor 3 auf aktiv.
                    if (moniNr == 3) Properties.Settings.Default.eMonitorAktiv4 = true;                   // Setzt den Status von Monitor 4 auf aktiv.
                    Properties.Settings.Default.Save();                                                   // Speichert die Einstellungen.
                    Thread.Sleep(1000);                                                                   // Wartet eine Sekunde.
                    Log.inf("Einstellungen gespeichert und eine Sekunde gewartet.");
                }
                catch (Exception ex)
                {
                    Log.err("Ausführungs missgeschick, beiManueller Steuerung korekte steuerung achten, oder schaum mal nach dem Programm. ", ex, true); //Fehler Text festlegen
                }
            }
            else
            {                                
                Log.err("Der gewünschten Monitor hat den Status inaktiv oder die Speicherdatei fehlt, \nDas Neu Laden der Daten könnte helfen.", null,true);//Fehler Text festlegen
            }
        }

        public string GetExeVersion()                                                                     // Methode zum Auslesen der Version von DesktopOK.exe.
        {
            if (File.Exists(pathExe))
            {
                FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(pathExe);                    // Holt die Versionsinformationen.
                return versionInfo.FileVersion;                                                           // Gibt die Dateiversion zurück.
            }
            else
            {
                return "Datei nicht gefunden.";                                                           // Gibt eine Fehlermeldung zurück, wenn die Datei nicht existiert.
            }
        }
    }
}
