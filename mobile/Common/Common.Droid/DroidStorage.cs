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
using Common.Common;

namespace Common.Droid
{
    public class DroidStorage : IStorage
    {
        const string PreferenceFile = "ShowdownAppPreferences";

        private ISharedPreferences Preferences
        {
            get
            {
                return context.GetSharedPreferences(PreferenceFile, FileCreationMode.Private);
            }
        }

        private Context context;
        public DroidStorage(Context context)
        {
            this.context = context;
        }

        public void AddToList(string key, string stringToSave)
        {
            var prefEditor = Preferences.Edit();
            var stringSet = Preferences.GetStringSet(key, new List<string>());
            stringSet.Add(stringToSave);
            prefEditor.PutStringSet(key, stringSet);
            prefEditor.Commit();
        }

        public bool GetBool(string key)
        {
            return Preferences.GetBoolean(key, false);
        }

        public List<string> GetList(string key)
        {
            return Preferences.GetStringSet(key, new List<string>()).ToList<string>();
        }

        public string GetString(string key, string defaultValue)
        {
            return Preferences.GetString(key, defaultValue);
        }

        public void RemoveFromList(string key, string stringToRemove)
        {
            var prefEditor = Preferences.Edit();
            var stringSet = Preferences.GetStringSet(key, new List<string>());
            stringSet.Remove(stringToRemove);
            prefEditor.PutStringSet(key, stringSet);
            prefEditor.Commit();
        }

        public void Save(string key, string stringToSave)
        {
            var prefEditor = Preferences.Edit();
            prefEditor.PutString(key, stringToSave);
            prefEditor.Commit();
        }

        public void Save(string key, bool boolToSave)
        {
            var prefEditor = Preferences.Edit();
            prefEditor.PutBoolean(key, boolToSave);
            prefEditor.Commit();
        }

        public void SaveList(string key, List<string> values)
        {
            var prefEditor = Preferences.Edit();
            prefEditor.PutStringSet(key, values);
            prefEditor.Commit();
        }
    }
}