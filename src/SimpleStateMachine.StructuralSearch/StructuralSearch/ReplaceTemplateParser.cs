using System;
using System.Collections.Generic;
using System.Linq;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Rules.Parameters;
using SimpleStateMachine.StructuralSearch.Rules.Parameters.Types;
using SimpleStateMachine.StructuralSearch.Templates.ReplaceTemplate;

namespace SimpleStateMachine.StructuralSearch.StructuralSearch;

internal static class ReplaceTemplateParser
{
    // private static readonly Parser<char, IRuleParameter> Parameter =
    //     Parser.OneOf(ParametersParser.Parameter, ParametersParser.StringParameter)
    //         .Then(ParametersParser.Change, (parameter, func) => func(parameter));

    private static readonly Parser<char, IStringRuleParameter> StringParameter =
        StringParameterParser.String.Select<IStringRuleParameter>(v => new StringParameter(v));

    private static readonly Parser<char, IStringRuleParameter> StringOptionalParameter =
        StringParameterParser.StringOptional.Select<IStringRuleParameter>(v => new StringParameter(v));
    
    private static readonly Parser<char, IStringRuleParameter> StringJoinParameter =
        Parser.OneOf(ParametersParser.PlaceholderParameter.Cast<IStringRuleParameter>(), StringParameter)
            .AtLeastOnce()
            .Select(JoinParameters);

    private static readonly Parser<char, IStringRuleParameter> ParameterInParentheses =
        Parsers.Parsers.BetweenParentheses
            (
                expr: Parser.OneOf
                (
                    Parser.Rec(() => ParameterInParentheses ?? throw new ArgumentNullException(nameof(ParameterInParentheses))),
                    StringJoinParameter,
                    StringOptionalParameter // Empty parentheses
                ),
                mapFunc: (c1, value, c2) => new ParenthesisedParameter(GetParenthesisType((c1, c2)), value)
            )
            .As<char, ParenthesisedParameter, IStringRuleParameter>();

    private static readonly Parser<char, IStringRuleParameter> ReplaceParser =
        Parser.OneOf(ParameterInParentheses, StringJoinParameter)
            .AtLeastOnceUntil(CommonParser.Eof)
            .Select(JoinParameters);

    internal static IReplaceBuilder ParseTemplate(string? str)
        => string.IsNullOrEmpty(str)
            ? ReplaceBuilder.Empty
            : ReplaceParser
                .Select(steps => new ReplaceBuilder(steps))
                .ParseOrThrow(str);

    private static ParenthesisType GetParenthesisType((char c1, char c2) parenthesis)
        => parenthesis switch
        {
            (Constant.LeftParenthesis, Constant.RightParenthesis) => ParenthesisType.Usual,
            (Constant.LeftSquareParenthesis, Constant.RightSquareParenthesis) => ParenthesisType.Square,
            (Constant.LeftCurlyParenthesis, Constant.RightCurlyParenthesis) => ParenthesisType.Curly,
            _ => throw new ArgumentOutOfRangeException(nameof(parenthesis), parenthesis, null)
        };

    private static IStringRuleParameter JoinParameters(IEnumerable<IStringRuleParameter> parameters)
    {
        var list = parameters.ToList();
        return list.Count == 1 ? list[0] : new StringJoinParameter(list);
    }
}