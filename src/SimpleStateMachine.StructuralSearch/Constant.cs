﻿using System.Collections.Generic;
using System.Linq;

namespace SimpleStateMachine.StructuralSearch;

internal static class Constant
{
    /// <summary>
    /// Parenthesis empty string
    /// </summary>
    public static readonly string EmptyString = string.Empty;

    /// <summary>
    /// String: "Not"
    /// </summary>
    public const string Not = "Not";

    /// <summary>
    /// String: "Then"
    /// </summary>
    public const string Then = "Then";

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
    /// Char: '\''
    /// </summary>
    public const char SingleQuotes = '\'';

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
    /// Char: ':'
    /// </summary>
    public const char Colon = ':';

    /// <summary>
    /// String: "=>"
    /// </summary>
    public static readonly string Should = $"{Equals}{More}";

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
    public static readonly (char, char)[] AllParentheses = [Parenthesis, SquareParenthesis, CurlyParenthesis];

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

    public static readonly IReadOnlySet<char> InvalidStringLiteralChars = new HashSet<char>(AllParenthesis)
    {
        CarriageReturn,
        LineFeed,
        Space
    };

    public static readonly IReadOnlySet<char> All = new HashSet<char>(InvalidStringLiteralChars)
    {
        PlaceholderSeparator,
    };

    public static readonly IReadOnlySet<char> CharsToEscape = new HashSet<char>
    {
        DoubleQuotes,
        PlaceholderSeparator,
        Dote
    };

    public static readonly IReadOnlySet<char> LanguageSyntaxChars = AllParenthesis.Union(CharsToEscape).ToHashSet();
}