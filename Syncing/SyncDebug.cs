using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DeveloperSample.Syncing
{
    public class SyncDebug
    {
        public static async Task<List<string>> InitializeList(IEnumerable<string> items)
        {
            var bag = new ConcurrentBag<string>();

            var tasks = items.Select(async item =>
            {
                var result = await Task.Run(() => item).ConfigureAwait(false);
                bag.Add(result);
            });

            await Task.WhenAll(tasks).ConfigureAwait(false);

            return bag.ToList();
        }


        public static async Task<Dictionary<int, string>> InitializeDictionary(Func<int, string> getItem)
        {
            var itemsToInitialize = Enumerable.Range(0, 100).ToList();
            var concurrentDictionary = new ConcurrentDictionary<int, string>();

            var tasks = Enumerable.Range(0, 3)
                .Select(_ => Task.Run(() =>
                {
                    foreach (var item in itemsToInitialize)
                    {
                        concurrentDictionary.AddOrUpdate(item, key => getItem(key), (_, existingValue) => existingValue);
                    }
                }))
                .ToArray();

            await Task.WhenAll(tasks).ConfigureAwait(false);

            return concurrentDictionary.ToDictionary(kv => kv.Key, kv => kv.Value);
        }

    }
}