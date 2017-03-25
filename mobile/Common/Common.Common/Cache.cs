using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Common
{
    public class Cache<T>
    {
        public TimeSpan TimeToLive { get; set; }
        public Func<T, string> KeyFunc { get; set; }
        public Func<string, Task<T>> FetchFunc { get; set; }


        private ConcurrentDictionary<string, CacheRecord<T>> _cache;

        public Cache(TimeSpan timeToLive, Func<T, string> keyFunc, Func<string, Task<T>> fetch)
        {
            TimeToLive = timeToLive;
            KeyFunc = keyFunc;
            FetchFunc = fetch;

            _cache = new ConcurrentDictionary<string, CacheRecord<T>>();
        }

        public async Task<T> GetAsync(string key)
        {
            CacheRecord<T> record;
            var found = _cache.TryGetValue(key, out record);

            if (found)
            {
                // Hasn't expired yet
                if (record.Expiration > DateTime.Now)
                {
                    return record.Value;
                }
            }

            var result =  await FetchFunc(key);
            AddItemToCache(key, result);

            return result;
        }

        public void Add(T item)
        {
            AddItemToCache(KeyFunc(item), item);
        }

        public void Add(IEnumerable<T> objects)
        {
            foreach (var item in objects)
            {
                Add(item);
            }
        }

        private void AddItemToCache(string key, T item)
        {
            var resultRecord = new CacheRecord<T>() {Value = item, Expiration = DateTime.Now + TimeToLive};

            _cache[key] = resultRecord;
        }

        private struct CacheRecord<T>
        {
            public T Value { get; set; }
            public DateTime Expiration { get; set; }
        }
    }
}