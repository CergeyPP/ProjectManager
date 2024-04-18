using ProjectManager.Core.Model.Project;
using ProjectManager.Core.Modules.Project;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectManager.Core.ViewModel
{

    public class ProjectsViewModel : DependencyObject, INotifyPropertyChanged
    {
        public ProjectModel SelectedProject 
        {
            get { return (ProjectModel)GetValue(SelectedProjectProperty); }
            set { SetValue(SelectedProjectProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedProjectProperty =
            DependencyProperty.Register("SelectedProject", typeof(ProjectModel), typeof(ProjectsViewModel), new PropertyMetadata(null));

        public ObservableCollection<ProjectModel> Projects { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] string nomPropriete = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(nomPropriete));
        }

        private ProjectController _controller;

        public ProjectsViewModel()
        {
            var app = Application.Current as ProjectManagerApp;
            _controller = app.ProjectController;

            Projects = _controller.Projects;
            _controller.LoadProjects();
        }
    }
}
