using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using System;
using Function.Models;

public async Task<RuntimeResponse> Main(RuntimeRequest req, RuntimeResponse res)
{
    try
    {
        var payload = JsonConvert.DeserializeObject<Dictionary<string, string>>(req.Payload);

        var phoneNumber = payload["phoneNumber"];
        var latitude = payload["latitude"];
        var longitude = payload["longitude"];

        var radarEndpoint = new Uri($"https://api.radar.io/v1/geocode/reverse?coordinates={latitude},{longitude}");

        HttpClient client = new HttpClient();
        client.BaseAddress = radarEndpoint;
        client.DefaultRequestHeaders.Add("Authorization", req.Variables["RADAR_SECRET"]);

        var radarApiResponse = await client.GetAsync(radarEndpoint);
        var radarApiResponseMessage = await radarApiResponse.Content.ReadAsStringAsync();
        var location = JsonConvert.DeserializeObject<RadarApiResponse>(radarApiResponseMessage);

        if (location != null)
        {
            TwilioClient.Init(req.Variables["TWILIO_ACCOUNT_SID"], req.Variables["TWILIO_AUTH_TOKEN"]);

            var message = MessageResource.Create(
                to: new PhoneNumber(phoneNumber),
                from: new PhoneNumber(req.Variables["TWILIO_PHONE_NUMBER"]),
                body: $"SOS ALERT:\n\nPlease get help at \n\nCoordinates: {latitude},{longitude}\nPossible Location: {location?.Addresses[0]?.AddressLabel}\n{location?.Addresses[0]?.FormattedAddress}"
            );

            return res.Json(new()
            {
                { "sos", true },
                { "radar", location },
                { "twilio", message }
            });
        }

        return res.Json(new()
        {
            { "sos", false }
        });
    }
    catch(Exception ex)
    {
        Console.WriteLine(ex.Message + "\n\n" + ex.Source + "\n\n" + ex.StackTrace + "\n\n" + ex.InnerException);
        return res.Json(new()
        {
            { "sos", false }
        });
    }
}