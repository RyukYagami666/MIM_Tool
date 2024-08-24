using System.Drawing;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Windows.Forms;
using MIM_Tool.Helpers;

namespace MIM_Tool.Funktions
{
    class FunktionDefaultPath
    {
        //-------------------------------------------------------------------------------------------------Standart/Inizial Funktionen-------------------------------------------------------------------------------------------------
        public void DefaultPath()
        {
            Log.inf("Start der DefaultPath Funktion Mit einem Abgleich ab dir Einstellungsdatei geändert wurde");
            string pathVergleich = @"C:\Users\%username%\Documents\MIM_Files";                                                      // Standardpfad mit Platzhalter für den Benutzernamen.
            string pathWithVariable = Properties.Settings.Default.pfadDeskOK;                                                       // Pfad aus den Einstellungen.
            if (pathVergleich == pathWithVariable)
            {   
                Log.inf("Der DefaultPath wird ausgeführt, da die Einstellungsdatei Standard einstellungen hat");
                string username = Environment.GetEnvironmentVariable("USERNAME") ?? "DefaultUser";                                  // Holt den Benutzernamen aus den Umgebungsvariablen.
                string resolvedPath = pathWithVariable.Replace("%username%", username);                                             // Ersetzt den Platzhalter durch den tatsächlichen Benutzernamen.
                string resolvedDocPath = resolvedPath.Replace("Documents\\MIM_Files", "Documents");                                 // Ersetzt den Pfad für Dokumente.
                Properties.Settings.Default.eUsername = username;                                                                   // Speichert den Benutzernamen in den Einstellungen.
                Properties.Settings.Default.Save();                                                                                 // Speichert die Einstellungen.

                Log.inf("Was wurde für den Standardpfad der Name des Nutzers rausgesucht um ihn zu vervollständigen und nutzbar zu machen ");

                if (System.IO.Directory.Exists(resolvedPath))
                {
                    Log.inf("Der standart Speicherordner existiert bereits auf dem Standardpfad.");
                    try
                    {
                        Properties.Settings.Default.pfadDeskOK = resolvedPath;                                                      // Aktualisiert den Pfad in den Einstellungen.
                        Properties.Settings.Default.Save();                                                                         // Speichert die Einstellungen.

                        if (!System.IO.Directory.Exists(resolvedPath + "\\Icons")) System.IO.Directory.CreateDirectory(resolvedPath + "\\Icons");                 // Erstellt das Verzeichnis "Icons".
                        if (!System.IO.Directory.Exists(resolvedPath + "\\BackUps")) System.IO.Directory.CreateDirectory(resolvedPath + "\\BackUps");             // Erstellt das Verzeichnis "BackUps".
                        Log.inf("Wenn Lydisch wurden die notwendigen Unterordner noch erstellt");
                    }
                    catch (Exception ex)
                    {
                        
                        Log.err("Dein Erstellen der Unterordner ist etwas schiefgelaufen, Schauer evtl nach der Zugriffsberechtigung", ex,true);//ex,alarm               // Speichert den Fehler in den Einstellungen.
                        if (Properties.Settings.Default.InizialisierungAktiv) Properties.Settings.Default.InizialisierungsFehler = true;                                // Setzt den Initialisierungsfehler auf true.
                        Properties.Settings.Default.Save();
                    }
                }
                else if (System.IO.Directory.Exists(resolvedDocPath))
                {
                    Log.inf("Der Dokument Ordner wurde gefunden sodass Ordner erstellt werden können.");
                    try
                    {
                        System.IO.Directory.CreateDirectory(resolvedPath);                                             // Erstellt das Verzeichnis.
                        System.IO.Directory.CreateDirectory(resolvedPath + "\\Icons");                                 // Erstellt das Verzeichnis "Icons".
                        System.IO.Directory.CreateDirectory(resolvedPath + "\\BackUps");                               // Erstellt das Verzeichnis "BackUps".
                        Properties.Settings.Default.pfadDeskOK = resolvedPath;                                         // Aktualisiert den Pfad in den Einstellungen.
                        Properties.Settings.Default.Save();                                                            // Speichert die Einstellungen.
                        Log.inf("Der Standardpfad wurde erstellt und die Unterordner wurden erstellt.");
                    }
                    catch (Exception ex)
                    {
                        Log.err("Beim ausstellen das Hauptspeicher Ortners und dessen Unterordner ist etwas schief gelaufen , Schauer evtl nach der Zugriffsberechtigung",ex,true);// Speichert den Fehler in den Einstellungen.
                        Properties.Settings.Default.LetzterFehler = $"Fehler(DefaultPath2) beim Erstellen des Unterordners: {ex.Message}";                              
                        if (Properties.Settings.Default.InizialisierungAktiv) Properties.Settings.Default.InizialisierungsFehler = true;                                // Setzt den Initialisierungsfehler auf true.
                        Properties.Settings.Default.Save();
                    }
                }
                else
                {
                    Log.err("Der Benutzer Dokumentenordner wurde nicht gefunden, Du kannst in den Einstellungen selber einen Pfad angeben wohin der Standardspeicherordner gespeichert werden soll.",null,true);//ex,alarm// Speichert den Fehler in den Einstellungen.
                    if (Properties.Settings.Default.InizialisierungAktiv) Properties.Settings.Default.InizialisierungsFehler = true;                                // Setzt den Initialisierungsfehler auf true.
                    Properties.Settings.Default.Save();
                }
            }
            else
            {
                Log.inf("Dir default pass wurde ausgeführt obwohl Settings nicht zurücksetzt wurde, deswegen wird werden die Ordner Konntroliert ");
                OrdnerAbfrage();                                                                                       // Ruft die Methode OrdnerAbfrage auf.
            }
        }
        public void OrdnerAbfrage()
        {
            try 
            { 
                if (!System.IO.Directory.Exists(Properties.Settings.Default.pfadDeskOK))
                {
                    System.IO.Directory.CreateDirectory(Properties.Settings.Default.pfadDeskOK);                                                                                                          // Erstellt das Verzeichnis.
                    System.IO.Directory.CreateDirectory(Properties.Settings.Default.pfadDeskOK + "\\Icons");                                                                                              // Erstellt das Verzeichnis "Icons".
                    System.IO.Directory.CreateDirectory(Properties.Settings.Default.pfadDeskOK + "\\BackUps");                                                                                             // Erstellt das Verzeichnis "BackUps".
                    Log.inf("Es wurden alle notwendigen Ordner und Unterordner erstellt, wie in den Einstellungen gespeichert.: " + Properties.Settings.Default.pfadDeskOK);
                }
                else if (!System.IO.Directory.Exists(Properties.Settings.Default.pfadDeskOK + "\\Icons") || !System.IO.Directory.Exists(Properties.Settings.Default.pfadDeskOK + "\\BackUps"))
                {
                    if (!System.IO.Directory.Exists(Properties.Settings.Default.pfadDeskOK + "\\Icons")) { System.IO.Directory.CreateDirectory(Properties.Settings.Default.pfadDeskOK + "\\Icons"); }     // Erstellt das Verzeichnis "Icons", falls es nicht existiert.
                    if (!System.IO.Directory.Exists(Properties.Settings.Default.pfadDeskOK + "\\BackUps")) { System.IO.Directory.CreateDirectory(Properties.Settings.Default.pfadDeskOK + "\\BackUps"); } // Erstellt das Verzeichnis "BackUps", falls es nicht existiert.
                    Log.inf("Es wurden alle notwendigen Unterordner erstellt (\\Icons ; \\BackUps ) ");
                }
                else if (System.IO.Directory.Exists(Properties.Settings.Default.pfadDeskOK) && System.IO.Directory.Exists(Properties.Settings.Default.pfadDeskOK + "\\Icons") && System.IO.Directory.Exists(Properties.Settings.Default.pfadDeskOK + "\\BackUps"))
                {
                    Log.inf("Alle notwendigen Ordner und Unterordner sind bereits vorhanden, wie in den Einstellungen gespeichert.: " + Properties.Settings.Default.pfadDeskOK);
                }
            }
            catch (Exception ex)
            {
                Log.err($"Beim Erstellen des Pfades: {Properties.Settings.Default.pfadDeskOK} ist etwas schiefgelaufen",ex);//ex,alarm                                       // Speichert den Fehler in den Einstellungen.
                if (Properties.Settings.Default.InizialisierungAktiv) Properties.Settings.Default.InizialisierungsFehler = true;                                                                // Setzt den Initialisierungsfehler auf true.
                Properties.Settings.Default.Save();
            }
            
        }
        public void ResetPath()
        {
            Log.inf("Startabfrage ob der Benutzer wirklich resetten möchte und alle Daten löschen.");
            var result = MessageBox.Show("Möchten Sie Wirklich alle Einstellungen und Daten Zurücksetzten?", "Zurücksetzten", MessageBoxButtons.YesNo, MessageBoxIcon.Question); // Fragt den Benutzer, ob er alle Einstellungen zurücksetzen möchte.

            if (result == DialogResult.Yes)
            {
                Directory.Delete(Properties.Settings.Default.pfadDeskOK, true);                      // Löscht das Verzeichnis und alle Unterverzeichnisse.
                Properties.Settings.Default.Reset();                                                 // Setzt die Einstellungen zurück.
                Properties.Settings.Default.Save();                                                  // Speichert die Einstellungen. 
                Log.inf("Daten gelöscht, Einstellungsdatei zurückgesetzt, DefaultPath wird ausgeführt ");
                DefaultPath();                                                                       // Ruft die Methode DefaultPath auf.
            }
            else
            {                                  
                Log.war("Zurücksetzten abgebrochen.");//2s                                          // Zeigt eine Abbruchmeldung an.
            }
        }
        //-------------------------------------------------------------------------------------------------Admin Funktionen-------------------------------------------------------------------------------------------------
        public void OpenConfig()
        {
            Log.inf("Anfangen dir config Datei also einstellungsdatei zu suchen und zu öffnen");
            var result = MessageBox.Show("Möchten Sie die Einstellungens Datei Öffnen?", "Öffnen", MessageBoxButtons.YesNo, MessageBoxIcon.Question);                            // Fragt den Benutzer, ob er die Konfigurationsdatei öffnen möchte.
            var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);                                                                        // Holt den Pfad zum lokalen Anwendungsdatenverzeichnis.
            var appFolder = Path.Combine(localAppData, "MIM_Tool");                                                                                                              // Kombiniert den Pfad mit dem Anwendungsordner.
            var configFolder = Directory.GetDirectories(appFolder, "MIM_Tool_Url_*")
                                        .OrderByDescending(d => new DirectoryInfo(d).CreationTime)
                                        .FirstOrDefault();                                                                                                                       // Holt den neuesten Konfigurationsordner.
            if (configFolder != null)
            {
                Log.inf("Konfigurationsordner gefunden ");
                var filePath = Path.Combine(configFolder, "1.0.0.0", "user.config");                                                                                             // Kombiniert den Pfad zur Konfigurationsdatei.
                if (result == DialogResult.Yes)
                {
                    Log.inf("Benutzer bestätigt Konfekt wird gesucht ");
                    try
                    {
                        if (System.IO.File.Exists(filePath))
                        {

                            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(filePath) { UseShellExecute = true });                                      // Öffnet die Konfigurationsdatei.
                            Log.inf("Die Datei wurde gefunden und geöffnet ");
                        }
                        else
                        {
                            Log.err("Hallo die Dateien : ist nicht vorhanden , \nwahrscheinlich ist es in einem anderen Unterordner, \nUnter %appdata% Temp MIM_Tool, müsste irgendwo die Datei sein ",null,true);                                // Zeigt eine Fehlermeldung an, wenn die Datei nicht gefunden wurde.
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.err("Das Öffnen wollte irgendwie nich",ex,true);//ex,alarm    MessageBox.Show($"Fehler beim Öffnen der Datei: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);                                    // Zeigt eine Fehlermeldung an, wenn ein Fehler auftritt.
                    }
                }
                else
                {
                    Log.war("Was möchtest du nicht in die Tiefe der Skripte eintauchen.");//2s                                                                                  // Zeigt eine Abbruchmeldung an.
                }
            }
            else
            {
                Log.err("Kein gültiger Konfigurationsordner gefunden.");//ex,alarm      MessageBox.Show("Kein gültiger Konfigurationsordner gefunden.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);                                           // Zeigt eine Fehlermeldung an, wenn kein Konfigurationsordner gefunden wurde.
            }
        }
    }
}

