using System.Windows;

namespace NET.Paint.Helper
{
    public static class AnchorableProperties
    {
        public static readonly DependencyProperty HideTitleProperty =
            DependencyProperty.RegisterAttached(
                "HideTitle",
                typeof(bool),
                typeof(AnchorableProperties),
                new PropertyMetadata(false));

        public static void SetHideTitle(DependencyObject element, bool value)
        {
            element.SetValue(HideTitleProperty, value);
        }

        public static bool GetHideTitle(DependencyObject element)
        {
            return (bool)element.GetValue(HideTitleProperty);
        }
    }
}
