using Mobile_IP.ViewModels;

namespace Mobile_IP.Pages;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		//InitializeComponent();
		var vm = new LoginViewModel();
		this.BindingContext = vm;
		vm.DisplayInvalidLoginPrompt += () => DisplayAlert("Error", "Invalid Login, try again", "OK");
		InitializeComponent();

        emailEntry.Completed += (object sender, EventArgs e) =>
		{
			passwordEntry.Focus();
		};

        passwordEntry.Completed += (object sender, EventArgs e) =>
		{
			vm.SubmitCommand.Execute(null);
		};
	}
}