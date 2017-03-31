using System;
using System.Threading.Tasks;
using Common.Common;
using NUnit.Framework;

namespace Common.Test
{
    [TestFixture]
    public class CacheTests
    {
        [Test]
        public async void TestCache()
        {
            var counter = 0;

            var cache = new Cache<int>(
                TimeSpan.FromMilliseconds(50),
                i => i.ToString(),
                s =>
                {
                    counter++;
                    return Task.FromResult(counter);
                }
            );

            int count1 = await cache.GetAsync("9");
            Assert.AreEqual(count1, 1);

            int count2 = await cache.GetAsync("9");
            Assert.AreEqual(count2, 1);

            await Task.Delay(100);

            int count3 = await cache.GetAsync("9");
            Assert.AreEqual(count3, 2);

            cache.Add(9);
            int count4 = await cache.GetAsync("9");
            Assert.AreEqual(count4, 9);

            await Task.Delay(100);

            int count5 = await cache.GetAsync("9");
            Assert.AreEqual(count5, 3);
        }
    }
}