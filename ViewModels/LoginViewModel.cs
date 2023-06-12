using Mobile_IP.Models;
using Nancy;
using Nancy.Json;
using System.ComponentModel;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Windows.Input;

using HttpStatusCode = System.Net.HttpStatusCode;

namespace Mobile_IP.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public Action DisplayInvalidLoginPrompt;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        HttpClient httpClient = new HttpClient();

        private string AuthUri { get => "http://34.140.195.43:80/"; }

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

            httpClient.BaseAddress = new Uri(AuthUri);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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

            HttpResponseMessage response = await httpClient.PostAsJsonAsync("api/Auth", values);
            try
            {
                if (VerifyLoginCredentials(response))
                {
                    Application.Current.MainPage = new AppShell();
                }

                TokenClass token = new JavaScriptSerializer().Deserialize<TokenClass>(response.Content.ReadAsStringAsync().Result);
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.Token);
            }
            catch (Exception e)
            {
                await HandleInvalidLogin();
            }
        }
        
        private bool VerifyLoginCredentials(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.OK)
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
