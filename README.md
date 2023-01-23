# SOS-Function

## 🤖 Documentation

Reverse geolocate the inputted coordinates using [Radar](https://radar.com) and sends SOS message with those details to the inputted phone number via [Twilio](https://twilio.com)

_Example input:_

```json
{
    "latitude": "40.70390",
    "longitude": "-73.98670",
    "phoneNumber": "+919876543210"
}
```

_Example success output:_

```json
{
    "sos": true
}
```

_Example failure output:_

```json
{
    "sos": false
}
```

> ℹ️ _Error logs are shown as console output on the Appwrite Function's console._

## 📝 Environment Variables

Go to Settings tab of your Appwrite Function and add the following environment variables:

- `TWILIO_ACCOUNT_SID`: Twilio Account SID
- `TWILIO_AUTH_TOKEN`: Twilio Auth Token
- `TWILIO_PHONE_NUMBER`: Twilio Phone Number to make the call from
- `RADAR_SECRET`: Radar API Key

> ℹ️ _The Twilio Account SID and Auth Token can be obtained from your Twilio console. You can purchase a Twilio phone number using [this guide](https://support.twilio.com/hc/en-us/articles/223135247-How-to-Search-for-and-Buy-a-Twilio-Phone-Number-from-Console). Radar's API key can be obtained from their Dashboard_

## 🚀 Deployment

There are two ways of deploying the Appwrite function, both having the same results, but each using a different process. We highly recommend using CLI deployment to achieve the best experience.

### Using CLI

Make sure you have [Appwrite CLI](https://appwrite.io/docs/command-line#installation) installed, and you have successfully logged into your Appwrite server. To make sure Appwrite CLI is ready, you can use the command `appwrite client --debug` and it should respond with green text `✓ Success`.

Make sure you are in the same folder as your `appwrite.json` file and run `appwrite deploy function` to deploy your function. You will be prompted to select which functions you want to deploy.

### Manual using tar.gz

Manual deployment has no requirements and uses Appwrite Console to deploy the tag. First, enter the folder of your function. Then, create a tarball of the whole folder and gzip it. After creating `.tar.gz` file, visit Appwrite Console, click on the `Deploy Tag` button and switch to the `Manual` tab. There, set the `entrypoint` to `src/Index.cs`, and upload the file we just generated.
