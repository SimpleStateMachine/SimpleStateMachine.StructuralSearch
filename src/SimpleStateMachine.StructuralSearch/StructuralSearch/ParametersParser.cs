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
    private static readonly Parser<char, IEnumerable<char>> String =
        CommonParser.Escaped(Constant.Parameter.Escape)
            .Or(Parser.AnyCharExcept(Constant.Parameter.Excluded))
            .AtLeastOnce();

    public static readonly Parser<char, IRuleParameter> StringParameter = 
        Parser.OneOf(Parser.Rec(() => Parenthesised ?? throw new ArgumentNullException()), String)
        .AtLeastOnce()
        .SelectMany()
        .AsString()
        .Select(x => new StringParameter(x))
        .As<char, StringParameter, IRuleParameter>()
        .Try();

    public static readonly Parser<char, PlaceholderParameter> PlaceholderParameter =
        CommonTemplateParser.Placeholder
            .Select(x => new PlaceholderParameter(x))
            // .TrimStart()
            .Try();

    private static readonly Parser<char, Func<IRuleParameter, IRuleParameter>> ChangeParameter =
        Parser.CIEnum<ChangeType>()
            .Select(changeType => new Func<IRuleParameter, IRuleParameter>(placeholder => new ChangeParameter(placeholder, changeType)))
            .Try();
    
    public static readonly Parser<char, IEnumerable<char>> StringWithParenthesised = 
        Parser.OneOf(Parser.Rec(() => Parenthesised ?? throw new ArgumentNullException()), String)
            .Optional()
            .Select(x => x.HasValue ? x.Value : [])
            .Try();
    
    private static readonly Parser<char, IEnumerable<char>> Parenthesised =
        Parsers.Parsers.BetweenOneOfChars
            (
                leftRight: x => Parser.Char(x).Try(),
                expr: Parser.Rec(() => StringWithParenthesised ?? throw new ArgumentNullException(nameof(StringWithParenthesised))),
                values: Constant.AllParentheses
            )
            .Try();

    private static readonly Parser<char, Func<IRuleParameter, IRuleParameter>> ChangeUnaryParameter = Parser.Map
        (
            func: (type, arg) => (type, arg),
            parser1: Parser.CIEnum<ChangeUnaryType>(),
            parser2: CommonParser.Parenthesised(Parser.Rec(() => Parameter ?? throw new ArgumentNullException()), Parser.Char)
        )
        .Select(pair => new Func<IRuleParameter, IRuleParameter>(placeholder => new ChangeUnaryParameter(placeholder, pair.type, pair.arg)))
        .Try();

    public static readonly Parser<char, Func<IRuleParameter, IRuleParameter>> Change = 
        CommonParser.Dote.Then(Parser.OneOf(ChangeParameter, ChangeUnaryParameter))
        .Many()
        .Select(funcs => new Func<IRuleParameter, IRuleParameter>(placeholder => funcs.Aggregate(placeholder, (parameter, func) => func(parameter))));

    private static readonly Parser<char, IRuleParameter> PlaceholderOrPropertyRuleParameter = 
        PlaceholderParameter
        .Then(PlaceholderPropertyParser.PlaceholderPropertyParameter, (placeholder, func) => func(placeholder))
        .Then(Change, (parameter, func) => func(parameter))
        .Try();
    
    public static readonly Parser<char, IRuleParameter> StringFormatParameter = 
        Parser.OneOf(Parser.Rec(() => PlaceholderOrPropertyRuleParameter ?? throw new ArgumentNullException()), StringParameter)
            .AtLeastOnce()
            .Between(CommonParser.DoubleQuotes)
            .Select(parameters => new StringFormatParameter(parameters))
            .As<char, StringFormatParameter, IRuleParameter>()
            // .TrimStart()
            .Try();

    public static readonly Parser<char, IRuleParameter> Parameter = Parameter = Parser.OneOf(StringFormatParameter, Parser.Rec(() => PlaceholderOrPropertyRuleParameter ?? throw new ArgumentNullException())).Try();
    public static readonly Parser<char, IEnumerable<IRuleParameter>> Parameters = Parameter.Trim().SeparatedAtLeastOnce(CommonParser.Comma.Trim());
}