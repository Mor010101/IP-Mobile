using Microsoft.Maui.Controls;

namespace Mobile_IP;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
		BindingContext = new ViewModels.PulsViewModel();
	}

	private async void OnEditImageButton_Clicked(object sender, EventArgs e)
	{
		SetEntryTextToLabelText();
		ActivateSetMaxValuesFrame();

		maxValuesFrame.IsEnabled = false;

		setMaxValuesFrame.Opacity = 0;
		await Microsoft.Maui.Controls.ViewExtensions.FadeTo(setMaxValuesFrame, 1);
	}

	private void SetEntryTextToLabelText()
	{
		bpmMaxValueEntry.Text = bpmMaxValueLabel.Text;
		tempMaxValueEntry.Text = tempMaxValueLabel.Text.Substring(0, tempMaxValueLabel.Text.Length - 1);
	}

	private void ActivateSetMaxValuesFrame()
	{
		setMaxValuesFrame.IsEnabled = true;
		setMaxValuesFrame.IsVisible = true;
	}

	private async void OnSaveButton_Clicked(object sender, EventArgs e)
	{
		SetLabelTextToEntryText();

        maxValuesFrame.IsEnabled = true;

		await Microsoft.Maui.Controls.ViewExtensions.FadeTo(setMaxValuesFrame, 0);
		DeactivateSetMaxValuesFrame();
	}

	private void SetLabelTextToEntryText()
	{
        bpmMaxValueLabel.Text = bpmMaxValueEntry.Text;
        tempMaxValueLabel.Text = tempMaxValueEntry.Text + "°";
    }

	private void DeactivateSetMaxValuesFrame()
	{
		setMaxValuesFrame.IsEnabled = false;
		setMaxValuesFrame.IsVisible = false;
	}
}