using System.Collections.Generic;
using System.Linq;
using SimpleStateMachine.StructuralSearch.Extensions;

namespace SimpleStateMachine.StructuralSearch;

internal static partial class Constant
{
    public static class FindTemplate
    {
        public static readonly IReadOnlySet<char> All = new HashSet<char>(AllParenthesisArray)
        {
            PlaceholderSeparator,
            CarriageReturn,
            LineFeed,
            Space
        };

        public static char[] AllExclude(params char[] excluded) 
            => All.Where(x => !excluded.Contains(x)).ToArray();
    }
        
    public static class Parameter
    {
        public static readonly char[] Escape = 
        {
            DoubleQuotes,
            PlaceholderSeparator,
            Dote,
        };

        public static readonly IReadOnlySet<char> Excluded = AllParenthesisArray.Union(Escape).ToHashSet();
    }
}