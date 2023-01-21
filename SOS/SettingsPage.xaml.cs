using SOS.Models;
using System.Text.RegularExpressions;

namespace SOS;

public partial class SettingsPage : ContentPage
{
	public SettingsData settings;

	public SettingsPage()
	{
		InitializeComponent();

        Task.Run(async () => await SetNumberIfExists());
	}

	private async Task SetNumberIfExists()
	{
		var settingsDbResponse = await App.SettingsRepo.IsNumberSavedAsync();
		settings = settingsDbResponse.SettingsData;

		if(settingsDbResponse.Status is true)
		{
			SavedNumberLabel.Text = $"Saved Number: {settings.PhoneNumber}";
		}

		else 
		{
			SavedNumberLabel.Text = settingsDbResponse.StatusMessage;
			settings = new SettingsData();
		}
	}

    private async void SaveSettingsButtonClicked(object sender, EventArgs e)
    {        
		string phoneNumber = PhoneNumber.Text;

        Regex validatePhoneNumberRegex = new Regex("^\\+?[1-9][0-9]{7,14}$");
        
		if(String.IsNullOrEmpty(phoneNumber) || validatePhoneNumberRegex.IsMatch(phoneNumber) is not true)
		{
			await DisplayAlert("Alert", "Enter phone number in proper format", "Ok");
			return;
		}

		var settingsDbResponse = await App.SettingsRepo.SaveNumber(phoneNumber);

		if(settingsDbResponse.Status is false) 
		{
			await DisplayAlert("Error", settingsDbResponse.StatusMessage, "Ok");
        }

		await SetNumberIfExists();
    }
}
