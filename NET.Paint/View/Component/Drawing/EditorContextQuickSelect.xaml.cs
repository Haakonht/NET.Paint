using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace NET.Paint.View.Component.Editor
{
    public partial class EditorContextQuickSelect : UserControl
    {
        // Define routed events
        public static readonly RoutedEvent OpenToolContextRequestedEvent =
            EventManager.RegisterRoutedEvent("OpenToolContextRequested", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(EditorContextQuickSelect));

        public static readonly RoutedEvent CloseToolContextRequestedEvent =
            EventManager.RegisterRoutedEvent("CloseToolContextRequested", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(EditorContextQuickSelect));

        public static readonly RoutedEvent OpenSpecificToolContextEvent =
            EventManager.RegisterRoutedEvent("OpenSpecificToolContext", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(EditorContextQuickSelect));

        public event RoutedEventHandler OpenToolContextRequested
        {
            add { AddHandler(OpenToolContextRequestedEvent, value); }
            remove { RemoveHandler(OpenToolContextRequestedEvent, value); }
        }

        public event RoutedEventHandler CloseToolContextRequested
        {
            add { AddHandler(CloseToolContextRequestedEvent, value); }
            remove { RemoveHandler(CloseToolContextRequestedEvent, value); }
        }

        public event RoutedEventHandler OpenSpecificToolContext
        {
            add { AddHandler(OpenSpecificToolContextEvent, value); }
            remove { RemoveHandler(OpenSpecificToolContextEvent, value); }
        }

        public EditorContextQuickSelect()
        {
            InitializeComponent();
        }

        private void OpenToolContext(object sender, MouseButtonEventArgs e)
        {
            // Raise the routed event instead of directly accessing ToolContextMenu
            RaiseEvent(new RoutedEventArgs(OpenToolContextRequestedEvent));
            e.Handled = true;
        }

        private void OpenToolContextSpecific(object sender, MouseEventArgs e)
        {
            if (sender is Path element)
            {
                // Create custom event args that include the tab index
                var eventArgs = new ToolContextEventArgs(OpenSpecificToolContextEvent, element.Tag);
                RaiseEvent(eventArgs);
            }
            e.Handled = true;
        }   

        private void CloseToolContext(object sender, MouseEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(CloseToolContextRequestedEvent));
            e.Handled = true;
        }

        private void Ellipse_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is Ellipse element)
            {
                // Create custom event args that include the tab index
                var eventArgs = new ToolContextEventArgs(OpenSpecificToolContextEvent, element.Tag);
                RaiseEvent(eventArgs);
            }
            e.Handled = true;
        }
    }

    public class ToolContextEventArgs : RoutedEventArgs
    {
        public object TabIndex { get; }

        public ToolContextEventArgs(RoutedEvent routedEvent, object tabIndex) : base(routedEvent)
        {
            TabIndex = tabIndex;
        }
    }
}
