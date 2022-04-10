using System.Linq;

namespace SimpleStateMachine.StructuralSearch.Templates.FindTemplate
{
    public static class Constant
    {
        public static class FindTemplate
        {
            public static char[] All()
            {
                var all = new[]{
                    SimpleStateMachine.StructuralSearch.Constant.LeftParenthesis,
                    SimpleStateMachine.StructuralSearch.Constant.RightParenthesis,
                    SimpleStateMachine.StructuralSearch.Constant.LeftSquareParenthesis,
                    SimpleStateMachine.StructuralSearch.Constant.RightSquareParenthesis,
                    SimpleStateMachine.StructuralSearch.Constant.LeftCurlyParenthesis,
                    SimpleStateMachine.StructuralSearch.Constant.RightCurlyParenthesis,
                    SimpleStateMachine.StructuralSearch.Constant.PlaceholderSeparator,
                    SimpleStateMachine.StructuralSearch.Constant.CarriageReturn,
                    SimpleStateMachine.StructuralSearch.Constant.LineFeed,
                    SimpleStateMachine.StructuralSearch.Constant.Space
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