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


        public void AuswahlFürISP_Loaded(object sender, RoutedEventArgs e)
        {
            btnMonitorAuswahl1.Visibility = Properties.Settings.Default.eMonitorVorhanden1 ? Visibility.Visible : Visibility.Collapsed;
            btnMonitorAuswahl2.Visibility = Properties.Settings.Default.eMonitorVorhanden2 ? Visibility.Visible : Visibility.Collapsed;
            btnMonitorAuswahl3.Visibility = Properties.Settings.Default.eMonitorVorhanden3 ? Visibility.Visible : Visibility.Collapsed;
            btnMonitorAuswahl4.Visibility = Properties.Settings.Default.eMonitorVorhanden4 ? Visibility.Visible : Visibility.Collapsed;
        }
        private void btnMonitorAuswahl1_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
        private void btnMonitorAuswahl2_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
        private void btnMonitorAuswahl3_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            
        }
        private void btnMonitorAuswahl4_Click(object sender, System.Windows.RoutedEventArgs e)
        { 

        }
    }
}