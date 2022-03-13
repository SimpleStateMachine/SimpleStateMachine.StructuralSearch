using System.Collections.Generic;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;

namespace SimpleStateMachine.StructuralSearch.Rules
{
    public static class SubRuleParser
    {
        public static readonly Parser<char, IRuleParameter> PlaceholderParameter =
            CommonTemplateParser.Placeholder
                .Trim()
                .Try().Select(x => new PlaceholderParameter(x))
                .As<char, PlaceholderParameter, IRuleParameter>()
                .Try();


        public static readonly Parser<char, IRuleParameter> StringParameter =
            Parser.AnyCharExcept(Constant.DoubleQuotes)
                .AtLeastOnceString()
                .Between(CommonParser.DoubleQuotes)
                .Select(x => new StringParameter(x))
                .As<char, StringParameter, IRuleParameter>()
                .Trim()
                .Try();

        public static readonly Parser<char, IRuleParameter> Parameter =
            Parser.OneOf(PlaceholderParameter, StringParameter);

        public static readonly Parser<char, IEnumerable<IRuleParameter>> Parameters =
            Parameter.AtLeastOnce();

        public static readonly Parser<char, SubRuleType> SubRuleType =
            Parsers.EnumExcept(true, Rules.SubRuleType.Is, Rules.SubRuleType.In)
                .Trim();

        public static readonly Parser<char, IRule> UnarySubRule =
            Parser.Map((type, param) => new UnarySubRule(type, param),
                    SubRuleType, Parameter)
                .As<char, UnarySubRule, IRule>()
                .Try();

        public static readonly Parser<char, PlaceholderType> PlaceholderType =
            Parsers.Enum<PlaceholderType>(true)
                .Trim();

        public static readonly Parser<char, IRule> IsSubRule =
            Parser.Map((type, param) => new IsRule(type, param),
                    Parsers.EnumValue(Rules.SubRuleType.Is, true)
                        .Trim(),
                    PlaceholderType)
                .As<char, IsRule, IRule>()
                .Try();

        public static readonly Parser<char, IRule> InSubRule =
            Parser.Map((type, param) => new InRule(type, param),
                    Parsers.EnumValue(Rules.SubRuleType.In, true), Parameters)
                .As<char, InRule, IRule>()
                .Try();
    }
}