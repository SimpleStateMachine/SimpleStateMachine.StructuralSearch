using System;
using System.Collections.Generic;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;

namespace SimpleStateMachine.StructuralSearch.Rules
{
    public static class SubRuleParser
    {
        public static readonly Parser<char, SubRuleType> SubRuleType =
            Parser.CIEnum<SubRuleType>().TrimStart();

        public static Parser<char, IRule> BinarySubRule(IRuleParameter left, SubRuleType ruleType) =>
            ParametersParser.Parameter.Select(right => new BinarySubRule(ruleType, left, right))
                .As<char, BinarySubRule, IRule>()
                .Try();

        public static readonly Parser<char, PlaceholderType> PlaceholderType =
            Parser.CIEnum<PlaceholderType>()
                .TrimStart();

        public static Parser<char, IRule> IsSubRule(IRuleParameter left, SubRuleType ruleType) =>
            PlaceholderType.Select(arg => new IsSubRule(left, arg))
                .As<char, IsSubRule, IRule>()
                .Try();

        public static Parser<char, IRule> InSubRule(IRuleParameter left, SubRuleType ruleType) =>
            Parser.OneOf(CommonParser.Parenthesised(ParametersParser.Parameters, x => x.Trim())
                        .Try(),
                    ParametersParser.Parameters)
                .Select(args => new InSubRule(left, args))
                .As<char, InSubRule, IRule>()
                .Try();

        public static readonly Parser<char, IRule> OneOfSubRule =
            Parser.Map((left, ruleType) => (left, ruleType), 
                    ParametersParser.Parameter, SubRuleType)
                .Then(x => x.ruleType switch
                {
                    Rules.SubRuleType.In => InSubRule(x.left, x.ruleType),
                    Rules.SubRuleType.Is => IsSubRule(x.left, x.ruleType),
                    _ => BinarySubRule(x.left, x.ruleType)
                });
    }
}