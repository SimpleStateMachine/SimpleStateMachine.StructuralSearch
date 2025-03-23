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
    private static readonly Parser<char, IRuleParameter> ParenthesisedParameter =
        Parsers.Parsers.BetweenOneOfChars
            (
                resultFunc: (c1, c2) => GetParenthesisType((c1, c2)),
                expr: Parser.Rec(() => Parameter ?? throw new ArgumentNullException(nameof(Parameter))),
                values: Constant.AllParentheses
            )
            .Select(x => new ParenthesisedParameter(x.Item1, x.Item2))
            .As<char, ParenthesisedParameter, IRuleParameter>()
            .Try();

    public static readonly Parser<char, IRuleParameter> Parameter =
        Parser.OneOf(ParenthesisedParameter, ParametersParser.Parameter, ParametersParser.StringParameter)
            .Then(ParametersParser.Change, (parameter, func) => func(parameter))
            .Try();

    private static readonly Parser<char, IEnumerable<IRuleParameter>> Parenthesised = Parameter.AtLeastOnce().Try();

    private static readonly Parser<char, IEnumerable<IRuleParameter>> ParenthesisedParameters =
        Parsers.Parsers.BetweenOneOfChars
            (
                leftRight: c => Parser.Char(c).Try().Select(x => new StringParameter(x.ToString())).As<char, StringParameter, IRuleParameter>(),
                expr: Parser.Rec(() => Parenthesised),
                values: Constant.AllParentheses
            )
            .Try();

    private static readonly Parser<char, IEnumerable<IRuleParameter>> Parameters =
        Parser.OneOf(ParenthesisedParameters, Parenthesised).AtLeastOnceUntil(CommonParser.Eof).SelectMany().Try();

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