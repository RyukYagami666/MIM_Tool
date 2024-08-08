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

namespace App3.Funktions
{
    internal class FunktionMultiMonitor
    {
        string pathFolder = Properties.Settings.Default.pfadDeskOK; 
        string pathFile = $"{Properties.Settings.Default.pfadDeskOK}\\multimonitortool-x64.zip";
        string pathExe = $"{Properties.Settings.Default.pfadDeskOK}\\MultiMonitorTool.exe";
        string pathBackUP = $"{Properties.Settings.Default.pfadDeskOK}\\BackUps";
        string pathBackUpFile = $"{Properties.Settings.Default.pfadDeskOK}\\BackUps\\MultiMonitorTool{Properties.Settings.Default.eMultiMonDownloadReady}.zip";
        string pathLastData = Properties.Settings.Default.eMultiMonLastSave;

        public void MMDKontrolle()                  // Kontrolle ob DesktopOK bereit ist zum Downloaden------------------------------------------------------------------------------------
        {
            if (System.IO.Directory.Exists(pathFolder)){
                if (Properties.Settings.Default.InfoMonitor1 != null){
                    if (ISPSaveState.IsReady){
                        Properties.Settings.Default.eMultiMonDownloadReady = true;
                        Properties.Settings.Default.Save();
                    }
                    else {
                        Properties.Settings.Default.eMultiMonDownloadReady = false;
                        Properties.Settings.Default.Save();
                    }
                }
                else{
                    Properties.Settings.Default.eMultiMonDownloadReady = false;
                    Properties.Settings.Default.Save();
                }
            }
            else{
                Properties.Settings.Default.eMultiMonDownloadReady = false;
                Properties.Settings.Default.Save();
            }

        }
        public void MMSKontrolle()                  // Kontrolle ob DesktopOK bereit ist zum Speichern der IconPos------------------------------------------------------------------------------------
        {
            if (System.IO.Directory.Exists(pathFolder)){
                if (System.IO.File.Exists(pathExe)){
                    if (Properties.Settings.Default.eMultiMonDownloadDone){
                        Properties.Settings.Default.eMultiMonSavePosReady = true;
                        Properties.Settings.Default.Save();
                    }
                    else{
                        Properties.Settings.Default.eMultiMonSavePosReady = false;
                        Properties.Settings.Default.Save();
                        MessageBox.Show("download nicht abgeschlossen");
                    }
                }
                else {
                    Properties.Settings.Default.eMultiMonSavePosReady = false;
                    Properties.Settings.Default.Save();
                    MessageBox.Show("Exe Fehler!");
                }
            }
            else{
                Properties.Settings.Default.eMultiMonSavePosReady = false;
                Properties.Settings.Default.Save();
                MessageBox.Show("Pfad Fehler!");
            }

        }
        public void MMRKontrolle()                  // Kontrolle ob DesktopOK bereit ist zum Lesenund Vergleichen------------------------------------------------------------------------------------
        {
            if (System.IO.Directory.Exists(pathFolder)){
                if (System.IO.File.Exists(Properties.Settings.Default.eMultiMonLastSave)){
                    if (Properties.Settings.Default.eMultiMonSavePosDone){
                        Properties.Settings.Default.eMultiMonDataReedReady = true;
                        Properties.Settings.Default.Save();
                    }
                    else{
                        Properties.Settings.Default.eMultiMonDataReedReady = false;
                        Properties.Settings.Default.Save();
                    }
                }
                else{
                    Properties.Settings.Default.eMultiMonDataReedReady = false;
                    Properties.Settings.Default.Save();
                }
            }
            else{
                Properties.Settings.Default.eMultiMonDataReedReady = false;
                Properties.Settings.Default.Save();
            }

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
                            System.IO.Directory.CreateDirectory(pathBackUP);
                            System.IO.File.Move(pathFile, pathBackUpFile);
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


        public void MonitorSaveConfig()                                   //mit MultiMonitorTool Daten Speichern------------------------------------------------------------------------------------
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

                    Process.Start(startInfo);

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

        public static void Monitor1Deaktivieren(string pathExe)             //Monitor 1 ausschalten (Nach Icon move to Storrage)-----------------------------------------------------------------------------------         
        {
            if(!string.IsNullOrEmpty(Properties.Settings.Default.eMultiMonLastSave) && Properties.Settings.Default.eMonitorAktiv1)
            {
                try
                {
                   
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = pathExe; // Pfad zur Anwendung
                    startInfo.Arguments = "/disable \\\\.\\DISPLAY2"; // Argumente (z. B. Dateipfad)
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden; // Fenster nicht anzeigen

                    //Process.Start(startInfo);
                    Process process = Process.Start(startInfo);
                    process.WaitForExit();

                    Properties.Settings.Default.eMonitorAktiv1 = false;
                    Properties.Settings.Default.Save();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fehler beim Öffnen des Dokuments: " + ex.Message);
                }

            }
            else
            {
                Monitor1Aktivieren(pathExe);
                MessageBox.Show("Bedingung nicht erfüllt");
            }


        }
        public static void Monitor1Aktivieren(string pathExe)             //mit DesktopOK Positionen der Icons wiederherstellen-----------------------------------------------------------------------------------         
        {
            if (!string.IsNullOrEmpty(Properties.Settings.Default.eMultiMonLastSave))
            {
                try
                {

                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = pathExe; // Pfad zur Anwendung
                    startInfo.Arguments = "/enable \\\\.\\DISPLAY2"; // Argumente (z. B. Dateipfad)
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden; // Fenster nicht anzeigen

                    //Process.Start(startInfo);
                    Process process = Process.Start(startInfo);
                    process.WaitForExit();

                    var loadConfig = new FunktionMultiMonitor();
                    loadConfig.MonitorLoadConfig();

                    Properties.Settings.Default.eMonitorAktiv1 = true;
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
    }
}
