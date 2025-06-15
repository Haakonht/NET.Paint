using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Model.Shape;
using NET.Paint.Drawing.Model.Structure;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NET.Paint.View.Component
{
    /// <summary>
    /// Interaction logic for Overview.xaml
    /// </summary>
    public partial class Imagetree : UserControl
    {
        public Imagetree()
        {
            InitializeComponent();
        }

        private void SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var context = DataContext as XImage;

            if (context != null)
            {
                context.Selected = e.NewValue;
                context.Tools.ActiveTool = ToolType.Selector;

                if (e.NewValue is XLayer)
                    context.ActiveLayer = e.NewValue as XLayer;             
            }
        }

        private void Unselect(object sender, MouseButtonEventArgs e)
        {
            var context = DataContext as XImage;

            if (context != null)
                context.Selected = null;             
        }

        private void SelectImage(object sender, MouseButtonEventArgs e)
        {
            var context = DataContext as XImage;

            if (context != null)
                context.Selected = context;

            e.Handled = true;
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            var context = DataContext as XImage;

            if (context != null)
                context.Layers.Add(new XLayer() { Title = $"Layer {context.Layers.Count}" });
        }

        private void Remove(object sender, RoutedEventArgs e)
        {
            var context = DataContext as XImage;
            var item = sender as MenuItem;

            if (context != null)
            {
                if (item.DataContext is XLayer layer && context.Layers.Count > 1)
                {
                    context.Layers.Remove(layer);
                    context.ActiveLayer = context.Layers.First();
                }

                if (item.DataContext is XRenderable renderable)
                {
                    foreach (var xlayer in context.Layers)
                    {
                        for (int i = 0; i < context.Layers.Count; i++)
                        {
                            if (context.Layers[i].Shapes.Contains(renderable))
                                context.Layers[i].Shapes.Remove(renderable);
                        }
                    }
                }
            }
        }

        private void Cut(object sender, RoutedEventArgs e)
        {
            var context = DataContext as XImage;
            var item = sender as MenuItem;

            if (context != null)
            {
                if (item.DataContext is XLayer layer && context.Layers.Count > 1)
                {
                    context.Layers.Remove(layer);
                }

                if (item.DataContext is XRenderable renderable)
                {
                    foreach (var xlayer in context.Layers)
                    {
                        for (int i = 0; i < context.Layers.Count; i++)
                        {
                            if (context.Layers[i].Shapes.Contains(renderable))
                                context.Layers[i].Shapes.Remove(renderable);
                        }
                    }
                }
            }
        }

        private void Copy(object sender, RoutedEventArgs e)
        {
            var context = DataContext as XImage;
            var item = sender as MenuItem;

            if (context != null)
            {
                if (item.DataContext is XLayer layer && context.Layers.Count > 1)
                {
                    context.Layers.Remove(layer);
                    context.ActiveLayer = context.Layers.First();
                }

                if (item.DataContext is XRenderable renderable)
                {
                    foreach (var xlayer in context.Layers)
                    {
                        for (int i = 0; i < context.Layers.Count; i++)
                        {
                            if (context.Layers[i].Shapes.Contains(renderable))
                                context.Layers[i].Shapes.Remove(renderable);
                        }
                    }
                }
            }
        }

        private void Paste(object sender, RoutedEventArgs e)
        {
            var context = DataContext as XImage;
            var item = sender as MenuItem;

            if (context != null)
            {
                if (item.DataContext is XLayer layer && context.Layers.Count > 1)
                {
                    context.Layers.Remove(layer);
                    context.ActiveLayer = context.Layers.First();
                }

                if (item.DataContext is XRenderable renderable)
                {
                    foreach (var xlayer in context.Layers)
                    {
                        for (int i = 0; i < context.Layers.Count; i++)
                        {
                            if (context.Layers[i].Shapes.Contains(renderable))
                                context.Layers[i].Shapes.Remove(renderable);
                        }
                    }
                }
            }
        }
    }
}
