using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProjectManager.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public static RoutedCommand LoginCommand = new RoutedCommand("Login", typeof(LoginPage));
        public LoginPage()
        {
            InitializeComponent();
        }

        private void Login(object sender, ExecutedRoutedEventArgs e)
        {
            ProjectManagerApp app = (ProjectManagerApp)Application.Current;
            var loginController = app.LoginController;

            if (loginController.TryLogin(emailTextBox.Text, passwordTextBox.Password))
            {
                // супер, залогировались
            }
            else
            {
                ErrorTextBox.Text = "Ошибка при входе, проверьте данные";
                ErrorTextBox.Visibility = Visibility.Visible;
            }
        }
        private void CanLogin(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = emailTextBox.Text.Length > 0 && emailTextBox.Text.Contains("@") && passwordTextBox.Password.Length > 0;
        }
    }
}
