using System;
using System.Collections.Generic;
using System.Linq;
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

		public string GetString(string key, string defaultValue)
		{
			var plist = NSUserDefaults.StandardUserDefaults;
			string result = plist.StringForKey(key);
			return result ?? defaultValue;
		}

		public void Save(string key, bool boolToSave)
		{
			var plist = NSUserDefaults.StandardUserDefaults;
			plist.SetBool(boolToSave, key);
			plist.Synchronize();
		}

		public void Save(string key, string stringToSave)
		{
			var plist = NSUserDefaults.StandardUserDefaults;
			plist.SetString(stringToSave, key);
			plist.Synchronize();
		}

		public Task SaveAsync(string key, string result)
		{
			var plist = NSUserDefaults.StandardUserDefaults;
			plist.SetString(result, key);
			plist.Synchronize();

			return Task.CompletedTask;
		}

		public List<string> GetList(string key)
		{
			var plist = NSUserDefaults.StandardUserDefaults;
			var result = plist.StringArrayForKey(key);
			if (result == null) return new List<string>();
			return result.ToList();
		}

		public void AddToList(string key, string stringToSave) // No duplicates...
		{
			var current = GetList(key);
			current.Add(stringToSave);
			current = new HashSet<string>(current).ToList(); // not super efficient, but should be ok
			SaveList(key, current);
		}

		public void RemoveFromList(string key, string stringToRemove)
		{
			var current = GetList(key);
			current.Remove(stringToRemove);
			SaveList(key, current);
		}

		public void SaveList(string key, List<string> values)
		{
			NSString[] strings = values.Select(s => new NSString(s)).ToArray();
			var plist = NSUserDefaults.StandardUserDefaults;
			NSArray<NSString> toSave = NSArray<NSString>.FromNSObjects(strings);
			plist.SetValueForKey(toSave, new NSString(key));
			plist.Synchronize();
		}

	}
}
