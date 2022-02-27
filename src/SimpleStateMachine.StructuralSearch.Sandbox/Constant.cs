using System.Linq;

namespace SimpleStateMachine.StructuralSearch.Sandbox
{
    public static class Constant
    {
        /// <summary>
        /// Parenthesis char: '('
        /// </summary>
        public static readonly char LeftParenthesis = '(';
        
        /// <summary>
        /// Parenthesis char: ')'
        /// </summary>
        public static readonly char RightParenthesis = ')';
        
        /// <summary>
        /// Parenthesis char: '['
        /// </summary>
        public static readonly char LeftSquareParenthesis = '[';
        
        /// <summary>
        /// Parenthesis char: ']'
        /// </summary>
        public static readonly char RightSquareParenthesis = ']';
        
        /// <summary>
        /// Parenthesis char: '{'
        /// </summary>
        public static readonly char LeftCurlyParenthesis = '{';
        
        /// <summary>
        /// Parenthesis char: '}'
        /// </summary>
        public static readonly char RightCurlyParenthesis = '}';
        
        /// <summary>
        /// Char: '$'
        /// </summary>
        public static readonly char PlaceholderSeparator = '$';
        
        /// <summary>
        /// Char: '\r'
        /// </summary>
        public static readonly char CarriageReturn = '\r';
        
        /// <summary>
        /// Char: '\n'
        /// </summary>
        public static readonly char LineFeed = '\n';
        
        /// <summary>
        /// Char: ' '
        /// </summary>
        public static readonly char Space = ' ';
        
        /// <summary>
        /// Parenthesis chars: '(' and ')'
        /// </summary>
        public static readonly (char, char) Parenthesis = (LeftParenthesis, RightParenthesis);
        
        /// <summary>
        /// Parenthesis chars: '[' and ']'
        /// </summary>
        public static readonly (char, char) SquareParenthesis = (LeftSquareParenthesis, RightSquareParenthesis);
        
        /// <summary>
        /// Parenthesis chars: '{ and '}'
        /// </summary>
        public static readonly (char, char) CurlyParenthesis = (LeftCurlyParenthesis, RightCurlyParenthesis);
        
        /// <summary>
        /// Parenthesis chars: '(' and ')', '{ and '}', '{ and '}'
        /// </summary>
        public static readonly (char, char)[] AllParenthesised = { Parenthesis, SquareParenthesis, CurlyParenthesis };
        
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