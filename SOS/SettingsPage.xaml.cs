namespace SOS;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
	}

    private async void SaveSettingsButtonClicked(object sender, EventArgs e)
    {
		await DisplayAlert("Alert", "Button Works", "Ok");
    }
}
