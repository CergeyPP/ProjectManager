using ProjectManager.Core.Modules;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ProjectManager.Core.Modules.Database;
using ProjectManager.Core.Modules.Employee;
using ProjectManager.Core.Modules.Project;
using ProjectManager.Core.Model.Project;

namespace ProjectManager
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class ProjectManagerApp : Application
    {
        private LoginController _loginController;
        private ProjectController _projectController;

        public LoginController LoginController => _loginController;
        public ProjectController ProjectController => _projectController;
        private void ApplicationStartup(object sender, StartupEventArgs e)
        {
            DatabaseProvider databaseProvider = new DatabaseProvider();

            UserDatabaseProvider userDatabaseProvider = new UserDatabaseProvider(databaseProvider);
            _loginController = new LoginController(userDatabaseProvider);

            ProjectDatabaseProvider projectDatabaseProvider = new ProjectDatabaseProvider(databaseProvider);
            ProjectLocalPathProvider projectLocalPathProvider = new ProjectLocalPathProvider();

            _projectController = new ProjectController(projectDatabaseProvider, projectLocalPathProvider);
        }
    }
}
