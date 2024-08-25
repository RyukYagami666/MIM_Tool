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
            Log.inf("Ausgewählte Icons wurden zum Monitor 1 zugewiesen Start des Speicherns in Liste ");
            if (!string.IsNullOrEmpty(Properties.Settings.Default.eMonitorIconsZugewiesen1))                                        // Überprüft, ob bereits Icons zugewiesen sind.
            {
                Log.inf("Die ausgewählte Iconliste 1 hat schon gespeicherte Icons starte userabfrage ");
                var result = CustomMSGBox.Show("Es sind schon Icons in Liste 1 zugewiesen! \nWählen Sie eine Option:", "Aktion wählen!", "Alle Überschreiben", "Einzeln hinzufügen", "Abbrechen");
                                                                                                                                    // Verarbeitet die Benutzerantwort.
                switch (result)
                {
                    case DialogResult.Yes:
                        Log.inf("Das Überschreiben der Liste 1 wurde ausgewählt und wird ausgeführt ");
                        Properties.Settings.Default.eMonitorIconsZugewiesen1 = Properties.Settings.Default.DeskIconPfadMTemp;       // Setzt die neuen Icons.
                        Properties.Settings.Default.eMonitorIconsCount1 = Properties.Settings.Default.eMonitorIconsCountTemp;       // Setzt die neue Icon-Anzahl.
                        Log.war("Alle Icons wurden überschrieben.",3000);                                                           // Zeigt eine Bestätigungsmeldung an.
                        break;
                    case DialogResult.No:
                        Log.inf("Das Hinzufügen einzelner Icons zur Liste 1 wurde ausgewählt und wird ausgeführt ");
                        int mNummer = 1;                                                                                            // Setzt die Monitor-Nummer.
                        EinzelnesIconHinzufügen einzelnesIconHinzufügen = new EinzelnesIconHinzufügen();                            // Erstellt eine Instanz der Klasse EinzelnesIconHinzufügen.
                        einzelnesIconHinzufügen.Vergleich(mNummer);                                                                 // Ruft die Methode Vergleich auf.
                        Log.war("Das einzelne Hinzufügen von Icons zu Liste 1 ist abgeschlossen ",4000);                            
                        break;
                    case DialogResult.Cancel:
                        Log.war("Die Aktion wurde abgebrochen.");                                                                   // Zeigt eine Abbruchmeldung an.
                        break;
                }
            }
            else
            {
                Properties.Settings.Default.eMonitorIconsZugewiesen1 = Properties.Settings.Default.DeskIconPfadMTemp;               // Setzt die neuen Icons.
                Properties.Settings.Default.eMonitorIconsCount1 = Properties.Settings.Default.eMonitorIconsCountTemp;               // Setzt die neue Icon-Anzahl.
                Log.inf("Schreiben der icons zur liste 1, ohne Probleme erfolgreich"); 
            }
            Properties.Settings.Default.DeskIconPfadMTemp = "";                                                                     // Setzt den temporären Icon-Pfad zurück.
            Properties.Settings.Default.eMonitorIconsCountTemp = 0;                                                                 // Setzt die temporäre Icon-Anzahl zurück.
            Properties.Settings.Default.Save();                                                                                     // Speichert die Einstellungen.
            Log.inf("Speichern abgeschlossen, Auswahl zurückgesetzt und gespeichert ");
        }

        // Diese Methode wird aufgerufen, wenn der zweite Monitor ausgewählt wird.-------------------------------------------------------------------------------
        public void MonitorGewählt2()
        {
            Log.inf("Ausgewählte Icons morgen zum Monitor 2 zugewiesen Start des Speicherns in Liste ");
            if (!string.IsNullOrEmpty(Properties.Settings.Default.eMonitorIconsZugewiesen2))
            {
                Log.inf("Die ausgewählte Iconliste 2 hat schon gespeicherte Icons starte userabfrage ");
                var result = CustomMSGBox.Show("Es sind schon Icons in Liste 2 zugewiesen! \nWählen Sie eine Option:", "Aktion wählen!", "Alle Überschreiben", "Einzeln hinzufügen", "Abbrechen");
                                                                                                                                    // Verarbeitet die Benutzerantwort.
                switch (result)
                {
                    case DialogResult.Yes:
                        Log.inf("Das Überschreiben der Liste 2 wurde ausgewählt und wird ausgeführt ");
                        Properties.Settings.Default.eMonitorIconsZugewiesen2 = Properties.Settings.Default.DeskIconPfadMTemp;       // Setzt die neuen Icons.
                        Properties.Settings.Default.eMonitorIconsCount2 = Properties.Settings.Default.eMonitorIconsCountTemp;       // Setzt die neue Icon-Anzahl.
                        Log.war("Alle Icons wurden überschrieben.", 3000);                                                          // Zeigt eine Bestätigungsmeldung an.
                        break;
                    case DialogResult.No:
                        Log.inf("Das Hinzufügen einzelner Icons zur Liste 2 wurde ausgewählt und wird ausgeführt ");
                        int mNummer = 2;                                                                                            // Setzt die Monitor-Nummer.
                        EinzelnesIconHinzufügen einzelnesIconHinzufügen = new EinzelnesIconHinzufügen();                            // Erstellt eine Instanz der Klasse EinzelnesIconHinzufügen.
                        einzelnesIconHinzufügen.Vergleich(mNummer);                                                                 // Ruft die Methode Vergleich auf.
                        Log.war("Das einzelne Hinzufügen von Icons zu Liste 2 ist abgeschlossen ", 4000);                           
                        break;
                    case DialogResult.Cancel:
                        Log.war("Die Aktion wurde abgebrochen.");                                                                   // Zeigt eine Abbruchmeldung an.
                        break;
                }
            }
            else
            {
                Properties.Settings.Default.eMonitorIconsZugewiesen2 = Properties.Settings.Default.DeskIconPfadMTemp;               // Setzt die neuen Icons.
                Properties.Settings.Default.eMonitorIconsCount2 = Properties.Settings.Default.eMonitorIconsCountTemp;               // Setzt die neue Icon-Anzahl.
                Log.inf("Schreiben der icons zur liste 2, ohne Probleme erfolgreich");
            }
            Properties.Settings.Default.DeskIconPfadMTemp = "";                                                                     // Setzt den temporären Icon-Pfad zurück.
            Properties.Settings.Default.eMonitorIconsCountTemp = 0;                                                                 // Setzt die temporäre Icon-Anzahl zurück.
            Properties.Settings.Default.Save();                                                                                     // Speichert die Einstellungen.
            Log.inf("Speichern abgeschlossen, Auswahl zurückgesetzt und gespeichert ");
        }

        //.------------------------------------------------------------------------------- Diese Methode wird aufgerufen, wenn der dritte Monitor ausgewählt wird
        public void MonitorGewählt3()
        {
            Log.inf("Ausgewählte Icons morgen zum Monitor 3 zugewiesen Start des Speicherns in Liste ");
            if (!string.IsNullOrEmpty(Properties.Settings.Default.eMonitorIconsZugewiesen3))
            {
                Log.inf("Die ausgewählte Iconliste 3 hat schon gespeicherte Icons starte userabfrage ");
                var result = CustomMSGBox.Show("Es sind schon Icons in Liste 3 zugewiesen! \nWählen Sie eine Option:", "Aktion wählen!", "Alle Überschreiben", "Einzeln hinzufügen", "Abbrechen");
                                                                                                                                    // Verarbeitet die Benutzerantwort.
                switch (result)
                {
                    case DialogResult.Yes:
                        Log.inf("Das Überschreiben der Liste 3 wurde ausgewählt und wird ausgeführt ");
                        Properties.Settings.Default.eMonitorIconsZugewiesen3 = Properties.Settings.Default.DeskIconPfadMTemp;       // Setzt die neuen Icons.
                        Properties.Settings.Default.eMonitorIconsCount3 = Properties.Settings.Default.eMonitorIconsCountTemp;       // Setzt die neue Icon-Anzahl.
                        Log.war("Alle Icons wurden überschrieben.", 3000);                                                          // Zeigt eine Bestätigungsmeldung an.
                        break;
                    case DialogResult.No:
                        Log.inf("Das Hinzufügen einzelner Icons zur Liste 3 wurde ausgewählt und wird ausgeführt ");
                        int mNummer = 3;                                                                                            // Setzt die Monitor-Nummer.
                        EinzelnesIconHinzufügen einzelnesIconHinzufügen = new EinzelnesIconHinzufügen();                            // Erstellt eine Instanz der Klasse EinzelnesIconHinzufügen.
                        einzelnesIconHinzufügen.Vergleich(mNummer);                                                                 // Ruft die Methode Vergleich auf.
                        Log.war("Das einzelne Hinzufügen von Icons zu Liste 3 ist abgeschlossen ", 4000);                           
                        break;
                    case DialogResult.Cancel:
                        Log.war("Die Aktion wurde abgebrochen.");                                                                   // Zeigt eine Abbruchmeldung an.
                        break;
                }
            }
            else
            {
                Properties.Settings.Default.eMonitorIconsZugewiesen3 = Properties.Settings.Default.DeskIconPfadMTemp;               // Setzt die neuen Icons.
                Properties.Settings.Default.eMonitorIconsCount3 = Properties.Settings.Default.eMonitorIconsCountTemp;               // Setzt die neue Icon-Anzahl.
                Log.inf("Schreiben der icons zur liste 3, ohne Probleme erfolgreich");
            }
            Properties.Settings.Default.DeskIconPfadMTemp = "";                                                                     // Setzt den temporären Icon-Pfad zurück.
            Properties.Settings.Default.eMonitorIconsCountTemp = 0;                                                                 // Setzt die temporäre Icon-Anzahl zurück.
            Properties.Settings.Default.Save();                                                                                     // Speichert die Einstellungen.
            Log.inf("Speichern abgeschlossen, Auswahl zurückgesetzt und gespeichert ");
        }
        //.------------------------------------------------------------------------------- Diese Methode wird aufgerufen, wenn der vierte Monitor ausgewählt wird------------------------------------
        public void MonitorGewählt4()
        {
            Log.inf("Ausgewählte Icons morgen zum Monitor 4 zugewiesen Start des Speicherns in Liste ");
            if (!string.IsNullOrEmpty(Properties.Settings.Default.eMonitorIconsZugewiesen4))
            {
                Log.inf("Die ausgewählte Iconliste 4 hat schon gespeicherte Icons starte userabfrage ");
                var result = CustomMSGBox.Show("Es sind schon Icons in Liste 4 zugewiesen! \nWählen Sie eine Option:", "Aktion wählen!", "Alle Überschreiben", "Einzeln hinzufügen", "Abbrechen");
                                                                                                                                    // Verarbeitet die Benutzerantwort.
                switch (result)
                {
                    case DialogResult.Yes:
                        Log.inf("Das Überschreiben der Liste 4 wurde ausgewählt und wird ausgeführt ");
                        Properties.Settings.Default.eMonitorIconsZugewiesen4 = Properties.Settings.Default.DeskIconPfadMTemp;       // Setzt die neuen Icons.
                        Properties.Settings.Default.eMonitorIconsCount4 = Properties.Settings.Default.eMonitorIconsCountTemp;       // Setzt die neue Icon-Anzahl.
                        Log.war("Alle Icons wurden überschrieben.", 3000);                                                          // Zeigt eine Bestätigungsmeldung an.
                        break;
                    case DialogResult.No:
                        Log.inf("Das Hinzufügen einzelner Icons zur Liste 4 wurde ausgewählt und wird ausgeführt ");
                        int mNummer = 4;                                                                                            // Setzt die Monitor-Nummer.
                        EinzelnesIconHinzufügen einzelnesIconHinzufügen = new EinzelnesIconHinzufügen();                            // Erstellt eine Instanz der Klasse EinzelnesIconHinzufügen.
                        einzelnesIconHinzufügen.Vergleich(mNummer);                                                                 // Ruft die Methode Vergleich auf.
                        Log.war("Das einzelne Hinzufügen von Icons zu Liste 4 ist abgeschlossen ", 4000);                           
                        break;
                    case DialogResult.Cancel:
                        Log.war("Die Aktion wurde abgebrochen.");                                                                   // Zeigt eine Abbruchmeldung an.
                        break;
                }
            }
            else
            {
                Properties.Settings.Default.eMonitorIconsZugewiesen4 = Properties.Settings.Default.DeskIconPfadMTemp;               // Setzt die neuen Icons.
                Properties.Settings.Default.eMonitorIconsCount4 = Properties.Settings.Default.eMonitorIconsCountTemp;               // Setzt die neue Icon-Anzahl.
                Log.inf("Schreiben der icons zur liste 4, ohne Probleme erfolgreich");
            }
            Properties.Settings.Default.DeskIconPfadMTemp = "";                                                                     // Setzt den temporären Icon-Pfad zurück.
            Properties.Settings.Default.eMonitorIconsCountTemp = 0;                                                                 // Setzt die temporäre Icon-Anzahl zurück.
            Properties.Settings.Default.Save();                                                                                     // Speichert die Einstellungen.
            Log.inf("Speichern abgeschlossen, Auswahl zurückgesetzt und gespeichert ");
        }
        //--------------------------------------------------------------------------------Speichern und Vergleich einzelner liste -----------------------------------------------------------------------------
        // Diese Klasse enthält die Logik zum Hinzufügen einzelner Icons.
        public class EinzelnesIconHinzufügen
        {
                                                                                                                            // Diese Methode vergleicht und fügt einzelne Icons hinzu.
            public void Vergleich(int mNummer)
            {
                Log.inf("Einzeln hinzufügen wurde aktiviert, somit müssen alle gewählten Icons, mit der gespeicherten IconsListe verglichen werden ");
                String geWählteIcons = Properties.Settings.Default.DeskIconPfadMTemp;                                       // Temporäre Icons-Pfade.
                string[] geWählteIconsArray = geWählteIcons.Split(';');                                                     // Teilt die Pfade in ein Array.
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
                Log.inf("Listen wurden zum Vergleichen vorbereitet dies Passiert im nächsten Schritt.");

                                                                                                                            // Überprüft jedes ausgewählte Icon, ob es bereits vorhanden ist.
                foreach (var icon in geWählteIconsArray)
                {
                    if (!string.IsNullOrEmpty(icon) && vorhandeneIcons.Contains(icon))
                    {
                        Log.war($"Das ausgewählte Icon: \n{icon} \nIst bereits zur Liste hinzugefügt wurden.", 4000);      // Beendet die Methode, wenn eine Übereinstimmung gefunden wurde
                    }
                    else
                    {
                        Log.inf($"Ausgewählte Icon: {icon} \nist nicht in Liste vorhanden, also kann es Gespeichert werden.");
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
                Log.inf("Vergleichen und Speichern abgeschlossen");
            }
        }
    }
}
