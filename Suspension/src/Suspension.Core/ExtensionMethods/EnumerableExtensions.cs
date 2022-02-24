using System.Collections.Generic;

namespace MudRunner.Suspension.Core.ExtensionMethods
{
    /// <summary>
    /// It contains extension methods for enumerable entities.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// This method indicates if the list is null or empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this List<T> list)
        {
            if (list == null)
                return true;

            if (list.Count <= 0)
                return true;

            return false;
        }
    }
}
