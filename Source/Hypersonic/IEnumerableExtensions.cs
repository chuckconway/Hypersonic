using System;
using System.Collections.Generic;

namespace Hypersonic
{
    public static class EnumerableExtensions
    {
        /// <summary> Enumerates traverse in this collection. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="source">    The source to act on. </param>
        /// <param name="fnRecurse"> The recurse. </param>
        /// <returns>
        /// An enumerator that allows foreach to be used to process traverse&lt; t&gt; in this collection.
        /// </returns>
        public static IEnumerable<T> Traverse<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> fnRecurse)
        {
            foreach (T item in source)
            {
                yield return item;

                IEnumerable<T> seqRecurse = fnRecurse(item);

                if (seqRecurse != null)
                {
                    foreach (T itemRecurse in Traverse(seqRecurse, fnRecurse))
                    {
                        yield return itemRecurse;
                    }
                }
            }

        }
    }
}
