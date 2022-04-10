using System.Collections.Generic;

namespace SimpleStateMachine.StructuralSearch.Extensions
{
    public static class EnumerableStringExtensions
    {
        public static string JoinToString(this IEnumerable<string> enumerable, string separator = null)
        {
            return string.Join(separator, enumerable);
        }
        
        public static string JoinToString(this List<string> enumerable, string separator = null)
        {
            return string.Join(separator, enumerable);
        }
    }
}