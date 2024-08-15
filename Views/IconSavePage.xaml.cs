﻿using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using App3.Funktions;
using App3.Services;
using System.Linq;
using static App3.Funktions.FunktionIconListe; // Für die Verwendung von LINQ

namespace App3.Views;

public partial class IconSavePage : Page, INotifyPropertyChanged
{
    public IconSavePage()
    {
        InitializeComponent();
        DataContext = this;
        FunktionIconListe.Execute(); // Rufen Sie die Execute-Methode von Funktion1 auf
        IconListView.ItemsSource = App3.Funktions.FunktionIconListe.LastExecutedFiles;
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
        Properties.Settings.Default.eMonitorIconsCountTemp = selectedPaths.Length;
        Properties.Settings.Default.Save();
        MessageBox.Show($"Anzahl der ausgewählten Elemente: {selectedPaths.Length}\nPfad: {Properties.Settings.Default.DeskIconPfadMTemp}", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

        if (selectedPaths.Length > 0)
        {
            if (Properties.Settings.Default.SelectetMonitor == 10)
            {
                var auswahlFürISP = new AuswahlFürISP();
                auswahlFürISP.Show();
            }
            else if (Properties.Settings.Default.SelectetMonitor == 0)
            {
                FunktionAuswahlZiel auswahlFunk= new FunktionAuswahlZiel();
                auswahlFunk.MonitorGewählt1();

                this.NavigationService.Navigate(new HauptseitePage());
            }
            else if (Properties.Settings.Default.SelectetMonitor == 1)
            {
                FunktionAuswahlZiel auswahlFunk = new FunktionAuswahlZiel();
                auswahlFunk.MonitorGewählt2();

                this.NavigationService.Navigate(new HauptseitePage());
            }
            else if (Properties.Settings.Default.SelectetMonitor == 2)
            {
                FunktionAuswahlZiel auswahlFunk = new FunktionAuswahlZiel();
                auswahlFunk.MonitorGewählt3();

                this.NavigationService.Navigate(new HauptseitePage());
            }
            else if (Properties.Settings.Default.SelectetMonitor == 3)
            {
                FunktionAuswahlZiel auswahlFunk = new FunktionAuswahlZiel();
                auswahlFunk.MonitorGewählt4();

                this.NavigationService.Navigate(new HauptseitePage());
            }
        }
        else
        {
            MessageBox.Show("Bitte wähle Icons aus um zu speichern", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}