using System.Collections.Generic;
using System.Linq;

namespace SimpleStateMachine.StructuralSearch;

internal static class Constant
{
    /// <summary>
    /// String: "If"
    /// </summary>
    public const string If = "If";
    
    /// <summary>
    /// String: "Then"
    /// </summary>
    public const string Then = "Then";
    
    /// <summary>
    /// String: "Not"
    /// </summary>
    public const string Not = "Not";
    
    /// <summary>
    /// String: "Is"
    /// </summary>
    public const string Is = "Is";
    
    /// <summary>
    /// String: "Match"
    /// </summary>
    public const string Match = "Match";
    
    /// <summary>
    /// String: "In"
    /// </summary>
    public const string In = "In";
    
    /// <summary>
    /// String: "Length"
    /// </summary>
    public const string Length = "Length";
    
    /// <summary>
    /// String: "Input"
    /// </summary>
    public const string Input = "Input";

    /// <summary>
    /// Parenthesis char: '('
    /// </summary>
    public const char LeftParenthesis = '(';

    /// <summary>
    /// Parenthesis char: ')'
    /// </summary>
    public const char RightParenthesis = ')';

    /// <summary>
    /// Parenthesis char: '['
    /// </summary>
    public const char LeftSquareParenthesis = '[';

    /// <summary>
    /// Parenthesis char: ']'
    /// </summary>
    public const char RightSquareParenthesis = ']';

    /// <summary>
    /// Parenthesis char: '{'
    /// </summary>
    public const char LeftCurlyParenthesis = '{';

    /// <summary>
    /// Parenthesis char: '}'
    /// </summary>
    public const char RightCurlyParenthesis = '}';

    /// <summary>
    /// Char: '$'
    /// </summary>
    public const char PlaceholderSeparator = '$';

    /// <summary>
    /// Char: '\r'
    /// </summary>
    public const char CarriageReturn = '\r';

    /// <summary>
    /// Char: '\n'
    /// </summary>
    public const char LineFeed = '\n';

    /// <summary>
    /// Char: ' '
    /// </summary>
    public const char Space = ' ';

    /// <summary>
    /// Char: ','
    /// </summary>
    public const char Comma = ',';

    /// <summary>
    /// Char: '\"'
    /// </summary>
    public const char DoubleQuotes = '\"';

    /// <summary>
    /// Char: '\"'
    /// </summary>
    public const char BackSlash = '\\';

    /// <summary>
    /// Char: '.'
    /// </summary>
    public const char Dote = '.';

    /// <summary>
    /// Char: '='
    /// </summary>
    private new const char Equals = '=';

    /// <summary>
    /// Char: '>'
    /// </summary>
    public const char More = '>';

    /// <summary>
    /// Char: '_'
    /// </summary>
    public const char Underscore = '_';

    /// <summary>
    /// String: "=>"
    /// </summary>
    public static readonly string Should = $"{Equals}{More}";

    /// <summary>
    /// Parenthesis chars: '(' and ')'
    /// </summary>
    public static readonly (char, char) Parentheses = (LeftParenthesis, RightParenthesis);

    /// <summary>
    /// Parenthesis chars: '[' and ']'
    /// </summary>
    public static readonly (char, char) SquareParentheses = (LeftSquareParenthesis, RightSquareParenthesis);

    /// <summary>
    /// Parenthesis chars: '{ and '}'
    /// </summary>
    public static readonly (char, char) CurlyParentheses = (LeftCurlyParenthesis, RightCurlyParenthesis);

    /// <summary>
    /// Parenthesis chars: '(' and ')', '{ and '}', '{ and '}'
    /// </summary>
    public static readonly (char, char)[] AllParentheses = [Parentheses, SquareParentheses, CurlyParentheses];

    /// <summary>
    /// Parenthesis chars: '(' and ')', '{ and '}', '{ and '}'
    /// </summary>
    public static readonly IReadOnlySet<char> AllParenthesis = new HashSet<char>
    {
        LeftParenthesis,
        RightParenthesis,
        LeftSquareParenthesis,
        RightSquareParenthesis,
        LeftCurlyParenthesis,
        RightCurlyParenthesis
    };
    
    public static readonly IReadOnlySet<char> StringLiteralCharsToEscape = new HashSet<char>()
    {
        BackSlash,
        DoubleQuotes
    };

    public static readonly IReadOnlySet<char> InvalidStringLiteralChars = new HashSet<char>(AllParenthesis)
    {
        CarriageReturn,
        LineFeed,
        Space
    };

    public static readonly IReadOnlySet<char> CharsToEscape = new HashSet<char>
    {
        DoubleQuotes,
        BackSlash,
        PlaceholderSeparator,
        Dote
    };

    public static readonly IReadOnlySet<char> LanguageSyntaxChars = AllParenthesis.Union(CharsToEscape).ToHashSet();
}