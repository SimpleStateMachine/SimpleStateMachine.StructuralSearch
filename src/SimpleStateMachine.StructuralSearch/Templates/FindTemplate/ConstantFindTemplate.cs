using System.Linq;
using SimpleStateMachine.StructuralSearch.Extensions;

namespace SimpleStateMachine.StructuralSearch
{
    public static partial class Constant
    {
        public static class FindTemplate
        {
            public static readonly char[] All = AllParenthesisArray.Add
            (
                PlaceholderSeparator,
                CarriageReturn,
                LineFeed,
                Space
            );

            public static char[] AllExclude(params char[] excluded)
            {
                return All.Where(x => !excluded.Contains(x)).ToArray();
            }
        }
        
        public static class Parameter
        {
            public static readonly char[] Escape = 
            {
                DoubleQuotes,
                PlaceholderSeparator,
                Dote,
            };

            public static readonly char[] Excluded = AllParenthesisArray.Add(Escape);
        }
    }
}