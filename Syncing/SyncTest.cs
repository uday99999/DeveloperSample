using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DeveloperSample.Syncing
{
    public class SyncTest
    {
        [Fact]
        public async void CanInitializeCollection()
        {
            var items = new List<string> { "one", "two" };
            var result = await SyncDebug.InitializeList(items);
            Assert.Equal(items.Count, result.Count);
        }

        [Fact]
        public async Task ItemsOnlyInitializeOnce()
        {
            var count = 0;
            var dictionary = await SyncDebug.InitializeDictionary(i =>
            {
                Interlocked.Increment(ref count);
                return i.ToString();
            });

            Assert.Equal(100, count); 
            Assert.Equal(100, dictionary.Count);
        }
    }
}