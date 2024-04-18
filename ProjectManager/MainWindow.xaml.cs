using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ProjectManager.Core.ViewModel;
using ProjectManager.Core.Model;

namespace ProjectManager
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public class WindowCommands
    {
        static WindowCommands()
        {
            OpenPage = new RoutedCommand("OpenPage", typeof(MainWindow));
            Login = new RoutedCommand("Login", typeof(MainWindow));
            Logout = new RoutedCommand("Logout", typeof(MainWindow));
        }
        public static RoutedCommand OpenPage { get; set; }
        public static RoutedCommand Login { get; set; }
        public static RoutedCommand Logout { get; set; }
    }
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            Loaded += WindowLoaded;
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            LoggedUserViewModel loggedUserVM = Resources["LoggedUserVM"] as LoggedUserViewModel;

            ProjectManagerApp app = (ProjectManagerApp)Application.Current;
            var loginController = app.LoginController;
            loginController.UserChanged += loggedUserVM.OnLoggedUserChanged;

            CurrentPage.Navigated += PageNavigated;

            WindowCommands.OpenPage.Execute(typeof(Pages.LoginPage), this);
        }

        private void PageOpened(object sender, ExecutedRoutedEventArgs e)
        {
            if (CurrentPage.Navigate(Activator.CreateInstance(e.Parameter as Type) as Page) && CurrentPage.CanGoBack)
            {
                while (CurrentPage.CanGoBack)
                    CurrentPage.NavigationService.RemoveBackEntry();
            }
        }

        private void Logout(object sender, ExecutedRoutedEventArgs e)
        {
            ProjectManagerApp app = (ProjectManagerApp)Application.Current;
            var loginController = app.LoginController;
            if (loginController.TryLogout())
            {
                WindowCommands.OpenPage.Execute(typeof(Pages.LoginPage), this);
            }
        }

        private void PageNavigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            CurrentPage.NavigationService.RemoveBackEntry();
        }
    }
}
