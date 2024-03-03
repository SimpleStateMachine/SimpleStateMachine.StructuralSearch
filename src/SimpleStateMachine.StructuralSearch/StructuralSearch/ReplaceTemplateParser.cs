using System;
using System.Collections.Generic;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Helper;
using SimpleStateMachine.StructuralSearch.ReplaceTemplate;
using SimpleStateMachine.StructuralSearch.Rules;
using SimpleStateMachine.StructuralSearch.Rules.Parameters;

namespace SimpleStateMachine.StructuralSearch
{
    internal static class ReplaceTemplateParser
    {
        static ReplaceTemplateParser()
        {
            ParenthesisedParameter = Parsers.BetweenOneOfChars((c1, c2) =>
                        MatchHelper.GetParenthesisType((c1, c2)),
                    Parser.Rec(() => Parameter ?? throw new ArgumentNullException(nameof(Parameter))),
                    Constant.AllParenthesised)
                .Select(x => new ParenthesisedParameter(x.Item1, new[] { x.Item2 }))
                .As<char, ParenthesisedParameter, IRuleParameter>()
                .Try();

            Parameter = Parser.OneOf(ParenthesisedParameter, ParametersParser.Parameter, ParametersParser.StringParameter)
                .Then(ParametersParser.Change, (parameter, func) => func(parameter))
                .Try();


            Parenthesised = Parameter.AtLeastOnce().Try();

            ParenthesisedParameters = Parsers.BetweenOneOfChars(
                x => Parser.Char(x).Try()
                    .Select(x => new StringParameter(x.ToString()))
                    .As<char, StringParameter, IRuleParameter>(),
                Parser.Rec(() => Parenthesised),
                Constant.AllParenthesised)
                .Try();
            

            Parameters = Parser.OneOf(ParenthesisedParameters, Parenthesised).AtLeastOnceUntil(CommonParser.EOF)
                .MergerMany()
                .Try();
        }
        
        public static readonly Parser<char, IRuleParameter> ParenthesisedParameter;
        public static readonly Parser<char, IRuleParameter> Parameter;
        public static readonly Parser<char, IEnumerable<IRuleParameter>> Parenthesised;
        public static readonly Parser<char, IEnumerable<IRuleParameter>> ParenthesisedParameters;
        public static readonly Parser<char, IEnumerable<IRuleParameter>> Parameters;

        internal static IReplaceBuilder ParseTemplate(string? str)
        {
            return string.IsNullOrEmpty(str)
                ? ReplaceBuilder.Empty
                : Parameters
                .Select(steps => new ReplaceBuilder(steps))
                .ParseOrThrow(str);
        }
    }
}