# Quickstart (iOS)
For on-device development and distribution

## Add your device in the Apple Developer Center
1. Get the login information for the LSC developer account
2. Add your phone's UDID to the account - Instructions for finding UDID and adding it can be easily Googled

## Set up for Development
Before proceeding, have fastlane installed and availabe on your ```PATH```
```bash
fastlane match development --force_for_new_devices # In this directory
fastlane pem --development # Upload the p12 file to Notification Hub to work with push notifications sandbox
```

In Xamarin Client.iOS bundle signing settings, use the ```match development certificate for org.msa-texas.showdown``` 
provisioning profile.

## Set up for Distribution
Before proceeding, have fastlane installed and availabe on your ```PATH```
```bash
fastlane match appstore --force_for_new_devices # In this directory
fastlane pem # Upload the p12 file to Notification Hub to work with push notifications production
```

In Xamarin Client.iOS bundle signing settings, use the ```match appstore certificate for org.msa-texas.showdown``` 
provisioning profile.

