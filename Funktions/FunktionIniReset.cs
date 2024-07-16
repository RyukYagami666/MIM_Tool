using App3.Funktions;
using IniParser.Model;
using IniParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace App3.Funktions
{
    class FunktionIniReset
    {
        private string iniFilePath;

        public FunktionIniReset(string filePath)
        {
            iniFilePath = filePath;
            MessageBox.Show(iniFilePath);
        }

        public void Initialisierungs()
        {
            FunktionIniPars iniPars = new FunktionIniPars();
            IniData iniData = iniPars.ReadIniFile();

            // Überprüfen, ob die Sektionen existieren
            bool monitorEinsExists = iniData.Sections.ContainsSection("Monitor Eins");
            bool einstellungExists = iniData.Sections.ContainsSection("Einstellung");

            // Überprüfen, ob die Schlüssel in den Sektionen existieren
            bool monitorNameExists = monitorEinsExists && iniData["Monitor Eins"].ContainsKey("Name");
            bool desktopOKInstallExists = einstellungExists && iniData["Einstellung"].ContainsKey("DesktopOKInstall");

            if (!monitorNameExists || !desktopOKInstallExists)
            {
                MessageBoxResult result = MessageBox.Show("Die INI-Datei existiert nicht oder ist unvollständig. Möchten Sie eine neue Datei erstellen?", "Datei nicht gefunden oder unvollständig", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    // Benutzer hat Ja gewählt, führe die Aktion aus
                    List<string> iconPaths1 = new List<string>();
                    List<string> iconPaths2 = new List<string>();
                    List<string> iconPaths3 = new List<string>();
                    WriteDataToIniFile(iniFilePath, iconPaths1, iconPaths2, iconPaths3);
                }
                else
                {
                    // Benutzer hat Nein gewählt, führe eine alternative Aktion aus oder beende
                    MessageBox.Show("Vorgang abgebrochen.");
                }
            }
            else
            {
                MessageBox.Show("INI-Datei erfolgreich geladen.");
            }
        }

        public void WriteDataToIniFile(string filePath, List<string> iconPaths1, List<string> iconPaths2, List<string> iconPaths3)
        {
            try
            {
                // Erstellen eines IniData-Objekts
                var data = new IniData();

                // Hinzufügen von Sektionen und Schlüssel-Wert-Paaren
                AddMonitorSection(data, "Monitor Eins", iconPaths1, "MeinHauptMonitor", "true", "10");
                AddMonitorSection(data, "Monitor Zwei", iconPaths2, "MeinNebenMonitor", "false", "15");
                AddMonitorSection(data, "Monitor Drei", iconPaths3, "MeinXXXMonitor", "false", "3");

                // Einstellungen hinzufügen
                AddSettingsSection(data);

                // Erstellen des Parser-Objekts und Schreiben der Daten in die Datei
                var parser = new FileIniDataParser();
                parser.WriteFile(filePath, data);

                MessageBox.Show("INI-Datei wurde erfolgreich zurückgesetzt.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Schreiben in die INI-Datei: {ex.Message}");
            }
        }

        private void AddMonitorSection(IniData data, string sectionName, List<string> iconPaths, string alias, string isPrimary, string iconCount)
        {
            data.Sections.AddSection(sectionName);
            data[sectionName]["Name"] = "monitName";
            data[sectionName]["Alias"] = alias;
            data[sectionName]["Primary"] = isPrimary;
            data[sectionName]["Resolution"] = "1920x1080";
            data[sectionName]["Active"] = "true";
            data[sectionName]["Start"] = "false";
            data[sectionName]["IconsGewählt"] = "true";
            data[sectionName]["IconsZahl"] = iconCount;
            data[sectionName]["LastBackUp"] = "2024, 12, 31, 5, 10, 20";

            // Icons Sektion
            var iconsSection = new SectionData($"{sectionName} Icons");
            for (int i = 0; i < iconPaths.Count; i++)
            {
                iconsSection.Keys.AddKey($"Icon{i + 1}", iconPaths[i]);
            }
            data.Sections.Add(iconsSection);
        }

        private void AddSettingsSection(IniData data)
        {
            data.Sections.AddSection("Einstellung");
            data["Einstellung"]["Design"] = "dark";
            data["Einstellung"]["DesktopOKInstall"] = "true";
            data["Einstellung"]["DesktopOKPfad"] = "C:\\Users\\Ryuk\\Documents\\DSM_Files";
            data["Einstellung"]["DesktopOKStatus"] = "true";
            data["Einstellung"]["DesktopOKLastSave"] = "2024, 12, 31, 5, 10, 20";
        }
    }
}
