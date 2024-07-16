using System;
using System.IO;
using System.Reflection;
using System.Windows;

namespace App3.Funktions
{
    class FunktionIniStartUp
    {
        public FunktionIniStartUp()
        {
            Initialisieren();
        }

        private void Initialisieren()
        {
            try
            {
                koppyForDebug();
                SichereBackupDaten();
                // Weitere Initialisierungsaktionen hier
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ein Fehler ist aufgetreten: {ex.Message}");
            }
        }

        private void SichereBackupDaten()
        {
            string appBasePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string targetPath = Path.Combine(appBasePath, "Funktions", "sepain.txt");
            string iniPfad = File.ReadAllText(targetPath).Trim();
            string iniVerzeichnis = Path.GetDirectoryName(iniPfad);

            string backupPfad = Path.Combine(iniVerzeichnis, "Backups", "backup.txt");

            string backupVerzeichnis = Path.GetDirectoryName(backupPfad);
            if (!Directory.Exists(backupVerzeichnis))
            {
                Directory.CreateDirectory(backupVerzeichnis);
            }

            File.WriteAllText(backupPfad, "Backup-Daten hier...");
        }

        private void koppyForDebug()
        {
            string appBasePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string targetPath = Path.Combine(appBasePath, "Funktions", "sepain.txt");

            // Vereinfachung der Pfadmanipulation
            string sourcePath = Path.Combine(appBasePath, "Funktions", "sepain.txt");

            string targetDirectory = Path.GetDirectoryName(targetPath);
            if (!Directory.Exists(targetDirectory))
            {
                Directory.CreateDirectory(targetDirectory);
            }

            if (File.Exists(sourcePath) && !File.Exists(targetPath))
            {
                File.Copy(sourcePath, targetPath, overwrite: false);
            }
        }
    }
}
