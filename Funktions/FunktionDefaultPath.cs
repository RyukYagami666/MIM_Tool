using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App3.Funktions
{
    class FunktionDefaultPath
    {
        public void DefaultPath()
        {
            // Lesen des Pfades aus den Einstellungen
            string pathVergleich = @"C:\Users\%username%\Documents\DSM_Files";
            string pathWithVariable = Properties.Settings.Default.pfadDeskOK;
            if(pathVergleich == pathWithVariable)
            {
                CopyMSGBox.Show("Vergleich OK");
                // Ersetzen der Umgebungsvariable %username% durch den tatsächlichen Benutzernamen
                string username = Environment.GetEnvironmentVariable("USERNAME") ?? "DefaultUser";
                string resolvedPath = pathWithVariable.Replace("%username%", username);
                string resolvedDocPath = resolvedPath.Replace("Documents\\DSM_Files", "Documents");

                Properties.Settings.Default.eUsername = username;
                Properties.Settings.Default.Save();

                // Verwenden des aufgelösten Pfades
                // Zum Beispiel: Überprüfen, ob die Datei existiert
                if (System.IO.Directory.Exists(resolvedPath))
                {
                    Properties.Settings.Default.pfadDeskOK = resolvedPath;
                    Properties.Settings.Default.Save();
                    CopyMSGBox.Show($"Der Pfad wurde erfolgreich aktualisiert. \n {resolvedPath}");
                    // Datei existiert
                }
                else if (System.IO.Directory.Exists(resolvedDocPath))
                {
                    try
                    {
                        // Erstellt den Unterordner, falls er noch nicht existiert
                        System.IO.Directory.CreateDirectory(resolvedPath);
                        System.IO.Directory.CreateDirectory(resolvedPath + "\\Icons");
                        System.IO.Directory.CreateDirectory(resolvedPath + "\\BackUps");
                        Properties.Settings.Default.pfadDeskOK = resolvedPath;
                        Properties.Settings.Default.Save();
                        CopyMSGBox.Show($"Unterordner DSM_Files wurde erfolgreich erstellt. \n Pfad: {resolvedPath}");
                    }
                    catch (Exception ex)
                    {
                        // Fehlerbehandlung, falls beim Erstellen des Unterordners ein Fehler auftritt
                        MessageBox.Show($"Fehler beim Erstellen des Unterordners DSM_Files: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                }
                else
                {
                    CopyMSGBox.Show($"Der Pfad: \n {resolvedDocPath} \n wurde Nicht gefunden");
                    // Datei existiert nicht
                }
               
                

            }
        }
        public void ResetPath()
        {
            // Benutzer fragen, ob die Daten gespeichert werden sollen
            var result = MessageBox.Show("Möchten Sie Wirklich alle Einstellungen und Daten Zurücksetzten?", "Zurücksetzten", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Überprüfen der Benutzerantwort
            if (result == DialogResult.Yes)
            {
                Properties.Settings.Default.Reset();
                Properties.Settings.Default.Save();
                CopyMSGBox.Show(Properties.Settings.Default.pfadDeskOK);
                DefaultPath();
            }
            else
            {
                MessageBox.Show("Zurücksetzten abgebrochen."); // Benachrichtigung, dass das Speichern abgebrochen wurde
            }
        }
        public void OpenConfig()
        {
            // Benutzer fragen, ob die Daten gespeichert werden sollen
            var result = MessageBox.Show("Möchten Sie die Einstellungens Datei Öffnen?", "Öffnen", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            var filePath = @"C:\Users\Ryuk\AppData\Local\App3\App3_Url_1gihmeoyxgyrw5xqwgmdh0mo3pzhcqpe\1.0.0.0\user.config"; // user.config"; // Pfad zur user.config Datei

            // Überprüfen der Benutzerantwort
            if (result == DialogResult.Yes)
            {
                try
                {
                    // Überprüfen, ob die Datei existiert
                    if (System.IO.File.Exists(filePath))
                    {
                        // Öffnet die Datei mit dem Standardprogramm des Systems
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(filePath) { UseShellExecute = true });
                    }
                    else
                    {
                        MessageBox.Show($"Die Datei {filePath} wurde nicht gefunden.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Fehler beim Öffnen der Datei: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Öffnen abgebrochen."); // Benachrichtigung, dass das Speichern abgebrochen wurde
            }
        }
    }
}
