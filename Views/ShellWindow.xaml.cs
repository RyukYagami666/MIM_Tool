﻿using MIM_Tool.Contracts.Services;
using MIM_Tool.Contracts.Views;
using MIM_Tool.Helpers;
using MahApps.Metro.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace MIM_Tool.Views;

public partial class ShellWindow : MetroWindow, IShellWindow, INotifyPropertyChanged
{
    private readonly INavigationService _navigationService;           // Dienst zur Navigation
    private bool _canGoBack;                                          // Gibt an, ob eine Rücknavigation möglich ist
    private HamburgerMenuItem _selectedMenuItem;                      // Aktuell ausgewähltes Menüelement
    private HamburgerMenuItem _selectedOptionsMenuItem;               // Aktuell ausgewähltes Optionen-Menüelement

    public bool CanGoBack
    {
        get { return _canGoBack; }                                    // Gibt zurück, ob eine Rücknavigation möglich ist
        set { Set(ref _canGoBack, value); }                           // Setzt den Wert für die Rücknavigation
    }

    public HamburgerMenuItem SelectedMenuItem
    {
        get { return _selectedMenuItem; }                             // Gibt das aktuell ausgewählte Menüelement zurück
        set { Set(ref _selectedMenuItem, value); }                    // Setzt das aktuell ausgewählte Menüelement
    }

    public HamburgerMenuItem SelectedOptionsMenuItem
    {
        get { return _selectedOptionsMenuItem; }                      // Gibt das aktuell ausgewählte Optionen-Menüelement zurück
        set { Set(ref _selectedOptionsMenuItem, value); }             // Setzt das aktuell ausgewählte Optionen-Menüelement
    }

    public ObservableCollection<HamburgerMenuItem> MenuItems { get; } = new ObservableCollection<HamburgerMenuItem>()
    {
        // Initialisiert die Menüelemente
        new HamburgerMenuGlyphItem() { Label = Properties.Resources.ShellHauptseitePage, Glyph = "\uE10F", TargetPageType = typeof(HauptseitePage) },
        new HamburgerMenuGlyphItem() { Label = Properties.Resources.ShellIconSavePage, Glyph = "\uE771", TargetPageType = typeof(IconSavePage) },
        new HamburgerMenuGlyphItem() { Label = Properties.Resources.ShellFunktionPage, Glyph = "\uEDA9", TargetPageType = typeof(FunktionPage) },
    };

    public ObservableCollection<HamburgerMenuItem> OptionMenuItems { get; } = new ObservableCollection<HamburgerMenuItem>()
    {
        // Initialisiert die Optionen-Menüelemente
        new HamburgerMenuGlyphItem() { Label = Properties.Resources.ShellSettingsPage, Glyph = "\uE713", TargetPageType = typeof(SettingsPage) }
    };

    public ShellWindow(INavigationService navigationService)
    {
        _navigationService = navigationService;                                // Initialisiert den Navigationsdienst
        InitializeComponent();                                                 // Initialisiert die Komponenten des Fensters
        DataContext = this;                                                    // Setzt den Datenkontext auf die aktuelle Instanz
        Log.inf("ShellWindow initialisiert. Komponenten und Datenkontext gesetzt.");
    }

    public Frame GetNavigationFrame()
    {
        Log.inf("GetNavigationFrame aufgerufen.");
        return shellFrame;                                                     // Gibt das Navigations-Frame zurück
    }

    public void ShowWindow()
    {
        Log.inf("ShowWindow aufgerufen.");
        Show();                                                                // Zeigt das Fenster an
    }

    public void CloseWindow()
    {
        Log.inf("CloseWindow aufgerufen.");
        Close();                                                               // Schließt das Fenster
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        _navigationService.Navigated += OnNavigated;                           // Abonniert das Navigated-Ereignis
        Log.inf("ShellWindow geladen. Navigated-Ereignis abonniert.");
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        _navigationService.Navigated -= OnNavigated;                           // Deabonniert das Navigated-Ereignis
        Log.inf("ShellWindow entladen. Navigated-Ereignis deabonniert.");
    }

    private void OnItemClick(object sender, ItemClickEventArgs args)
    {
        Log.inf($"Menüelement geklickt: {SelectedMenuItem?.Label}");
        NavigateTo(SelectedMenuItem.TargetPageType);                           // Navigiert zur ausgewählten Seite
    }

    private void OnOptionsItemClick(object sender, ItemClickEventArgs args)
    {
        Log.inf($"Optionen-Menüelement geklickt: {SelectedOptionsMenuItem?.Label}");
        NavigateTo(SelectedOptionsMenuItem.TargetPageType);                    // Navigiert zur ausgewählten Optionen-Seite
    }

    private void NavigateTo(Type targetPage)
    {
        if (targetPage != null)
        {
            Log.inf($"Navigiere zu Seite: {targetPage.Name}");
            _navigationService.NavigateTo(targetPage);                         // Führt die Navigation zur Zielseite durch
        }
    }

    private void OnNavigated(object sender, Type pageType)
    {
        Log.inf($"Navigiert zu Seite: {pageType.Name}");
        // Aktualisiert das ausgewählte Menüelement basierend auf der Zielseite
        var item = MenuItems
                    .OfType<HamburgerMenuItem>()
                    .FirstOrDefault(i => pageType == i.TargetPageType);
        if (item != null)
        {
            SelectedMenuItem = item;                                           // Setzt das ausgewählte Menüelement
            Log.inf($"Ausgewähltes Menüelement gesetzt: {item.Label}");
        }
        else
        {
            SelectedOptionsMenuItem = OptionMenuItems
                    .OfType<HamburgerMenuItem>()
                    .FirstOrDefault(i => pageType == i.TargetPageType);        // Setzt das ausgewählte Optionen-Menüelement
            Log.inf($"Ausgewähltes Optionen-Menüelement gesetzt: {SelectedOptionsMenuItem?.Label}");
        }

        CanGoBack = _navigationService.CanGoBack;                              // Aktualisiert den Rücknavigation-Status
        Log.inf($"Rücknavigation-Status aktualisiert: {CanGoBack}");
    }

    private void OnGoBack(object sender, RoutedEventArgs e)
    {
        Log.inf("Button 'Zurück' geklickt.");
        _navigationService.GoBack();                                           // Führt die Rücknavigation durch
    }

    public event PropertyChangedEventHandler PropertyChanged;                  // Ereignis für Eigenschaftsänderungen

    private void Set<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
    {
        if (Equals(storage, value))
        {
            return;                                                            // Beendet die Methode, wenn der Wert gleich ist
        }

        storage = value;                                                       // Setzt den neuen Wert
        OnPropertyChanged(propertyName);                                       // Benachrichtigt über die Eigenschaftsänderung
        Log.inf($"Eigenschaft {propertyName} geändert auf {value}.");
    }

    private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); // Methode zur Benachrichtigung über Eigenschaftsänderungen
}
