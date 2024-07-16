using System.Windows.Controls;

namespace App3.Helpers;

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
