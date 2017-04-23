# Quickstart (iOS)
For on-device development and distribution

## Add your device in the Apple Developer Center
1. Get the login information for the LSC developer account
2. Add your phone's UDID to the account - Instructions for finding UDID and adding it can be easily Googled

## Set up for Development
Before proceeding, have fastlane installed and availabe on your ```PATH```
```bash
fastlane match development --force_for_new_devices # In this directory
```

In Xamarin Client.iOS bundle signing settings, use the ```match development certificate for org.msa-texas.showdown.admin``` 
provisioning profile.

## Set up for Distribution
**CURRENTLY, THE APP IS REJECTED FROM THE APP STORE. THESE INSTRUCTIONS "GET AROUND" THAT**

*For each device that will be using this app, make sure they are registered in the Apple Developer Center!*

Before proceeding, have fastlane installed and availabe on your ```PATH```
```bash
fastlane match adhoc --force_for_new_devices # In this directory
```

In Xamarin Client.iOS bundle signing settings, use the ```match adhoc certificate for org.msa-texas.showdown.admin``` 
provisioning profile.

Archive the IPA bundle and then distribute with [Diawi](https://www.diawi.com/)
