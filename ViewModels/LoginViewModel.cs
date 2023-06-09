using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mobile_IP.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public Action DisplayInvalidLoginPrompt;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private string email;
        private int loginAttempts = 0;
        private const int maxLoginAttempts = 3;
        private const int loginCooldown = 30;
        private bool isSubmitCommandAvailable = true;

        public string Email
        { get { return email; } 
            set
            {
                email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }

        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }
        public ICommand SubmitCommand { protected set; get; }
        public LoginViewModel()
        {
            SubmitCommand = new Command(OnSubmit, CanExecuteSubmitCommand);
        }

        public bool IsSubmitCommandAvailable
        {
            get { return isSubmitCommandAvailable; }
            set
            {
                isSubmitCommandAvailable = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsSubmitCommandAvailable"));
            }
        }
        public async void OnSubmit()
        {
            if (VerifyLoginCredentials())
            {
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                //DisplayInvalidLoginPrompt();
                await HandleInvalidLogin();
            }
        }
        private bool CanExecuteSubmitCommand()
        {
            return isSubmitCommandAvailable;
        }
        
        private bool VerifyLoginCredentials()
        {
            if (email == "test" && password == "test")
            {
                loginAttempts = 0;
                return true;
            } else
            {
                loginAttempts++;
                return false;
            }
        }
        private async Task HandleInvalidLogin()
        {
            if (loginAttempts == maxLoginAttempts)
            {
                IsSubmitCommandAvailable = false;
                await Application.Current.MainPage.DisplayAlert("Login failed", $"You have reached the maximum number of login attempts. Please try again after {loginCooldown} seconds.", "OK");
                await Task.Delay(TimeSpan.FromSeconds(loginCooldown));
                isSubmitCommandAvailable = true;
            }   
            else
            {
                DisplayInvalidLoginPrompt();
            }
        }
    }
}
