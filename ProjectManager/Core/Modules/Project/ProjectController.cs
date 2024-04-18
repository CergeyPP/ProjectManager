using ProjectManager.Core.Model.Project;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Core.Modules.Project
{
    public class ProjectController
    {
        private ProjectDatabaseProvider _databaseProvider;
        private ProjectLocalPathProvider _localPathProvider;

        private ObservableCollection<ProjectModel> _projects = new ObservableCollection<ProjectModel>();
        public ObservableCollection<ProjectModel> Projects => _projects;

        public void LoadProjects()
        {
            _projects.Clear();
            foreach (var project in _databaseProvider.GetAllProjects())
            {
                _projects.Add(project);
            }
        }

        public ProjectLinkedPath GetProjectLocalPath(ProjectModel project)
        {
            return _localPathProvider.GetProjectPathById((int)project.Id);
        }

        public ProjectController(ProjectDatabaseProvider databaseProvider, ProjectLocalPathProvider localPathProvider)
        {
            _databaseProvider = databaseProvider;
            _localPathProvider = localPathProvider;
        }
    }
}
