using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Nancy.Json;

namespace Mobile_IP.ViewModels
{
    internal class LoginViewModel : INotifyPropertyChanged
    {
        public Action DisplayInvalidLoginPrompt;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://34.140.195.43:80/api/Auth");
        StreamWriter streamWriter;


        private string email;
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
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        }
        public void OnSubmit()
        {
            var values = new Dictionary<string, string>
            {
                  { "email", email},
                  { "password", password}
            };

            using (streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(values);
                streamWriter.Write(json);
                streamWriter.Flush();
            }

            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                if(httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    Application.Current.MainPage = new AppShell();
                }
            }
            catch(Exception e)
            {
                DisplayInvalidLoginPrompt();
            }

        }

    }
}
