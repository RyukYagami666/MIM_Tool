using MIM_Tool.Contracts.Services;                                                                  
using MIM_Tool.Contracts.Views;                                                                     
using MIM_Tool.Helpers;                                                                             
using System.Windows.Controls;                                                                      
using System.Windows.Navigation;                                                                    

namespace MIM_Tool.Services
{
    public class NavigationService : INavigationService                                              // Implementiert den INavigationService.
    {
        private readonly IServiceProvider _serviceProvider;                                          // Dienstanbieter für die Abhängigkeitsinjektion.
        private Frame _frame;                                                                        // Rahmen für die Navigation.
        private object _lastParameterUsed;                                                           // Letztes verwendetes Parameter.

        public event EventHandler<Type> Navigated;                                                   // Ereignis, das ausgelöst wird, wenn die Navigation abgeschlossen ist.

        public bool CanGoBack => _frame.CanGoBack;                                                   // Überprüft, ob eine Rücknavigation möglich ist.

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;                                                      // Initialisiert den Dienstanbieter.
        }

        public void Initialize(Frame shellFrame)                                                     // Initialisiert den Navigationsrahmen.
        {
            if (_frame == null)
            {
                _frame = shellFrame;                                                                 // Setzt den Navigationsrahmen.
                _frame.Navigated += OnNavigated;                                                     // Abonniert das Navigationsereignis.
            }
        }

        public void UnsubscribeNavigation()                                                          // Deabonniert das Navigationsereignis.
        {
            _frame.Navigated -= OnNavigated;                                                         // Entfernt das Abonnement des Navigationsereignisses.
            _frame = null;                                                                           // Setzt den Navigationsrahmen auf null.
        }

        public void GoBack()                                                                         // Methode zur Rücknavigation.
        {
            if (_frame.CanGoBack)
            {
                var pageBeforeNavigation = _frame.Content;                                           // Holt die aktuelle Seite vor der Navigation.
                _frame.GoBack();                                                                     // Führt die Rücknavigation durch.
                if (pageBeforeNavigation is INavigationAware navigationAware)
                {
                    navigationAware.OnNavigatedFrom();                                               // Benachrichtigt die Seite, dass sie verlassen wurde.
                }
            }
        }

        public bool NavigateTo(Type pageType, object parameter = null, bool clearNavigation = false) // Methode zur Navigation zu einer bestimmten Seite.
        {
            if (_frame.Content?.GetType() != pageType || (parameter != null && !parameter.Equals(_lastParameterUsed)))
            {
                _frame.Tag = clearNavigation;                                                        // Setzt das Tag des Rahmens, um anzugeben, ob die Navigation bereinigt werden soll.
                var page = _serviceProvider.GetService(pageType) as Page;                            // Holt die Seite aus dem Dienstanbieter.

                var navigated = _frame.Navigate(page, parameter);                                    // Navigiert zur Seite mit dem angegebenen Parameter.
                if (navigated)
                {
                    _lastParameterUsed = parameter;                                                  // Speichert das zuletzt verwendete Parameter.
                    if (_frame.Content is INavigationAware navigationAware)
                    {
                        navigationAware.OnNavigatedFrom();                                           // Benachrichtigt die Seite, dass sie verlassen wurde.
                    }
                }

                return navigated;                                                                    // Gibt zurück, ob die Navigation erfolgreich war.
            }

            return false;                                                                            // Gibt false zurück, wenn die Navigation nicht durchgeführt wurde.
        }

        public void CleanNavigation()
            => _frame.CleanNavigation();                                                             // Bereinigt die Navigation.

        private void OnNavigated(object sender, NavigationEventArgs e)                               // Ereignishandler für die Navigation.
        {
            if (sender is Frame frame)
            {
                bool clearNavigation = (bool)frame.Tag;                                              // Holt das Tag des Rahmens.
                if (clearNavigation)
                {
                    frame.CleanNavigation();                                                         // Bereinigt die Navigation, wenn das Tag gesetzt ist.
                }

                if (frame.Content is INavigationAware navigationAware)
                {
                    navigationAware.OnNavigatedTo(e.ExtraData);                                      // Benachrichtigt die Seite, dass sie navigiert wurde.
                }

                Navigated?.Invoke(sender, frame.Content.GetType());                                  // Löst das Navigated-Ereignis aus.
            }
        }
    }
}


