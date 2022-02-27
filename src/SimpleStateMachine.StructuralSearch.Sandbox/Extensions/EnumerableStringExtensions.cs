using System.Collections.Generic;
using System.Linq;

namespace SimpleStateMachine.StructuralSearch.Sandbox.Extensions
{
    public static class EnumerableStringExtensions
    {
        public static string JoinToString(this IEnumerable<string> enumerable, string separator = null)
        {
            return string.Join(separator, enumerable);
        }
    }
}