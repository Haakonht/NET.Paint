using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;

namespace NET.Paint.Resources.Extensions
{
    public static class TreeViewExtensions
    {
        /// <summary>
        /// Applies a search filter to all items of a TreeView recursively
        /// </summary>
        public static void Filter(this TreeView self, Predicate<object> predicate)
        {
            if (self == null) return;
            ICollectionView view = CollectionViewSource.GetDefaultView(self.ItemsSource);
            if (view == null)
                return;
            view.Filter = predicate;
            foreach (var obj in self.ItemContainerGenerator.Items)
            {
                var item = self.ItemContainerGenerator.ContainerFromItem(obj) as TreeViewItem;
                if (item == null)
                {
                    self.UpdateLayout();
                    item = self.ItemContainerGenerator.ContainerFromItem(obj) as TreeViewItem;
                }
                FilterRecursively(item, predicate);
            }
        }

        private static void FilterRecursively(TreeViewItem item, Predicate<object> predicate)
        {
            if (item == null) return;
            ICollectionView view = CollectionViewSource.GetDefaultView(item.ItemsSource);
            if (view == null) return;
            view.Filter = predicate;
            foreach (var obj in item.ItemContainerGenerator.Items)
            {
                var childItem = item.ItemContainerGenerator.ContainerFromItem(obj) as TreeViewItem;
                if (childItem == null)
                {
                    item.UpdateLayout();
                    childItem = item.ItemContainerGenerator.ContainerFromItem(obj) as TreeViewItem;
                }
                FilterRecursively(childItem, predicate);
            }
        }
    }
}
