using System.Windows.Forms; 
using MIM_Tool.Helpers; 

namespace MIM_Tool.Funktions
{
    class FunktionAuswahlZiel
    {
        //--------------------------------------------------------------------------------Speichern der ausgewählten Icons für Monitor -----------------------------------------------------------------------------
        // Diese Methode wird aufgerufen, wenn der erste Monitor ausgewählt wird.
        public void MonitorGewählt1()                                                                                        
        {
            if (!string.IsNullOrEmpty(Properties.Settings.Default.eMonitorIconsZugewiesen1))                                // Überprüft, ob bereits Icons zugewiesen sind.
            {
                var result = CustomMSGBox.Show("Es sind schon Icons in Liste 1 zugewiesen! \nWählen Sie eine Option:", "Aktion wählen!", "Alle Überschreiben", "Einzeln hinzufügen", "Abbrechen");
                // Verarbeitet die Benutzerantwort.
                switch (result)
                {
                    case DialogResult.Yes:
                        Properties.Settings.Default.eMonitorIconsZugewiesen1 = Properties.Settings.Default.DeskIconPfadMTemp; // Setzt die neuen Icons.
                        Properties.Settings.Default.eMonitorIconsCount1 = Properties.Settings.Default.eMonitorIconsCountTemp; // Setzt die neue Icon-Anzahl.
                        MessageBox.Show("Alle Icons wurden überschrieben.");                                                  // Zeigt eine Bestätigungsmeldung an.
                        break;
                    case DialogResult.No:
                        int mNummer = 1;                                                                                      // Setzt die Monitor-Nummer.
                        MessageBox.Show("einzelne Icons werden hinzugefügt.");                                                      // Zeigt eine Bestätigungsmeldung an.
                        EinzelnesIconHinzufügen einzelnesIconHinzufügen = new EinzelnesIconHinzufügen();                      // Erstellt eine Instanz der Klasse EinzelnesIconHinzufügen.
                        einzelnesIconHinzufügen.Vergleich(mNummer);                                                           // Ruft die Methode Vergleich auf.
                        break;
                    case DialogResult.Cancel:
                        MessageBox.Show("Aktion abgebrochen.");                                                               // Zeigt eine Abbruchmeldung an.
                        break;
                }
            }
            else
            {
                Properties.Settings.Default.eMonitorIconsZugewiesen1 = Properties.Settings.Default.DeskIconPfadMTemp;         // Setzt die neuen Icons.
                Properties.Settings.Default.eMonitorIconsCount1 = Properties.Settings.Default.eMonitorIconsCountTemp;         // Setzt die neue Icon-Anzahl.
            }
            Properties.Settings.Default.DeskIconPfadMTemp = "";                                                               // Setzt den temporären Icon-Pfad zurück.
            Properties.Settings.Default.eMonitorIconsCountTemp = 0;                                                           // Setzt die temporäre Icon-Anzahl zurück.
            Properties.Settings.Default.Save();                                                                               // Speichert die Einstellungen.
        }

        // Diese Methode wird aufgerufen, wenn der zweite Monitor ausgewählt wird.-------------------------------------------------------------------------------
        public void MonitorGewählt2()
        {
            if (!string.IsNullOrEmpty(Properties.Settings.Default.eMonitorIconsZugewiesen2))
            {
                var result = CustomMSGBox.Show("Es sind schon Icons in Liste 2 zugewiesen! \nWählen Sie eine Option:", "Aktion wählen!", "Alle Überschreiben", "Einzeln hinzufügen", "Abbrechen");
                // Verarbeitet die Benutzerantwort.
                switch (result)
                {
                    case DialogResult.Yes:
                        Properties.Settings.Default.eMonitorIconsZugewiesen2 = Properties.Settings.Default.DeskIconPfadMTemp; // Setzt die neuen Icons.
                        Properties.Settings.Default.eMonitorIconsCount2 = Properties.Settings.Default.eMonitorIconsCountTemp; // Setzt die neue Icon-Anzahl.
                        MessageBox.Show("Alle Icons wurden überschrieben.");                                                  // Zeigt eine Bestätigungsmeldung an.
                        break;
                    case DialogResult.No:
                        int mNummer = 2;                                                                                      // Setzt die Monitor-Nummer.
                        MessageBox.Show("einzelne Icons werden hinzugefügt.");                                                      // Zeigt eine Bestätigungsmeldung an.
                        EinzelnesIconHinzufügen einzelnesIconHinzufügen = new EinzelnesIconHinzufügen();                      // Erstellt eine Instanz der Klasse EinzelnesIconHinzufügen.
                        einzelnesIconHinzufügen.Vergleich(mNummer);                                                           // Ruft die Methode Vergleich auf.
                        break;
                    case DialogResult.Cancel:
                        MessageBox.Show("Aktion abgebrochen.");                                                               // Zeigt eine Abbruchmeldung an.
                        break;
                }
            }
            else
            {
                Properties.Settings.Default.eMonitorIconsZugewiesen2 = Properties.Settings.Default.DeskIconPfadMTemp;         // Setzt die neuen Icons.
                Properties.Settings.Default.eMonitorIconsCount2 = Properties.Settings.Default.eMonitorIconsCountTemp;         // Setzt die neue Icon-Anzahl.
            }
            Properties.Settings.Default.DeskIconPfadMTemp = "";                                                               // Setzt den temporären Icon-Pfad zurück.
            Properties.Settings.Default.eMonitorIconsCountTemp = 0;                                                           // Setzt die temporäre Icon-Anzahl zurück.
            Properties.Settings.Default.Save();                                                                               // Speichert die Einstellungen.
        }

        // Diese Methode wird aufgerufen, wenn der dritte Monitor ausgewählt wird.-------------------------------------------------------------------------------
        public void MonitorGewählt3()
        {
            if (!string.IsNullOrEmpty(Properties.Settings.Default.eMonitorIconsZugewiesen3))
            {
                var result = CustomMSGBox.Show("Es sind schon Icons in Liste 3 zugewiesen! \nWählen Sie eine Option:", "Aktion wählen!", "Alle Überschreiben", "Einzeln hinzufügen", "Abbrechen");
                // Verarbeitet die Benutzerantwort.
                switch (result)
                {
                    case DialogResult.Yes:
                        Properties.Settings.Default.eMonitorIconsZugewiesen3 = Properties.Settings.Default.DeskIconPfadMTemp; // Setzt die neuen Icons.
                        Properties.Settings.Default.eMonitorIconsCount3 = Properties.Settings.Default.eMonitorIconsCountTemp; // Setzt die neue Icon-Anzahl.
                        MessageBox.Show("Alle Icons wurden überschrieben.");                                                  // Zeigt eine Bestätigungsmeldung an.
                        break;
                    case DialogResult.No:
                        int mNummer = 3;                                                                                      // Setzt die Monitor-Nummer.
                        MessageBox.Show("einzelne Icons werden hinzugefügt.");                                                      // Zeigt eine Bestätigungsmeldung an.
                        EinzelnesIconHinzufügen einzelnesIconHinzufügen = new EinzelnesIconHinzufügen();                      // Erstellt eine Instanz der Klasse EinzelnesIconHinzufügen.
                        einzelnesIconHinzufügen.Vergleich(mNummer);                                                           // Ruft die Methode Vergleich auf.
                        break;
                    case DialogResult.Cancel:
                        MessageBox.Show("Aktion abgebrochen.");                                                               // Zeigt eine Abbruchmeldung an.
                        break;
                }
            }
            else
            {
                Properties.Settings.Default.eMonitorIconsZugewiesen3 = Properties.Settings.Default.DeskIconPfadMTemp;         // Setzt die neuen Icons.
                Properties.Settings.Default.eMonitorIconsCount3 = Properties.Settings.Default.eMonitorIconsCountTemp;         // Setzt die neue Icon-Anzahl.
            }
            Properties.Settings.Default.DeskIconPfadMTemp = "";                                                               // Setzt den temporären Icon-Pfad zurück.
            Properties.Settings.Default.eMonitorIconsCountTemp = 0;                                                           // Setzt die temporäre Icon-Anzahl zurück.
            Properties.Settings.Default.Save();                                                                               // Speichert die Einstellungen.
        }

        // Diese Methode wird aufgerufen, wenn der vierte Monitor ausgewählt wird.-------------------------------------------------------------------------------
        public void MonitorGewählt4()
        {
            if (!string.IsNullOrEmpty(Properties.Settings.Default.eMonitorIconsZugewiesen4))
            {
                var result = CustomMSGBox.Show("Es sind schon Icons in Liste 4 zugewiesen! \nWählen Sie eine Option:", "Aktion wählen!", "Alle Überschreiben", "Einzeln hinzufügen", "Abbrechen");
                // Verarbeitet die Benutzerantwort.
                switch (result)
                {
                    case DialogResult.Yes:
                        Properties.Settings.Default.eMonitorIconsZugewiesen4 = Properties.Settings.Default.DeskIconPfadMTemp; // Setzt die neuen Icons.
                        Properties.Settings.Default.eMonitorIconsCount4 = Properties.Settings.Default.eMonitorIconsCountTemp; // Setzt die neue Icon-Anzahl.
                        MessageBox.Show("Alle Icons wurden überschrieben.");                                                  // Zeigt eine Bestätigungsmeldung an.
                        break;
                    case DialogResult.No:
                        int mNummer = 4;                                                                                      // Setzt die Monitor-Nummer.
                        MessageBox.Show("einzelne Icons werden hinzugefügt.");                                                      // Zeigt eine Bestätigungsmeldung an.
                        EinzelnesIconHinzufügen einzelnesIconHinzufügen = new EinzelnesIconHinzufügen();                      // Erstellt eine Instanz der Klasse EinzelnesIconHinzufügen.
                        einzelnesIconHinzufügen.Vergleich(mNummer);                                                           // Ruft die Methode Vergleich auf.
                        break;
                    case DialogResult.Cancel:
                        MessageBox.Show("Aktion abgebrochen.");                                                               // Zeigt eine Abbruchmeldung an.
                        break;
                }
            }
            else
            {
                Properties.Settings.Default.eMonitorIconsZugewiesen4 = Properties.Settings.Default.DeskIconPfadMTemp;         // Setzt die neuen Icons.
                Properties.Settings.Default.eMonitorIconsCount4 = Properties.Settings.Default.eMonitorIconsCountTemp;         // Setzt die neue Icon-Anzahl.
            }
            Properties.Settings.Default.DeskIconPfadMTemp = "";                                                               // Setzt den temporären Icon-Pfad zurück.
            Properties.Settings.Default.eMonitorIconsCountTemp = 0;                                                           // Setzt die temporäre Icon-Anzahl zurück.
            Properties.Settings.Default.Save();                                                                               // Speichert die Einstellungen.
        }
        //--------------------------------------------------------------------------------Speichern und Vergleich einzelner liste -----------------------------------------------------------------------------
        // Diese Klasse enthält die Logik zum Hinzufügen einzelner Icons.
        public class EinzelnesIconHinzufügen
        {
            // Diese Methode vergleicht und fügt einzelne Icons hinzu.
            public void Vergleich(int mNummer)
            {
                String geWählteIcons = Properties.Settings.Default.DeskIconPfadMTemp;                                 // Temporäre Icons-Pfade.
                string[] geWählteIconsArray = geWählteIcons.Split(';');                                               // Teilt die Pfade in ein Array.
                string vorhandeneIcons = "";

                // Bestimmt die vorhandenen Icons basierend auf der Monitor-Nummer.
                switch (mNummer)
                {
                    case 1:
                        vorhandeneIcons = Properties.Settings.Default.eMonitorIconsZugewiesen1;
                        break;
                    case 2:
                        vorhandeneIcons = Properties.Settings.Default.eMonitorIconsZugewiesen2;
                        break;
                    case 3:
                        vorhandeneIcons = Properties.Settings.Default.eMonitorIconsZugewiesen3;
                        break;
                    case 4:
                        vorhandeneIcons = Properties.Settings.Default.eMonitorIconsZugewiesen4;
                        break;
                }

                // Überprüft jedes ausgewählte Icon, ob es bereits vorhanden ist.
                foreach (var icon in geWählteIconsArray)
                {
                    if (!string.IsNullOrEmpty(icon) && vorhandeneIcons.Contains(icon))
                    {
                        MessageBox.Show($"Fehler: Das Icon '{icon}' ist bereits zugewiesen.", "Übereinstimmung gefunden", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;                                                                                             // Beendet die Methode, wenn eine Übereinstimmung gefunden wurde
                    }
                    else
                    {
                        vorhandeneIcons = vorhandeneIcons + (string.IsNullOrEmpty(vorhandeneIcons) ? "" : ";") + icon;      // Hinzufügen des neuen Icons zum vorhandenen String, getrennt durch ";"
                        switch (mNummer)                                                                                    // Aktualisiert die Einstellungen basierend auf der Monitor-Nummer.
                        {
                            case 1:
                                Properties.Settings.Default.eMonitorIconsZugewiesen1 = vorhandeneIcons;
                                Properties.Settings.Default.eMonitorIconsCount1 += 1;
                                break;
                            case 2:
                                Properties.Settings.Default.eMonitorIconsZugewiesen2 = vorhandeneIcons;
                                Properties.Settings.Default.eMonitorIconsCount2 += 1;
                                break;
                            case 3:
                                Properties.Settings.Default.eMonitorIconsZugewiesen3 = vorhandeneIcons;
                                Properties.Settings.Default.eMonitorIconsCount3 += 1;
                                break;
                            case 4:
                                Properties.Settings.Default.eMonitorIconsZugewiesen4 = vorhandeneIcons;
                                Properties.Settings.Default.eMonitorIconsCount4 += 1;
                                break;
                        }
                    }
                }
            }
        }
    }
}
