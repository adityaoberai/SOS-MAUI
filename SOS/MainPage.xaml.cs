namespace SOS;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    private async void SOSButtonClicked(object sender, EventArgs e)
    {
        SOSButton.BackgroundColor = Colors.Crimson;
        await DisplayAlert("Alert", "Button Works", "Ok");
        SOSButton.BackgroundColor = Colors.Red;
    }
}
