using System;
using System.Collections.Generic;

namespace Breadcrumbs.Core.Extensions
{
    public static class CollectionExtensions
    {
        public static IDictionary<TKey, TValue> GetDictionary<TKey, TValue>(this IReadOnlyCollection<TValue> collection, Func<TValue, TKey> keySelector)
        {
            var result = new Dictionary<TKey, TValue>(collection.Count);

            foreach (var item in collection)
            {
                var key = keySelector(item);
                if (result.ContainsKey(key))
                    continue;

                result.Add(key, item);
            }

            return result;
        }
    }
}
