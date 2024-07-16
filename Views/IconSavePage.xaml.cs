using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using App3.Services;

namespace App3.Views;

public partial class IconSavePage : Page, INotifyPropertyChanged
{
    public IconSavePage()
    {
        InitializeComponent();
        DataContext = this;
        IconListView.ItemsSource = App3.Funktions.Funktion1.LastExecutedFiles;
        this.Loaded += IconSavePage_Loaded;

    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void IconSavePage_Loaded(object sender, RoutedEventArgs e)
    {
        btnListeSpeichern.Visibility =ISPSaveState.IsReady ? Visibility.Visible : Visibility.Collapsed;
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

    private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}