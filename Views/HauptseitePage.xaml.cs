using App3.Funktions;
using App3.Services;
using MahApps.Metro.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace App3.Views;

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
            MessageBox.Show("Bitte zuerst die Einstellungen vornehmen");
            var inizialisiert = new Funktion1Initialisieren();
            inizialisiert.Initialisieren();
            return;
        }

        LoadInfoMonitor();
        MonitorGröße();
        MultiMonPos();

        
        Moni1.Visibility = Properties.Settings.Default.eMonitorVorhanden1 ? Visibility.Visible : Visibility.Collapsed;
        Moni2.Visibility = Properties.Settings.Default.eMonitorVorhanden2 ? Visibility.Visible : Visibility.Collapsed;
        Moni3.Visibility = Properties.Settings.Default.eMonitorVorhanden3 ? Visibility.Visible : Visibility.Collapsed;
        Moni4.Visibility = Properties.Settings.Default.eMonitorVorhanden4 ? Visibility.Visible : Visibility.Collapsed;

        var transparentGreen = new SolidColorBrush(Color.FromArgb(5, 0, 255, 0)); // 50% Transparenz grünen
        var transparentGray = new SolidColorBrush(Color.FromArgb(30, 10, 10, 10)); // 50% Transparenz roten
        var transparentDarkGray = new SolidColorBrush(Color.FromArgb(60, 10, 10, 10)); // 50% Transparenz roten

        if (monitorData1.Length > 1){
            Moni1.Background = (monitorData1[11] == "Yes") ? transparentGreen : transparentGray;}
            Moni1.Background = Properties.Settings.Default.eMonitorAktiv1 ? transparentGray : transparentDarkGray;        
        if (monitorData2.Length > 1){
            Moni2.Background = (monitorData2[11] == "Yes") ? transparentGreen : transparentGray;}
            Moni2.Background = Properties.Settings.Default.eMonitorAktiv2 ? transparentGray : transparentDarkGray;        
        if (monitorData3.Length > 1){
            Moni3.Background = (monitorData3[11] == "Yes") ? transparentGreen : transparentGray;}
            Moni3.Background = Properties.Settings.Default.eMonitorAktiv3 ? transparentGray : transparentDarkGray;        
        if (monitorData4.Length > 1){
            Moni4.Background = (monitorData4[11] == "Yes") ? transparentGreen : transparentGray;}
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

        // Finde die minimalen und maximalen Werte der Positionen
        double minX = positions.Min(p => p.X);
        double maxX = positions.Max(p => p.X);
        double minY = positions.Min(p => p.Y);
        double maxY = positions.Max(p => p.Y);

        // Normalisiere die Positionen auf einen Bereich von 0 bis 1
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = new Point(
                (positions[i].X - minX) / (maxX - minX),
                (positions[i].Y - minY) / (maxY - minY)
            );
        }

        // Skaliere die normalisierten Positionen auf den gewünschten Bereich
        double maxMarginX = 193; // Beispielwert, anpassen nach Bedarf
        double maxMarginY = 108; // Beispielwert, anpassen nach Bedarf
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = new Point(
                positions[i].X * maxMarginX,
                positions[i].Y * maxMarginY
            );
        }

        // Setze die Margin-Eigenschaft der Monitore basierend auf den skalierten Werten
        if (positions.Length > 0) Moni1.Margin = new Thickness(positions[0].X+20,positions[0].Y+ 20,  0, 0);
        if (positions.Length > 1) Moni2.Margin = new Thickness(positions[1].X + 20, positions[1].Y+ 20,  0, 0); // Tausch mit Moni3
        if (positions.Length > 2) Moni3.Margin = new Thickness(positions[2].X+ 20, positions[2].Y+ 20,  0, 0); // Tausch mit Moni2
        if (positions.Length > 3) Moni4.Margin = new Thickness(positions[3].X + 20, positions[3].Y+ 20, 0, 0);
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

        if (monitorData[GewählterMonitor].Length > 1)
        {
            t2a1.Text = monitorData[GewählterMonitor][1];
            t3a3.Text = monitorData[GewählterMonitor][3];
            t4a5.Text = monitorData[GewählterMonitor][5];
            t5a7.Text = monitorData[GewählterMonitor][7];
            t6a9.Text = monitorData[GewählterMonitor][9];
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
        this.Loaded += HauptPage_Loaded;
        t12a21.Text = Properties.Settings.Default.eMonitorIconsCount1.ToString();
        List<FunktionIconListe.FileIconInfo> savedIcons = FunktionIconListe.gespeicherteIcons();
        if (savedIcons != null)
        {
            SavedLists.ItemsSource = savedIcons;
        }
        Thread.Sleep(1000);
        savedIcons = null;
    }
    private void Moni2_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        SavedLists.ItemsSource = null;
        Properties.Settings.Default.SelectetMonitor = 1;
        Properties.Settings.Default.Save();
        TextSchreiben(Properties.Settings.Default.SelectetMonitor);
        this.Loaded += HauptPage_Loaded;
        t12a21.Text = Properties.Settings.Default.eMonitorIconsCount2.ToString();
        List<FunktionIconListe.FileIconInfo> savedIcons = FunktionIconListe.gespeicherteIcons();
        if (savedIcons != null)
        {
            SavedLists.ItemsSource = savedIcons;
        }
        Thread.Sleep(1000);
        savedIcons = null;
    }
    private void Moni3_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        SavedLists.ItemsSource = null;
        Properties.Settings.Default.SelectetMonitor = 2;
        Properties.Settings.Default.Save();
        TextSchreiben(Properties.Settings.Default.SelectetMonitor);
        this.Loaded += HauptPage_Loaded;
        t12a21.Text = Properties.Settings.Default.eMonitorIconsCount3.ToString();
        List<FunktionIconListe.FileIconInfo> savedIcons = FunktionIconListe.gespeicherteIcons();
        if (savedIcons != null)
        {
            SavedLists.ItemsSource = savedIcons;
        }
        Thread.Sleep(1000);
        savedIcons = null;
    }
    private void Moni4_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        SavedLists.ItemsSource = null;
        Properties.Settings.Default.SelectetMonitor = 3;
        Properties.Settings.Default.Save();
        TextSchreiben(Properties.Settings.Default.SelectetMonitor);
        this.Loaded += HauptPage_Loaded;
        t12a21.Text = Properties.Settings.Default.eMonitorIconsCount4.ToString();
        List<FunktionIconListe.FileIconInfo> savedIcons = FunktionIconListe.gespeicherteIcons();
        if (savedIcons != null)
        {
            SavedLists.ItemsSource = savedIcons;
        }
        Thread.Sleep(1000);
        savedIcons = null;
    }

    private void btnSaveIconPos_Click(object sender, RoutedEventArgs e)
    {
        if (Properties.Settings.Default.SelectetMonitor < 4)
        {
            FunktionIconListe.Execute(); // Rufen Sie die Execute-Methode von Funktion1 auf
            ISPSaveState.IsReady = true;
            
            // Navigation zur IconSavePage
            this.NavigationService.Navigate(new IconSavePage());
        }
        else         {
            MessageBox.Show("Bitte wählen Sie erst Monitor aus");
        }
    }

    private void btnCreateShortCut_Click(object sender, RoutedEventArgs e)
    {

    }

    private void btnMonitorSwitch_Click(object sender, RoutedEventArgs e)
    {

    }

    private void btnIconsVerschieben_Click(object sender, RoutedEventArgs e)
    {

    }
}
