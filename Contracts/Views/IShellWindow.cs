using System.Windows.Controls;

namespace MIM_Tool.Contracts.Views;

public interface IShellWindow
{
    Frame GetNavigationFrame();

    void ShowWindow();

    void CloseWindow();
}
