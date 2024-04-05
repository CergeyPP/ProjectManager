using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using ProjectManager.Core.Model;

namespace ProjectManager.Core.ViewModel
{
    public class LoggedUserViewModel : DependencyObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName]string nomPropriete = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(nomPropriete));
        }
        public User LoggedUser
        {
            get { return (User)GetValue(LoggedUserProperty); }
            set { 
                SetValue(LoggedUserProperty, value); 
                IsLogged = value != null && value.IsValid;
                NotifyPropertyChanged();
            }
        }

        // Using a DependencyProperty as the backing store for LoggedUser.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LoggedUserProperty =
            DependencyProperty.Register("LoggedUser", typeof(User), typeof(LoggedUserViewModel), new PropertyMetadata(new User()));

        public bool IsLogged
        {
            get { return (bool)GetValue(IsLoggedProperty); }
            private set { 
                SetValue(IsLoggedProperty, value);
                NotifyPropertyChanged();
                IsNotLogged = !IsLogged;
            }
        }

        // Using a DependencyProperty as the backing store for IsLogged.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsLoggedProperty =
            DependencyProperty.Register("IsLogged", typeof(bool), typeof(LoggedUserViewModel), new PropertyMetadata(false));

        public bool IsNotLogged
        {
            get { return (bool)GetValue(IsNotLoggedProperty); }
            private set {
                SetValue(IsNotLoggedProperty, value);
                NotifyPropertyChanged("IsNotLogged");
            }
        }

        // Using a DependencyProperty as the backing store for IsNotLogged.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsNotLoggedProperty =
            DependencyProperty.Register("IsNotLogged", typeof(bool), typeof(LoggedUserViewModel), new PropertyMetadata(true));

        public LoggedUserViewModel()
        {
            LoggedUser = null;
        }
    }
}
