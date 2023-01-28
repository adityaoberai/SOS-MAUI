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

        var location = await ReverseGeocodeLocation(latitude, longitude, req.Variables["RADAR_SECRET"]);

        if (location != null)
        {
            var message = SendSOSMessage(req.Variables["TWILIO_ACCOUNT_SID"], req.Variables["TWILIO_AUTH_TOKEN"], phoneNumber, req.Variables["TWILIO_PHONE_NUMBER"], location);

            var call = SendSOSCall(req.Variables["TWILIO_ACCOUNT_SID"], req.Variables["TWILIO_AUTH_TOKEN"], phoneNumber, req.Variables["TWILIO_PHONE_NUMBER"], location);

            Console.WriteLine($"Radar response:\n\n{JsonConvert.SerializeObject(location, Formatting.Indented)}\n\n Twilio SMS response:\n\n{JsonConvert.SerializeObject(message, Formatting.Indented)}\n\n Twilio Call response:\n\n{JsonConvert.SerializeObject(call, Formatting.Indented)}");

            return res.Json(new()
            {
                { "sos", true }
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

public async Task<RadarApiResponse> ReverseGeocodeLocation(string latitude, string longitude, string radarSecret)
{
    var radarEndpoint = new Uri($"https://api.radar.io/v1/geocode/reverse?coordinates={latitude},{longitude}");

    HttpClient client = new HttpClient();
    client.BaseAddress = radarEndpoint;
    client.DefaultRequestHeaders.Add("Authorization", radarSecret);

    var radarApiResponse = await client.GetAsync(radarEndpoint);
    var radarApiResponseMessage = await radarApiResponse.Content.ReadAsStringAsync();

    return JsonConvert.DeserializeObject<RadarApiResponse>(radarApiResponseMessage);
}

public MessageResource SendSOSMessage(string twilioAccountSid, string twilioAuthToken, string toPhoneNumber, string twilioPhoneNumber, RadarApiResponse location)
{
    TwilioClient.Init(twilioAccountSid, twilioAuthToken);

    return MessageResource.Create(
        to: new PhoneNumber(toPhoneNumber),
        from: new PhoneNumber(twilioPhoneNumber),
        body: $"SOS ALERT:\n\nPlease get help at \n\nCoordinates: {location?.Addresses[0]?.Latitude},{location?.Addresses[0]?.Longitude}\nPossible Location: {location?.Addresses[0]?.AddressLabel}\n{location?.Addresses[0]?.FormattedAddress}"
    );
}

public CallResource SendSOSCall(string twilioAccountSid, string twilioAuthToken, string toPhoneNumber, string twilioPhoneNumber, RadarApiResponse location)
{
    TwilioClient.Init(twilioAccountSid, twilioAuthToken);

    return CallResource.Create(
        to: new PhoneNumber(toPhoneNumber),
        from: new PhoneNumber(twilioPhoneNumber),
        twiml: new Twiml($"<Response><Say>SOS Alert. Please get help at {location?.Addresses[0]?.AddressLabel}{location?.Addresses[0]?.FormattedAddress}. Check your SMS once for coordinates.</Say></Response>")
    );
}