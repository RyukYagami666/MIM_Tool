using System.IO; 
using System.Windows.Forms;

namespace MIM_Tool.Funktions
{
    class FunktionDefaultPath
    {
        //-------------------------------------------------------------------------------------------------Standart/Inizial Funktionen-------------------------------------------------------------------------------------------------
        public void DefaultPath()
        {
            string pathVergleich = @"C:\Users\%username%\Documents\DSM_Files";                                                      // Standardpfad mit Platzhalter für den Benutzernamen.
            string pathWithVariable = Properties.Settings.Default.pfadDeskOK;                                                       // Pfad aus den Einstellungen.
            if (pathVergleich == pathWithVariable)
            {                                                                                  
                string username = Environment.GetEnvironmentVariable("USERNAME") ?? "DefaultUser";                                  // Holt den Benutzernamen aus den Umgebungsvariablen.
                string resolvedPath = pathWithVariable.Replace("%username%", username);                                             // Ersetzt den Platzhalter durch den tatsächlichen Benutzernamen.
                string resolvedDocPath = resolvedPath.Replace("Documents\\DSM_Files", "Documents");                                 // Ersetzt den Pfad für Dokumente.
                Properties.Settings.Default.eUsername = username;                                                                   // Speichert den Benutzernamen in den Einstellungen.
                Properties.Settings.Default.Save();                                                                                 // Speichert die Einstellungen.

                if (System.IO.Directory.Exists(resolvedPath))
                {
                    try
                    {
                        Properties.Settings.Default.pfadDeskOK = resolvedPath;                                                      // Aktualisiert den Pfad in den Einstellungen.
                        Properties.Settings.Default.Save();                                                                         // Speichert die Einstellungen.

                        if (!System.IO.Directory.Exists(resolvedPath + "\\Icons")) System.IO.Directory.CreateDirectory(resolvedPath + "\\Icons");                 // Erstellt das Verzeichnis "Icons".
                        if (!System.IO.Directory.Exists(resolvedPath + "\\BackUps")) System.IO.Directory.CreateDirectory(resolvedPath + "\\BackUps");             // Erstellt das Verzeichnis "BackUps".
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Fehler beim Erstellen des Unterordners DSM_Files: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error); // Zeigt eine Fehlermeldung an.
                    }
                }
                else if (System.IO.Directory.Exists(resolvedDocPath))
                {
                    try
                    {
                        System.IO.Directory.CreateDirectory(resolvedPath);                                             // Erstellt das Verzeichnis.
                        System.IO.Directory.CreateDirectory(resolvedPath + "\\Icons");                                 // Erstellt das Verzeichnis "Icons".
                        System.IO.Directory.CreateDirectory(resolvedPath + "\\BackUps");                               // Erstellt das Verzeichnis "BackUps".
                        Properties.Settings.Default.pfadDeskOK = resolvedPath;                                         // Aktualisiert den Pfad in den Einstellungen.
                        Properties.Settings.Default.Save();                                                            // Speichert die Einstellungen.
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Fehler beim Erstellen des Unterordners DSM_Files: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error); // Zeigt eine Fehlermeldung an.
                    }
                }
                else
                {
                    MessageBox.Show($"Der Pfad: \n {resolvedDocPath} \n wurde Nicht gefunden");                        // Zeigt eine Fehlermeldung an, wenn der Pfad nicht gefunden wurde.
                }
            }
            else
            {
                OrdnerAbfrage();                                                                                       // Ruft die Methode OrdnerAbfrage auf.
            }
        }
        public void OrdnerAbfrage()
        {
            if (!System.IO.Directory.Exists(Properties.Settings.Default.pfadDeskOK))
            {
                System.IO.Directory.CreateDirectory(Properties.Settings.Default.pfadDeskOK);                                                                                                          // Erstellt das Verzeichnis.
                System.IO.Directory.CreateDirectory(Properties.Settings.Default.pfadDeskOK + "\\Icons");                                                                                              // Erstellt das Verzeichnis "Icons".
                System.IO.Directory.CreateDirectory(Properties.Settings.Default.pfadDeskOK + "\\BackUps");                                                                                            // Erstellt das Verzeichnis "BackUps".
            }
            else if (!System.IO.Directory.Exists(Properties.Settings.Default.pfadDeskOK + "\\Icons") || !System.IO.Directory.Exists(Properties.Settings.Default.pfadDeskOK + "\\BackUps"))
            {
                if (!System.IO.Directory.Exists(Properties.Settings.Default.pfadDeskOK + "\\Icons")) { System.IO.Directory.CreateDirectory(Properties.Settings.Default.pfadDeskOK + "\\Icons"); }     // Erstellt das Verzeichnis "Icons", falls es nicht existiert.
                if (!System.IO.Directory.Exists(Properties.Settings.Default.pfadDeskOK + "\\BackUps")) { System.IO.Directory.CreateDirectory(Properties.Settings.Default.pfadDeskOK + "\\BackUps"); } // Erstellt das Verzeichnis "BackUps", falls es nicht existiert.
            }
            else if (System.IO.Directory.Exists(Properties.Settings.Default.pfadDeskOK) && System.IO.Directory.Exists(Properties.Settings.Default.pfadDeskOK + "\\Icons") && System.IO.Directory.Exists(Properties.Settings.Default.pfadDeskOK + "\\BackUps"))
            {
            }
            else
            {
                MessageBox.Show("Fehler beim Erstellen des Pfades: " + Properties.Settings.Default.pfadDeskOK);                                                                                       // Zeigt eine Fehlermeldung an, wenn ein Fehler auftritt.
            }
        }
        public void ResetPath()
        {
            var result = MessageBox.Show("Möchten Sie Wirklich alle Einstellungen und Daten Zurücksetzten?", "Zurücksetzten", MessageBoxButtons.YesNo, MessageBoxIcon.Question); // Fragt den Benutzer, ob er alle Einstellungen zurücksetzen möchte.

            if (result == DialogResult.Yes)
            {
                Directory.Delete(Properties.Settings.Default.pfadDeskOK, true);                      // Löscht das Verzeichnis und alle Unterverzeichnisse.
                Properties.Settings.Default.Reset();                                                 // Setzt die Einstellungen zurück.
                Properties.Settings.Default.Save();                                                  // Speichert die Einstellungen. 
                DefaultPath();                                                                       // Ruft die Methode DefaultPath auf.
            }
            else
            {
                MessageBox.Show("Zurücksetzten abgebrochen.");                                       // Zeigt eine Abbruchmeldung an.
            }
        }
        //-------------------------------------------------------------------------------------------------Admin Funktionen-------------------------------------------------------------------------------------------------
        public void OpenConfig()
        {
            var result = MessageBox.Show("Möchten Sie die Einstellungens Datei Öffnen?", "Öffnen", MessageBoxButtons.YesNo, MessageBoxIcon.Question);                            // Fragt den Benutzer, ob er die Konfigurationsdatei öffnen möchte.
            var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);                                                                        // Holt den Pfad zum lokalen Anwendungsdatenverzeichnis.
            var appFolder = Path.Combine(localAppData, "MIM_Tool");                                                                                                              // Kombiniert den Pfad mit dem Anwendungsordner.
            var configFolder = Directory.GetDirectories(appFolder, "MIM_Tool_Url_*")
                                        .OrderByDescending(d => new DirectoryInfo(d).CreationTime)
                                        .FirstOrDefault();                                                                                                                       // Holt den neuesten Konfigurationsordner.
            if (configFolder != null)
            {
                var filePath = Path.Combine(configFolder, "1.0.0.0", "user.config");                                                                                             // Kombiniert den Pfad zur Konfigurationsdatei.
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        if (System.IO.File.Exists(filePath))
                        {
                            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(filePath) { UseShellExecute = true });                                      // Öffnet die Konfigurationsdatei.
                        }
                        else
                        {
                            MessageBox.Show($"Die Datei {filePath} wurde nicht gefunden.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);                                // Zeigt eine Fehlermeldung an, wenn die Datei nicht gefunden wurde.
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Fehler beim Öffnen der Datei: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);                                    // Zeigt eine Fehlermeldung an, wenn ein Fehler auftritt.
                    }
                }
                else
                {
                    MessageBox.Show("Öffnen abgebrochen.");                                                                                                                      // Zeigt eine Abbruchmeldung an.
                }
            }
            else
            {
                MessageBox.Show("Kein gültiger Konfigurationsordner gefunden.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);                                           // Zeigt eine Fehlermeldung an, wenn kein Konfigurationsordner gefunden wurde.
            }
        }
    }
}

