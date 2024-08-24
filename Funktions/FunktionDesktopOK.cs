 using System.Net; 
using System.Windows.Forms; 
using System.IO.Compression;
using System.Diagnostics;
using System.IO; 
using System.Net.NetworkInformation;
using MIM_Tool.Helpers;

namespace MIM_Tool.Funktions
{
    internal class FunktionDesktopOK
    {
        //------------------------------------------------------------------------------------------------------------------------------------------Abfragen-------------------------------------------------------------------------
        string pathFolder = Properties.Settings.Default.pfadDeskOK;                                                // Pfad zum Verzeichnis.
        string pathFile = $"{Properties.Settings.Default.pfadDeskOK}\\DesktopOK.zip";                              // Pfad zur DesktopOK.zip.
        string pathExe = $"{Properties.Settings.Default.pfadDeskOK}\\DesktopOK.exe";                               // Pfad zur DesktopOK.exe.
        string pathBackUP = $"{Properties.Settings.Default.pfadDeskOK}\\BackUps";                                  // Pfad zum Backup-Verzeichnis.
        string pathBackUpFile;                                                                                     // Variable für den Pfad zur Backup-Datei.
        string pathLastData = Properties.Settings.Default.eDeskOkLastSave;                                         // Pfad zur zuletzt gespeicherten Datei.

        public void DODKontrolle()                                                                                 // Kontrolle, ob DesktopOK bereit ist zum Downloaden.
        {
            if (System.IO.Directory.Exists(pathFolder) && NetworkInterface.GetIsNetworkAvailable()) Properties.Settings.Default.eDeskOkDownloadReady = true;                                            // Setzt die Einstellung auf bereit.
            else Properties.Settings.Default.eDeskOkDownloadReady = false;                                         // Setzt die Einstellung auf nicht bereit
            Properties.Settings.Default.Save();                                                                    // Speichert die Einstellungen.
        }

        public void DOSKontrolle()                                                                                 // Kontrolle, ob DesktopOK bereit ist zum Speichern der Icon-Positionen.
        {
            if (System.IO.Directory.Exists(pathFolder) && System.IO.File.Exists(pathExe) && Properties.Settings.Default.eDeskOkDownloadDone) Properties.Settings.Default.eDeskOkSavePosReady = true;    // Setzt die Einstellung auf bereit.
            else Properties.Settings.Default.eDeskOkSavePosReady = false;                                          // Setzt die Einstellung auf nicht bereit.
            Properties.Settings.Default.Save();                                                                    // Speichert die Einstellungen.
        }

        public void DORKontrolle()                                                                                 // Kontrolle, ob DesktopOK bereit ist zum Lesen und Vergleichen.
        {
            if (System.IO.Directory.Exists(pathFolder) && System.IO.File.Exists(Properties.Settings.Default.eDeskOkLastSave) && Properties.Settings.Default.eDeskOkSavePosDone) Properties.Settings.Default.eDeskOkDataReedReady = true;   // Setzt die Einstellung auf bereit.
            else Properties.Settings.Default.eDeskOkDataReedReady = false;                                         // Setzt die Einstellung auf nicht bereit.
            Properties.Settings.Default.Save();                                                                    // Speichert die Einstellungen.
        }

        public void GEVersion()                                                                                    // Methode zum Generieren des Backup-Dateipfads mit Versionsnummer.
        {
            pathBackUpFile = $"{Properties.Settings.Default.pfadDeskOK}\\BackUps\\DesktopOk{Properties.Settings.Default.eDeskOkVers}.zip"; // Setzt den Pfad zur Backup-Datei.
        }

        //--------------------------------------------------------------------Inizial Funktionen DesktopOK-------------------------------------------------------------------------------------------------------------------
        public void DODStart()                                                                                                                      // Download von DesktopOK.
        {
            Log.inf("DODStart gestartet.");
            if (Properties.Settings.Default.eDeskOkDownloadReady)
            {
                Log.inf("eDeskOkDownloadReady ist wahr.");
                if (System.IO.File.Exists(pathFile))
                {
                    Log.inf($"Datei {pathFile} existiert.");
                    var result = MessageBox.Show("Datei Erneut Herrunterladen?", "Downloaden", MessageBoxButtons.YesNo, MessageBoxIcon.Question);   // Fragt den Benutzer, ob die Datei erneut heruntergeladen werden soll.
                    if (result == DialogResult.Yes)
                    {
                        Log.inf("Benutzer hat 'Ja' zum erneuten Herunterladen gewählt.");
                        try
                        {
                            GEVersion();                                                                                                            // Generiert den Backup-Dateipfad.
                            if (System.IO.Directory.Exists(pathBackUP)) System.IO.Directory.CreateDirectory(pathBackUP);                            // Erstellt das Backup-Verzeichnis, falls es nicht existiert.
                            if (!System.IO.File.Exists(pathBackUpFile)) System.IO.File.Move(pathFile, pathBackUpFile);                              // Verschiebt die Datei ins Backup-Verzeichnis.
                            if (System.IO.File.Exists(pathBackUpFile)) System.IO.File.Delete(pathFile);                                             // Löscht die alte Datei.
                            Log.inf("Starte Download der Datei.");
                            WebClient client = new WebClient();
                            client.DownloadFile("https://www.softwareok.com/Download/DesktopOK.zip", pathFile);                                     // Lädt die Datei herunter.
                            Thread.Sleep(1000);                                                                                                     // Wartet eine Sekunde.
                            ZipFile.ExtractToDirectory(pathFile, pathFolder, true);                                                                 // Entpackt die Datei.
                            Log.inf("Download und Entpacken abgeschlossen.");
                            Properties.Settings.Default.eDeskOkDownloadDone = true;                                                                 // Setzt die Einstellung auf abgeschlossen.
                            Properties.Settings.Default.eDeskOkDownloadReady = false;                                                               // Setzt die Einstellung auf nicht bereit.
                            Properties.Settings.Default.eDeskOkDownloadDate = Convert.ToString(DateTime.Now);                                       // Speichert das Download-Datum.
                            Properties.Settings.Default.eDeskOkVers = $"{GetExeVersion()}";                                                         // Speichert die Version des Programms.
                            Properties.Settings.Default.Save();                                                                                     // Speichert die Einstellungen.
                            Log.inf("Herunterlaaden von DesktopOk war erfolgreich, Einstellungen gespeichert.");
                        }
                        catch (Exception ex)
                        {
                            Log.err($"Fehler beim erneuten Herunterladen der Datei", ex, true);                                                     // Speichert den Fehler in den Einstellungen und zeig ihn an
                            if (Properties.Settings.Default.InizialisierungAktiv) Properties.Settings.Default.InizialisierungsFehler = true;        // Setzt den Initialisierungsfehler auf true.
                            Properties.Settings.Default.Save();                                                                                     // Speichert die Einstellungen.
                        }
                    }
                }
                else if (System.IO.Directory.Exists(pathFolder))
                {
                    Log.inf($"Verzeichnis {pathFolder} existiert.");
                    try
                    {
                        WebClient client = new WebClient();
                        client.DownloadFile("https://www.softwareok.com/Download/DesktopOK.zip", pathFile);                                          // Lädt die Datei herunter.
                        Thread.Sleep(1000);                                                                                                         // Wartet eine Sekunde.
                        ZipFile.ExtractToDirectory(pathFile, pathFolder, true);                                                                     // Entpackt die Datei.
                        Log.inf("Entpacke die heruntergeladene DesktopOk.");
                        Properties.Settings.Default.eDeskOkDownloadDone = true;                                                                     // Setzt die Einstellung auf abgeschlossen.
                        Properties.Settings.Default.eDeskOkDownloadReady = false;                                                                   // Setzt die Einstellung auf nicht bereit.
                        Properties.Settings.Default.eDeskOkDownloadDate = Convert.ToString(DateTime.Now);                                           // Speichert das Download-Datum.
                        Properties.Settings.Default.Save();                                                                                         // Speichert die Einstellungen.
                        Log.inf("Herunterlaaden von DesktopOk war erfolgreich, Einstellungen gespeichert.");
                    }
                    catch (Exception ex)
                    {
                        Log.err($"Fehler beim Herunterladen der Datei", ex, true);                                                                  // Speichert den Fehler in den Einstellungen und zeig ihn an
                        if (Properties.Settings.Default.InizialisierungAktiv) Properties.Settings.Default.InizialisierungsFehler = true;            // Setzt den Initialisierungsfehler auf true.
                        Properties.Settings.Default.Save();                                                                                         // Speichert die Einstellungen.
                    }
                }
                else
                {
                    Log.err($"Fehler: Unterordner {pathFolder} nicht vorhanden.", null, true);                                                      // Speichert den Fehler in den Einstellungen und zeig ihn an
                    if (Properties.Settings.Default.InizialisierungAktiv) Properties.Settings.Default.InizialisierungsFehler = true;                // Setzt den Initialisierungsfehler auf true.
                    Properties.Settings.Default.Save();                                                                                             // Speichert die Einstellungen.
                }
            }
            else
            {
                Log.err("Fehler: eDeskOkDownloadReady ist falsch oder keine Internetverbindung.",null,true);                                        // Speichert den Fehler in den Einstellungen und zeig ihn an
                if (Properties.Settings.Default.InizialisierungAktiv) Properties.Settings.Default.InizialisierungsFehler = true;                    // Setzt den Initialisierungsfehler auf true.
                Properties.Settings.Default.Save();                                                                                                 // Speichert die Einstellungen.
            }
        }

        public void IconSavePos()                                                                                                                   // Mit DesktopOK Positionen speichern.
        {
            Log.inf("IconSavePos gestartet.");
            if (Properties.Settings.Default.eDeskOkSavePosReady)
            {
                Log.inf("eDeskOkSavePosReady ist wahr.");
                try
                {
                    if (!string.IsNullOrEmpty(Properties.Settings.Default.eDeskOkLastSave) && System.IO.File.Exists(Properties.Settings.Default.eDeskOkLastSave))
                    {
                        Log.inf("Letzte gespeicherte Datei existiert.");
                        string fileToMove = pathLastData.Replace($"{pathFolder}", "");                                                              // Ermittelt den Dateinamen.
                        System.IO.Directory.CreateDirectory(pathBackUP);                                                                            // Erstellt das Backup-Verzeichnis, falls es nicht existiert.
                        System.IO.File.Move(Properties.Settings.Default.eDeskOkLastSave, $"{pathBackUP}{fileToMove}");                              // Verschiebt die alte Datei ins Backup-Verzeichnis.
                        Log.inf("Alte Datei ins Backup-Verzeichnis verschoben.");
                    }
                    ProcessStartInfo startInfo = new ProcessStartInfo();                                                                            // Prozess für das Öffnen des Dokuments starten
                    startInfo.FileName = pathExe;                                                                                                   // Pfad zur Anwendung.
                    startInfo.Arguments = $"/save /silent {pathFolder}\\DeskOk_date_time_.dok";                                                     // Argumente (z. B. Dateipfad).
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;                                                                              // Fenster nicht anzeigen.
                    Log.inf("Starte DesktopOK Prozess zum Speichern der Icon-Positionen.");
                    Process process = Process.Start(startInfo);
                    process.WaitForExit();                                                                                                          // Warten, bis der Prozess abgeschlossen ist.
                    Log.inf("DesktopOK Prozess abgeschlossen.");
                    var directoryInfo = new DirectoryInfo(pathFolder);
                    var myFile = directoryInfo.GetFiles()
                                              .OrderByDescending(f => f.LastWriteTime)
                                              .FirstOrDefault();                                                                                    // Ermittelt die zuletzt erstellte Datei.
                    Log.inf("Zuletzt erstellte Datei ermittelt.");
                    if (myFile != null)
                    {
                        Properties.Settings.Default.eDeskOkLastSave = myFile.FullName;                                                              // Pfad der zuletzt erstellten Datei speichern.
                        Properties.Settings.Default.eDeskOkSavePosDone = true;
                        Properties.Settings.Default.eDeskOkSavePosReady = false;
                        Properties.Settings.Default.eDeskOkSavePosDate = Convert.ToString(DateTime.Now);
                        Properties.Settings.Default.Save();                                                                                         // Speichert die Einstellungen.
                        Log.inf("Icon-Positionen erfolgreich gespeichert und Einstellungen aktualisiert.");
                    }
                    else
                    {
                        Log.err("DesktopOk hat keine Daten geschrieben, möglicherweise ein Zugriffsfehler.", null, true);
                        if (Properties.Settings.Default.DatenLesenAktiv) Properties.Settings.Default.DatenLesenFehler = true;                       // Setzt den Initialisierungsfehler auf true.
                        Properties.Settings.Default.Save();
                    }
                }
                catch (Exception ex)
                {
                    Log.err("beim Ausführen des Programms DesktopOk. Überprüfen Sie, ob das Programm im Hintergrund-Ordner vorhanden ist.", ex, true);
                    if (Properties.Settings.Default.DatenLesenAktiv) Properties.Settings.Default.DatenLesenFehler = true;                           // Setzt den DatenLesen Ablauf auf true.
                    Properties.Settings.Default.Save();                                                                                             // Speichert die Einstellungen.
                }
                Thread.Sleep(2000);                                                                                                                 // Wartet zwei Sekunden.        
            }
        }

        public string[] DataRead(string pathLastData)                                                                                               // Mit DesktopOK Positionen lesen und vergleichen.
        {
            Log.inf("DataRead gestartet.");
            if (Properties.Settings.Default.eDeskOkDataReedReady)
            {
                Log.inf("eDeskOkDataReedReady ist wahr.");
                try
                {
                    if (File.Exists(pathLastData))
                    {
                        Log.inf("Letzte gespeicherte Datei existiert.");
                        string[] zeilen = File.ReadAllLines(pathLastData);                                                    // Lesen aller Zeilen der Datei in ein Array.
                        Properties.Settings.Default.eDeskOkDataReedReady = false;
                        Properties.Settings.Default.eDeskOkDataReedDone = true;
                        Properties.Settings.Default.eDeskOkDataReedDate = Convert.ToString(DateTime.Now);                     // Speichert das Lese-Datum.
                        Log.inf("Daten erfolgreich gelesen.");
                        string deskOkData = string.Join(";", zeilen);                                                         // Verbindet die Zeilen zu einem String.
                        deskOkData = deskOkData.Replace("=", ";");                                                            // Ersetzt Gleichheitszeichen durch Semikolons.
                        Properties.Settings.Default.DeskOkData = deskOkData;                                                  // Speichert die Daten in den Einstellungen.
                        Properties.Settings.Default.Save();                                                                   // Speichert die Einstellungen.
                        Log.inf("Daten erfolgreich in den Einstellungen gespeichert.");
                        return zeilen;                                                                                        // Gibt die Zeilen zurück.
                    }
                    else
                    {
                        Log.err("Speicherdatei nicht gefunden. Bitte überprüfen Sie den Hintergrund-Ordner.",null,true);
                        if (Properties.Settings.Default.DatenLesenAktiv) Properties.Settings.Default.DatenLesenFehler = true; // Setzt den DatenLesen Ablauf auf true.
                        Properties.Settings.Default.Save();
                        return new string[0];                                                                                 // Gibt ein leeres Array zurück.
                    }
                }
                catch (Exception ex)
                {
                    Log.err("beim Lesen der Speicherdaten. Überprüfen Sie, ob das Programm Zugriffsrechte hat.", ex, true);
                    if (Properties.Settings.Default.DatenLesenAktiv) Properties.Settings.Default.DatenLesenFehler = true;     // Setzt den DatenLesen Ablauf auf true.
                    Properties.Settings.Default.Save();                                                                       // Speichert die Einstellungen.
                    return new string[0];                                                                                     // Gibt ein leeres Array zurück.
                }
            }
            else
            {
                Log.err("Es ist möglicherweise etwas beim Speichern schiefgelaufen. Überprüfen Sie die Zugriffsrechte.", null, true);
                if (Properties.Settings.Default.DatenLesenAktiv) Properties.Settings.Default.DatenLesenFehler = true;         // Setzt den DatenLesen Ablauf auf true.
                Properties.Settings.Default.Save();                                                                           // Speichert die Einstellungen.
                return new string[0];                                                                                         // Gibt ein leeres Array zurück.
            }
        }
        //--------------------------------------------------------------------Haupt Funktion Icon-Positionen-------------------------------------------------------------------------------------------------------------------
        public static void IconRestore(string pathExe, string pathLastData)                                                    // Mit DesktopOK Positionen der Icons wiederherstellen.
        {
            Log.inf("IconRestore gestartet.");
            if (Properties.Settings.Default.eDeskOkDataReedDone && !string.IsNullOrEmpty(Properties.Settings.Default.eDeskOkLastSave))
            {
                Log.inf("eDeskOkDataReedDone ist wahr und letzte gespeicherte Datei ist vorhanden.");
                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = pathExe;                                                                              // Pfad zur Anwendung.
                    startInfo.Arguments = "/load /silent " + pathLastData;                                                     // Argumente (z. B. Dateipfad).
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;                                                         // Fenster nicht anzeigen.
                    Log.inf("Starte DesktopOK Prozess zum Wiederherstellen der Icon-Positionen.");
                    Process process = Process.Start(startInfo);
                    process.WaitForExit();                                                                                     // Warten, bis der Prozess abgeschlossen ist.
                    Log.inf("DesktopOK Prozess abgeschlossen.");
                }
                catch (Exception ex)
                {
                    Log.err("Fehler beim Öffnen des Dokuments.", ex, true);                                                     // Speichert den Fehler in den Einstellungen und zeig ihn an       // Speichert die Einstellungen.
                }
            }
            else
            {
                Log.err("Das Lesen der Daten ist nicht abgeschlossen oder die letzte gespeicherte Datei ist nicht vorhanden.");// Speichert den Fehler in den Einstellungen und zeig i
            }
        }

        public string GetExeVersion()                                                                                          // Methode zum Auslesen der Version von DesktopOK.exe.
        {
            Log.inf("GetExeVersion gestartet.");
            if (File.Exists(pathExe))
            {
                FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(pathExe);
                Log.inf("Version von DesktopOK.exe erfolgreich ausgelesen.");
                return versionInfo.FileVersion;                                                                                // Gibt die Dateiversion zurück.
            }
            else
            {
                return "Datei nicht gefunden.";                                                                                // Gibt eine Fehlermeldung zurück, wenn die Datei nicht existiert.
            }
        }
    }
}
