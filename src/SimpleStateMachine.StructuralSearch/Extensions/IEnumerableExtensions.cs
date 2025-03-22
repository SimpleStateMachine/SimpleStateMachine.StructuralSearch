using System.Collections.Generic;
using System.Linq;

namespace SimpleStateMachine.StructuralSearch.Extensions;

internal static class IEnumerableExtensions
{
    public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T>? enumerable) 
        => enumerable ?? Enumerable.Empty<T>();
}