using System.Collections.Generic;
using System.Linq;

namespace SimpleStateMachine.StructuralSearch.Helper
{
    public static class EnumerableHelper
    {
        public static bool SequenceNullableEqual<TSource>(IEnumerable<TSource>? first, IEnumerable<TSource>? second)
        {
            if (first is null && second is null)
                return true;
            
            if (first is null || second is null)
                return false;

            return first.SequenceEqual(second);
        }
    }
}