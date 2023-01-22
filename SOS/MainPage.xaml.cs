using Newtonsoft.Json;
using SOS.Helpers;
using SOS.Models;

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
            SOSButton.BackgroundColor = Colors.Red;
            return;
        }

        SettingsData settings = settingsDbResponse.SettingsData;
        string phoneNumber = settings.PhoneNumber;
        
        SOSButton.BackgroundColor = Colors.Crimson;

        var coordinates = await App.LocationService.GetCurrentLocation();
        if (coordinates["found"] is false)
        {
            await DisplayAlert("Error", "Not able to get location", "Ok");
            SOSButton.BackgroundColor = Colors.Red;
            return;
        }

        string latitude = coordinates["latitude"].ToString();
        string longitude = coordinates["longitude"].ToString();

        NetworkAccess accessType = Connectivity.Current.NetworkAccess;

        if (accessType != NetworkAccess.Internet)
        {
            await LocalSMSApp(phoneNumber, latitude, longitude);
        }
        else
        {
            var endpoint = $"/v1/functions/{AppwriteConstants.FunctionId}/executions";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(AppwriteConstants.AppwriteUrl);
            client.DefaultRequestHeaders.Add("X-Appwrite-Response-Format", "1.0.0");
            client.DefaultRequestHeaders.Add("X-Appwrite-Project", AppwriteConstants.ProjectId);
            client.DefaultRequestHeaders.Add("X-Appwrite-Key", AppwriteConstants.ApiKey);

            Dictionary<string, string> requestData = new Dictionary<string, string>();
            requestData.Add("phoneNumber", settings.PhoneNumber);
            requestData.Add("latitude", coordinates["latitude"].ToString());
            requestData.Add("longitude", coordinates["longitude"].ToString());

            var appwriteRequestInput = new Dictionary<string, string>() 
            {
                { "data", JsonConvert.SerializeObject(requestData) }
            };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(appwriteRequestInput), System.Text.Encoding.UTF8, "application/json");

            var sosResponse = await client.PostAsync(endpoint, jsonContent);
            var sosResponseContent = await sosResponse.Content.ReadAsStringAsync();
            var sosResponseData = JsonConvert.DeserializeObject<AppwriteApiResponse>(sosResponseContent);

            var appwriteFunctionResponse = JsonConvert.DeserializeObject<Dictionary<string, object>>(sosResponseData.Response);
            if (Convert.ToBoolean(appwriteFunctionResponse["sos"]))
            {
                await DisplayAlert("Alert", $"Sent SOS Request to {settings.PhoneNumber}", "Ok");
            }
            else
            {
                await DisplayAlert("Alert", "SOS Message Not Sent\n\nOpening Local SMS App", "Ok");
                await LocalSMSApp(phoneNumber, latitude, longitude);
            }
        }
        SOSButton.BackgroundColor = Colors.Red;
    }

    private async Task LocalSMSApp(string phoneNumber, string latitude, string longitude)
    {
        if (Sms.Default.IsComposeSupported)
        {
            string[] recipients = new[] { phoneNumber };
            string text = $"SOS ALERT:\n\nPlease get help at \n\nCoordinates: {latitude},{longitude}";

            var message = new SmsMessage(text, recipients);

            await Sms.Default.ComposeAsync(message);
        }
    }
}
