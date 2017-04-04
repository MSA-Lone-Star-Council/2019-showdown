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
    public class Storage : IStorage
    {
        public void AddToList(string key, string stringToSave)
        {
            throw new NotImplementedException();
        }

        public bool GetBool(string key)
        {
            throw new NotImplementedException();
        }

        public List<string> GetList(string key)
        {
            throw new NotImplementedException();
        }

        public string GetString(string key, string defaultValue)
        {
            throw new NotImplementedException();
        }

        public void RemoveFromList(string key, string stringToRemove)
        {
            throw new NotImplementedException();
        }

        public void Save(string key, string stringToSave)
        {
            throw new NotImplementedException();
        }

        public void Save(string key, bool boolToSave)
        {
            throw new NotImplementedException();
        }

        public void SaveList(string key, List<string> values)
        {
            throw new NotImplementedException();
        }
    }
}