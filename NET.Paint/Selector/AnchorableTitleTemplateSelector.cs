using AvalonDock.Layout;
using NET.Paint.View.Component.Overview;
using System.Windows;
using System.Windows.Controls;

namespace NET.Paint.View.Component.Base.Converters
{
    public class AnchorableTitleTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ToolBoxTemplate { get; set; }

        public DataTemplate OverviewTemplate { get; set; }

        public DataTemplate DataGridTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is LayoutAnchorable anchorable)
            {
                var toolBoxComponent = anchorable.Content as Tools.ToolBar;
                if (toolBoxComponent != null)
                    return ToolBoxTemplate;

                var overviewComponent = anchorable.Content as ProjectTree;
                if (overviewComponent != null)
                    return OverviewTemplate;
            }

            return base.SelectTemplate(item, container);
        }
    }
}
