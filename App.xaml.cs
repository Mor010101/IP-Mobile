using Mobile_IP.Pages;

namespace Mobile_IP;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		//MainPage = new AppShell();
		MainPage = new LoginPage();
	}
}
