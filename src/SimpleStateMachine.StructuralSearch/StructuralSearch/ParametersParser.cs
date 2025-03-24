using System;
using System.Collections.Generic;
using System.Linq;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Rules.Parameters;
using SimpleStateMachine.StructuralSearch.Rules.Parameters.Types;

namespace SimpleStateMachine.StructuralSearch.StructuralSearch;

internal static class ParametersParser
{
    internal delegate IRuleParameter BuildParameter(IRuleParameter parameter);
    
    internal static readonly Parser<char, string> String =
        Parser.OneOf(CommonParser.EscapedChar, Grammar.NonLanguageSyntaxChar).AtLeastOnceString();

    internal static readonly Parser<char, string> StringInParentheses =
        Parsers.Parsers.BetweenParentheses
        (
            expr: Parser.OneOf
            (
                // Recursive
                Parser.Rec(() => StringInParentheses ?? throw new ArgumentNullException(nameof(StringInParentheses))),
                // Optional string
                String.Optional().Select(x => x.GetValueOrDefault())
            ),
            mapFunc: (left, result, right) => $"{left}{result}{right}"
        );

    internal static readonly Parser<char, IRuleParameter> StringParameter =
        Parser.OneOf(StringInParentheses, String)
            .AtLeastOnce()
            .Select<IRuleParameter>(x => new StringParameter(string.Join(string.Empty, x)));

    internal static readonly Parser<char, PlaceholderParameter> PlaceholderParameter =
        Grammar.Placeholder.Select(x => new PlaceholderParameter(x)).Try();

    private static readonly Parser<char, BuildParameter> ChangeParameter =
        Parser.CIEnum<ChangeType>()
            .Select<BuildParameter>(changeType => 
                placeholder => new ChangeParameter(placeholder, changeType));

    private static readonly Parser<char, BuildParameter> ChangeUnaryParameter = Parser.Map
        (
            func: (type, arg) => (type, arg),
            parser1: Parser.CIEnum<ChangeUnaryType>(),
            parser2: CommonParser.Parenthesised(Parser.Rec(() => Parameter ?? throw new ArgumentNullException()), Parser.Char)
        )
        .Select<BuildParameter>(pair => 
            placeholder => new ChangeUnaryParameter(placeholder, pair.type, pair.arg));

    internal static readonly Parser<char, BuildParameter> Change =
        CommonParser.Dote.Then(Parser.OneOf(ChangeParameter, ChangeUnaryParameter.Try()))
            .Many()
            .Select<BuildParameter>(funcList => 
                placeholder => funcList.Aggregate(placeholder, (parameter, func) => func(parameter)));

    private static readonly Parser<char, IRuleParameter> PlaceholderOrPropertyRuleParameter =
        PlaceholderParameter
            .Then(PlaceholderPropertyParser.PlaceholderPropertyParameter, (placeholder, func) => func(placeholder))
            .Then(Change, (parameter, func) => func(parameter));

    public static readonly Parser<char, IRuleParameter> StringFormatParameter =
        Parser.OneOf
            (
                Parser.Rec(() => PlaceholderOrPropertyRuleParameter ?? throw new ArgumentNullException()).Try(), 
                StringParameter
            )
            .AtLeastOnce()
            .Between(CommonParser.DoubleQuotes)
            .Select(IRuleParameter (parameters)=> new StringFormatParameter(parameters));

    public static readonly Parser<char, IRuleParameter> Parameter = Parameter = Parser.OneOf
    (
        StringFormatParameter.Try(),
        Parser.Rec(() => PlaceholderOrPropertyRuleParameter ?? throw new ArgumentNullException())
    );

    public static readonly Parser<char, IEnumerable<IRuleParameter>> Parameters =
        Parameter.Try().Trim().SeparatedAtLeastOnce(CommonParser.Comma.Trim());
}