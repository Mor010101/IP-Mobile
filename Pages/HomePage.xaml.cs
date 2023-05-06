namespace Mobile_IP;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

    private void OnEditImageButton_Clicked(object sender, EventArgs e)
    {
        setMaxValuesFrame.IsEnabled = true;
        setMaxValuesFrame.IsVisible = true;

        maxValuesFrame.IsEnabled = false;
    }

    private void OnSaveButton_Clicked(object sender, EventArgs e)
    {
        setMaxValuesFrame.IsEnabled = false;
        setMaxValuesFrame.IsVisible = false;

        maxValuesFrame.IsEnabled = true;
    }
}