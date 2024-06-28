using IniParser;
using IniParser.Model;
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
            // Initialisierung des INI-Datei-Parsers und Lesen der Daten
            string appBasePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            settingsPathFile = Path.Combine(appBasePath, "Funktions", "sepain.txt");

            // Lese den aktuellen Pfad zur INI-Datei aus der settingsPath.txt
            DataIniSpeicherPfad = File.ReadAllText(settingsPathFile).Trim();
            MessageBox.Show(DataIniSpeicherPfad);
        }

        public IniData ReadIniFile()
        {
            var parser = new FileIniDataParser();
            return parser.ReadFile(DataIniSpeicherPfad);
        }

        public void WriteIniFile(IniData data)
        {
            var parser = new FileIniDataParser();
            parser.WriteFile(DataIniSpeicherPfad, data);
        }

        public void UpdateIniFilePath(string newPath)
        {
            DataIniSpeicherPfad = newPath;
            File.WriteAllText(settingsPathFile, newPath);
        }
    }
}
