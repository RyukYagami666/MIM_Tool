using System.Diagnostics;
using System.IO;
using System.Management;
using System.Windows.Forms;
using App3.Funktions;
using App3.Helpers;
using Windows.UI.Accessibility;



namespace App3.Funktions
{
    class FunktionDeskScen
    {

        public void MonitorSaveData()                                   //mit MultiMonitorTool Daten Speichern------------------------------------------------------------------------------------
        {
            string pathFolder = Properties.Settings.Default.pfadDeskOK;
            string pathExe = $"{Properties.Settings.Default.pfadDeskOK}\\MultiMonitorTool.exe";
            string pathData = $"{Properties.Settings.Default.pfadDeskOK}\\MonitorDaten.txt";

            if (File.Exists(pathData))
            {
                File.Delete(pathData);
            }

            if (Properties.Settings.Default.eMultiMonSaveDataReady && string.IsNullOrEmpty(Properties.Settings.Default.InfoMonitor1))
            {
                try
                {
                    // Prozess für das Öffnen des Dokuments starten
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = pathExe; // Pfad zur Anwendung
                    startInfo.Arguments = $"/stext {pathData}"; // Argumente (z. B. Dateipfad)/SaveConfig
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden; // Fenster nicht anzeigen

                    Process process = Process.Start(startInfo);
                    process.WaitForExit(); // Warten, bis der Prozess abgeschlossen ist

                    if (System.IO.File.Exists(pathData))
                    {
                        // Pfad der zuletzt erstellten Datei speichern
                        Properties.Settings.Default.eMultiMonSaveDataDone = true;
                        Properties.Settings.Default.eMultiMonSaveDataReady = false;
                        Properties.Settings.Default.eMultiMonSaveDataDate = Convert.ToString(DateTime.Now);
                        Properties.Settings.Default.Save();

                        CopyMSGBox.Show($"Daten Gespeichert: {pathData}");
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
            else if (Properties.Settings.Default.eMultiMonSaveDataReady && !string.IsNullOrEmpty(Properties.Settings.Default.InfoMonitor1))
            {
                // Benutzer fragen, ob die Daten gespeichert werden sollen
                var result = MessageBox.Show("Möchten Sie Wirklich alle Monitor Daten Neu Einlesen", "Neu Lesen", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // Überprüfen der Benutzerantwort
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        // Prozess für das Öffnen des Dokuments starten
                        ProcessStartInfo startInfo = new ProcessStartInfo();
                        startInfo.FileName = pathExe; // Pfad zur Anwendung
                        startInfo.Arguments = $"/stext {pathData}"; // Argumente (z. B. Dateipfad)/SaveConfig
                        startInfo.WindowStyle = ProcessWindowStyle.Hidden; // Fenster nicht anzeigen

                        Process process = Process.Start(startInfo);
                        process.WaitForExit(); // Warten, bis der Prozess abgeschlossen ist

                        if (System.IO.File.Exists(pathData))
                        {
                            // Pfad der zuletzt erstellten Datei speichern
                            Properties.Settings.Default.eMultiMonSaveDataDone = true;
                            Properties.Settings.Default.eMultiMonSaveDataReady = false;
                            Properties.Settings.Default.eMultiMonSaveDataDate = Convert.ToString(DateTime.Now);
                            Properties.Settings.Default.Save();

                            CopyMSGBox.Show($"Daten Gespeichert: {pathData}");
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
                else
                {
                    MessageBox.Show("Zurücksetzten abgebrochen."); // Benachrichtigung, dass das Speichern abgebrochen wurde
                }
            }
            else
            {
                CopyMSGBox.Show("Bedingung nicht erfüllt");
            }
        }

        public string[] DataRead(string pathData)                                   //mit DesktopOK Positionen Lesen und Vergleichen------------------------------------------------------------------------------------
        {
            if (Properties.Settings.Default.eMultiMonSaveDataDone)
            {
                try
                {
                    if (File.Exists(pathData))
                    {
                        MessageBox.Show("Datei gefunden.");
                        // Lesen aller Zeilen der Datei in ein Array
                        string[] zeilen = File.ReadAllLines(pathData);

                        string multiMonData = string.Join(";", zeilen);
                        Properties.Settings.Default.MultiMonData = multiMonData;
                        Properties.Settings.Default.Save();

                        return zeilen;
                    }
                    else
                    {
                        CopyMSGBox.Show("Datei nicht gefunden.");
                        return new string[0];
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ein Fehler ist aufgetreten: {ex.Message}");
                    return new string[0];
                }
            }
            else
            {
                MessageBox.Show("Bedingung nicht erfüllt");
                return new string[0];
            }

        }
    }
}