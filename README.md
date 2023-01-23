# SOS App

![SOS-Maui](https://user-images.githubusercontent.com/31401437/214029540-d7256f29-561d-4135-aa4e-e958b55ab7ad.png)

## Description

**SOS App** is a cross-platform app that allows the user to send an SOS message with their location to a saved phone number in times of distress.

##  Components

* The [```main```](https://github.com/adityaoberai/SOS-MAUI) branch contains the **.NET MAUI 6.0** project used to build the Android app that gets the coordinates of the phone through the **.NET MAUI Essentials Geolocation API** and call the SOS **Appwrite Function**. 

* The [```appwrite-function```](https://github.com/adityaoberai/SOS-MAUI/tree/appwrite-function) branch contains the **Azure Function** that reverse geocodes the coordinates to get the address from the **Radar Geocoding API** and uses **Twilio Programmable Message** to send an SOS message to predecided number.   

## Demo

https://user-images.githubusercontent.com/31401437/214029495-2fcddc9a-a88f-44f1-a676-bbb80d2ef9a9.mp4

