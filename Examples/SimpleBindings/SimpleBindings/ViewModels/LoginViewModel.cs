using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace SimpleBindings.ViewModels
{
    public class LoginViewModel : Screen
    {
        public LoginViewModel()
        {
            Username = DateTime.Now.ToString();
            Password = "password";
        }

        public string Username
        {
            get { return _username; }
            set
            {
                if (value == _username)
                    return;
                _username = value;
                NotifyOfPropertyChange();
            }
        }

        private string _username;

        public string Password
        {
            get { return _password; }
            set
            {
                if (value == _password)
                    return;
                _password = value;
                NotifyOfPropertyChange();
            }
        }

        private string _password;

        public async Task AuthenticateAsync()
        {
            Debug.WriteLine("AuthenticateAsync()");
        }
    }
}
