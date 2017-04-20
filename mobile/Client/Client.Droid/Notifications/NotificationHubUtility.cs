using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Client.Common;
using Common.Common;
using WindowsAzure.Messaging;
using Android.Util;
using System.Threading.Tasks;

namespace Client.Droid
{
    public class NotificationHubUtility : INotificationHub
    {
        Context context;

        public string Token { get; set; }

        public NotificationHubUtility(Context c)
        {
            context = c;
        }

        public async Task SaveTags(List<string> tags)
        {
            var hub = new NotificationHub(Secrets.NotificationHubPath, Secrets.AzureConnectionString, context);
            try
            {
                await Task.Run(() => hub.UnregisterAll(Token));
                await Task.Run(() => hub.Register(Token, tags.ToArray()));
            }
            catch (Exception ex)
            {
                Log.Error("ShowdownApp", ex.Message);
            }
        }
    }
}