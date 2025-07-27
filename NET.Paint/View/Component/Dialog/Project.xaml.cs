using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Service;
using System.Windows;
using System.Windows.Controls;

namespace NET.Paint.View.Component.Dialog
{
    /// <summary>
    /// Interaction logic for ProjectDialog.xaml
    /// </summary>
    public partial class Project : UserControl
    {
        public static readonly DependencyProperty ProjectTemplateProperty =
            DependencyProperty.Register("ProjectTemplate", typeof(XProject), typeof(Project),
                new PropertyMetadata(new XProject()));

        public XProject ProjectTemplate
        {
            get { return (XProject)GetValue(ProjectTemplateProperty); }
            set { SetValue(ProjectTemplateProperty, value); }
        }

        public Project()
        {
            InitializeComponent();
        }

        private void Create(object sender, RoutedEventArgs e)
        {
            if (DataContext is XService service)
            {
                service.Project = ProjectTemplate;
                ProjectTemplate = new XProject();
                service.Preferences.ProjectDialogVisible = false;
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            if (DataContext is XService service)
                service.Preferences.ProjectDialogVisible = false;
        }
    }
}
