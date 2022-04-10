using System.Collections.Generic;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.StructuralSearch;

namespace SimpleStateMachine.StructuralSearch.Rules.Parameters
{
    public static class ParametersParser
    {
        public static readonly Parser<char, IRuleParameter> PlaceholderParameter =
            CommonTemplateParser.Placeholder
                .Select(x => new PlaceholderParameter(x))
                .As<char, PlaceholderParameter, IRuleParameter>()
                .TrimStart()
                .Try();
        
        public static readonly Parser<char, IRuleParameter> StringParameter =
            CommonParser.Escaped(Constant.DoubleQuotes, Constant.PlaceholderSeparator)
                .Or(Parser.AnyCharExcept(Constant.DoubleQuotes, Constant.PlaceholderSeparator))
                .AtLeastOnceString()
                .Select(x => new StringParameter(x))
                .As<char, StringParameter, IRuleParameter>()
                .TrimStart()
                .Try();
        
        public static readonly Parser<char, IRuleParameter> StringFormatParameter =
            Parser.OneOf(StringParameter, PlaceholderPropertyParser.PlaceholderPropertyParameter, PlaceholderParameter)
                .AtLeastOnce()
                .Between(CommonParser.DoubleQuotes)
                .Select(parameters => new StringFormatParameter(parameters))
                .As<char, StringFormatParameter, IRuleParameter>()
                .TrimStart()
                .Try();
        
        public static readonly Parser<char, IRuleParameter> Parameter =
            Parser.OneOf(PlaceholderPropertyParser.PlaceholderPropertyParameter, PlaceholderParameter, StringFormatParameter)
                .TrimStart()
                .Try();

        public static readonly Parser<char, IEnumerable<IRuleParameter>> Parameters =
            Parameter.AtLeastOnce();

    }
}