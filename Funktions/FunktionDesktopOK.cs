using System.Net; 
using System.Windows.Forms; 
using System.IO.Compression;
using System.Diagnostics;
using System.IO; 
using System.Net.NetworkInformation;

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

        public void GEVersion()                                                                                    // Methode zum Generieren des Backup-Dateipfads mit Versionsnummer.
        {
            pathBackUpFile = $"{Properties.Settings.Default.pfadDeskOK}\\BackUps\\DesktopOk{GetExeVersion()}.zip"; // Setzt den Pfad zur Backup-Datei.
        }

        public void DODKontrolle()                                                                                 // Kontrolle, ob DesktopOK bereit ist zum Downloaden.
        {
            if (System.IO.Directory.Exists(pathFolder) && NetworkInterface.GetIsNetworkAvailable()) Properties.Settings.Default.eDeskOkDownloadReady = true;                                            // Setzt die Einstellung auf bereit.
            else Properties.Settings.Default.eDeskOkDownloadReady = false;                                         // Setzt die Einstellung auf nicht bereit.
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
        //--------------------------------------------------------------------Inizial Funktionen DesktopOK-------------------------------------------------------------------------------------------------------------------
        public void DODStart()                                                                                     // Download von DesktopOK.
        {
            if (Properties.Settings.Default.eDeskOkDownloadReady)
            {
                if (System.IO.File.Exists(pathFile))
                {
                    var result = MessageBox.Show("Datei Erneut Herrunterladen?", "Downloaden", MessageBoxButtons.YesNo, MessageBoxIcon.Question);       // Fragt den Benutzer, ob die Datei erneut heruntergeladen werden soll.
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            GEVersion();                                                                                                              // Generiert den Backup-Dateipfad.
                            if (System.IO.Directory.Exists(pathBackUP)) System.IO.Directory.CreateDirectory(pathBackUP);                              // Erstellt das Backup-Verzeichnis, falls es nicht existiert.
                            if (!System.IO.File.Exists(pathBackUpFile)) System.IO.File.Move(pathFile, pathBackUpFile);                                // Verschiebt die Datei ins Backup-Verzeichnis.
                            if (System.IO.File.Exists(pathBackUpFile)) System.IO.File.Delete(pathFile);                                               // Löscht die alte Datei.
                            WebClient client = new WebClient();
                            client.DownloadFile("https://www.softwareok.com/Download/DesktopOK.zip", pathFile);                                       // Lädt die Datei herunter.
                            Thread.Sleep(1000);                                                                                                        // Wartet eine Sekunde.
                            ZipFile.ExtractToDirectory(pathFile, pathFolder, true);                                                                   // Entpackt die Datei.
                            Properties.Settings.Default.eDeskOkDownloadDone = true;                                                                   // Setzt die Einstellung auf abgeschlossen.
                            Properties.Settings.Default.eDeskOkDownloadReady = false;                                                                 // Setzt die Einstellung auf nicht bereit.
                            Properties.Settings.Default.eDeskOkDownloadDate = Convert.ToString(DateTime.Now);                                         // Speichert das Download-Datum.
                            Properties.Settings.Default.Save();                                                                                       // Speichert die Einstellungen.
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Fehler beim Downloaden der Datei: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error); // Zeigt eine Fehlermeldung an.
                        }
                    }
                }
                else if (System.IO.Directory.Exists(pathFolder))
                {
                    try
                    {
                        WebClient client = new WebClient();
                        client.DownloadFile("https://www.softwareok.com/Download/DesktopOK.zip", pathFile);                                       // Lädt die Datei herunter.
                        Thread.Sleep(1000);                                                                                                       // Wartet eine Sekunde.
                        ZipFile.ExtractToDirectory(pathFile, pathFolder, true);                                                                   // Entpackt die Datei.
                        Properties.Settings.Default.eDeskOkDownloadDone = true;                                                                   // Setzt die Einstellung auf abgeschlossen.
                        Properties.Settings.Default.eDeskOkDownloadReady = false;                                                                 // Setzt die Einstellung auf nicht bereit.
                        Properties.Settings.Default.eDeskOkDownloadDate = Convert.ToString(DateTime.Now);                                         // Speichert das Download-Datum.
                        Properties.Settings.Default.Save();                                                                                       // Speichert die Einstellungen.
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Fehler beim Downloaden der Datei: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error); // Zeigt eine Fehlermeldung an.
                    }
                }
                else
                {
                    MessageBox.Show("Pfad Fehler!");                                                                                              // Zeigt eine Fehlermeldung an, wenn der Pfad nicht existiert.
                }
            }
            else
            {
                MessageBox.Show("Download bedingung nicht erfüllt!");                                                                             // Zeigt eine Fehlermeldung an, wenn die Bedingungen nicht erfüllt sind.
            }
        }

        public void IconSavePos()                                                                                      // Mit DesktopOK Positionen speichern.
        {
            if (Properties.Settings.Default.eDeskOkSavePosReady)
            {
                try
                {
                    if (!string.IsNullOrEmpty(Properties.Settings.Default.eDeskOkLastSave) && System.IO.File.Exists(Properties.Settings.Default.eDeskOkLastSave))
                    {
                        string fileToMove = pathLastData.Replace($"{pathFolder}", "");                                 // Ermittelt den Dateinamen.
                        System.IO.Directory.CreateDirectory(pathBackUP);                                               // Erstellt das Backup-Verzeichnis, falls es nicht existiert.
                        System.IO.File.Move(Properties.Settings.Default.eDeskOkLastSave, $"{pathBackUP}{fileToMove}"); // Verschiebt die alte Datei ins Backup-Verzeichnis.
                    }
                    ProcessStartInfo startInfo = new ProcessStartInfo();                                               // Prozess für das Öffnen des Dokuments starten
                    startInfo.FileName = pathExe;                                                                      // Pfad zur Anwendung.
                    startInfo.Arguments = $"/save /silent {pathFolder}\\DeskOk_date_time_.dok";                        // Argumente (z. B. Dateipfad).
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;                                                 // Fenster nicht anzeigen.

                    Process process = Process.Start(startInfo);
                    process.WaitForExit();                                                                             // Warten, bis der Prozess abgeschlossen ist.

                    var directoryInfo = new DirectoryInfo(pathFolder);
                    var myFile = directoryInfo.GetFiles()
                                              .OrderByDescending(f => f.LastWriteTime)
                                              .FirstOrDefault();                                                       // Ermittelt die zuletzt erstellte Datei.
                    if (myFile != null)
                    {
                        Properties.Settings.Default.eDeskOkLastSave = myFile.FullName;                                 // Pfad der zuletzt erstellten Datei speichern.
                        Properties.Settings.Default.eDeskOkSavePosDone = true;
                        Properties.Settings.Default.eDeskOkSavePosReady = false;
                        Properties.Settings.Default.eDeskOkSavePosDate = Convert.ToString(DateTime.Now);
                        Properties.Settings.Default.Save();                                                            // Speichert die Einstellungen.
                    }
                    else
                    {
                        MessageBox.Show("Keine Datei gefunden.");                                                      // Zeigt eine Fehlermeldung an.
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fehler beim Öffnen des Dokuments: " + ex.Message);                                // Zeigt eine Fehlermeldung an.
                }
                Thread.Sleep(2000);                                                                                    // Wartet zwei Sekunden.
            }
        }

        public string[] DataRead(string pathLastData)                                                     // Mit DesktopOK Positionen lesen und vergleichen.
        {
            if (Properties.Settings.Default.eDeskOkDataReedReady)
            {
                try
                {
                    if (File.Exists(pathLastData))
                    {                                                                         
                        string[] zeilen = File.ReadAllLines(pathLastData);                                  // Lesen aller Zeilen der Datei in ein Array.
                        Properties.Settings.Default.eDeskOkDataReedReady = false;
                        Properties.Settings.Default.eDeskOkDataReedDone = true;
                        Properties.Settings.Default.eDeskOkDataReedDate = Convert.ToString(DateTime.Now); // Speichert das Lese-Datum.

                        string deskOkData = string.Join(";", zeilen);                                     // Verbindet die Zeilen zu einem String.
                        deskOkData = deskOkData.Replace("=", ";");                                        // Ersetzt Gleichheitszeichen durch Semikolons.
                        Properties.Settings.Default.DeskOkData = deskOkData;                              // Speichert die Daten in den Einstellungen.
                        Properties.Settings.Default.Save();                                               // Speichert die Einstellungen.
                        return zeilen;                                                                    // Gibt die Zeilen zurück.
                    }
                    else
                    {
                        MessageBox.Show("Datei nicht gefunden.");                                         // Zeigt eine Fehlermeldung an.
                        return new string[0];                                                             // Gibt ein leeres Array zurück.
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ein Fehler ist aufgetreten: {ex.Message}");                         // Zeigt eine Fehlermeldung an.
                    return new string[0];                                                                 // Gibt ein leeres Array zurück.
                }
            }
            else
            {
                MessageBox.Show("Bedingung nicht erfüllt");                                               // Zeigt eine Fehlermeldung an, wenn die Bedingungen nicht erfüllt sind.
                return new string[0];                                                                     // Gibt ein leeres Array zurück.
            }
        }
        //--------------------------------------------------------------------Haupt Funktion Icon-Positionen-------------------------------------------------------------------------------------------------------------------
        public static void IconRestore(string pathExe, string pathLastData)             // Mit DesktopOK Positionen der Icons wiederherstellen.
        {
            if (Properties.Settings.Default.eDeskOkDataReedDone && !string.IsNullOrEmpty(Properties.Settings.Default.eDeskOkLastSave))
            {
                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = pathExe;                                       // Pfad zur Anwendung.
                    startInfo.Arguments = "/load /silent " + pathLastData;              // Argumente (z. B. Dateipfad).
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;                  // Fenster nicht anzeigen.

                    Process process = Process.Start(startInfo);
                    process.WaitForExit();                                              // Warten, bis der Prozess abgeschlossen ist.
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fehler beim Öffnen des Dokuments: " + ex.Message); // Zeigt eine Fehlermeldung an.
                }
            }
            else
            {
                MessageBox.Show("Bedingung nicht erfüllt");                             // Zeigt eine Fehlermeldung an, wenn die Bedingungen nicht erfüllt sind.
            }
        }

        public string GetExeVersion()                                                   // Methode zum Auslesen der Version von DesktopOK.exe.
        {
            if (File.Exists(pathExe))
            {
                FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(pathExe);
                return versionInfo.FileVersion;                                         // Gibt die Dateiversion zurück.
            }
            else
            {
                return "Datei nicht gefunden.";                                         // Gibt eine Fehlermeldung zurück, wenn die Datei nicht existiert.
            }
        }
    }
}
