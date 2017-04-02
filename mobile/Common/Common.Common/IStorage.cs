using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Common
{
    public interface IStorage
    {
        void Save(string key, string stringToSave);
		string GetString(string key, string defaultValue);

		void Save(string key, bool boolToSave);
		bool GetBool(string key);

		void AddToList(string key, string stringToSave);
		void RemoveFromList(string key, string stringToRemove);
		List<string> GetList(string key);
		void SaveList(string key, List<string> values);
    }
}