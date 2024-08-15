using MIM_Tool.Funktions;
using MIM_Tool.Services;
using MahApps.Metro.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace MIM_Tool.Views;
public partial class HauptseitePage : Page, INotifyPropertyChanged
    {
    public string[] monitorData1;
    public string[] monitorData2;
    public string[] monitorData3;
    public string[] monitorData4;
    private const double ScaleFactor = 0.05;

    public HauptseitePage()
    {
        InitializeComponent();
        DataContext = this;

        this.Loaded += HauptPage_Loaded;
    }
    public event PropertyChangedEventHandler PropertyChanged;

    
    private void HauptPage_Loaded(object sender, RoutedEventArgs e)
    {

        if (!Properties.Settings.Default.Inizialisiert)
        {
            MessageBox.Show("Herzlich Willkommen bei Monitor Icon Manager. " +
                "\nDies ist ein Tool Zum Steuern von Monitoren und Verschieben von ausgewählten Icons. " +
                "\nIcons können einzeln mit einem Monitor verknüpft werden. " +
                "\nDies kann notwendig werden wenn man z.B. Verzeichnisse auf mehreren Monitoren haben möchte. " +
                "\n\nIm Hintergrund werden Zusatzprogramme erstellt und Ordner für Config Daten, " +
                "\ndieser Ordner lässt sich unter Einstellungen ändern. ", "Tutorial");
            MessageBox.Show("Bevor es losgeht, stelle dein Monitor Setup vollständig ein, mit Icons ETC!" +
                "\nDa nach dem Tutorial das Programm initialisiert wird muss dieser Schritt vorher abgeschlossen sein. " +
                "\nBei der Initialisierung werden Hintergrundprogramme, config Dateien und Einstellungen erstellt." +
                "\nEs werden die Positionen der Icons und die Informationen der Monitore gespeichern.  " +
                "\nWenn sich etwas ändert kannst du mit einem Reload Button Daten neu Speichern  " +
                "\nnNach dem ersten Start verknüpfe Icons mit Monitoren, " +
                "\nDazu gibt es 2 Möglichkeiten entweder du wählst auf der Hauptseite ein Monitor aus " +
                "\nund Klickst auf den unteren Button (Icon Auswahl) oder gehst im seitlichen Menü auf IconSave. " +
                "\nWähle nun alle Icons aus der Liste, die du für einen entsprechenden Monitor wählen möchtest und klicke dann auf Speichern. " +
                "\nGewählte Icons werden in einer Liste gespeichert, diese lässt sich entweder überschreiben oder Icons können hinzugefügt werden. " +
                "\nNach dem Zuweisen kannst du Verknüpfungen zum Desktop erstellen mit Button (Verknüpfung Erstellen), " +
                "\num einzelne Monitore zuschalten, dabei werden die verknüpften Icons in den Hintergrund verschoben. " +
                "\nSomit sind die Grundlagen fertig. " +
                "\nWenn du über einzelne Buttons oder Felder mit der Maus stehenbleibst, " +
                "\nbekommst du noch einen Tooltip und auf der InfoSeite gibt es noch weitere Anleitungen ", "Tutorial");
            var inizialisiert = new Funktion1Initialisieren();
            inizialisiert.Initialisieren();
            return;
        }

        LoadInfoMonitor();
        MonitorGröße();
        MultiMonPos();

        Properties.Settings.Default.SelectetMonitor = 10;
        Properties.Settings.Default.Save();

        Moni1.Visibility = Properties.Settings.Default.eMonitorVorhanden1 ? Visibility.Visible : Visibility.Collapsed;
        Moni2.Visibility = Properties.Settings.Default.eMonitorVorhanden2 ? Visibility.Visible : Visibility.Collapsed;
        Moni3.Visibility = Properties.Settings.Default.eMonitorVorhanden3 ? Visibility.Visible : Visibility.Collapsed;
        Moni4.Visibility = Properties.Settings.Default.eMonitorVorhanden4 ? Visibility.Visible : Visibility.Collapsed;

        var transparentGray = new SolidColorBrush(Color.FromArgb(30, 10, 10, 10)); // 50% Transparenz roten
        var transparentDarkGray = new SolidColorBrush(Color.FromArgb(200, 10, 10, 10)); // 50% Transparenz roten

        Moni1.Background = Properties.Settings.Default.eMonitorAktiv1 ? transparentGray : transparentDarkGray;     
        Moni2.Background = Properties.Settings.Default.eMonitorAktiv2 ? transparentGray : transparentDarkGray;        
        Moni3.Background = Properties.Settings.Default.eMonitorAktiv3 ? transparentGray : transparentDarkGray;        
        Moni4.Background = Properties.Settings.Default.eMonitorAktiv4 ? transparentGray : transparentDarkGray;        
    }
    

    private void Set<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
    {
        if (Equals(storage, value))
        {
            return;
        }

        storage = value;
        OnPropertyChanged(propertyName);
    }

    public void LoadInfoMonitor()
    {
            string infoMonitor1 = Properties.Settings.Default.InfoMonitor1;
            monitorData1 = infoMonitor1.Split(';');
            string infoMonitor2 = Properties.Settings.Default.InfoMonitor2;
            monitorData2 = infoMonitor2.Split(';');
            string infoMonitor3 = Properties.Settings.Default.InfoMonitor3;
            monitorData3 = infoMonitor3.Split(';');
            string infoMonitor4 = Properties.Settings.Default.InfoMonitor4;
            monitorData4 = infoMonitor4.Split(';');
    }

    public void MultiMonPos()
    {

        // Extrahiere die Positionen aus den monitorData-Arrays
        Point[] positions = new Point[4];
        if (monitorData1.Length > 1)
        {
            var positionParts1 = monitorData1[7].Split(',');
            positions[0] = new Point(int.Parse(positionParts1[0].Trim()), int.Parse(positionParts1[1].Trim()));
        }
        if (monitorData2.Length > 1)
        {
            var positionParts2 = monitorData2[7].Split(',');
            positions[1] = new Point(int.Parse(positionParts2[0].Trim()), int.Parse(positionParts2[1].Trim()));
        }
        if (monitorData3.Length > 1)
        {
            var positionParts3 = monitorData3[7].Split(',');
            positions[2] = new Point(int.Parse(positionParts3[0].Trim()), int.Parse(positionParts3[1].Trim()));
        }
        if (monitorData4.Length > 1)
        {
            var positionParts4 = monitorData4[7].Split(',');
            positions[3] = new Point(int.Parse(positionParts4[0].Trim()), int.Parse(positionParts4[1].Trim()));
        }

        // Extrahiere die Positionen aus den monitorData-Arrays
        Point[] positionsM = new Point[4];
        if (monitorData1.Length > 1)
        {
            var positionParts1 = monitorData1[7].Split(',');
            var positionPartsR1 = monitorData1[5].Split('X');
            positionsM[0] = new Point(int.Parse(positionParts1[0].Trim()) + int.Parse(positionPartsR1[0].Trim()), int.Parse(positionParts1[1].Trim()) + int.Parse(positionPartsR1[1].Trim()));
        }
        if (monitorData2.Length > 1)
        {
            var positionParts2 = monitorData2[7].Split(',');
            var positionPartsR2 = monitorData2[5].Split('X');
            positionsM[1] = new Point(int.Parse(positionParts2[0].Trim()) + int.Parse(positionPartsR2[0].Trim()), int.Parse(positionParts2[1].Trim()) + int.Parse(positionPartsR2[1].Trim()));
        }
        if (monitorData3.Length > 1)
        {
            var positionParts3 = monitorData3[7].Split(',');
            var positionPartsR3 = monitorData3[5].Split('X');
            positionsM[2] = new Point(int.Parse(positionParts3[0].Trim()) + int.Parse(positionPartsR3[0].Trim()), int.Parse(positionParts3[1].Trim()) + int.Parse(positionPartsR3[1].Trim()));
        }
        if (monitorData4.Length > 1)
        {
            var positionParts4 = monitorData4[7].Split(',');
            var positionPartsR4 = monitorData4[5].Split('X');
            positionsM[3] = new Point(int.Parse(positionParts4[0].Trim()) + int.Parse(positionPartsR4[0].Trim()), int.Parse(positionParts4[1].Trim()) + int.Parse(positionPartsR4[1].Trim()));
        }

        // Finde die minimalen und maximalen Werte der Positionen
        double minX = positions.Min(p => p.X);
        double maxX = positionsM.Max(p => p.X);
        double minY = positions.Min(p => p.Y);
        double maxY = positionsM.Max(p => p.Y);


        // Normalisiere die Positionen auf einen Bereich von 0 bis 1
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = new Point(
                (positions[i].X - minX) / (maxX - minX) ,
                (positions[i].Y - minY) / (maxY - minY)
            );
        }

        double scaleX;
        double scaleY;
        if (minX < 0) scaleX = (maxX + Math.Abs(minX)) * ScaleFactor;
        else scaleX = maxX * ScaleFactor;

        if (minY < 0) scaleY = (maxY + Math.Abs(minY)) * ScaleFactor;
        else scaleY = maxY * ScaleFactor;
        

        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = new Point(
                positions[i].X * scaleX,
                positions[i].Y * scaleY
            );
        }

        // Setze die Margin-Eigenschaft der Monitore basierend auf den skalierten Werten
        if (positions.Length > 0) Moni1.Margin = new Thickness(positions[0].X + 20, positions[0].Y + 20, 0, 0);
        if (positions.Length > 1) Moni2.Margin = new Thickness(positions[1].X + 20, positions[1].Y + 20, 0, 0);
        if (positions.Length > 2) Moni3.Margin = new Thickness(positions[2].X + 20, positions[2].Y + 20, 0, 0);
        if (positions.Length > 3) Moni4.Margin = new Thickness(positions[3].X + 20, positions[3].Y + 20, 0, 0);
    }


    public void MonitorGröße()
    {
        var BlauerText = new SolidColorBrush(Color.FromArgb(250, 50, 50, 255));

        if (monitorData1.Length > 1) { 
            var resolutionParts1 = monitorData1[5].Split('X');
            Moni1.Width = int.Parse(resolutionParts1[0].Trim()) * ScaleFactor;
            Moni1.Height = int.Parse(resolutionParts1[1].Trim()) * ScaleFactor;
            Moni1.Content = $"{monitorData1[1]}\n{monitorData1[5]}\n{monitorData1[7]}";
            if (monitorData1[11] == "Yes"){
                Moni1.Content = new TextBlock
                {
                    Inlines = {
                new Run($"{monitorData1[1]}\n") { FontWeight = FontWeights.Bold , Foreground = BlauerText },
                new Run($"{monitorData1[5]}\n") { FontWeight = FontWeights.Normal },
                new Run($"{monitorData1[7]}")   { FontWeight = FontWeights.Normal }
                }
                };
            }
            else {
                Moni1.Content = new TextBlock
                {
                    Inlines = {
                new Run($"{monitorData1[1]}\n") { FontWeight = FontWeights.Bold  },
                new Run($"{monitorData1[5]}\n") { FontWeight = FontWeights.Normal },
                new Run($"{monitorData1[7]}")   { FontWeight = FontWeights.Normal }
                }
                };
            }
           
        }
        if (monitorData2.Length > 1){
            var resolutionParts2 = monitorData2[5].Split('X');
            Moni2.Width = int.Parse(resolutionParts2[0].Trim()) * ScaleFactor;
            Moni2.Height = int.Parse(resolutionParts2[1].Trim()) * ScaleFactor;
            Moni2.Content = $"{monitorData2[1]}\n{monitorData2[5]}\n{monitorData2[7]}";
            if (monitorData2[11] == "Yes")
            {
                Moni2.Content = new TextBlock
                {
                    Inlines = {
                        new Run($"{monitorData2[1]}\n") { FontWeight = FontWeights.Bold, Foreground = BlauerText },
                        new Run($"{monitorData2[5]}\n"){ FontWeight = FontWeights.Normal },
                        new Run($"{monitorData2[7]}")  { FontWeight = FontWeights.Normal }
                    }
                };
            }
            else
            {
                Moni2.Content = new TextBlock
                {
                    Inlines = {
                        new Run($"{monitorData2[1]}\n") { FontWeight = FontWeights.Bold},
                        new Run($"{monitorData2[5]}\n"){ FontWeight = FontWeights.Normal },
                        new Run($"{monitorData2[7]}")  { FontWeight = FontWeights.Normal }
                    }
                };
            }
        }
        if (monitorData3.Length > 1){
            var resolutionParts3 = monitorData3[5].Split('X');
            Moni3.Width = int.Parse(resolutionParts3[0].Trim()) * ScaleFactor;
            Moni3.Height = int.Parse(resolutionParts3[1].Trim()) * ScaleFactor;
            if (monitorData3[11] == "Yes")
            {
                Moni3.Content = new TextBlock
                {
                    Inlines = {
                        new Run($"{monitorData3[1]}\n") { FontWeight = FontWeights.Bold , Foreground = BlauerText},
                        new Run($"{monitorData3[5]}\n"){ FontWeight = FontWeights.Normal },
                        new Run($"{monitorData3[7]}")  { FontWeight = FontWeights.Normal }
                    }
                };
            }
            else
            {
                Moni3.Content = new TextBlock
                {
                    Inlines = {
                        new Run($"{monitorData3[1]}\n") { FontWeight = FontWeights.Bold },
                        new Run($"{monitorData3[5]}\n"){ FontWeight = FontWeights.Normal },
                        new Run($"{monitorData3[7]}")  { FontWeight = FontWeights.Normal }
                    }
                };
            }
        }
        if (monitorData4.Length > 1){
            var resolutionParts4 = monitorData4[5].Split('X');
            Moni4.Width = int.Parse(resolutionParts4[0].Trim()) * ScaleFactor;
            Moni4.Height = int.Parse(resolutionParts4[1].Trim()) * ScaleFactor;
            if (monitorData4[11] == "Yes")
            {
                Moni4.Content = new TextBlock
                {
                    Inlines = {
                        new Run($"{monitorData4[1]}\n") { FontWeight = FontWeights.Bold , Foreground = BlauerText},
                        new Run($"{monitorData4[5]}\n"){ FontWeight = FontWeights.Normal },
                        new Run($"{monitorData4[7]}")  { FontWeight = FontWeights.Normal }
                    }
                };
            }
            else
            {
                Moni4.Content = new TextBlock
                {
                    Inlines = {
                        new Run($"{monitorData4[1]}\n") { FontWeight = FontWeights.Bold},
                        new Run($"{monitorData4[5]}\n"){ FontWeight = FontWeights.Normal },
                        new Run($"{monitorData4[7]}")  { FontWeight = FontWeights.Normal }
                    }
                };
            }
            
        }
    }

    public void TextSchreiben(int GewählterMonitor)
    {
        string[][] monitorData =
        {
            monitorData1,
            monitorData2,
            monitorData3,
            monitorData4
        };
        bool[] aktiv =
        {
            Properties.Settings.Default.eMonitorAktiv1,
            Properties.Settings.Default.eMonitorAktiv2,
            Properties.Settings.Default.eMonitorAktiv3,
            Properties.Settings.Default.eMonitorAktiv4
        };

        if (monitorData[GewählterMonitor].Length > 1)
        {
            t2a1.Text = monitorData[GewählterMonitor][1];
            t3a3.Text = monitorData[GewählterMonitor][3];
            t4a5.Text = monitorData[GewählterMonitor][5];
            t5a7.Text = monitorData[GewählterMonitor][7];
            t6a9.Text = aktiv[GewählterMonitor] ? "Aktiv" : "Inaktiv";
            t7a11.Text = monitorData[GewählterMonitor][11];
            t8a13.Text = monitorData[GewählterMonitor][13];
            t9a15.Text = monitorData[GewählterMonitor][15];
            t10a17.Text = monitorData[GewählterMonitor][17];
            t11a19.Text = monitorData[GewählterMonitor][19];
        }
    }

    private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private void Moni1_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        SavedLists.ItemsSource = null;
        Properties.Settings.Default.SelectetMonitor = 0;
        Properties.Settings.Default.Save();
        TextSchreiben(Properties.Settings.Default.SelectetMonitor);
        t12a21.Text = Properties.Settings.Default.eMonitorIconsCount1.ToString();
        List<FunktionIconListe.FileIconInfo> savedIcons = FunktionIconListe.gespeicherteIcons();
        if (savedIcons != null)
        {
            SavedLists.ItemsSource = savedIcons;
        }
        Thread.Sleep(300);
        savedIcons = null;
    }
    private void Moni2_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        SavedLists.ItemsSource = null;
        Properties.Settings.Default.SelectetMonitor = 1;
        Properties.Settings.Default.Save();
        TextSchreiben(Properties.Settings.Default.SelectetMonitor);
        t12a21.Text = Properties.Settings.Default.eMonitorIconsCount2.ToString();
        List<FunktionIconListe.FileIconInfo> savedIcons = FunktionIconListe.gespeicherteIcons();
        if (savedIcons != null)
        {
            SavedLists.ItemsSource = savedIcons;
        }
        Thread.Sleep(300);
        savedIcons = null;
    }
    private void Moni3_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        SavedLists.ItemsSource = null;
        Properties.Settings.Default.SelectetMonitor = 2;
        Properties.Settings.Default.Save();
        TextSchreiben(Properties.Settings.Default.SelectetMonitor);
        t12a21.Text = Properties.Settings.Default.eMonitorIconsCount3.ToString();
        List<FunktionIconListe.FileIconInfo> savedIcons = FunktionIconListe.gespeicherteIcons();
        if (savedIcons != null)
        {
            SavedLists.ItemsSource = savedIcons;
        }
        Thread.Sleep(300);
        savedIcons = null;
    }
    private void Moni4_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        SavedLists.ItemsSource = null;
        Properties.Settings.Default.SelectetMonitor = 3;
        Properties.Settings.Default.Save();
        TextSchreiben(Properties.Settings.Default.SelectetMonitor);
        t12a21.Text = Properties.Settings.Default.eMonitorIconsCount4.ToString();
        List<FunktionIconListe.FileIconInfo> savedIcons = FunktionIconListe.gespeicherteIcons();
        if (savedIcons != null)
        {
            SavedLists.ItemsSource = savedIcons;
        }
        Thread.Sleep(300);
        savedIcons = null;
    }

    private void btnSaveIconPos_Click(object sender, RoutedEventArgs e)
    {
        if (Properties.Settings.Default.SelectetMonitor < 4)
        {
            // Navigation zur IconSavePage
            this.NavigationService.Navigate(new IconSavePage());
        }
        else         
        {
            MessageBox.Show("Bitte wählen Sie erst Monitor aus");
        }
    }

    private void btnCreateShortCut_Click(object sender, RoutedEventArgs e)
    {
        var verknüpfung = new FunktionVerknüpfung();
        verknüpfung.VerknüpfungStart();
    }

    private void btnMonitorSwitch_Click(object sender, RoutedEventArgs e)
    {
        string[][] monitorData =
        {
            monitorData1,
            monitorData2,
            monitorData3,
            monitorData4
        };
        if (Properties.Settings.Default.SelectetMonitor < 4)
        {
            string pathExe = $"{Properties.Settings.Default.pfadDeskOK}\\MultiMonitorTool.exe";
            FunktionMultiMonitor.MonitorDeaktivieren(pathExe, monitorData[Properties.Settings.Default.SelectetMonitor][17], Properties.Settings.Default.SelectetMonitor);
            HauptPage_Loaded(this, new RoutedEventArgs());
        }
        else
        {
            MessageBox.Show("Bitte wählen Sie erst Monitor aus");
        }
    }

    private void btnIconsVerschieben_Click(object sender, RoutedEventArgs e)
    {
        string[] iconsZugewiesen =
        {
            Properties.Settings.Default.eMonitorIconsZugewiesen1,
            Properties.Settings.Default.eMonitorIconsZugewiesen2,
            Properties.Settings.Default.eMonitorIconsZugewiesen3,
            Properties.Settings.Default.eMonitorIconsZugewiesen4
        };
        bool[] iconsVerstaut =
        {
            Properties.Settings.Default.eMonitorIconsVerstaut1,
            Properties.Settings.Default.eMonitorIconsVerstaut2,
            Properties.Settings.Default.eMonitorIconsVerstaut3,
            Properties.Settings.Default.eMonitorIconsVerstaut4
        };
        if (Properties.Settings.Default.SelectetMonitor < 4)
        {
            if (!string.IsNullOrEmpty(iconsZugewiesen[Properties.Settings.Default.SelectetMonitor]) && iconsVerstaut[Properties.Settings.Default.SelectetMonitor] == false)
            {
                var moveIcons = new FunktionVerschieben();
                moveIcons.MoveDeskToPath(Properties.Settings.Default.SelectetMonitor);
            }
            else if (!string.IsNullOrEmpty(iconsZugewiesen[Properties.Settings.Default.SelectetMonitor]) && iconsVerstaut[Properties.Settings.Default.SelectetMonitor] == true)
            {
                var moveIcons = new FunktionVerschieben();
                moveIcons.MovePathToDesk(Properties.Settings.Default.SelectetMonitor);
            }
            else
            {
                MessageBox.Show("Bitte speichere erst Icons zum Monitor!");
            }
        }
        else
        {
            MessageBox.Show("Bitte wählen Sie erst Monitor aus");
        }

    }

        private void btnReload_Click(object sender, RoutedEventArgs e)
    {
        var reloadData = new Funktion2DatenLesen();
        reloadData.DatenLesenAbfrage();

        MessageBox.Show("Daten wurden neu geladen");

        HauptPage_Loaded(this, new RoutedEventArgs());

    }
}
