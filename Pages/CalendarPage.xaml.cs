using Mobile_IP.ViewModels;

namespace Mobile_IP;

public partial class CalendarPage : ContentPage
{
	public CalendarPage()
	{
		InitializeComponent();
		BindingContext = new EventCalendarViewModel();
	}
}