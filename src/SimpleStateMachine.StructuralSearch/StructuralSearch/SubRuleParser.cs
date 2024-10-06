using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;

namespace SimpleStateMachine.StructuralSearch.Rules
{
    public static class SubRuleParser
    {
        public static readonly Parser<char, SubRuleType> SubRuleType =
            Parser.CIEnum<SubRuleType>()
                .Trim();

        public static Parser<char, IFindRule> BinarySubRule(IRuleParameter left, SubRuleType ruleType) =>
            ParametersParser.Parameter
                .TrimStart()
                .Select(right => new BinarySubRule(ruleType, left, right))
                .As<char, BinarySubRule, IFindRule>()
                .Try();

        public static readonly Parser<char, PlaceholderType> PlaceholderType =
            Parser.CIEnum<PlaceholderType>()
                .TrimStart();

        public static Parser<char, IFindRule> IsSubRule(IRuleParameter left, SubRuleType ruleType) =>
            PlaceholderType.Select(arg => new IsSubRule(left, arg))
                .As<char, IsSubRule, IFindRule>()
                .Try();

        public static Parser<char, IFindRule> InSubRule(IRuleParameter left, SubRuleType ruleType) =>
            ParametersParser.Parameters
                .ParenthesisedOptional(x => Parser.Char(x).Trim())
                .TrimStart()
                .Select(args => new InSubRule(left, args))
                .As<char, InSubRule, IFindRule>()
                .Try();

        public static readonly Parser<char, IFindRule> OneOfSubRule =
            Parser.Map((left, ruleType) => (left, ruleType), 
                    ParametersParser.Parameter.Trim(), SubRuleType)
                .Then(x => x.ruleType switch
                {
                    Rules.SubRuleType.In => InSubRule(x.left, x.ruleType),
                    Rules.SubRuleType.Is => IsSubRule(x.left, x.ruleType),
                    _ => BinarySubRule(x.left, x.ruleType)
                });
    }
}