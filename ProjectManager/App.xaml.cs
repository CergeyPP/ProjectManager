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

namespace ProjectManager
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class ProjectManagerApp : Application
    {
        private DatabaseProvider _databaseProvider;
        private UserDatabaseProvider _userDatabaseProvider;
        private LoginController _loginController;

        public LoginController LoginController => _loginController;
        private void ApplicationStartup(object sender, StartupEventArgs e)
        {
            _databaseProvider = new DatabaseProvider();
            _userDatabaseProvider = new UserDatabaseProvider(_databaseProvider);
            _loginController = new LoginController(_userDatabaseProvider);
        }
    }
}
