using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProjectManager.Core.Model
{
    public class User
    {
        private uint _id = 0;
        private string _family = "";
        private string _name = "";
        private string _lastName = "";
        private string _gitUsername = "";
        private string _token = "";
        private string _email = "";
        private bool _isValid = false;
        private Role _role = new Role();
        public uint Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
                IsValid = Id > 0 && Role.Id > 0;
            }
        }
        public string Family { get
            {
                return _family;
            }
            set
            {
                _family = value;
                NotifyPropertyChanged("Family");
                UpdateFullName();
            } 
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                NotifyPropertyChanged("Name");
                UpdateFullName();
            }
        }
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
                NotifyPropertyChanged("LastName");
                UpdateFullName();
            }
        }
        public string GitUsername
        {
            get
            {
                return _gitUsername;
            }
            set
            {
                _gitUsername = value;
                NotifyPropertyChanged("GitUsername");
            }
        }
        public string Token
        {
            get
            {
                return _token;
            }
            set
            {
                _token = value;
                NotifyPropertyChanged("Token");
            }
        }
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                NotifyPropertyChanged("Email");
            }
        }
        public Role Role
        {
            get
            {
                return _role;
            }
            set
            {
                _role = value;
                NotifyPropertyChanged("Role");
                IsValid = Id > 0 && _role.Id > 0;
            }
        }

        public bool IsValid
        {
            get
            {
                return _isValid;
            }
            set
            {
                bool lastValid = _isValid;
                _isValid = value;
                if (lastValid != _isValid) 
                    NotifyPropertyChanged("IsValid");
            }
        }

        private string _fullName = "";
        private void UpdateFullName()
        {
            FullName = Family + " " + Name[0] + "." + (LastName != null && LastName.Length > 0 ? LastName[0].ToString() + '.' : "");

        }
        public string FullName
        {
            get
            {
                return _fullName;
            }
            set
            {
                _fullName = value;
                NotifyPropertyChanged();
            }
        }

        public User()
        {

        }

        public User(uint id, string family, string name, string lastName, string gitUsername, string token, string email)
        {
            Id = id;
            _family = family;
            _name = name;
            _lastName = lastName;
            _gitUsername = gitUsername;
            _token = token;
            _email = email;
            UpdateFullName();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
