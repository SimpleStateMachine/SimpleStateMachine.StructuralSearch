using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Rules.FindRule;
using SimpleStateMachine.StructuralSearch.Rules.Parameters;

namespace SimpleStateMachine.StructuralSearch.StructuralSearch
{
    public static class SubRuleParser
    {
        public static readonly Parser<char, SubRuleType> SubRuleType =
            Parsers.Parsers.EnumExcept(true, Rules.FindRule.SubRuleType.Is, Rules.FindRule.SubRuleType.In)
                .TrimStart();

        public static readonly Parser<char, IRule> UnarySubRule =
            Parser.Map((type, param) => new UnarySubRule(type, param),
                    SubRuleType, ParametersParser.Parameter)
                .As<char, UnarySubRule, IRule>()
                .Try();

        public static readonly Parser<char, PlaceholderType> PlaceholderType =
            Parsers.Parsers.Enum<PlaceholderType>(true);

        public static readonly Parser<char, IRule> IsSubRule =
            Parser.Map((type, param) => new IsRule(type, param),
                    Parsers.Parsers.EnumValue(Rules.FindRule.SubRuleType.Is, true)
                        .TrimStart(),
                    PlaceholderType)
                .As<char, IsRule, IRule>()
                .Try();

        public static readonly Parser<char, IRule> InSubRule =
            Parser.Map((type, param) => new InRule(type, param),
                    Parsers.Parsers.EnumValue(Rules.FindRule.SubRuleType.In, true)
                        .TrimStart(), ParametersParser.Parameters)
                .As<char, InRule, IRule>()
                .Try();
    }
}