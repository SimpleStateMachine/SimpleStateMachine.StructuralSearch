using System;
using Pidgin;
using SimpleStateMachine.StructuralSearch.CustomParsers;
using SimpleStateMachine.StructuralSearch.Rules.Parameters;

namespace SimpleStateMachine.StructuralSearch.StructuralSearch;

internal static class StringParameterParser
{
    internal static readonly Parser<char, string> String =
        Parser.OneOf(CommonParser.EscapedChar, Grammar.NonLanguageSyntaxChar).AtLeastOnceString();

    internal static readonly Parser<char, string> StringOptional =
        String.Optional().Select(x => x.GetValueOrDefault(string.Empty));

    internal static readonly Parser<char, string> StringInParentheses =
        Parsers.BetweenParentheses
        (
            expr: Parser.OneOf
            (
                // Recursive
                Parser.Rec(() => StringInParentheses ?? throw new ArgumentNullException(nameof(StringInParentheses))),
                StringOptional
            ),
            mapFunc: (left, result, right) => $"{left}{result}{right}"
        );

    internal static readonly Parser<char, IStringRuleParameter> WhiteSpaceParameter =
        Grammar.WhiteSpaces.Select<IStringRuleParameter>(str => new StringParameter(str));
    
    internal static readonly Parser<char, IStringRuleParameter> StringParameterOld =
        Parser.OneOf(StringInParentheses, String)
            .Select<IStringRuleParameter>(str => new StringParameter(str));
    
    internal static readonly Parser<char, IStringRuleParameter> StringFormatParameter =
        String
            .Between(CommonParser.DoubleQuotes)
            .Select<IStringRuleParameter>(str => new StringFormatParameter(str));
    
    internal static readonly Parser<char, IStringRuleParameter> StringParameter =
        Parser.OneOf(StringParameterOld, StringFormatParameter);

    // internal static readonly Parser<char, IStringRuleParameter> StringJoinParameter =
    //     Parser.OneOf(StringParameter, String)
    //         .AtLeastOnce()
    //         .Select<IStringRuleParameter>(parameters => new StringJoinParameter(parameters.ToList()));
}