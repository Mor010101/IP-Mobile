using Mobile_IP.ViewModel;

namespace Mobile_IP;

public partial class CalendarPage : ContentPage
{
	public CalendarPage()
	{
		InitializeComponent();
		BindingContext = new CalendarViewModel();
	}
}