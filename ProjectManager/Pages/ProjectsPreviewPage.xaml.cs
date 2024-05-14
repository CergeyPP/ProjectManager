using System;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks.Sources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using ProjectManager.Core.Model.Project;
using ProjectManager.Core.Modules;
using ProjectManager.Core.Modules.Project;
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
        private ProjectController _projectController;
        public ProjectsPreviewPage()
        {
            InitializeComponent();
            _projectViewModel = Resources["ProjectVM"] as ProjectsViewModel;
            ProjectManagerApp app = System.Windows.Application.Current as ProjectManagerApp;
            _projectController = app.ProjectController;
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

        private void SetPathClick(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            var result = fbd.ShowDialog();

            if (result.ToString() != string.Empty)
            {
                _projectController.TryAddOrCloneLocalPath(_projectViewModel.SelectedProject, fbd.SelectedPath);
                _projectViewModel.NotifyPropertyChanged("ProjectLocalPath");
            }
        }

        private void OpenFolderClick(object sender, RoutedEventArgs e)
        {
            Process.Start(_projectViewModel.ProjectLocalPath.Path); 
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

    public class NullToVisibleInverseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
