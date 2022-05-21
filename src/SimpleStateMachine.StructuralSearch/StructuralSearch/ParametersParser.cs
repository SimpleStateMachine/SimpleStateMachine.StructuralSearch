using System;
using System.Collections.Generic;
using System.Linq;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Rules;

namespace SimpleStateMachine.StructuralSearch
{
    public static class ParametersParser
    {
        static ParametersParser()
        {
            // TODO optimize
            Parenthesised = Parsers.BetweenOneOfChars(x => Parser.Char(x).Try(),
                Parser.Rec(() => StringWithParenthesised),
                Constant.AllParenthesised)
                .Try()
                .Labelled($"{nameof(ParametersParser)}.{nameof(Parenthesised)}");
            
            StringWithParenthesised = Parser.OneOf(Parenthesised, String)
                .Optional()
                .Select(x => x.HasValue ? x.Value : Enumerable.Empty<char>())
                .Try();

            StringParameter = Parser.OneOf(Parenthesised, String)
                .AtLeastOnce()
                .MergerMany()
                .AsString()
                .Select(x => new StringParameter(x))
                .As<char, StringParameter, IRuleParameter>()
                .Try()
                .Labelled($"{nameof(ParametersParser)}.{nameof(StringParameter)}");
            
            StringFormatParameter =
                Parser.OneOf(PlaceholderOrPropertyRuleParameter, StringParameter)
                    .AtLeastOnce()
                    .Between(CommonParser.DoubleQuotes)
                    .Select(parameters => new StringFormatParameter(parameters))
                    .As<char, StringFormatParameter, IRuleParameter>()
                    // .TrimStart()
                    .Try();

            Parameter = Parser.OneOf(StringFormatParameter, PlaceholderOrPropertyRuleParameter)
                    // .TrimStart()
                    .Try();

            Parameters = Parameter.Trim().SeparatedAtLeastOnce(CommonParser.Comma.Trim());
        }

        public static readonly Parser<char, IEnumerable<char>> String =
            CommonParser.Escaped(Constant.Parameter.Escape)
                .Or(Parser.AnyCharExcept(Constant.Parameter.Excluded))
                .AtLeastOnce()
                .Labelled($"{nameof(ParametersParser)}.{nameof(String)}");
        
        public static readonly Parser<char, IRuleParameter> StringParameter;
        
        public static readonly Parser<char, PlaceholderParameter> PlaceholderParameter =
            CommonTemplateParser.Placeholder
                .Select(x => new PlaceholderParameter(x))
                // .TrimStart()
                .Try()
                .Labelled($"{nameof(ParametersParser)}.{nameof(PlaceholderParameter)}");
        
        public static readonly Parser<char, Func<IRuleParameter, IRuleParameter>> ChangeParameter =
            CommonParser.Dote.Then(Parser.CIEnum<ChangeType>())
                .Optional()
                .Select(changeType => new Func<IRuleParameter, IRuleParameter>(placeholder =>
                    changeType.HasValue ? new ChangeParameter(placeholder, changeType.Value) : placeholder))
                .Try()
                .Labelled($"{nameof(ParametersParser)}.{nameof(ChangeParameter)}");
        
        public static readonly Parser<char, IRuleParameter> PlaceholderOrPropertyRuleParameter =
            PlaceholderParameter.Then(PlaceholderPropertyParser.PlaceholderPropertyParameter,
                    (placeholder, func) => func(placeholder))
                .Then(ChangeParameter, (parameter, func) => func(parameter))
                .Try()
                .Labelled($"{nameof(ParametersParser)}.{nameof(PlaceholderOrPropertyRuleParameter)}");

        
        public static readonly Parser<char, IEnumerable<char>> StringWithParenthesised;
        
        public static readonly Parser<char, IEnumerable<char>> Parenthesised;

        public static readonly Parser<char, IRuleParameter> StringFormatParameter;

        public static readonly Parser<char, IRuleParameter> Parameter;

        public static readonly Parser<char, IEnumerable<IRuleParameter>> Parameters;
    }
}