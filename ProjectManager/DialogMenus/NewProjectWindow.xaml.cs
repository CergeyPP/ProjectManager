using ProjectManager.Core.Modules;
using ProjectManager.Core.Modules.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Application = System.Windows.Application;

namespace ProjectManager.DialogMenus
{
    /// <summary>
    /// Логика взаимодействия для NewProjectWindow.xaml
    /// </summary>
    public partial class NewProjectWindow : Window
    {
        private ProjectController _projectController;

        public NewProjectWindow()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            ProjectManagerApp app = Application.Current as ProjectManagerApp;
            _projectController = app.ProjectController;
        }

        private void CreateClick(object sender, RoutedEventArgs e)
        {
            _projectController.CreateNewProject(
                    ProjectName.Text,
                    ProjectDescription.Text,
                    FolderPath.Text
                );

            Close();
        }
        private void CancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void PathClick(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            var result = fbd.ShowDialog();

            if (result.ToString() != string.Empty)
            {
                FolderPath.Text = fbd.SelectedPath;
            }
        }
    }
}
