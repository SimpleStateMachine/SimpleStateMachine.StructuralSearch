namespace SimpleStateMachine.StructuralSearch
{
    public static partial class Constant
    {
        /// <summary>
        /// Parenthesis empty string
        /// </summary>
        public static readonly string Empty = string.Empty;
        
        /// <summary>
        /// String: "Not"
        /// </summary>
        public static readonly string Not ="Not";
        
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
        /// Char: ','
        /// </summary>
        public static readonly char Comma = ',';

        /// <summary>
        /// Char: '\''
        /// </summary>
        public static readonly char SingleQuotes = '\'';
        
        /// <summary>
        /// Char: '\"'
        /// </summary>
        public static readonly char DoubleQuotes = '\"';
        
        /// <summary>
        /// Char: '\"'
        /// </summary>
        public static readonly char BackSlash = '\\';
        
        /// <summary>
        /// Char: '.'
        /// </summary>
        public static readonly char Dote = '.';
        
        /// <summary>
        /// Char: '='
        /// </summary>
        public static readonly char Equal = '=';
        
        /// <summary>
        /// Char: '>'
        /// </summary>
        public static readonly char More = '>';
        
        /// <summary>
        /// String: "=>"
        /// </summary>
        public static readonly string Should = $"{Equal}{More}";
        
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
    }
}