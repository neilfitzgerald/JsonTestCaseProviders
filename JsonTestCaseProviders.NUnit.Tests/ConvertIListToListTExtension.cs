using System.Collections.Generic;
using System.Collections;

namespace JsonTestCaseProviders.NUnit.Tests
{
    public static class ConvertIListToListTExtension
    {
        /// <summary>
        /// Converts an old style IList to a List<typeparamref name="T"/> via a item by item copy.
        /// Assumes items in the list are of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Type to create a new list of</typeparam>
        /// <param name="list">An old style IList Collection containing objects</param>
        /// <returns>List<typeparamref name="T"/></returns>
        public static List<T> ToList<T>(this IList list)
        {
            var result = new List<T>();
            foreach (var item in list)
            {
                result.Add((T)item);
            }
            return result;
        }
    }
}