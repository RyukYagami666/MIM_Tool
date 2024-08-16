using MahApps.Metro.Controls;                                                                
using System.Windows;                                                                        
using System.Windows.Controls;                                                               

namespace MIM_Tool.TemplateSelectors
{
    public class MenuItemTemplateSelector : DataTemplateSelector                             // Erbt von DataTemplateSelector, um benutzerdefinierte Vorlagen auszuwählen.
    {
        public DataTemplate GlyphDataTemplate { get; set; }                                  // Datenvorlage für Glyphen.
        public DataTemplate ImageDataTemplate { get; set; }                                  // Datenvorlage für Bilder.

        public override DataTemplate SelectTemplate(object item, DependencyObject container) // Überschreibt die Methode zur Auswahl der Vorlage.
        {
            if (item is HamburgerMenuGlyphItem)                                              // Überprüft, ob das Element ein HamburgerMenuGlyphItem ist.
            {
                return GlyphDataTemplate;                                                    // Gibt die Glyphen-Datenvorlage zurück.
            }

            if (item is HamburgerMenuImageItem)                                              // Überprüft, ob das Element ein HamburgerMenuImageItem ist.
            {
                return ImageDataTemplate;                                                    // Gibt die Bild-Datenvorlage zurück.
            }

            return base.SelectTemplate(item, container);                                     // Gibt die Standardvorlage zurück, wenn keine Übereinstimmung gefunden wurde.
        }
    }
}



