using System.Windows;

namespace MIM_Tool.Views
{
    public partial class MonitorSelectionWindow : Window
    {
        public int SelectedMonitor { get; private set; }

        public MonitorSelectionWindow()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (MonitorComboBox.SelectedIndex >= 0)
            {
                SelectedMonitor = MonitorComboBox.SelectedIndex;
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Bitte wählen Sie einen Monitor aus.");
            }
        }
    }
}