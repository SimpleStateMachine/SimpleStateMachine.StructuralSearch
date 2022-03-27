using System.Linq;

namespace SimpleStateMachine.StructuralSearch
{
    public static partial class Constant
    {
        public static class FindTemplate
        {
            public static char[] All()
            {
                var all = new char[]{
                    LeftParenthesis,
                    RightParenthesis,
                    LeftSquareParenthesis,
                    RightSquareParenthesis,
                    LeftCurlyParenthesis,
                    RightCurlyParenthesis,
                    PlaceholderSeparator,
                    CarriageReturn,
                    LineFeed,
                    Space
                };

                return all;
            }
        
            public static char[] AllExclude(params char[] excluded)
            {
                return All().Where(x => !excluded.Contains(x)).ToArray();
            }
        }
    }
}