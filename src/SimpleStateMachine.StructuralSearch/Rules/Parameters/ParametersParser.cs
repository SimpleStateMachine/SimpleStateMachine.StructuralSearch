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

        public static readonly Parser<char, IRuleParameter> PlaceholderRuleParameter =
            PlaceholderParameter
                .As<char, PlaceholderParameter, IRuleParameter>();

        public static readonly Parser<char, IRuleParameter> StringParameter =
            CommonParser.Escaped(Constant.DoubleQuotes, Constant.PlaceholderSeparator)
                .Or(Parser.AnyCharExcept(Constant.DoubleQuotes, Constant.PlaceholderSeparator))
                .AtLeastOnceString()
                .Select(x => new StringParameter(x))
                .As<char, StringParameter, IRuleParameter>()
                .TrimStart()
                .Try();
        
        public static readonly Parser<char, IRuleParameter> StringFormatParameter =
            Parser.OneOf(StringParameter, PlaceholderPropertyParser.PlaceholderPropertyParameter, PlaceholderRuleParameter)
                .AtLeastOnce()
                .Between(CommonParser.DoubleQuotes)
                .Select(parameters => new StringFormatParameter(parameters))
                .As<char, StringFormatParameter, IRuleParameter>()
                .TrimStart()
                .Try();
        
        public static readonly Parser<char, IRuleParameter> Parameter =
            Parser.OneOf(PlaceholderPropertyParser.PlaceholderPropertyParameter, PlaceholderRuleParameter, StringFormatParameter)
                .TrimStart()
                .Try();

        public static readonly Parser<char, IEnumerable<IRuleParameter>> Parameters =
            Parameter.AtLeastOnce();

    }
}