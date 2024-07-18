﻿using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using App3.Funktions;
using App3.Services;
using System.Linq; // Für die Verwendung von LINQ

namespace App3.Views;

public partial class IconSavePage : Page, INotifyPropertyChanged
{
    public IconSavePage()
    {
        InitializeComponent();
        DataContext = this;
        IconListView.ItemsSource = App3.Funktions.Funktion1.LastExecutedFiles;
        this.Loaded += IconSavePage_Loaded;
        var dodStatus = new FunktionDesktopOK();
        dodStatus.DODKontrolle();

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
  // private void btnListeSpeichern_Click(object sender, RoutedEventArgs e)
  // {
  //     var selectedItems = IconListView.SelectedItems.Cast<FileIconInfo>().ToArray();
  //     MessageBox.Show($"Anzahl der ausgewählten Elemente: {selectedItems.Length}", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
  //
  // }
    private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private void btnListeSpeichern_Click(object sender, RoutedEventArgs e)
    {
        var selectedPaths = IconListView.SelectedItems.Cast<FileIconInfo>().Select(item => item.Path).ToArray();

        string selectedItemsString = string.Join(";", selectedPaths);
        Properties.Settings.Default.DeskIconPfadMTemp = selectedItemsString;
        Properties.Settings.Default.Save();
        MessageBox.Show($"Anzahl der ausgewählten Elemente: {selectedPaths.Length}", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        // Erstellen und Anzeigen des Fensters AuswahlFürISP nach dem Schließen der MessageBox
        
        if (selectedPaths.Length > 0)
        {
            FunktionAuswahlZiel auswahlFunkFürISP = new FunktionAuswahlZiel();
            auswahlFunkFürISP.Prüfen();
            MessageBox.Show("Drin");
            AuswahlFürISP auswahlFürISP = new AuswahlFürISP();
            auswahlFürISP.Show();

        }
    }
}