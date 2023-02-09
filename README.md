# SOS App

![SOS-Maui](https://user-images.githubusercontent.com/31401437/214029540-d7256f29-561d-4135-aa4e-e958b55ab7ad.png)

## Description

**SOS App** is a cross-platform app that allows the user to send an SOS message with their location to a saved phone number in times of distress.

##  Components

* The [`main`](https://github.com/adityaoberai/SOS-MAUI) branch contains the **.NET MAUI 6** project used to build the app that gets the coordinates of the phone through the **.NET MAUI Essentials Geolocation API** and call the SOS **Appwrite Function**. 

* The [`appwrite-function`](https://github.com/adityaoberai/SOS-MAUI/tree/appwrite-function) branch contains the **Appwrite Function** that reverse geocodes the coordinates to get the address from the **Radar Geocoding API** and uses **Twilio Programmable Message** to send an SOS message to predecided number.   

## Steps To Setup

### For SOS Appwrite Function

* [Setup an Appwrite instance](https://appwrite.io/docs/installation), create a new admin account and a new project
  * Enable the **.NET 6.0 runtime for Appwrite Functions** (check the note below)
* [Install the Appwrite CLI](https://appwrite.io/docs/command-line#installation) and login with your Appwrite credentials
* Create an account on [Twilio](https://twilio.com), obtain your Twilio Account SID and Auth Token from your Twilio console, and await a Phone Number (using this [guide](https://support.twilio.com/hc/en-us/articles/223135247-How-to-Search-for-and-Buy-a-Twilio-Phone-Number-from-Console))
* Create an account on [Radar](https://radar.com) and grab an API key (*Test secret(server) should be fine*)
* Visit the SOS Appwrite Function Readme in the [`appwrite-function`](https://github.com/adityaoberai/SOS-MAUI/tree/appwrite-function) branch for more details on setting up and deploying the function
  * Visit the SOS Function's Settings page and add **Execute Access** for `any` role

> Note: In order to enable the .NET runtime for Appwrite Functions, you need to update the `.env` file in the Appwrite installation folder. Find the file and add `dotnet-6.0` to the comma-separated list in the environment variable `_APP_FUNCTIONS_RUNTIMES`. This will make the .NET runtime available in Appwrite Functions. You can then load the updated configuration using the `docker-compose up -d` command.

### For .NET MAUI App

* Install latest version of **Visual Studio 2022** with the **.NET Multi-platform App UI development workload** ([Reference](https://learn.microsoft.com/en-us/dotnet/maui/get-started/installation?view=net-maui-6.0&tabs=vswin))
* Clone this repo
* Open the `SOS\` folder and run the following command
  ```sh
  dotnet restore
  ```
* Visit the `SOS\Constants` folder and create a class `AppwriteConstants.cs` as follows:
  ```csharp
  namespace SOS.Constants
  {
      public static class AppwriteConstants
      {
          public const string AppwriteUrl = "<Enter Appwrite API Endpoint>";
          public const string ProjectId = "<Enter Appwrite Project Id>";
          public const string FunctionId = "<Enter Appwrite Function Id>";
      }
  }
  ```
* Build your app and deploy it to your preferred mobile platform ([Reference](https://learn.microsoft.com/en-us/dotnet/maui/get-started/first-app?view=net-maui-6.0&tabs=vswin&pivots=devices-android))

## Demo

https://user-images.githubusercontent.com/31401437/214029495-2fcddc9a-a88f-44f1-a676-bbb80d2ef9a9.mp4

## Attribution

Thanks to Freepik for the [menu](https://www.flaticon.com/free-icon/menu_2976215), [settings](https://www.flaticon.com/free-icon/setting_2040504), and [sos](https://www.flaticon.com/free-icon/sos_2133802) icons used in the flyout menu in the app
