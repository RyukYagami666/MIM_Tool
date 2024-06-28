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
                    WriteDataToIniFile("C:\\Konfigurationen\\Monitore.ini", iconPaths1, iconPaths2, iconPaths3);
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
            // Erstellen eines IniData-Objekts
            var data = new IniData();

            // Hinzufügen von Sektionen und Schlüssel-Wert-Paaren
            data.Sections.AddSection("Monitor Eins Kommentar");
            data["Monitor Eins Kommentar"]["Hinweins"] = "Kannst alles noch meine Gute, imMo mit Ruhe ";

            //Daten
            data.Sections.AddSection("Monitor Eins");
            data["Monitor Eins"]["Name"] = "monitName";
            data["Monitor Eins"]["Alias"] = "MeinHauptMonitor";
            data["Monitor Eins"]["Nummer"] = "monitNum1";
            data["Monitor Eins"]["Primary"] = "true";
            data["Monitor Eins"]["Resolution"] = "1920x1080";
            data["Monitor Eins"]["Active"] = "true";
            data["Monitor Eins"]["Start"] = "false";
            data["Monitor Eins"]["IconsGewählt"] = "true";
            data["Monitor Eins"]["IconsZahl"] = "10";
            data["Monitor Eins"]["LastBackUp"] = "2024, 12, 31, 5, 10, 20";

            // Monitor Eins Icons Sektion
            var iconsSection1 = new SectionData("Monitor Eins Icons");
            for (int i = 0; i < iconPaths1.Count; i++)
            {
                iconsSection1.Keys.AddKey($"Icon{i + 1}", iconPaths1[i]);
            }
            data.Sections.Add(iconsSection1);

            // Trennzeichen / Kommentar (optional)
            data.Sections.AddSection("Kommentar1");
            data["Kommentar1"].AddKey("Trennzeichen1", "----------------------------------------------------------------");


            //Block 2
            data.Sections.AddSection("Monitor zwei Kommentar");
            data["Monitor zwei Kommentar"]["Hinweins"] = "Kannst alles noch meine Gute, imMo mit Ruhe ";

            // Daten
            data.Sections.AddSection("Monitor Zwei");
            data["Monitor Zwei"]["Name"] = "monitName";
            data["Monitor Zwei"]["Alias"] = "MeinNebenMonitor";
            data["Monitor Zwei"]["Nummer"] = "monitNum2";
            data["Monitor Zwei"]["Primary"] = "false";
            data["Monitor Zwei"]["Resolution"] = "1920x1080";
            data["Monitor Zwei"]["Active"] = "true";
            data["Monitor Zwei"]["Start"] = "false";
            data["Monitor Zwei"]["IconsGewählt"] = "true";
            data["Monitor Zwei"]["IconsZahl"] = "15";
            data["Monitor Zwei"]["LastBackUp"] = "2024, 12, 31, 5, 10, 20";

            // Monitor Zwei Icons Sektion
            var iconsSection2 = new SectionData("Monitor Zwei Icons");
            for (int i = 0; i < iconPaths2.Count; i++)
            {
                iconsSection2.Keys.AddKey($"Icon{i + 1}", iconPaths2[i]);
            }
            data.Sections.Add(iconsSection2);

            // Trennzeichen / Kommentar (optional)
            data.Sections.AddSection("Kommentar2");
            data["Kommentar2"].AddKey("Trennzeichen2", "----------------------------------------------------------------");


            //Block 3 
            data.Sections.AddSection("Monitor Drei Kommentar");
            data["Monitor Drei Kommentar"]["Hinweins"] = "Kannst alles noch meine Gute, imMo mit Ruhe ";

            // Daten
            data.Sections.AddSection("Monitor Drei");
            data["Monitor Drei"]["Name"] = "monitName";
            data["Monitor Drei"]["Alias"] = "MeinXXXMonitor";
            data["Monitor Drei"]["Nummer"] = "monitNum3";
            data["Monitor Drei"]["Primary"] = "false";
            data["Monitor Drei"]["Resolution"] = "1920x1080";
            data["Monitor Drei"]["Active"] = "false";
            data["Monitor Drei"]["Start"] = "true";
            data["Monitor Drei"]["IconsGewählt"] = "true";
            data["Monitor Drei"]["IconsZahl"] = "3";
            data["Monitor Drei"]["LastBackUp"] = "2024, 12, 31, 5, 10, 20";

            // Monitor Drei Icons Sektion
            var iconsSection3 = new SectionData("Monitor Drei Icons");
            for (int i = 0; i < iconPaths3.Count; i++)
            {
                iconsSection3.Keys.AddKey($"Icon{i + 1}", iconPaths3[i]);
            }
            data.Sections.Add(iconsSection3);

            // Trennzeichen / Kommentar (optional)
            data.Sections.AddSection("Kommentar3");
            data["Kommentar3"].AddKey("Trennzeichen3", "----------------------------------------------------------------");




            //Block Einstellung
            data.Sections.AddSection("Einstellungs Kommentar");
            data["Einstellungs Kommentar"]["Hinweins"] = "Kannst alles noch meine Gute, imMo mit Ruhe ";
            data.Sections.AddSection("Einstellung");
            data["Einstellung"]["Design"] = "dark";
            data["Einstellung"]["DesktopOKInstall"] = "true";
            data["Einstellung"]["DesktopOKPfad"] = "C:\\Users\\Ryuk\\Documents\\DSM_Files";
            data["Einstellung"]["DesktopOKStatus"] = "true";
            data["Einstellung"]["DesktopOKLastSave"] = "2024, 12, 31, 5, 10, 20";




            // Erstellen des Parser-Objekts
            var parser = new FileIniDataParser();

            // Schreiben der Daten in die Datei
            parser.WriteFile(filePath, data);
        }


        public IniData ReadDataFromIniFile(string filePath)
        {
            var parser = new FileIniDataParser(); // Erstellt ein neues Parser-Objekt
            IniData data = parser.ReadFile(filePath); // Liest die INI-Datei und speichert die Daten in 'data'
            return data; // Gibt das 'data'-Objekt zurück
        }
    }
}
