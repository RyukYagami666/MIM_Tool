using App3.Services;
using System.Net;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;

namespace App3.Funktions
{
    internal class FunktionDesktopOK
    {
      
        public void DODKontrolle()
        {
            if (System.IO.Directory.Exists(Properties.Settings.Default.pfadDeskOK))
            {
                if (Properties.Settings.Default.InfoMonitor1 != null)
                {
                    if (ISPSaveState.IsReady)
                    {
                        DesktopOkState.IsReady = true;
                    }
                    else
                    {
                        DesktopOkState.IsReady = false;
                    }
                }
                else
                {
                    DesktopOkState.IsReady = false;
                }
            }
            else
            {
                DesktopOkState.IsReady = false;
            }

        }

        public void DODStart()
        {
            string pathFolder = Properties.Settings.Default.pfadDeskOK;
            string pathFile = $"{Properties.Settings.Default.pfadDeskOK}\\DesktopOK.zip";
            string pathExe = $"{Properties.Settings.Default.pfadDeskOK}\\DesktopOK.exe";
            string pathBackUP = $"{Properties.Settings.Default.pfadDeskOK}\\BackUps";
            string pathBackUpFile = $"{Properties.Settings.Default.pfadDeskOK}\\BackUps\\DesktopOk{Properties.Settings.Default.eDeskOKVers}.zip";
            if (DesktopOkState.IsReady)
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

        
       

    }
}
