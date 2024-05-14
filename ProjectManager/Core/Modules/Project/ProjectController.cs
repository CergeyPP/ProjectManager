using LibGit2Sharp;
using ProjectManager.Core.Model.Project;
using ProjectManager.Core.Modules.Employee;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectManager.Core.Modules.Project
{
    public class ProjectController
    {
        private ProjectDatabaseProvider _databaseProvider;
        private ProjectLocalPathProvider _localPathProvider;

        private LoginController _loginController;

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

            return project == null ? null :_localPathProvider.GetProjectPathById(project.Id);
        }

        public ProjectController(ProjectDatabaseProvider databaseProvider, ProjectLocalPathProvider localPathProvider, LoginController loginController)
        {
            _databaseProvider = databaseProvider;
            _localPathProvider = localPathProvider;
            _loginController = loginController;
        }

        public void CreateNewProject(string name, string description, string localpath)
        {
            if (Repository.IsValid(localpath))
            {
                Repository repo = new Repository(localpath);
                string link = repo.Network.Remotes["origin"].Url;
                ProjectModel model = new ProjectModel();
                model.Name = name;
                model.Description = description;
                model.GitLink = link;
                int id = _databaseProvider.RegisterNewProject(model);
                if (id > 0)
                {
                    model.Id = (uint)id;
                }
                else
                {
                    MessageBox.Show("Не удалось добавить репозиторий", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                ProjectLinkedPath linkedPath = new ProjectLinkedPath(model.Id, localpath);
                _localPathProvider.AddOrUpdateProjectPathById(linkedPath);

                _projects.Add(model);
            }
            else
            {
                MessageBox.Show("Папка не является рабочей директорией Git", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void TryAddOrCloneLocalPath(ProjectModel model, string path)
        {
            if (model == null || path == "")
                return;

            if (!Repository.IsValid(path))
            {
                var co = new CloneOptions()
                {
                    FetchOptions =
                    {
                        CredentialsProvider = (_url, _user, _cred) => new UsernamePasswordCredentials
                        {
                            Username = "x-access-token",
                            Password = _loginController.LoggedUser.Token
                        }
                    }
                };

                try
                {
                    Repository.Clone(model.GitLink, path, co);
                    _localPathProvider.AddOrUpdateProjectPathById(
                        new ProjectLinkedPath(model.Id, path));
                }
                catch (LibGit2SharpException e)
                {
                    MessageBox.Show(e.Message);
                }
                
            }
            else
            {
                var repo = new Repository(path);
                string link = repo.Network.Remotes["origin"].Url;
                if (link == model.GitLink)
                    _localPathProvider.AddOrUpdateProjectPathById(
                        new ProjectLinkedPath(model.Id, path));
                else
                    MessageBox.Show($"Папка является рабочей директорией другого репозитория Git\nСсылка на гит проекта: {model.GitLink}\nСсылка на гит в директории: {repo.Info.Path}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

