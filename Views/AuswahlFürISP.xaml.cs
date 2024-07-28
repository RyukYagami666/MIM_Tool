using App3.Services;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using App3.Funktions;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Markup;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Net;


namespace App3.Views
{
    /// <summary>
    /// Interaktionslogik für AuswahlFürISP.xaml
    /// </summary>
    public partial class AuswahlFürISP : MetroWindow
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public AuswahlFürISP()
        {
            InitializeComponent();
            DataContext = this;
            this.Loaded += AuswahlFürISP_Loaded;
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public void CloseWindow()
         => Close();

        public void AuswahlFürISP_Loaded(object sender, RoutedEventArgs e)
        {
            btnMonitorAuswahl1.Visibility = Properties.Settings.Default.eMonitorVorhanden1 ? Visibility.Visible : Visibility.Collapsed;
            btnMonitorAuswahl2.Visibility = Properties.Settings.Default.eMonitorVorhanden2 ? Visibility.Visible : Visibility.Collapsed;
            btnMonitorAuswahl3.Visibility = Properties.Settings.Default.eMonitorVorhanden3 ? Visibility.Visible : Visibility.Collapsed;
            btnMonitorAuswahl4.Visibility = Properties.Settings.Default.eMonitorVorhanden4 ? Visibility.Visible : Visibility.Collapsed;

            if(Properties.Settings.Default.eMonitorVorhanden1)
            {
                string infoMonitor1str = Properties.Settings.Default.InfoMonitor1;
                string[] infoMonitor1 = infoMonitor1str.Split(';');
                btnMonitorAuswahl1.Content = "Monitor: " + infoMonitor1[1] + "\nNummer: " + infoMonitor1[3];
            }
            if (Properties.Settings.Default.eMonitorVorhanden2)
            {
                string infoMonitor2str = Properties.Settings.Default.InfoMonitor2;
                string[] infoMonitor2 = infoMonitor2str.Split(';');
                btnMonitorAuswahl2.Content = "Monitor: " + infoMonitor2[1] + "\nNummer: " + infoMonitor2[3];
            }
            if (Properties.Settings.Default.eMonitorVorhanden3)
            {
                string infoMonitor3str = Properties.Settings.Default.InfoMonitor3;
                string[] infoMonitor3 = infoMonitor3str.Split(';');
                btnMonitorAuswahl3.Content = "Monitor: " + infoMonitor3[1] + "\nNummer: " + infoMonitor3[3];
            }
            if (Properties.Settings.Default.eMonitorVorhanden4)
            {
                 string infoMonitor4str = Properties.Settings.Default.InfoMonitor4;
                 string[] infoMonitor4 = infoMonitor4str.Split(';');
                 btnMonitorAuswahl4.Content = "Monitor: " + infoMonitor4[1] + "\nNummer: " + infoMonitor4[3];
            }
            
        }
        

        private void btnMonitorAuswahl1_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            FunktionAuswahlZiel auswahlFunkFürISP1 = new FunktionAuswahlZiel();
            auswahlFunkFürISP1.MonitorGewählt1();
            CloseWindow();
        }
        private void btnMonitorAuswahl2_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            FunktionAuswahlZiel auswahlFunkFürISP2 = new FunktionAuswahlZiel();
            auswahlFunkFürISP2.MonitorGewählt2();
            CloseWindow();
        }
        private void btnMonitorAuswahl3_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            FunktionAuswahlZiel auswahlFunkFürISP3 = new FunktionAuswahlZiel();
            auswahlFunkFürISP3.MonitorGewählt3();
            CloseWindow();
        }
        private void btnMonitorAuswahl4_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            FunktionAuswahlZiel auswahlFunkFürISP4 = new FunktionAuswahlZiel();
            auswahlFunkFürISP4.MonitorGewählt4();
            CloseWindow();
        }

        
    }
}