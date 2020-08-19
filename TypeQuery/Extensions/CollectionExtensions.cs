using System;
using System.Collections.Generic;
using System.Text;
using TypeQuery.Collections;

namespace TypeQuery.Extensions
{
    public static class CollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> collection, params T[] items)
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }

        public static ParameterDictionary ToParameterDictionary(this IEnumerable<KeyValuePair<string, object>> collection)
        {
            return new ParameterDictionary(collection);
        }
    }
}
