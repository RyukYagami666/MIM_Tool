using System.Windows.Controls;

namespace MIM_Tool.Helpers;

public static class FrameExtensions
{
    public static void CleanNavigation(this Frame frame)
    {
        while (frame.CanGoBack)
        {
            frame.RemoveBackEntry();
        }
    }
}
