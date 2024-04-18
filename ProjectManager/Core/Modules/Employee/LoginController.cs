using ProjectManager.Core.Model;
using System;

namespace ProjectManager.Core.Modules.Employee
{
    public class LoginController
    {
        private User _loggedUser = null;
        public event Action<User> UserChanged;

        public User LoggedUser
        {
            get
            {
                return _loggedUser;
            }
            private set
            {
                _loggedUser = value;
                UserChanged.Invoke(_loggedUser);
            }
        }

        private UserDatabaseProvider _userDatabaseProvider;
        public LoginController(UserDatabaseProvider userDatabaseProvider)
        {
            _userDatabaseProvider = userDatabaseProvider;
        }

        public bool TryLogin(string email, string password)
        {
            User loginResult = _userDatabaseProvider.GetUserByEmailAndPassword(email, password);
            if (loginResult != null && loginResult.IsValid)
            {
                LoggedUser = loginResult;
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool TryLogout()
        {
            if (LoggedUser != null)
            {
                LoggedUser = null;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ChangeUserSettings(User user, string password = "")
        {
            if (user == null) return;
            if (_userDatabaseProvider.UpdateUserByID(user.Id, user, password))
            {
                LoggedUser = user;
            }
        }
    }
}
