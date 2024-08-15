using App3.Services;
using System.Net;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Diagnostics;
using System.IO;
using System.Drawing.Drawing2D;
using System.Net.NetworkInformation;

namespace App3.Funktions
{
    internal class FunktionMultiMonitor
    {
        string pathFolder = Properties.Settings.Default.pfadDeskOK; 
        string pathFile = $"{Properties.Settings.Default.pfadDeskOK}\\multimonitortool-x64.zip";
        string pathExe = $"{Properties.Settings.Default.pfadDeskOK}\\MultiMonitorTool.exe";
        string pathBackUP = $"{Properties.Settings.Default.pfadDeskOK}\\BackUps";
        string pathBackUpFile;
        string pathLastData = Properties.Settings.Default.eMultiMonLastSave;

        public void MMDKontrolle()                  // Kontrolle ob DesktopOK bereit ist zum Downloaden------------------------------------------------------------------------------------
        {
            if (System.IO.Directory.Exists(pathFolder) && NetworkInterface.GetIsNetworkAvailable())
            {
                 Properties.Settings.Default.eMultiMonDownloadReady = true;
            }
            else
            {
                Properties.Settings.Default.eMultiMonDownloadReady = false;
            }
            Properties.Settings.Default.Save();
        }
        public void MMSKontrolle()                  // Kontrolle ob DesktopOK bereit ist zum Speichern der IconPos------------------------------------------------------------------------------------
        {
            if (System.IO.Directory.Exists(pathFolder)&& System.IO.File.Exists(pathExe)&& Properties.Settings.Default.eMultiMonDownloadDone)
            {
                Properties.Settings.Default.eMultiMonSavePosReady = true;
            }
            else
            {
                Properties.Settings.Default.eMultiMonSavePosReady = false;
            }
            Properties.Settings.Default.Save();
        }
        public void MMRKontrolle()                  // Kontrolle ob DesktopOK bereit ist zum Lesenund Vergleichen------------------------------------------------------------------------------------
        {
            if (System.IO.Directory.Exists(pathFolder) && System.IO.File.Exists(Properties.Settings.Default.eMultiMonLastSave) && Properties.Settings.Default.eMultiMonSavePosDone)
            {
                Properties.Settings.Default.eMultiMonSaveDataReady = true;
            }
            else
            {
                Properties.Settings.Default.eMultiMonSaveDataReady = false;
            }
            Properties.Settings.Default.Save();
        }


        public void GEVersion()
        {
            pathBackUpFile = $"{Properties.Settings.Default.pfadDeskOK}\\BackUps\\MultiMonitorTool{GetExeVersion()}.zip";
        }

        public void MMDStart()          // Download von MultiMonitorTool------------------------------------------------------------------------------------
        {
            if (Properties.Settings.Default.eMultiMonDownloadReady)
            {
                if (System.IO.File.Exists(pathFile))
                {
                    var result = MessageBox.Show("MultiMonitorTool Erneut Herrunterladen?", "Download", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            GEVersion();
                            if (System.IO.Directory.Exists(pathBackUP)) System.IO.Directory.CreateDirectory(pathBackUP);
                            if (!System.IO.File.Exists(pathBackUpFile)) System.IO.File.Move(pathFile, pathBackUpFile);
                            if (System.IO.File.Exists(pathBackUpFile)) System.IO.File.Delete(pathFile);
                            WebClient client = new WebClient();
                            client.DownloadFile("https://www.nirsoft.net/utils/multimonitortool-x64.zip", pathFile);
                            CopyMSGBox.Show("Download abgeschlossen");
                            ZipFile.ExtractToDirectory(pathFile, pathFolder, true);
                            Properties.Settings.Default.eMultiMonDownloadDone = true;
                            Properties.Settings.Default.eMultiMonDownloadReady = false;
                            Properties.Settings.Default.eMultiMonDownloadDate = Convert.ToString(DateTime.Now);
                            Properties.Settings.Default.Save();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Fehler beim Downloaden von MultiMonitorTool: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else if (System.IO.Directory.Exists(pathFolder))
                {
                    var result = MessageBox.Show("MultiMonitorTool Herrunterladen?", "Download", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            WebClient client = new WebClient();
                            client.DownloadFile("https://www.nirsoft.net/utils/multimonitortool-x64.zip", pathFile);
                            CopyMSGBox.Show("Download abgeschlossen");
                            ZipFile.ExtractToDirectory(pathFile, pathFolder, true);
                            Properties.Settings.Default.eMultiMonDownloadDone = true;
                            Properties.Settings.Default.eMultiMonDownloadReady = false;
                            Properties.Settings.Default.eMultiMonDownloadDate = Convert.ToString(DateTime.Now);
                            Properties.Settings.Default.Save();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Fehler beim Downloaden von MultiMonitorTool: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    CopyMSGBox.Show("Pfad Fehler!");
                }
            }
            else
            {
                CopyMSGBox.Show("Download bedingung nicht erfüllt!");
            }
        }


        public void MonitorSaveConfig()                                   //mit MultiMonitorTool Monitor Config Speichern------------------------------------------------------------------------------------
        {
            string pathDeskOkDate = Properties.Settings.Default.eDeskOkLastSave;
            string pathDate = pathDeskOkDate.Replace($"{pathFolder}\\", "");
            pathDate = pathDate.Replace($"DeskOk", "");
            pathDate = pathDate.Replace($".dok", "");
            if (Properties.Settings.Default.eMultiMonSavePosReady) {

                try
                {
                    if (!string.IsNullOrEmpty(Properties.Settings.Default.eMultiMonLastSave) && System.IO.File.Exists(Properties.Settings.Default.eMultiMonLastSave))
                    {
                        string fileToMove = pathLastData.Replace($"{pathFolder}", "");
                        System.IO.Directory.CreateDirectory(pathBackUP);
                        System.IO.File.Move(Properties.Settings.Default.eMultiMonLastSave, $"{pathBackUP}{fileToMove}");

                    }
                    // Prozess für das Öffnen des Dokuments starten
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = pathExe; // Pfad zur Anwendung
                    startInfo.Arguments = $"/SaveConfig {pathFolder}\\MultiMon{pathDate}.cfg"; // Argumente (z. B. Dateipfad)/SaveConfig  _{pathDate}
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden; // Fenster nicht anzeigen

                    Process process = Process.Start(startInfo);
                    process.WaitForExit(); // Warten, bis der Prozess abgeschlossen ist

                    CopyMSGBox.Show($"Monitor Daten Gespeichert {pathDate}");

                    var directoryInfo = new DirectoryInfo(pathFolder);
                    var myFile = directoryInfo.GetFiles()
                                              .OrderByDescending(f => f.LastWriteTime)
                                              .FirstOrDefault();

                    if (myFile != null)
                    {
                        // Pfad der zuletzt erstellten Datei speichern
                        Properties.Settings.Default.eMultiMonLastSave = myFile.FullName;
                        Properties.Settings.Default.eMultiMonSavePosDone = true;
                        Properties.Settings.Default.eMultiMonSavePosReady = false;
                        Properties.Settings.Default.eMultiMonSavePosDate = Convert.ToString(DateTime.Now);
                        Properties.Settings.Default.Save();

                        CopyMSGBox.Show($"Positionen Gespeichert: {myFile.FullName}");
                    }
                    else
                    {
                        CopyMSGBox.Show("Keine Datei gefunden.");
                    }
                }
                catch (Exception ex)
                {
                    CopyMSGBox.Show("Fehler beim Öffnen des Dokuments: " + ex.Message);
                }
                
                Thread.Sleep(1000);
            }
        }

        public void MonitorLoadConfig()                                   //mit MultiMonitorTool Monitor Daten Laden------------------------------------------------------------------------------------     
        {
            if (!string.IsNullOrEmpty(Properties.Settings.Default.eMultiMonLastSave))
            {
                try
                {

                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = pathExe; // Pfad zur Anwendung
                    startInfo.Arguments = "/LoadConfig "+ Properties.Settings.Default.eMultiMonLastSave; // Argumente (z. B. Dateipfad)
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden; // Fenster nicht anzeigen

                    //Process.Start(startInfo);
                    Process process = Process.Start(startInfo);
                    process.WaitForExit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fehler beim Öffnen des Dokuments: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Bedingung nicht erfüllt");
            }


        }

        public static void MonitorDeaktivieren(string pathExe, string moniAuswhal,int moniNr)             //Monitor 1 ausschalten (Nach Icon move to Storrage)-----------------------------------------------------------------------------------         
        {
            bool[] aktiv =
            {
                Properties.Settings.Default.eMonitorAktiv1,
                Properties.Settings.Default.eMonitorAktiv2,
                Properties.Settings.Default.eMonitorAktiv3,
                Properties.Settings.Default.eMonitorAktiv4
            };
            MessageBox.Show($"Monitor {moniAuswhal} Aktiv: {aktiv[moniNr]}");
            if(!string.IsNullOrEmpty(Properties.Settings.Default.eMultiMonLastSave) && aktiv[moniNr])
            {
                try
                {

                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = pathExe; // Pfad zur Anwendung
                    startInfo.Arguments = $"/disable {moniAuswhal}"; // Argumente (z. B. Dateipfad)
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden; // Fenster nicht anzeigen

                    //Process.Start(startInfo);
                    Process process = Process.Start(startInfo);
                    process.WaitForExit();

                    if (moniNr == 0) Properties.Settings.Default.eMonitorAktiv1 = false;
                    if (moniNr == 1) Properties.Settings.Default.eMonitorAktiv2 = false;
                    if (moniNr == 2) Properties.Settings.Default.eMonitorAktiv3 = false;
                    if (moniNr == 3) Properties.Settings.Default.eMonitorAktiv4 = false;
                    Properties.Settings.Default.Save();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fehler beim Öffnen des Dokuments: " + ex.Message);
                }

            }
            else
            {
                MonitorAktivieren(pathExe, moniAuswhal, moniNr);
                MessageBox.Show("Bedingung nicht erfüllt");
            }


        }
        public static void MonitorAktivieren(string pathExe, string moniAuswhal, int moniNr)             //mit DesktopOK Positionen der Icons wiederherstellen-----------------------------------------------------------------------------------         
        {
            if (!string.IsNullOrEmpty(Properties.Settings.Default.eMultiMonLastSave))
            {
                try
                {

                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = pathExe; // Pfad zur Anwendung
                    startInfo.Arguments = $"/enable {moniAuswhal}"; // Argumente (z. B. Dateipfad)
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden; // Fenster nicht anzeigen

                    //Process.Start(startInfo);
                    Process process = Process.Start(startInfo);
                    process.WaitForExit();

                    var loadConfig = new FunktionMultiMonitor();
                    loadConfig.MonitorLoadConfig();

                    if (moniNr == 0) Properties.Settings.Default.eMonitorAktiv1 = true;
                    if (moniNr == 1) Properties.Settings.Default.eMonitorAktiv2 = true;
                    if (moniNr == 2) Properties.Settings.Default.eMonitorAktiv3 = true;
                    if (moniNr == 3) Properties.Settings.Default.eMonitorAktiv4 = true;
                    Properties.Settings.Default.Save();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fehler beim Öffnen des Dokuments: " + ex.Message);
                }

            }
            else
            {
                MessageBox.Show("Bedingung nicht erfüllt");
            }


        }
        public string GetExeVersion() // Methode zum Auslesen der Version von DesktopOK.exe
        {
            if (File.Exists(pathExe))
            {
                FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(pathExe);
                return versionInfo.FileVersion;
            }
            else
            {
                return "Datei nicht gefunden.";
            }
        }
    }
}
