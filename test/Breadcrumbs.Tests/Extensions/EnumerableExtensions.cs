using System;
using System.Collections.Generic;
using System.Linq;

namespace Breadcrumbs.Tests.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random randomizer)
        {
            var elements = source.ToArray();

            for (var i = elements.Length - 1; i >= 0; i--)
            {
                var swapIndex = randomizer.Next(i + 1);

                yield return elements[swapIndex];

                elements[swapIndex] = elements[i];
            }
        }
    }
}
