using System;
using System.Windows;
using System.Windows.Controls;
using ProjectManager.Core.ViewModel;
using ProjectManager.Core.Model;
using System.Linq;
using ProjectManager.Core.Modules.Employee;
using System.Windows.Input;

namespace ProjectManager.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    
    public class ProfilePageCommands
    {
        static ProfilePageCommands()
        {
            ChangeEditsCommand = new RoutedCommand("ChangeEdits", typeof(ProfilePage));
        }

        public static RoutedCommand ChangeEditsCommand;
    }

    public partial class ProfilePage : Page
    {
        private LoginController _loginController;
        public ProfilePage()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var loggedUserVM = Resources["LoggedUserVM"] as LoggedUserViewModel;

            ProjectManagerApp app = (ProjectManagerApp)Application.Current;
            _loginController = app.LoginController;
            _loginController.UserChanged += loggedUserVM.OnLoggedUserChanged;

            loggedUserVM.LoggedUser = _loginController.LoggedUser;
        }

        private void OnChangeEdits(object sender, ExecutedRoutedEventArgs e)
        {
            if (Family.Text == "")
                MessageBox.Show("Фамилия не может быть пустой");
            if (name.Text == "")
                MessageBox.Show("Имя не может быть пустым");
            if (GitUsername.Text == "")
                MessageBox.Show("Имя Git не может быть пустым");
            if (GitToken.Text == "")
                MessageBox.Show("Токен Git не может быть пустым");
            if (Password.Password != PasswordRetry.Password)
                MessageBox.Show("Пароли не совпадают. Проверьте корректность ввода");
            if (!Email.Text.Contains("@"))
                MessageBox.Show("E-mail должен содержать символ @");
            User newUser = new User(
                _loginController.LoggedUser.Id,
                Family.Text,
                name.Text,
                LastName.Text,
                GitUsername.Text,
                GitToken.Text,
                Email.Text);
            newUser.Role = _loginController.LoggedUser.Role;

            _loginController.ChangeUserSettings(_loginController.LoggedUser, Password.Password);
        }
    }
}
