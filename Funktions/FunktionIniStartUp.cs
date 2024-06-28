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
            koppyForDebug();
            SichereBackupDaten();
            // Weitere Initialisierungsaktionen hier
        }

        private void SichereBackupDaten()
        {

            string appBasePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string targetPath = Path.Combine(appBasePath, "Funktions", "sepain.txt");
            string iniPfad = File.ReadAllText(targetPath).Trim();
            string iniVerzeichnis = Path.GetDirectoryName(iniPfad);

            // Erstellen des Backup-Pfades durch Hinzufügen eines Unterordners zum INI-Pfad
            string backupPfad = Path.Combine(iniVerzeichnis, "Backups", "backup.txt");

            // Stellen Sie sicher, dass das Verzeichnis existiert
            string backupVerzeichnis = Path.GetDirectoryName(backupPfad);
            //CopyMSGBox.Show(backupVerzeichnis);
           if (!Directory.Exists(backupVerzeichnis))
           {
               Directory.CreateDirectory(backupVerzeichnis);                                                     //Backup-Verzeichnis erstellen
           }

            // Logik zum Sichern der Daten
            // Dies könnte das Schreiben von Daten in die Backup-Datei beinhalten
            File.WriteAllText(backupPfad, "Backup-Daten hier..."); // Beispiel-Inhalt
        }

        private void koppyForDebug()
        {
            string appBasePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string targetPath = Path.Combine(appBasePath, "Funktions", "sepain.txt");
            //string bearbeiteteQuelle;
            string[] worte = appBasePath.Split(new char[] {'\\'}, StringSplitOptions.RemoveEmptyEntries);//{ ' ', '\\', ':' }
            for (int i = 0; i < worte.Length-3; i++)
            {
                if (i == 0)
                {
                    worte[0] = worte[0] + "\\";
                }
                else
                {
                    worte[0] = worte[0] + worte[i] + "\\";
                }
            }
            string sourcePath = Path.Combine(worte[0], "Funktions", "sepain.txt");

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
