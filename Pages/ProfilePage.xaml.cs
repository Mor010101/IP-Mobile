using Mobile_IP.Pages;

namespace Mobile_IP;

public partial class ProfilePage : ContentPage
{
	public ProfilePage()
	{
		InitializeComponent();
	}

    private void OnLogoutButton_Clicked(object sender, EventArgs e)
    {
		Application.Current.MainPage = new LoginPage();
    }
}