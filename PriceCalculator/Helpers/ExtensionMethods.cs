using System;
using System.Collections.Generic;

namespace PriceCalculator.Helpers
{
    public static class ExtensionMethods
    {
        public static void Update<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
                action(item);
        }
    }
}
