using System;
using System.Configuration;
using System.Linq;
using System.Windows.Controls;
using ProjectManager.Core.Model.Project;
using ProjectManager.Core.Modules;

namespace ProjectManager.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProjectsPreviewPage.xaml
    /// </summary>
    public partial class ProjectsPreviewPage : Page
    {

        public ProjectsPreviewPage()
        {
            InitializeComponent();
            ProjectLinkedPathConfigSection section = ConfigurationManager.GetSection("ProjectLinkedPathSection") as ProjectLinkedPathConfigSection;

            ProjectList.ItemsSource = section.LinkedPaths.Cast<ProjectLinkedPath>();
        }
    }
}
