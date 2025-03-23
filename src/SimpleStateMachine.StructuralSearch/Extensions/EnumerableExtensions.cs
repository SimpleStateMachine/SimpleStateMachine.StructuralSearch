using System.Collections.Generic;

namespace SimpleStateMachine.StructuralSearch.Extensions;

internal static class EnumerableExtensions
{
    public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T>? enumerable) 
        => enumerable ?? [];
}