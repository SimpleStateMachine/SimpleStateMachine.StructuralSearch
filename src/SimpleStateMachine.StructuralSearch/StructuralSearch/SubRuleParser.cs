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

        public static Parser<char, IRule> SubRule(IRuleParameter left, SubRuleType ruleType) =>
            ParametersParser.Parameter.Select(right => new SubRule(ruleType, left, right))
                .As<char, SubRule, IRule>()
                .Try();

        public static readonly Parser<char, PlaceholderType> PlaceholderType =
            Parser.CIEnum<PlaceholderType>()
                .TrimStart();

        public static Parser<char, IRule> IsSubRule(IRuleParameter left, SubRuleType ruleType) =>
            PlaceholderType.Select(arg => new IsSubRule(left, arg))
                .As<char, IsSubRule, IRule>()
                .Try();

        public static Parser<char, IRule> InSubRule(IRuleParameter left, SubRuleType ruleType) =>
            Parser.OneOf(ParametersParser.Parameters,
                    CommonParser.Parenthesised(ParametersParser.Parameters, x => x.Trim()))
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
                    _ => SubRule(x.left, x.ruleType)
                });
    }
}