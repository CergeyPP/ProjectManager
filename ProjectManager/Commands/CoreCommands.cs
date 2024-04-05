using System.Windows.Input;

namespace ProjectManager.Commands
{
    public class CoreCommands
    {
        static CoreCommands()
        {
            Login = new RoutedCommand("Login", typeof(MainWindow));
            Logout = new RoutedCommand("Logout", typeof(MainWindow));
        }
        public static RoutedCommand OpenPage { get; set; }
        public static RoutedCommand Login { get; set; }
        public static RoutedCommand Logout { get; set; }
    }
}
