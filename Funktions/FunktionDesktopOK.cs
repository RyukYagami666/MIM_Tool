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

namespace App3.Funktions
{
    internal class FunktionDesktopOK
    {
        string pathFolder = Properties.Settings.Default.pfadDeskOK; 
        string pathFile = $"{Properties.Settings.Default.pfadDeskOK}\\DesktopOK.zip";
        string pathExe = $"{Properties.Settings.Default.pfadDeskOK}\\DesktopOK.exe";
        string pathBackUP = $"{Properties.Settings.Default.pfadDeskOK}\\BackUps";
        string pathBackUpFile = $"{Properties.Settings.Default.pfadDeskOK}\\BackUps\\DesktopOk{Properties.Settings.Default.eDeskOkDownloadReady}.zip";
        string pathLastData = Properties.Settings.Default.eDeskOkLastSave;

        public void DODKontrolle()                  // Kontrolle ob DesktopOK bereit ist zum Downloaden------------------------------------------------------------------------------------
        {
            if (System.IO.Directory.Exists(pathFolder)){
                if (Properties.Settings.Default.InfoMonitor1 != null){
                    if (ISPSaveState.IsReady){
                        Properties.Settings.Default.eDeskOkDownloadReady = true;
                        Properties.Settings.Default.Save();
                    }
                    else {
                        Properties.Settings.Default.eDeskOkDownloadReady = false;
                        Properties.Settings.Default.Save();
                    }
                }
                else{
                    Properties.Settings.Default.eDeskOkDownloadReady = false;
                    Properties.Settings.Default.Save();
                }
            }
            else{
                Properties.Settings.Default.eDeskOkDownloadReady = false;
                Properties.Settings.Default.Save();
            }

        }
        public void DOSKontrolle()                  // Kontrolle ob DesktopOK bereit ist zum Speichern der IconPos------------------------------------------------------------------------------------
        {
            if (System.IO.Directory.Exists(pathFolder)){
                if (System.IO.File.Exists(pathExe)){
                    if (Properties.Settings.Default.eDeskOkDownloadDone){
                        Properties.Settings.Default.eDeskOkSavePosReady = true;
                        Properties.Settings.Default.Save();
                    }
                    else{
                        Properties.Settings.Default.eDeskOkSavePosReady = false;
                        Properties.Settings.Default.Save();
                    }
                }
                else {
                    Properties.Settings.Default.eDeskOkSavePosReady = false;
                    Properties.Settings.Default.Save();
                }
            }
            else{
                Properties.Settings.Default.eDeskOkSavePosReady = false;
                Properties.Settings.Default.Save();
            }

        }
        public void DORKontrolle()                  // Kontrolle ob DesktopOK bereit ist zum Lesenund Vergleichen------------------------------------------------------------------------------------
        {
            if (System.IO.Directory.Exists(pathFolder)){
                if (System.IO.File.Exists(Properties.Settings.Default.eDeskOkLastSave)){
                    if (Properties.Settings.Default.eDeskOkSavePosDone){
                        Properties.Settings.Default.eDeskOkDataReedReady = true;
                        Properties.Settings.Default.Save();
                    }
                    else{
                        Properties.Settings.Default.eDeskOkDataReedReady = false;
                        Properties.Settings.Default.Save();
                    }
                }
                else{
                    Properties.Settings.Default.eDeskOkDataReedReady = false;
                    Properties.Settings.Default.Save();
                }
            }
            else{
                Properties.Settings.Default.eDeskOkDataReedReady = false;
                Properties.Settings.Default.Save();
            }

        }

        public void DODStart()          // Download von DesktopOK------------------------------------------------------------------------------------
        {
           

            if (Properties.Settings.Default.eDeskOkDownloadReady)
            {
                if (System.IO.File.Exists(pathFile))
                {
                    var result = MessageBox.Show("Datei Erneut Herrunterladen?", "Download", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            System.IO.Directory.CreateDirectory(pathBackUP);
                            System.IO.File.Move(pathFile, pathBackUpFile);  
                            WebClient client = new WebClient();
                            client.DownloadFile("https://www.softwareok.com/Download/DesktopOK.zip", pathFile);
                            CopyMSGBox.Show("Download abgeschlossen");
                            ZipFile.ExtractToDirectory(pathFile, pathFolder, true);
                            Properties.Settings.Default.eDeskOkDownloadDone = true;
                            Properties.Settings.Default.eDeskOkDownloadReady = false;
                            Properties.Settings.Default.eDeskOkDownloadDate = Convert.ToString(DateTime.Now);
                            Properties.Settings.Default.Save();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Fehler beim Downloaden der Datei: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else if (System.IO.Directory.Exists(pathFolder))
                {
                    var result = MessageBox.Show("Datei Herrunterladen?", "Download", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            WebClient client = new WebClient();
                            client.DownloadFile("https://www.softwareok.com/Download/DesktopOK.zip", pathFile);
                            CopyMSGBox.Show("Download abgeschlossen");
                            ZipFile.ExtractToDirectory(pathFile, pathFolder, true);
                            Properties.Settings.Default.eDeskOkDownloadDone = true;
                            Properties.Settings.Default.eDeskOkDownloadReady = false;
                            Properties.Settings.Default.eDeskOkDownloadDate = Convert.ToString(DateTime.Now);
                            Properties.Settings.Default.Save();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Fehler beim Downloaden der Datei: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


        public void IconSavePos()                                   //mit DesktopOK Positionen Speichern------------------------------------------------------------------------------------
        {
            if (Properties.Settings.Default.eDeskOkSavePosReady) {

                try
                {
                    if (!string.IsNullOrEmpty(Properties.Settings.Default.eDeskOkLastSave) && System.IO.File.Exists(Properties.Settings.Default.eDeskOkLastSave))
                    {
                        string fileToMove = pathLastData.Replace($"{pathFolder}", "");
                        System.IO.Directory.CreateDirectory(pathBackUP);
                        System.IO.File.Move(Properties.Settings.Default.eDeskOkLastSave, $"{pathBackUP}{fileToMove}");

                    }
                    // Prozess für das Öffnen des Dokuments starten
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = pathExe; // Pfad zur Anwendung
                    startInfo.Arguments = $"/save /silent {pathFolder}\\DeskOk_date_time_.dok"; // Argumente (z. B. Dateipfad)
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden; // Fenster nicht anzeigen

                    Process.Start(startInfo);

                    CopyMSGBox.Show($"Positionen Gespeichert");

                    var directoryInfo = new DirectoryInfo(pathFolder);
                    var myFile = directoryInfo.GetFiles()
                                              .OrderByDescending(f => f.LastWriteTime)
                                              .FirstOrDefault();

                    if (myFile != null)
                    {
                        // Pfad der zuletzt erstellten Datei speichern
                        Properties.Settings.Default.eDeskOkLastSave = myFile.FullName;
                        Properties.Settings.Default.eDeskOkSavePosDone = true;
                        Properties.Settings.Default.eDeskOkSavePosReady = false;
                        Properties.Settings.Default.eDeskOkSavePosDate = Convert.ToString(DateTime.Now);
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

        public string[] DataRead(string pathLastData)                                   //mit DesktopOK Positionen Lesen und Vergleichen------------------------------------------------------------------------------------
        {
            if (Properties.Settings.Default.eDeskOkDataReedReady)
            {
                try
                {
                    if (File.Exists(pathLastData))
                    {
                        // Lesen aller Zeilen der Datei in ein Array
                        string[] zeilen = File.ReadAllLines(pathLastData);

                        Properties.Settings.Default.eDeskOkDataReedReady = false;
                        Properties.Settings.Default.eDeskOkDataReedDone = true;
                        Properties.Settings.Default.eDeskOkDataReedDate = Convert.ToString(DateTime.Now);


                        string deskOkData = string.Join(";", zeilen);
                        deskOkData = deskOkData.Replace("=", ";");
                        Properties.Settings.Default.DeskOkData = deskOkData;
                        Properties.Settings.Default.Save();

                        return zeilen;
                    }
                    else
                    {
                        CopyMSGBox.Show("Datei nicht gefunden.");
                        // Hier müssen Sie entscheiden, was zurückgegeben werden soll, wenn die Datei nicht existiert.
                        // Zum Beispiel könnten Sie ein leeres Array zurückgeben:
                        return new string[0];
                    }

                    

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ein Fehler ist aufgetreten: {ex.Message}");
                    // Auch hier müssen Sie entscheiden, was im Fehlerfall zurückgegeben werden soll.
                    // Zum Beispiel könnten Sie ein leeres Array zurückgeben:
                    return new string[0];
                }
            }
            else 
            { 
                MessageBox.Show("Bedingung nicht erfüllt");
            // Wenn eDeskOkDataReedReady false ist, müssen Sie auch entscheiden, was zurückgegeben werden soll.
            // Zum Beispiel könnten Sie ein leeres Array zurückgeben:
            return new string[0];
            }
            
        }

        public static void IconRestore(string pathExe,string pathLastData)             //mit DesktopOK Positionen der Icons wiederherstellen-----------------------------------------------------------------------------------         
        {
            if(Properties.Settings.Default.eDeskOkDataReedDone && !string.IsNullOrEmpty(Properties.Settings.Default.eDeskOkLastSave))
            {
                try
                {
                   
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = pathExe; // Pfad zur Anwendung
                    startInfo.Arguments = "/load /silent " + pathLastData; // Argumente (z. B. Dateipfad)
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
    }
}
