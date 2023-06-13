using Mobile_IP.Models;
using Nancy.Json;
using System.ComponentModel;
using System.Net;
using System.Windows.Input;

namespace Mobile_IP.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public Action DisplayInvalidLoginPrompt;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private readonly Backend backend = new Backend();
        private string email;
        private int loginAttempts = 0;
        private const int maxLoginAttempts = 3;
        private const int loginCooldown = 30;
        private bool isCooldownActive = false;

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
            SubmitCommand = new Command(OnSubmit);

            ServicePointManager.SecurityProtocol =
                SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        }
        public async void OnSubmit()
        {
            if (isCooldownActive)
            {
                return;
            }

            var values = new Dictionary<string, string>
            {
                  { "email", email},
                  { "password", password}
            };

            backend.PostJsonAndGetResponseAsync("api/Auth", values);
            try
            {
                if (VerifyLoginCredentials(backend))
                {
                    Application.Current.MainPage = new AppShell();
                }

                TokenClass token = backend.DeserializeResponse<TokenClass>();
                backend.AddRequestHeader("Authorization", token.Token);
            }
            catch (Exception e)
            {
                await HandleInvalidLogin();
            }
        }
        
        private bool VerifyLoginCredentials(Backend backend)
        {
            if (backend.IsResponseStatusCodeOk())
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
            if (loginAttempts > maxLoginAttempts)
            {
                isCooldownActive = true;
                await Application.Current.MainPage.DisplayAlert("Login failed", $"You have reached the maximum number of login attempts. Please try again after {loginCooldown} seconds.", "OK");
                await Task.Delay(TimeSpan.FromSeconds(loginCooldown));
                isCooldownActive = false;
                loginAttempts = 0;
            }   
            else
            {
                DisplayInvalidLoginPrompt();
            }
        }
    }
}
