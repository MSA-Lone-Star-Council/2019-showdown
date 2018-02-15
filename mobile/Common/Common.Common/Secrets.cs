using System;
namespace Common.Common
{
	public class Secrets
	{
        public const string BACKEND_URL = "https://example.com";


        public const string AzureConnectionString = @"AzureConnectionStringExample";
		public const string NotificationHubPath = @"NotificationHubPathExample";

        //This is located underhttps://developers.facebook.com/apps/
        public const string FacebookAppId = "FacebookAppIdExample";

        // These are located at https://appcenter.ms/orgs/Lone-Star-Council/applications
        public const string AdminAndroidAppCenterSecret         = null;
        public const string AdminiOSAppCenterSecret             = "21ea6039-dc21-49bc-acf3-3c4d1c70b1d6";
        public const string clientAndroidAppCenterSecret        = "cdf659db-8534-41e6-8756-61230b0e97b9";
        public const string clientiOSAppCenterSecret            = "ae97a698-8672-400d-9cc6-d6ce2e3f46b2";
        public const string scorekeeperAndroidAppCenterSecret   = "4623fb04-ce8f-4db7-a946-cfc3d35ed05e";
        public const string scorekeeperiosAppCenterSecret       = null;

    }
}