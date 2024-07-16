using IniParser;
using IniParser.Model;
using System;
using System.IO;
using System.Reflection;
using System.Windows;

namespace App3.Funktions
{
    public class FunktionIniPars
    {
        private string settingsPathFile;
        public string DataIniSpeicherPfad { get; private set; }

        public FunktionIniPars()
        {
            try
            {
                // Initialisierung des INI-Datei-Parsers und Lesen der Daten
                string appBasePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                settingsPathFile = Path.Combine(appBasePath, "Funktions", "sepain.txt");

                // Überprüfen, ob die Datei existiert
                if (!File.Exists(settingsPathFile))
                {
                    MessageBox.Show("Die Datei sepain.txt wurde nicht gefunden.");
                    return;
                }

                // Lese den aktuellen Pfad zur INI-Datei aus der settingsPath.txt
                DataIniSpeicherPfad = File.ReadAllText(settingsPathFile).Trim();
                MessageBox.Show(DataIniSpeicherPfad);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ein Fehler ist aufgetreten: {ex.Message}");
            }
        }

        public IniData ReadIniFile()
        {
            try
            {
                var parser = new FileIniDataParser();
                return parser.ReadFile(DataIniSpeicherPfad);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Lesen der INI-Datei: {ex.Message}");
                return null; // Oder eine leere IniData-Instanz zurückgeben, je nach Anwendungslogik
            }
        }

        public void WriteIniFile(IniData data)
        {
            try
            {
                var parser = new FileIniDataParser();
                parser.WriteFile(DataIniSpeicherPfad, data);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Schreiben in die INI-Datei: {ex.Message}");
            }
        }

        public void UpdateIniFilePath(string newPath)
        {
            try
            {
                DataIniSpeicherPfad = newPath;
                File.WriteAllText(settingsPathFile, newPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Aktualisieren des INI-Dateipfads: {ex.Message}");
            }
        }
    }
}
