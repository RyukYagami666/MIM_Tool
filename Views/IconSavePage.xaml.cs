using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace App3.Views;

public partial class IconSavePage : Page, INotifyPropertyChanged
{
    public IconSavePage()
    {
        InitializeComponent();
        DataContext = this;
        IconListView.ItemsSource = App3.Funktions.Funktion1.LastExecutedFiles;

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
