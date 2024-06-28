using App3.Funktions;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace App3.Views;

public partial class FunktionPage : Page, INotifyPropertyChanged
{
    public FunktionPage()
    {
        InitializeComponent();
        DataContext = this;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void Set<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
    {
        if (Equals(storage, value))
        {
            return;
        }

        storage = value;
        OnPropertyChanged(propertyName);
    }

    private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        Funktion1.Execute(); // Rufen Sie die Execute-Methode von Funktion1 auf
    }

    private void ButMoniScann_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        var deskScen = new FunktionDeskScen();
        deskScen.AuslesenMonitoreUndPositionen();
    }

    private void IniziStart_Click(object sender, System.Windows.RoutedEventArgs e)
    {

        FunktionIniReset funktionIniReset = new FunktionIniReset();
        funktionIniReset.Initialisierungs(); // Rufen Sie die ResetIni-Methode von FunktionIniReset auf

    }
}
