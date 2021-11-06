using System.Collections.Generic;

namespace Helpers
{
    public static class ObjectExtensions
    {
        public static T Random<T>(this IList<T> list) => list[UnityEngine.Random.Range(0, list.Count)];
    }
}