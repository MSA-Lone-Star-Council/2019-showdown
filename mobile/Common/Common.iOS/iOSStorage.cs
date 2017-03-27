using System;
using System.Threading.Tasks;
using Common.Common;
using Foundation;

namespace Common.iOS
{
	public class iOSStorage : IStorage
	{

		public Task<string> GetStringAsync(string key, string defaultValue)
		{
			var plist = NSUserDefaults.StandardUserDefaults;
			string result = plist.StringForKey(key);
			return Task.FromResult(result ?? defaultValue);
		}

		public Task SaveAsync(string key, string result)
		{
			var plist = NSUserDefaults.StandardUserDefaults;
			plist.SetString(result, key);
			plist.Synchronize();

			return Task.CompletedTask;
		}
	}
}
