namespace SOS;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();

        Init();
	}

    private async void Init()
    {
        var settingsDbResponse = await App.SettingsRepo.IsNumberSavedAsync();
        if (settingsDbResponse.Status == false)
        {
            await DisplayAlert("Alert", "Please add SOS number", "Ok");
        }
    }

    private async void SOSButtonClicked(object sender, EventArgs e)
    {
        var settingsDbResponse = await App.SettingsRepo.IsNumberSavedAsync();
        if (settingsDbResponse.Status == false)
        {
            await DisplayAlert("Alert", "SOS number missing", "Ok");
            return;
        }

        var settings = settingsDbResponse.SettingsData;
        SOSButton.BackgroundColor = Colors.Crimson;
        await DisplayAlert("Alert", $"Saved number: {settings.PhoneNumber}", "Ok");
        SOSButton.BackgroundColor = Colors.Red;
    }
}
