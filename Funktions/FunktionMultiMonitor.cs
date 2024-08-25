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
                            client.DownloadFile("https://www.nirsoft.net/utils/multimonitortool-x64.zip", pathFile);     // Lädt die Datei herunter.
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
                        client.DownloadFile("https://www.nirsoft.net/utils/multimonitortool-x64.zip", pathFile);     // Lädt die Datei herunter.
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

                        // Löschen der ältesten Dateien, wenn mehr als 30 Backups vorhanden sind
                        var backupFiles = new DirectoryInfo(pathBackUP)
                            .GetFiles("*.cfg") // Nur Dateien mit der Erweiterung .dok berücksichtigen
                            .OrderBy(f => f.CreationTime)
                            .ToList();
                        if (backupFiles.Count > 30)
                        {
                            Log.inf("Mehr als 30 Backup-Dateien gefunden. Löschen der ältesten Dateien.");
                            for (int i = 0; i < backupFiles.Count - 30; i++)
                            {
                                Log.inf($"Lösche Datei: {backupFiles[i].FullName}");
                                backupFiles[i].Delete();
                            }
                        }
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
                    var myFile = directoryInfo.GetFiles("*.cfg")
                                              .Where(f => f.Name.Contains("MultiMon"))                                                                                  // Nur Dateien, die "DesktopOK" im Namen enthalten
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
            if (!string.IsNullOrEmpty(Properties.Settings.Default.eMultiMonLastSave) )
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

                    Thread.Sleep(1000);                                                                   // Wartet eine Sekunde.
                    Log.inf("Einstellungen gespeichert und eine Sekunde gewartet.");
                    if (Properties.Settings.Default.eMonitorAktiv1 && Properties.Settings.Default.eMonitorAktiv2 && Properties.Settings.Default.eMonitorAktiv3)
                    {
                        var loadConfig = new FunktionMultiMonitor();
                        loadConfig.MonitorLoadConfig();                                                       // Lädt die Monitor-Konfiguration.
                        Log.inf("Monitor-Konfiguration geladen.");
                    }
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
        public static void MonitorSwitch(string pathExe , int moniNr)            // Monitor deaktivieren.
        {
            Log.inf("Beginne mit dem Umschalten des Monitors.");
            if (!string.IsNullOrEmpty(Properties.Settings.Default.eMultiMonLastSave))
            {
                string moniAus1 = Properties.Settings.Default.InfoMonitor1;
                string[] moniArray1 = moniAus1.Split(';');
                string moniAus2 = Properties.Settings.Default.InfoMonitor2;
                string[] moniArray2 = moniAus2.Split(';');
                string moniAus3 = Properties.Settings.Default.InfoMonitor3;
                string[] moniArray3 = moniAus3.Split(';');
                string moniAus4 = Properties.Settings.Default.InfoMonitor4;
                string[] moniArray4 = moniAus4.Split(';');
                string[][] moniAuswhal =
                    {
                    moniArray1,
                    moniArray2,
                    moniArray3,
                    moniArray4
                    };
                MessageBox.Show($"Monitor {moniAuswhal[moniNr][17]} wird geschallten");
                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = pathExe;                                                         // Pfad zur Anwendung.
                    startInfo.Arguments = $"/switch {moniAuswhal[moniNr][17]}";                                      // Argumente für das Deaktivieren des Monitors.
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;                                    // Fenster nicht anzeigen.
                    Log.inf($"Starte Prozess: {startInfo.FileName} mit Argumenten: {startInfo.Arguments}");
                    Process process = Process.Start(startInfo);
                    process.WaitForExit();                                                                // Warten, bis der Prozess abgeschlossen ist.
                    MessageBox.Show("Monitor erfolgreich im Admin Modus geschallten, variabelen werden Ignoriert");
                    Log.inf("Monitor erfolgreich geschallten.");
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




        public static void MMTTest(int index)                                                                                                                                 // Mit MultiMonitorTool Monitor-Daten laden.
        {

            string[] setMonitor = //0=alle an 1=aus an an 2= an aus an 3=aus aus an
            {
                " /SetMonitors \"Name=MONITOR\\SAM0A7A\\{4d36e96e-e325-11ce-bfc1-08002be10318}\\0001 BitsPerPixel=32 Width=1920 Height=1080 DisplayFrequency=60 PositionX=299 PositionY=2160\" \"Name=MONITOR\\AOC2752\\{4d36e96e-e325-11ce-bfc1-08002be10318}\\0006 BitsPerPixel=32 Width=1920 Height=1080 DisplayFrequency=60 PositionX=4294965376 PositionY=1080\" \"Name=MONITOR\\HEC0030\\{4d36e96e-e325-11ce-bfc1-08002be10318}\\0009 Primary=1 BitsPerPixel=32 Width=3840 Height=2160 DisplayFrequency=60 PositionX=0 PositionY=0\" ",
                " /SetMonitors \"Name=MONITOR\\SAM0A7A\\{4d36e96e-e325-11ce-bfc1-08002be10318}\\0001 BitsPerPixel=0 Width=0 Height=0 DisplayFrequency=0 PositionX=0 PositionY=0\" \"Name=MONITOR\\AOC2752\\{4d36e96e-e325-11ce-bfc1-08002be10318}\\0006 BitsPerPixel=32 Width=1920 Height=1080 DisplayFrequency=60 PositionX=4294965376 PositionY=1080\" \"Name=MONITOR\\HEC0030\\{4d36e96e-e325-11ce-bfc1-08002be10318}\\0009 Primary=1 BitsPerPixel=32 Width=3840 Height=2160 DisplayFrequency=60 PositionX=0 PositionY=0\" ",
                " /SetMonitors \"Name=MONITOR\\SAM0A7A\\{4d36e96e-e325-11ce-bfc1-08002be10318}\\0001 BitsPerPixel=32 Width=1920 Height=1080 DisplayFrequency=60 PositionX=299 PositionY=2160\" \"Name=MONITOR\\AOC2752\\{4d36e96e-e325-11ce-bfc1-08002be10318}\\0006 BitsPerPixel=0 Width=0 Height=0 DisplayFrequency=0 PositionX=0 PositionY=0\" \"Name=MONITOR\\HEC0030\\{4d36e96e-e325-11ce-bfc1-08002be10318}\\0009 Primary=1 BitsPerPixel=32 Width=3840 Height=2160 DisplayFrequency=60 PositionX=0 PositionY=0\" ",
                " /SetMonitors \"Name=MONITOR\\SAM0A7A\\{4d36e96e-e325-11ce-bfc1-08002be10318}\\0001 BitsPerPixel=0 Width=0 Height=0 DisplayFrequency=0 PositionX=0 PositionY=0\" \"Name=MONITOR\\AOC2752\\{4d36e96e-e325-11ce-bfc1-08002be10318}\\0006 BitsPerPixel=0 Width=0 Height=0 DisplayFrequency=0 PositionX=0 PositionY=1080\" \"Name=MONITOR\\HEC0030\\{4d36e96e-e325-11ce-bfc1-08002be10318}\\0009 Primary=1 BitsPerPixel=32 Width=3840 Height=2160 DisplayFrequency=60 PositionX=0 PositionY=0\" ",

            };
            if (!string.IsNullOrEmpty(Properties.Settings.Default.eMultiMonLastSave))
            {
                try
                {
                    Log.inf($"test beim schalten ({index}) \n0=alle an 1=aus an an 2= an aus an 3=aus aus an");
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = "C:\\Users\\Ryuk\\Documents\\MIM_Files\\MultiMonitorTool.exe";                                                                                                                       // Pfad zur Anwendung.
                    startInfo.Arguments = setMonitor[index];                                                               // Argumente für das Laden der Konfiguration.
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;                                                                                                  // Fenster nicht anzeigen.
                    Log.inf($"Starte Prozess: {startInfo.FileName} mit Argumenten: {startInfo.Arguments}");
                    Process process = Process.Start(startInfo);
                    process.WaitForExit();                                                                                                                              // Warten, bis der Prozess abgeschlossen ist.
                    Log.inf("test Monitor-Konfiguration erfolgreich geladen.");
                }
                catch (Exception ex)
                {
                    Log.err("test Ladehemmung von MultiMonitorTool,Schau mal nach zugriffsberechtigung des Programms nach.", ex, true);                                       //Fehler Text festlegen                                                                                                                                                          // Speichert die Einstellungen.
                }
            }
            else
            {
                Log.err($"test Speicherdatei ({Properties.Settings.Default.eMultiMonLastSave}) nicht gefunden, \nkontrolliere bitte den Hintergrund Ordner.", null, true);//Fehler Text festlegen                                                                                                                                                          // Speichert die Einstellungen.
            }
        }
    }
}
