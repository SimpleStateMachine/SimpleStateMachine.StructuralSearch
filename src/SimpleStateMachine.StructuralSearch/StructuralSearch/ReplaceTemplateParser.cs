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
    static ReplaceTemplateParser()
    {
        ParenthesisedParameter = Parsers.Parsers.BetweenOneOfChars((c1, c2) => GetParenthesisType((c1, c2)),
                Parser.Rec(() => Parameter ?? throw new ArgumentNullException(nameof(Parameter))),
                Constant.AllParentheses)
            .Select(x => new ParenthesisedParameter(x.Item1, [x.Item2]))
            .As<char, ParenthesisedParameter, IRuleParameter>()
            .Try();

        Parameter = Parser.OneOf(ParenthesisedParameter, ParametersParser.Parameter, ParametersParser.StringParameter)
            .Then(ParametersParser.Change, (parameter, func) => func(parameter))
            .Try();
        
        Parenthesised = Parameter.AtLeastOnce().Try();

        ParenthesisedParameters = Parsers.Parsers.BetweenOneOfChars(
                x => Parser.Char(x).Try()
                    .Select(x => new StringParameter(x.ToString()))
                    .As<char, StringParameter, IRuleParameter>(),
                Parser.Rec(() => Parenthesised),
                Constant.AllParentheses)
            .Try();
            

        Parameters = Parser.OneOf(ParenthesisedParameters, Parenthesised).AtLeastOnceUntil(CommonParser.EOF)
            .MergerMany()
            .Try();
    }

    private static readonly Parser<char, IRuleParameter> ParenthesisedParameter;
    public static readonly Parser<char, IRuleParameter> Parameter;
    private static readonly Parser<char, IEnumerable<IRuleParameter>> Parenthesised;
    private static readonly Parser<char, IEnumerable<IRuleParameter>> ParenthesisedParameters;
    private static readonly Parser<char, IEnumerable<IRuleParameter>> Parameters;

    internal static IReplaceBuilder ParseTemplate(string? str) 
        => string.IsNullOrEmpty(str)
            ? ReplaceBuilder.Empty
            : Parameters
                .Select(steps => new ReplaceBuilder(steps))
                .ParseOrThrow(str);
    
    public static ParenthesisType GetParenthesisType((char c1, char c2) parenthesis)
        => parenthesis switch
        {
            (Constant.LeftParenthesis, Constant.RightParenthesis) => ParenthesisType.Usual,
            (Constant.LeftSquareParenthesis, Constant.RightSquareParenthesis) => ParenthesisType.Square,
            (Constant.LeftCurlyParenthesis, Constant.RightCurlyParenthesis) => ParenthesisType.Curly,
            _ => throw new ArgumentOutOfRangeException(nameof(parenthesis), parenthesis, null)
        };
}