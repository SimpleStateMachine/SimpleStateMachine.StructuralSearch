using System;
using System.Collections.Generic;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Rules;

namespace SimpleStateMachine.StructuralSearch
{
    public static class ParametersParser
    {
        public static readonly Parser<char, PlaceholderParameter> PlaceholderParameter =
            CommonTemplateParser.Placeholder
                .Select(x => new PlaceholderParameter(x))
                .TrimStart()
                .Try();

        // public static readonly Parser<char, IRuleParameter> PlaceholderRuleParameter =
        //     PlaceholderParameter
        //         .As<char, PlaceholderParameter, IRuleParameter>();

        public static readonly Parser<char, IRuleParameter> PlaceholderOrPropertyRuleParameter =
            PlaceholderParameter.Then(PlaceholderPropertyParser.PlaceholderPropertyParameter,
                    (placeholder, func) => func(placeholder))
                .Try();

        public static readonly Parser<char, IRuleParameter> StringParameter =
            CommonParser.Escaped(Constant.Parameter.Escape)
                .Or(Parser.AnyCharExcept(Constant.Parameter.Excluded))
                .AtLeastOnceString()
                .Select(x => new StringParameter(x))
                .As<char, StringParameter, IRuleParameter>()
                .Try();

        public static readonly Parser<char, IRuleParameter> StringFormatParameter =
            Parser.OneOf(PlaceholderOrPropertyRuleParameter, StringParameter)
                .AtLeastOnce()
                .Between(CommonParser.DoubleQuotes)
                .Select(parameters => new StringFormatParameter(parameters))
                .As<char, StringFormatParameter, IRuleParameter>()
                .TrimStart()
                .Try();

        public static readonly Parser<char, IRuleParameter> Parameter =
            Parser.OneOf(StringFormatParameter, PlaceholderOrPropertyRuleParameter)
                .TrimStart()
                .Try();

        public static readonly Parser<char, IEnumerable<IRuleParameter>> Parameters =
            Parameter.SeparatedAtLeastOnce(CommonParser.Comma);

        public static readonly Parser<char, Func<IRuleParameter, IRuleParameter>> ChangeTypeParameter =
            CommonParser.Dote.Then(Parser.CIEnum<ChangeType>())
                .Optional()
                .Select(changeType => new Func<IRuleParameter, IRuleParameter>(placeholder =>
                    changeType.HasValue ? new ChangeParameter(placeholder, changeType.Value) : placeholder))
                .Try();
    }
}