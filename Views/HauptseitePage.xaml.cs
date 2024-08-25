using MIM_Tool.Funktions;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using MIM_Tool.Helpers;

namespace MIM_Tool.Views;   //-----------------------------------------------------------------Inizialisiert die Hauptseite/Anwendung--------------------------------------------------------------------------------
public partial class HauptseitePage : Page, INotifyPropertyChanged
    {
    public string[] monitorData1;                                                     // Array zur Speicherung von Monitorinformationen für Monitor 1
    public string[] monitorData2;                                                     // Array zur Speicherung von Monitorinformationen für Monitor 2
    public string[] monitorData3;                                                     // Array zur Speicherung von Monitorinformationen für Monitor 3
    public string[] monitorData4;                                                     // Array zur Speicherung von Monitorinformationen für Monitor 4
    private const double ScaleFactor = 0.05;                                          // Skalierungsfaktor für die Darstellung der Monitore

    public HauptseitePage()
    {
        InitializeComponent();                                                        // Initialisiert die Komponenten der Seite
        DataContext = this;                                                           // Setzt den DataContext der Seite auf sich selbst
        Log.inf("HauptseitePage initialisiert.");
        this.Loaded += HauptPage_Loaded;                                              // Event-Handler für das Loaded-Ereignis der Seite
    }
    public event PropertyChangedEventHandler PropertyChanged;                         // Event für die Benachrichtigung über Änderungen an Eigenschaften

    private void HauptPage_Loaded(object sender, RoutedEventArgs e)
    {
        Log.inf("HauptseitePage geladen.");
        if (!Properties.Settings.Default.Inizialisiert)                               // Überprüft, ob die Anwendung initialisiert wurde
        {
            Log.war("Anwendung nicht initialisiert. Initialisierung wird durchgeführt.");
            var inizialisiert = new Funktion1Initialisieren();                        // Erstellt eine Instanz der Initialisierungsfunktion
            inizialisiert.Initialisieren();                                           // Führt die Initialisierung durch
            Log.inf("Initialisierung abgeschlossen.");
            return;                                                                   // Beendet die Methode, wenn die Initialisierung nicht abgeschlossen ist
        }
        else if (Properties.Settings.Default.DatenLesenAktiv)                         // Überprüft, ob das Lesen der Daten aktiv ist
        {
            Log.err("DatenLesenAktiv ist aktiv. aber ohne Prozess, Daten Lesen neu Starten.",null,true);
            var datenLesen = new Funktion2DatenLesen();                               // Erstellt eine Instanz der Datenlesungsfunktion
            datenLesen.DatenLesen();                                                  // Liest die Daten
            Log.inf("Daten geladen.");
            return;
        }

        Log.inf("Anwendung bereits initialisiert.");
        LoadInfoMonitor();                                                            // Lädt die Monitorinformationen
        Log.inf("Monitorinformationen geladen.");
        MonitorGröße();                                                               // Setzt die Größe der Monitore
        Log.inf("Monitorgrößen gesetzt.");
        MultiMonPos();                                                                // Setzt die Position der Monitore
        Log.inf("Monitorpositionen gesetzt.");

        // Setzt die Sichtbarkeit der Monitore basierend auf den Einstellungen
        Moni1.Visibility = Properties.Settings.Default.eMonitorVorhanden1 ? Visibility.Visible : Visibility.Collapsed;
        Moni2.Visibility = Properties.Settings.Default.eMonitorVorhanden2 ? Visibility.Visible : Visibility.Collapsed;
        Moni3.Visibility = Properties.Settings.Default.eMonitorVorhanden3 ? Visibility.Visible : Visibility.Collapsed;
        Moni4.Visibility = Properties.Settings.Default.eMonitorVorhanden4 ? Visibility.Visible : Visibility.Collapsed;
        Log.inf("Monitor-Sichtbarkeit basierend auf den Einstellungen gesetzt.");
        var transparentGray = new SolidColorBrush(Color.FromArgb(30, 10, 10, 10));                              // Definiert transparente Pinsel für die Hintergrundfarbe der Monitore 50% Transparenz grau
        var transparentDarkGray = new SolidColorBrush(Color.FromArgb(200, 10, 10, 10));                         // Definiert transparente Pinsel für die Hintergrundfarbe der Monitore 50% Transparenz dunkelgrau
        
        // Setzt die Hintergrundfarbe der Monitore basierend auf den Einstellungen
        Moni1.Background = Properties.Settings.Default.eMonitorAktiv1 ? transparentGray : transparentDarkGray;
        Moni2.Background = Properties.Settings.Default.eMonitorAktiv2 ? transparentGray : transparentDarkGray;
        Moni3.Background = Properties.Settings.Default.eMonitorAktiv3 ? transparentGray : transparentDarkGray;
        Moni4.Background = Properties.Settings.Default.eMonitorAktiv4 ? transparentGray : transparentDarkGray;
        Log.inf("Monitor-Hintergrundfarben basierend auf den Einstellungen gesetzt.");
    }

    private void Set<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
    {
        Log.inf($"Set-Methode aufgerufen für Eigenschaft: {propertyName}");
        if (Equals(storage, value))                                                                             // Überprüft, ob der neue Wert gleich dem alten Wert ist
        {
            Log.inf($"Der neue Wert für {propertyName} ist gleich dem alten Wert. Keine Änderung vorgenommen.");
            return;                                                                                             // Beendet die Methode, wenn die Werte gleich sind
        }
        Log.inf($"Der neue Wert für {propertyName} unterscheidet sich vom alten Wert. Wert wird aktualisiert.");
        storage = value;                                                                                        // Setzt den neuen Wert
        OnPropertyChanged(propertyName);                                                                        // Benachrichtigt über die Änderung der Eigenschaft
        Log.inf($"Eigenschaft {propertyName} wurde aktualisiert und PropertyChanged-Event ausgelöst.");
    }

    public void LoadInfoMonitor()                                                                               // Lädt die Monitorinformationen aus den Einstellungen und speichert sie in den Arrays
    {
        Log.inf("Lade Monitorinformationen.");
        string infoMonitor1 = Properties.Settings.Default.InfoMonitor1;
        monitorData1 = infoMonitor1.Split(';');
        Log.inf("Monitorinformationen für Monitor 1 geladen.");

        string infoMonitor2 = Properties.Settings.Default.InfoMonitor2;
        monitorData2 = infoMonitor2.Split(';');
        Log.inf("Monitorinformationen für Monitor 2 geladen.");

        string infoMonitor3 = Properties.Settings.Default.InfoMonitor3;
        monitorData3 = infoMonitor3.Split(';');
        Log.inf("Monitorinformationen für Monitor 3 geladen.");

        string infoMonitor4 = Properties.Settings.Default.InfoMonitor4;
        monitorData4 = infoMonitor4.Split(';');
        Log.inf("Monitorinformationen für Monitor 4 geladen.");
    }

    //----------------------------------------------------------------Definiert eine Methode zur Positionierung der Monitore-----------------------------------------------------------------

    public void MultiMonPos()
    {
        Log.inf("Starte MultiMonPos Methode.");
        Point[] positions = new Point[4];                                                                       // Array zur Speicherung der Positionen der Monitore
        if (monitorData1.Length > 1)                                                                            // Überprüft, ob Monitor 1 Daten hat
        {
            var positionParts1 = monitorData1[7].Split(',');                                                    // Teilt die Positionsdaten von Monitor 1
            positions[0] = new Point(int.Parse(positionParts1[0].Trim()), int.Parse(positionParts1[1].Trim())); // Erstellt einen Punkt mit den Positionsdaten
        }
        if (monitorData2.Length > 1)                                                                            // Überprüft, ob Monitor 2 Daten hat
        {
            var positionParts2 = monitorData2[7].Split(',');                                                    // Teilt die Positionsdaten von Monitor 2
            positions[1] = new Point(int.Parse(positionParts2[0].Trim()), int.Parse(positionParts2[1].Trim())); // Erstellt einen Punkt mit den Positionsdaten
        }
        if (monitorData3.Length > 1)                                                                            // Überprüft, ob Monitor 3 Daten hat
        {
            var positionParts3 = monitorData3[7].Split(',');                                                    // Teilt die Positionsdaten von Monitor 3
            positions[2] = new Point(int.Parse(positionParts3[0].Trim()), int.Parse(positionParts3[1].Trim())); // Erstellt einen Punkt mit den Positionsdaten
        }
        if (monitorData4.Length > 1)                                                                            // Überprüft, ob Monitor 4 Daten hat
        {
            var positionParts4 = monitorData4[7].Split(',');                                                    // Teilt die Positionsdaten von Monitor 4
            positions[3] = new Point(int.Parse(positionParts4[0].Trim()), int.Parse(positionParts4[1].Trim())); // Erstellt einen Punkt mit den Positionsdaten
        }
        Log.inf("Positionen der Monitore extrahiert.");
        Point[] positionsM = new Point[4];                                                                                                                                                     // Array zur Speicherung der modifizierten Positionen der Monitore
        if (monitorData1.Length > 1)                                                                                                                                                           // Überprüft, ob Monitor 1 Daten hat
        {
            var positionParts1 = monitorData1[7].Split(',');                                                                                                                                   // Teilt die Positionsdaten von Monitor 1
            var positionPartsR1 = monitorData1[5].Split('X');                                                                                                                                  // Teilt die Auflösungsdaten von Monitor 1
            positionsM[0] = new Point(int.Parse(positionParts1[0].Trim()) + int.Parse(positionPartsR1[0].Trim()), int.Parse(positionParts1[1].Trim()) + int.Parse(positionPartsR1[1].Trim())); // Erstellt einen Punkt mit den modifizierten Positionsdaten
        }
        if (monitorData2.Length > 1)                                                                                                                                                           // Überprüft, ob Monitor 2 Daten hat
        {
            var positionParts2 = monitorData2[7].Split(',');                                                                                                                                   // Teilt die Positionsdaten von Monitor 2
            var positionPartsR2 = monitorData2[5].Split('X');                                                                                                                                  // Teilt die Auflösungsdaten von Monitor 2
            positionsM[1] = new Point(int.Parse(positionParts2[0].Trim()) + int.Parse(positionPartsR2[0].Trim()), int.Parse(positionParts2[1].Trim()) + int.Parse(positionPartsR2[1].Trim())); // Erstellt einen Punkt mit den modifizierten Positionsdaten
        }
        if (monitorData3.Length > 1)                                                                                                                                                           // Überprüft, ob Monitor 3 Daten hat
        {
            var positionParts3 = monitorData3[7].Split(',');                                                                                                                                   // Teilt die Positionsdaten von Monitor 3
            var positionPartsR3 = monitorData3[5].Split('X');                                                                                                                                  // Teilt die Auflösungsdaten von Monitor 3
            positionsM[2] = new Point(int.Parse(positionParts3[0].Trim()) + int.Parse(positionPartsR3[0].Trim()), int.Parse(positionParts3[1].Trim()) + int.Parse(positionPartsR3[1].Trim())); // Erstellt einen Punkt mit den modifizierten Positionsdaten
        }
        if (monitorData4.Length > 1)                                                                                                                                                           // Überprüft, ob Monitor 4 Daten hat
        {
            var positionParts4 = monitorData4[7].Split(',');                                                                                                                                   // Teilt die Positionsdaten von Monitor 4
            var positionPartsR4 = monitorData4[5].Split('X');                                                                                                                                  // Teilt die Auflösungsdaten von Monitor 4
            positionsM[3] = new Point(int.Parse(positionParts4[0].Trim()) + int.Parse(positionPartsR4[0].Trim()), int.Parse(positionParts4[1].Trim()) + int.Parse(positionPartsR4[1].Trim())); // Erstellt einen Punkt mit den modifizierten Positionsdaten
        }
        Log.inf("Modifizierte Positionen der Monitore berechnet.");
        double minX = positions.Min(p => p.X);                                        // Findet den minimalen X-Wert der Positionen
        double maxX = positionsM.Max(p => p.X);                                       // Findet den maximalen X-Wert der modifizierten Positionen
        double minY = positions.Min(p => p.Y);                                        // Findet den minimalen Y-Wert der Positionen
        double maxY = positionsM.Max(p => p.Y);                                       // Findet den maximalen Y-Wert der modifizierten Positionen
        Log.inf("Minimale und maximale Werte der Positionen berechnet.");
        for (int i = 0; i < positions.Length; i++)                                    // Normalisiert die Positionen auf einen Bereich von 0 bis 1
        {
            positions[i] = new Point(
                (positions[i].X - minX) / (maxX - minX),
                (positions[i].Y - minY) / (maxY - minY)
            );
            Log.inf($"Gerechnete Position: {positions[i]}");
        }
        Log.inf("Positionen normalisiert.");
        double scaleX;                                                                // Variable zur Speicherung des Skalierungsfaktors für X
        double scaleY;                                                                // Variable zur Speicherung des Skalierungsfaktors für Y

        Log.inf("Skalierungsfaktoren berechnet.");
        if (minX < 0) scaleX = (maxX + Math.Abs(minX)) * ScaleFactor;                 // Berechnet den Skalierungsfaktor für X, wenn minX negativ ist
        else scaleX = maxX * ScaleFactor;                                             // Berechnet den Skalierungsfaktor für X, wenn minX nicht negativ ist
        if (minY < 0) scaleY = (maxY + Math.Abs(minY)) * ScaleFactor;                 // Berechnet den Skalierungsfaktor für Y, wenn minY negativ ist
        else scaleY = maxY * ScaleFactor;                                             // Berechnet den Skalierungsfaktor für Y, wenn minY nicht negativ ist
        Log.inf("Skalierungsfaktoren berechnet.");
        for (int i = 0; i < positions.Length; i++)                                    // Skaliert die normalisierten Positionen
        {
            positions[i] = new Point(
                positions[i].X * scaleX,
                positions[i].Y * scaleY
            );
        }
        Log.inf("Positionen skaliert.");
        if (positions.Length > 0) Moni1.Margin = new Thickness(positions[0].X + 20, positions[0].Y + 20, 0, 0); // Setze die Margin-Eigenschaft der Monitore basierend auf den skalierten Werten für Monitor 1
        if (positions.Length > 1) Moni2.Margin = new Thickness(positions[1].X + 20, positions[1].Y + 20, 0, 0); // Setze die Margin-Eigenschaft der Monitore basierend auf den skalierten Werten für Monitor 2
        if (positions.Length > 2) Moni3.Margin = new Thickness(positions[2].X + 20, positions[2].Y + 20, 0, 0); // Setze die Margin-Eigenschaft der Monitore basierend auf den skalierten Werten für Monitor 3
        if (positions.Length > 3) Moni4.Margin = new Thickness(positions[3].X + 20, positions[3].Y + 20, 0, 0); // Setze die Margin-Eigenschaft der Monitore basierend auf den skalierten Werten für Monitor 4
        Log.inf("Margin-Eigenschaften der Monitore gesetzt.");
    }

    //----------------------------------------------------------------Definiert eine Methode zur Anpassung der Monitorgröße und -inhalte-----------------------------------------------------------------                                                                                                  
    public void MonitorGröße()
    {
        Log.inf("MonitorGröße Methode gestartet");
        var BlauerText = new SolidColorBrush(Color.FromArgb(250, 50, 50, 255));             // Erstellt eine SolidColorBrush mit einer bestimmten Farbe
        if (monitorData1.Length > 1)                                                        // Überprüft, ob monitorData1 mehr als ein Element enthält
        {
            Log.inf("Verarbeite monitorData1");
            var resolutionParts1 = monitorData1[5].Split('X');                              // Teilt die Auflösungsdaten in Breite und Höhe auf
            Moni1.Width = int.Parse(resolutionParts1[0].Trim()) * ScaleFactor;              // Setzt die Breite des Monitors unter Berücksichtigung des Skalierungsfaktors
            Moni1.Height = int.Parse(resolutionParts1[1].Trim()) * ScaleFactor;             // Setzt die Höhe des Monitors unter Berücksichtigung des Skalierungsfaktors
            Moni1.Content = CreateMonitorContent(monitorData1, BlauerText);                 // Setzt den Inhalt des Monitors mit den entsprechenden Daten
        }
        if (monitorData2.Length > 1)                                                        // Wiederholt die obigen Schritte für monitorData2
        {
            Log.inf("Verarbeite monitorData2");
            var resolutionParts2 = monitorData2[5].Split('X');                              // Teilt die Auflösungsdaten in Breite und Höhe auf
            Moni2.Width = int.Parse(resolutionParts2[0].Trim()) * ScaleFactor;              // Setzt die Breite des Monitors unter Berücksichtigung des Skalierungsfaktors
            Moni2.Height = int.Parse(resolutionParts2[1].Trim()) * ScaleFactor;             // Setzt die Höhe des Monitors unter Berücksichtigung des Skalierungsfaktors
            Moni2.Content = CreateMonitorContent(monitorData2, BlauerText);                 // Setzt den Inhalt des Monitors mit den entsprechenden Daten
        }
        if (monitorData3.Length > 1)                                                        // Wiederholt die obigen Schritte für monitorData3
        {
            Log.inf("Verarbeite monitorData3");
            var resolutionParts3 = monitorData3[5].Split('X');                              // Teilt die Auflösungsdaten in Breite und Höhe auf
            Moni3.Width = int.Parse(resolutionParts3[0].Trim()) * ScaleFactor;              // Setzt die Breite des Monitors unter Berücksichtigung des Skalierungsfaktors
            Moni3.Height = int.Parse(resolutionParts3[1].Trim()) * ScaleFactor;             // Setzt die Höhe des Monitors unter Berücksichtigung des Skalierungsfaktors
            Moni3.Content = CreateMonitorContent(monitorData3, BlauerText);                 // Setzt den Inhalt des Monitors mit den entsprechenden Daten
        }
        if (monitorData4.Length > 1)                                                        // Wiederholt die obigen Schritte für monitorData4
        {
            Log.inf("Verarbeite monitorData4");
            var resolutionParts4 = monitorData4[5].Split('X');                              // Teilt die Auflösungsdaten in Breite und Höhe auf
            Moni4.Width = int.Parse(resolutionParts4[0].Trim()) * ScaleFactor;              // Setzt die Breite des Monitors unter Berücksichtigung des Skalierungsfaktors
            Moni4.Height = int.Parse(resolutionParts4[1].Trim()) * ScaleFactor;             // Setzt die Höhe des Monitors unter Berücksichtigung des Skalierungsfaktors
            Moni4.Content = CreateMonitorContent(monitorData4, BlauerText);                 // Setzt den Inhalt des Monitors mit den entsprechenden Daten
        }
    }

    public TextBlock CreateMonitorContent(string[] monitorData, SolidColorBrush blauerText)                              // Definiert eine Methode zur Erstellung eines TextBlock-Inhalts für einen Monitor
    {
        Log.inf("Erstelle Monitorinhalt");
        if (monitorData[11] == "Yes")                                                                                    // Überprüft, ob der Monitor aktiv ist
        {
            var textBlock = new TextBlock                                                                                // Erstellt und gibt einen TextBlock zurück, wenn der Monitor aktiv ist
            {
                Inlines = {                                                                                              // Definiert die Inline-Elemente des TextBlocks
            new Run($"{monitorData[1]}\n") { FontWeight = FontWeights.Bold, Foreground =  blauerText },                  // Fügt einen Run mit dem Monitor-Namen hinzu, fett und in blauer Farbe
            new Run($"{monitorData[5]}\n") { FontWeight = FontWeights.Normal },                                          // Fügt einen Run mit der Monitor-Auflösung hinzu, normaler Schrift
            new Run($"{monitorData[7]}")   { FontWeight = FontWeights.Normal }                                           // Fügt einen Run mit zusätzlichen Monitor-Informationen hinzu, normaler Schrift
            }
            };
            Log.inf("Monitorinhalt für aktiven Monitor erstellt");
            return textBlock;
        }
        else
        {
            var textBlock = new TextBlock                                                                                // Erstellt und gibt einen TextBlock zurück, wenn der Monitor nicht aktiv ist
            {
                Inlines = {                                                                                              // Definiert die Inline-Elemente des TextBlocks
            new Run($"{monitorData[1]}\n") { FontWeight = FontWeights.Bold },                                            // Fügt einen Run mit dem Monitor-Namen hinzu, fett
            new Run($"{monitorData[5]}\n") { FontWeight = FontWeights.Normal },                                          // Fügt einen Run mit der Monitor-Auflösung hinzu, normaler Schrift
            new Run($"{monitorData[7]}")   { FontWeight = FontWeights.Normal }                                           // Fügt einen Run mit zusätzlichen Monitor-Informationen hinzu, normaler Schrift
            }
            };
            Log.inf("Monitorinhalt für inaktiven Monitor erstellt");
            return textBlock;
        }
    }


    //----------------------------------------------------------------Definiert eine Methode zum Schreiben von Textinformationen für einen ausgewählten Monitor-----------------------------------------------------------------

    public void TextSchreiben(int GewählterMonitor)                                  // Definiert eine Methode zum Schreiben von Textinformationen für einen ausgewählten Monitor
    {
        Log.inf("TextSchreiben Methode gestartet");
        if (GewählterMonitor == 10) GewählterMonitor = 0;
        string[][] monitorData =                                                     // Erstellt ein Array von Monitor-Datenarrays
        {
            monitorData1,
            monitorData2,
            monitorData3,
            monitorData4
        };
        bool[] aktiv =                                                               // Erstellt ein Array von Aktivitätsstatus für die Monitore
        {
            Properties.Settings.Default.eMonitorAktiv1,
            Properties.Settings.Default.eMonitorAktiv2,
            Properties.Settings.Default.eMonitorAktiv3,
            Properties.Settings.Default.eMonitorAktiv4
        };
        bool[] verstaut =                                                            // Erstellt ein Array von Aktivitätsstatus für die Monitore
        {
            Properties.Settings.Default.eMonitorIconsVerstaut1,
            Properties.Settings.Default.eMonitorIconsVerstaut2,
            Properties.Settings.Default.eMonitorIconsVerstaut3,
            Properties.Settings.Default.eMonitorIconsVerstaut4
        };
        string[] count =                                                             // Erstellt ein Array von Monitor-Icon-Zählern
        {
            Properties.Settings.Default.eMonitorIconsCount1.ToString(),
            Properties.Settings.Default.eMonitorIconsCount2.ToString(),
            Properties.Settings.Default.eMonitorIconsCount3.ToString(),
            Properties.Settings.Default.eMonitorIconsCount4.ToString()
        };
        Log.inf("Monitor-Daten und Einstellungen geladen");
        if (monitorData[GewählterMonitor].Length > 1)                         // Überprüft, ob das ausgewählte Monitor-Datenarray mehr als ein Element enthält
        {                                                                   // Setzt die Textfelder mit den entsprechenden Monitor-Daten
            t2a1.Text = monitorData[GewählterMonitor][1];                   //Schreiben des Monitor-Namens
            t3a3.Text = monitorData[GewählterMonitor][3];                   //Schreiben der Monitor-Nummer
            t4a5.Text = monitorData[GewählterMonitor][5];                   //Schreiben des Monitor-Auflösung
            t5a7.Text = monitorData[GewählterMonitor][7];                   //Schreiben des Monitor-Position
            t6a9.Text = aktiv[GewählterMonitor] ? "Aktiv" : "Inaktiv";      //Schreiben des Monitor-Aktivitätsstatus
            t7a11.Text = monitorData[GewählterMonitor][11];                 //Schreiben des Monitor-Status
            t8a13.Text = monitorData[GewählterMonitor][13];                 //Schreiben des Monitor-Frequenz
            t9a15.Text = monitorData[GewählterMonitor][15];                 //Schreiben des Maximalen Monitor-Auflösung
            t10a17.Text = monitorData[GewählterMonitor][17];                //Schreiben des Monitor-DeviceNameID
            t11a19.Text = monitorData[GewählterMonitor][19];                //Schreiben des Monitor-Seriennummer
            t12a21.Text = count[GewählterMonitor];                          // Setzt den Text von t12a21 auf die Anzahl der Monitor-Icons
            t12a22.Text = verstaut[GewählterMonitor] ? "Verstaut" : "" ;    //Schreiben des Monitor-DeviceName
        
            Log.inf("Textinformationen für den ausgewählten Monitor geschrieben");
            }
        Log.inf("TextSchreiben Methode beendet");
    }

                                                                                                                                      // Benachrichtigt die Benutzeroberfläche über eine Eigenschaftsänderung
    private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); // Löst das PropertyChanged-Ereignis aus

    private void Moni1_Click(object sender, System.Windows.RoutedEventArgs e)                                                         // Ereignishandler für den Klick auf Moni1
    {
        Log.inf("Moni1_Click Methode gestartet");
        SavedLists.ItemsSource = null;                                                              // Setzt die Datenquelle der gespeicherten Listen auf null
        Properties.Settings.Default.SelectetMonitor = 0;                                            // Setzt den ausgewählten Monitor auf 0
        Properties.Settings.Default.Save();                                                         // Speichert die geänderten Einstellungen
        TextSchreiben(Properties.Settings.Default.SelectetMonitor);                                 // Schreibt die Textinformationen für den ausgewählten Monitor
        List<FunktionIconListe.FileIconInfo> savedIcons = FunktionIconListe.gespeicherteIcons();    // Ruft die gespeicherten Icons ab
        if (savedIcons != null) SavedLists.ItemsSource = savedIcons;                                // Setzt die Datenquelle der gespeicherten Listen auf die abgerufenen Icons
        Thread.Sleep(300);                                                                          // Wartet 300 Millisekunden
        savedIcons = null;                                                                          // Setzt die Variable savedIcons auf null
        Log.inf("Moni1_Click Methode beendet");
    }
    private void Moni2_Click(object sender, System.Windows.RoutedEventArgs e)                        // Ereignishandler für den Klick auf Moni1
    {
        Log.inf("Moni2_Click Methode gestartet");
        SavedLists.ItemsSource = null;                                                               // Setzt die Datenquelle der gespeicherten Listen auf null
        Properties.Settings.Default.SelectetMonitor = 1;                                             // Setzt den ausgewählten Monitor auf 0
        Properties.Settings.Default.Save();                                                          // Speichert die geänderten Einstellungen
        TextSchreiben(Properties.Settings.Default.SelectetMonitor);                                  // Schreibt die Textinformationen für den ausgewählten Monitor
        List<FunktionIconListe.FileIconInfo> savedIcons = FunktionIconListe.gespeicherteIcons();     // Ruft die gespeicherten Icons ab
        if (savedIcons != null) SavedLists.ItemsSource = savedIcons;                                 // Setzt die Datenquelle der gespeicherten Listen auf die abgerufenen Icons
        Thread.Sleep(300);                                                                           // Wartet 300 Millisekunden
        savedIcons = null;                                                                           // Setzt die Variable savedIcons auf null
        Log.inf("Moni2_Click Methode beendet");
    }
    private void Moni3_Click(object sender, System.Windows.RoutedEventArgs e)                        // Ereignishandler für den Klick auf Moni1
    {
        Log.inf("Moni3_Click Methode gestartet");
        SavedLists.ItemsSource = null;                                                               // Setzt die Datenquelle der gespeicherten Listen auf null
        Properties.Settings.Default.SelectetMonitor = 2;                                             // Setzt den ausgewählten Monitor auf 0
        Properties.Settings.Default.Save();                                                          // Speichert die geänderten Einstellungen
        TextSchreiben(Properties.Settings.Default.SelectetMonitor);                                  // Schreibt die Textinformationen für den ausgewählten Monitor
        List<FunktionIconListe.FileIconInfo> savedIcons = FunktionIconListe.gespeicherteIcons();     // Ruft die gespeicherten Icons ab
        if (savedIcons != null) SavedLists.ItemsSource = savedIcons;                                 // Setzt die Datenquelle der gespeicherten Listen auf die abgerufenen Icons
        Thread.Sleep(300);                                                                           // Wartet 300 Millisekunden
        savedIcons = null;                                                                           // Setzt die Variable savedIcons auf null
        Log.inf("Moni3_Click Methode beendet");
    }
    private void Moni4_Click(object sender, System.Windows.RoutedEventArgs e)                        // Ereignishandler für den Klick auf Moni1
    {
        Log.inf("Moni4_Click Methode gestartet"); 
        SavedLists.ItemsSource = null;                                                               // Setzt die Datenquelle der gespeicherten Listen auf null
        Properties.Settings.Default.SelectetMonitor = 3;                                             // Setzt den ausgewählten Monitor auf 0
        Properties.Settings.Default.Save();                                                          // Speichert die geänderten Einstellungen
        TextSchreiben(Properties.Settings.Default.SelectetMonitor);                                  // Schreibt die Textinformationen für den ausgewählten Monitor
        List<FunktionIconListe.FileIconInfo> savedIcons = FunktionIconListe.gespeicherteIcons();     // Ruft die gespeicherten Icons ab
        if (savedIcons != null) SavedLists.ItemsSource = savedIcons;                                 // Setzt die Datenquelle der gespeicherten Listen auf die abgerufenen Icons
        Thread.Sleep(300);                                                                           // Wartet 300 Millisekunden
        savedIcons = null;                                                                           // Setzt die Variable savedIcons auf null
        Log.inf("Moni4_Click Methode beendet");
    }

    private void btnSaveIconPos_Click(object sender, RoutedEventArgs e)                          // Ereignishandler für den Klick auf die Schaltfläche zum Speichern der Icon-Positionen
    {
        Log.inf("Speichern der Icon-Positionen gestartet.");
        if (Properties.Settings.Default.SelectetMonitor < 4)                                     // Überprüft, ob ein gültiger Monitor ausgewählt ist
        {
            Log.inf("Gültiger Monitor ausgewählt.");
            this.NavigationService.Navigate(new IconSavePage());                                 // Navigiert zur IconSavePage
        }
        else
        {
            Log.war("Bitte wählen Sie erst Monitor aus.");                               // Zeigt eine Nachricht an, wenn kein Monitor ausgewählt ist
        }
        HauptPage_Loaded(this, new RoutedEventArgs());                                           // Lädt die Hauptseite neu
        TextSchreiben(Properties.Settings.Default.SelectetMonitor);
    }

    private void btnCreateShortCut_Click(object sender, RoutedEventArgs e)                       // Ereignishandler für den Klick auf die Schaltfläche zum Erstellen einer Verknüpfung
    {
        Log.inf("Erstellen einer Verknüpfung gestartet."); ;
        var verknüpfung = new FunktionVerknüpfung();                                             // Erstellt eine neue Instanz von FunktionVerknüpfung
        verknüpfung.VerknüpfungStart();                                                          // Startet die Erstellung der Verknüpfung
    }

    private void btnMonitorSwitch_Click(object sender, RoutedEventArgs e)                        // Ereignishandler für den Klick auf die Schaltfläche zum Umschalten des Monitors
    {
        Log.inf("Umschalten des Monitors gestartet.");
        string[][] monitorData =
        {
            monitorData1,                                                                            // Daten für Monitor 1
            monitorData2,                                                                            // Daten für Monitor 2
            monitorData3,                                                                            // Daten für Monitor 3
            monitorData4                                                                             // Daten für Monitor 4
        };
        bool[] aktiv =
        {
            Properties.Settings.Default.eMonitorAktiv1,
            Properties.Settings.Default.eMonitorAktiv2,
            Properties.Settings.Default.eMonitorAktiv3,
            Properties.Settings.Default.eMonitorAktiv4
        };
        Log.inf("Monitor-Daten und Aktiv-Status geladen.");
        if (Properties.Settings.Default.SelectetMonitor < 4)                                         // Überprüft, ob ein gültiger Monitor ausgewählt ist
        {
            Log.inf("Gültiger Monitor ausgewählt.");
            string pathExe = $"{Properties.Settings.Default.pfadDeskOK}\\MultiMonitorTool.exe";      // Pfad zur MultiMonitorTool.exe
            if (aktiv[Properties.Settings.Default.SelectetMonitor])                                  // Überprüft, ob der ausgewählte Monitor aktiv ist
            {
                Log.inf("Monitor ist aktiv. Deaktivieren wird gestartet.");
                FunktionMultiMonitor.MonitorDeaktivieren(pathExe, monitorData[Properties.Settings.Default.SelectetMonitor][17], Properties.Settings.Default.SelectetMonitor); // Deaktiviert den ausgewählten Monitor
            }
            else
            {
                Log.inf("Monitor ist nicht aktiv. Aktivieren wird gestartet.");
                FunktionMultiMonitor.MonitorAktivieren(pathExe, monitorData[Properties.Settings.Default.SelectetMonitor][17], Properties.Settings.Default.SelectetMonitor); // Aktiviert den ausgewählten Monitor
            }
        }
        else
        {
            Log.war("Kein gültiger Monitor ausgewählt.");                                                                                                                       // Zeigt eine Nachricht an, wenn kein Monitor ausgewählt ist
        }
        HauptPage_Loaded(this, new RoutedEventArgs());                                                                                                                          // Lädt die Hauptseite neu
        TextSchreiben(Properties.Settings.Default.SelectetMonitor);
    }

    private void btnIconsVerschieben_Click(object sender, RoutedEventArgs e)                                                                                                    // Ereignishandler für den Klick auf die Schaltfläche zum Verschieben der Icons
    {
        Log.inf("Verschieben der Icons gestartet.");
        string[] iconsZugewiesen =
        {
        Properties.Settings.Default.eMonitorIconsZugewiesen1,                                                                                                                   // Zugewiesene Icons für Monitor 1
        Properties.Settings.Default.eMonitorIconsZugewiesen2,                                                                                                                   // Zugewiesene Icons für Monitor 2
        Properties.Settings.Default.eMonitorIconsZugewiesen3,                                                                                                                   // Zugewiesene Icons für Monitor 3
        Properties.Settings.Default.eMonitorIconsZugewiesen4                                                                                                                    // Zugewiesene Icons für Monitor 4
        };
        bool[] iconsVerstaut =
        {
        Properties.Settings.Default.eMonitorIconsVerstaut1,                                                                                                                     // Status der Icons für Monitor 1
        Properties.Settings.Default.eMonitorIconsVerstaut2,                                                                                                                     // Status der Icons für Monitor 2
        Properties.Settings.Default.eMonitorIconsVerstaut3,                                                                                                                     // Status der Icons für Monitor 3
        Properties.Settings.Default.eMonitorIconsVerstaut4                                                                                                                      // Status der Icons für Monitor 4
        };
        Log.inf("Icon-Zuweisungen und Verstau-Status geladen.");
        if (Properties.Settings.Default.SelectetMonitor < 4)                                                                                                                    // Überprüft, ob ein gültiger Monitor ausgewählt ist
        {
            Log.inf("Gültiger Monitor ausgewählt.");
            if (!string.IsNullOrEmpty(iconsZugewiesen[Properties.Settings.Default.SelectetMonitor]) && iconsVerstaut[Properties.Settings.Default.SelectetMonitor] == false)     // Überprüft, ob Icons zugewiesen und nicht verstaut sind
            {
                var moveIcons = new FunktionVerschieben();                                                                                                                      // Erstellt eine neue Instanz von FunktionVerschieben
                moveIcons.MoveDeskToPath(Properties.Settings.Default.SelectetMonitor);                                                                                          // Verschiebt die Icons vom Desktop zum Pfad
                Log.inf("Icons sind zugewiesen und nicht verstaut. Verschieben zum Pfad wird gestartet.");
            }
            else if (!string.IsNullOrEmpty(iconsZugewiesen[Properties.Settings.Default.SelectetMonitor]) && iconsVerstaut[Properties.Settings.Default.SelectetMonitor] == true) // Überprüft, ob Icons zugewiesen und verstaut sind
            {
                var moveIcons = new FunktionVerschieben();                                                                                                                      // Erstellt eine neue Instanz von FunktionVerschieben
                moveIcons.MovePathToDesk(Properties.Settings.Default.SelectetMonitor);                                                                                          // Verschiebt die Icons vom Pfad zum Desktop
                Log.inf("Icons sind zugewiesen und verstaut. Verschieben zum Desktop wird gestartet.");
            }
            else
            {
                Log.war("Bitte speichere erst Icons zum Monitor!");                                                                                                             // Zeigt eine Nachricht an, wenn keine Icons zugewiesen sind
            }
        }
        else
        {
            Log.war("Bitte wählen Sie erst Monitor aus");                                                                                                                       // Zeigt eine Nachricht an, wenn kein Monitor ausgewählt ist
        }
        HauptPage_Loaded(this, new RoutedEventArgs());                                                                                                                          // Lädt die Hauptseite neu
        TextSchreiben(Properties.Settings.Default.SelectetMonitor);
    }
  
    private void btnReload_Click(object sender, RoutedEventArgs e)                                                                                                              // Ereignishandler für den Klick auf die Schaltfläche zum Neuladen der Daten
    {
        Log.inf("Neuladen der Daten gestartet.");
        var reloadData = new Funktion2DatenLesen();                                                                                                                             // Erstellt eine neue Instanz von Funktion2DatenLesen
        reloadData.DatenLesenAbfrage();                                                                                                                                         // Führt die Datenleseabfrage durch

        Log.inf("Datenleseabfrage abgeschlossen.");
        HauptPage_Loaded(this, new RoutedEventArgs());                                                                                                                          // Lädt die Hauptseite neu
        TextSchreiben(Properties.Settings.Default.SelectetMonitor);
    }

}
