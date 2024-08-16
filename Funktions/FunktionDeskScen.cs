using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace MIM_Tool.Funktions
{
    class FunktionDeskScen
    {
        //-------------------------------------------------------------Speichern der Monitor Infos-----------------------------------------------------------------------------------------------------------
        public void MonitorSaveData()                                                           // Methode zum Speichern von Monitor-Daten mit MultiMonitorTool.
        {
            string pathFolder = Properties.Settings.Default.pfadDeskOK;                         // Pfad zum Verzeichnis.
            string pathExe = $"{Properties.Settings.Default.pfadDeskOK}\\MultiMonitorTool.exe"; // Pfad zur MultiMonitorTool.exe.
            string pathData = $"{Properties.Settings.Default.pfadDeskOK}\\MonitorDaten.txt";    // Pfad zur MonitorDaten.txt.

            if (File.Exists(pathData)) File.Delete(pathData);                                                          // Löscht die Datei, wenn sie existiert.

            if (Properties.Settings.Default.eMultiMonSaveDataReady && string.IsNullOrEmpty(Properties.Settings.Default.InfoMonitor1))
            {
                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo();                        // Prozess für das Öffnen des Dokuments starten.
                    startInfo.FileName = pathExe;                                               // Setzt den Dateinamen.
                    startInfo.Arguments = $"/stext {pathData}";                                 // Setzt die Argumente.
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;                          // Setzt den Fensterstil auf versteckt.

                    Process process = Process.Start(startInfo);                                 // Startet den Prozess.
                    process.WaitForExit();                                                      // Wartet, bis der Prozess beendet ist.

                    if (System.IO.File.Exists(pathData))
                    {
                        // Pfad der zuletzt erstellten Datei speichern.
                        Properties.Settings.Default.eMultiMonSaveDataDone = true;
                        Properties.Settings.Default.eMultiMonSaveDataReady = false;
                        Properties.Settings.Default.eMultiMonSaveDataDate = Convert.ToString(DateTime.Now);
                        Properties.Settings.Default.Save();                                     // Speichert die Einstellungen.
                    }
                    else
                    {
                        MessageBox.Show("Keine Datei gefunden.");                               // Zeigt eine Fehlermeldung an.
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fehler beim Öffnen des Dokuments: " + ex.Message);         // Zeigt eine Fehlermeldung an.
                }

                Thread.Sleep(1000);                                                             // Wartet eine Sekunde.
            }
            else if (Properties.Settings.Default.eMultiMonSaveDataReady && !string.IsNullOrEmpty(Properties.Settings.Default.InfoMonitor1))
            {
                var result = MessageBox.Show("Möchten Sie Wirklich alle Monitor Daten Neu Einlesen", "Neu Lesen", MessageBoxButtons.YesNo, MessageBoxIcon.Question);    // Benutzer fragen, ob die Daten gespeichert werden sollen.
                if (result == DialogResult.Yes)                                                 // Überprüfen der Benutzerantwort.
                {
                    try
                    {
                        ProcessStartInfo startInfo = new ProcessStartInfo();                    // Prozess für das Öffnen des Dokuments starten.
                        startInfo.FileName = pathExe;                                           // Setzt den Dateinamen.
                        startInfo.Arguments = $"/stext {pathData}";                             // Setzt die Argumente.
                        startInfo.WindowStyle = ProcessWindowStyle.Hidden;                      // Setzt den Fensterstil auf versteckt.

                        Process process = Process.Start(startInfo);                             // Startet den Prozess.
                        process.WaitForExit();                                                  // Wartet, bis der Prozess beendet ist.

                        if (System.IO.File.Exists(pathData))
                        {
                            // Pfad der zuletzt erstellten Datei speichern.
                            Properties.Settings.Default.eMultiMonSaveDataDone = true;
                            Properties.Settings.Default.eMultiMonSaveDataReady = false;
                            Properties.Settings.Default.eMultiMonSaveDataDate = Convert.ToString(DateTime.Now);
                            Properties.Settings.Default.Save();                                 // Speichert die Einstellungen.
                        }
                        else
                        {
                            MessageBox.Show("Keine Datei gefunden.");                           // Zeigt eine Fehlermeldung an.
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Fehler beim Öffnen des Dokuments: " + ex.Message);     // Zeigt eine Fehlermeldung an.
                    }

                    Thread.Sleep(1000);                                                         // Wartet eine Sekunde.
                }
                else
                {
                    MessageBox.Show("Zurücksetzten abgebrochen.");                              // Benachrichtigung, dass das Speichern abgebrochen wurde.
                }
            }
            else
            {
                MessageBox.Show("Bedingung nicht erfüllt");                                     // Zeigt eine Fehlermeldung an, wenn die Bedingungen nicht erfüllt sind.
            }
        }
        //-------------------------------------------------------------Lesen der Monitor Infos-----------------------------------------------------------------------------------------------------------
        public string[] DataRead(string pathData)                                               // Methode zum Lesen und Vergleichen von Positionen mit DesktopOK.
        {
            if (Properties.Settings.Default.eMultiMonSaveDataDone)
            {
                try
                {
                    if (File.Exists(pathData))
                    {                                                              
                        string[] zeilen = File.ReadAllLines(pathData);                          // Lesen aller Zeilen der Datei in ein Array.
                        string multiMonData = string.Join(";", zeilen);                         // Verbindet die Zeilen zu einem String.
                        Properties.Settings.Default.MultiMonData = multiMonData;                // Speichert die Daten in den Einstellungen.
                        Properties.Settings.Default.Save();                                     // Speichert die Einstellungen.
                        return zeilen;                                                          // Gibt die Zeilen zurück.
                    }
                    else
                    {
                        MessageBox.Show("Datei nicht gefunden.");                               // Zeigt eine Fehlermeldung an.
                        return new string[0];                                                   // Gibt ein leeres Array zurück.
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ein Fehler ist aufgetreten: {ex.Message}");               // Zeigt eine Fehlermeldung an.
                    return new string[0];                                                       // Gibt ein leeres Array zurück.
                }
            }
            else
            {
                MessageBox.Show("Bedingung nicht erfüllt");                                     // Zeigt eine Fehlermeldung an, wenn die Bedingungen nicht erfüllt sind.
                return new string[0];                                                           // Gibt ein leeres Array zurück.
            }
        }
    }
}