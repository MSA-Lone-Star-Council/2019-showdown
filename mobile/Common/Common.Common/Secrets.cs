using System;
namespace Common.Common
{
    public static class Secrets
    {
        public const string BACKEND_URL = "http://app.msa-texas.org";

        public const string AzureConnectionString = @"AZURE_CONNECTION_STRING";
        public const string NotificationHubPath = @"NOTIFICATION_HUB_PATH";

        //This is located under https://developers.facebook.com/apps/
        public const string FacebookAppId = "FACEBOOK_APP_ID";
        public const string FacebookAppSecret = "FACEBOOK_APP_SECRET";

        // These are located at https://appcenter.ms/orgs/Lone-Star-Council/applications
        public const string AdminAndroidAppCenterSecret = null;
        public const string AdminiOSAppCenterSecret = null;
        public const string clientAndroidAppCenterSecret = null;
        public const string clientiOSAppCenterSecret = null;
        public const string scorekeeperAndroidAppCenterSecret = null;
        public const string scorekeeperiosAppCenterSecret = null;
    }
}