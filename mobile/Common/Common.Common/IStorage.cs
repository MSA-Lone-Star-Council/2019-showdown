using System.Threading.Tasks;

namespace Common.Common
{
    public interface IStorage
    {
        Task SaveAsync(string key, string value);

        Task<string> GetStringAsync(string key, string defaultValue);

		void Save(string key, bool boolToSave);
		bool GetBool(string key);
    }
}