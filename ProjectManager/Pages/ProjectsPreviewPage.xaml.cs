using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks.Sources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ProjectManager.Core.Model.Project;
using ProjectManager.Core.Modules;
using ProjectManager.Core.ViewModel;
using ProjectManager.DialogMenus;

namespace ProjectManager.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProjectsPreviewPage.xaml
    /// </summary>
    public partial class ProjectsPreviewPage : Page
    {
        private ProjectsViewModel _projectViewModel;

        public ProjectsPreviewPage()
        {
            InitializeComponent();
            _projectViewModel = Resources["ProjectVM"] as ProjectsViewModel;
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _projectViewModel.SelectedProject = ProjectList.SelectedItem as ProjectModel;
        }

        private void TaskPageClick(object sender, RoutedEventArgs e)
        {
            WindowCommands.OpenPage.Execute(typeof(TasksPage), this);
        }
        private void NewProjectClick(object sender, RoutedEventArgs e)
        {
            NewProjectWindow newProjectWindow = new NewProjectWindow();



            newProjectWindow.Show();
        }
        private void NewTaskClick(object sender, RoutedEventArgs e)
        {

        }
        private void DeleteTaskClick(object sender, RoutedEventArgs e)
        {

        }
    }

    public class NullToVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
