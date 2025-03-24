using System;
using System.Collections.Generic;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Rules.Parameters;
using SimpleStateMachine.StructuralSearch.Rules.Parameters.Types;
using SimpleStateMachine.StructuralSearch.Templates.ReplaceTemplate;

namespace SimpleStateMachine.StructuralSearch.StructuralSearch;

internal static class ReplaceTemplateParser
{
    private static readonly Parser<char, IRuleParameter> ParameterInParentheses =
        Parsers.Parsers.BetweenParentheses
            (
                expr: Parser.Rec(() => Parameter ?? throw new ArgumentNullException(nameof(Parameter))),
                mapFunc: (c1, value, c2) => new ParenthesisedParameter(GetParenthesisType((c1, c2)), value)
            )
            .As<char, ParenthesisedParameter, IRuleParameter>()
            .Try();

    public static readonly Parser<char, IRuleParameter> Parameter =
        Parser.OneOf(ParameterInParentheses, ParametersParser.Parameter, ParametersParser.StringParameter)
            .Then(ParametersParser.Change, (parameter, func) => func(parameter))
            .Try();

    private static readonly Parser<char, IEnumerable<IRuleParameter>> Parameters =
        Parser.OneOf(ParameterInParentheses, Parameter).AtLeastOnceUntil(CommonParser.Eof);

    internal static IReplaceBuilder ParseTemplate(string? str)
        => string.IsNullOrEmpty(str)
            ? ReplaceBuilder.Empty
            : Parameters
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
}