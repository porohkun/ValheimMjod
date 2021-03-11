using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ValheimMjod
{
    public static class CollectionExtensions
    {
        public static void AddRange<T>(this Collection<T> collection, IEnumerable<T> items)
        {
            foreach (var item in items)
                collection.Add(item);
        }
    }
}
