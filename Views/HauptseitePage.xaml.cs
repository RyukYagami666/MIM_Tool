using App3.Funktions;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace App3.Views;

public partial class HauptseitePage : Page, INotifyPropertyChanged
{
    public HauptseitePage()
    {
        InitializeComponent();
        DataContext = this;
        var dodStatus = new FunktionDesktopOK();
        dodStatus.DODKontrolle();
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
}
