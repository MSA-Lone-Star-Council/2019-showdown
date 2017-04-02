using System;
using System.Threading.Tasks;
using Common.Common;
using Foundation;

namespace Common.iOS
{
	public class iOSStorage : IStorage
	{
		public bool GetBool(string key)
		{
			var plist = NSUserDefaults.StandardUserDefaults;
			var result = plist.BoolForKey(key);
			return result;
		}

		public Task<string> GetStringAsync(string key, string defaultValue)
		{
			var plist = NSUserDefaults.StandardUserDefaults;
			string result = plist.StringForKey(key);
			return Task.FromResult(result ?? defaultValue);
		}

		public void Save(string key, bool boolToSave)
		{
			var plist = NSUserDefaults.StandardUserDefaults;
			plist.SetBool(boolToSave, key);
			plist.Synchronize();
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
