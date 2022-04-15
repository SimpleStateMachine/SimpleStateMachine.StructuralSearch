using System;
using System.Collections.Generic;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;

namespace SimpleStateMachine.StructuralSearch.Rules
{
    public static class SubRuleParser
    {
        public static readonly Parser<char, SubRuleType> SubRuleType =
            Parsers.EnumExcept(true, Rules.SubRuleType.Is, Rules.SubRuleType.In)
                .TrimStart();

        public static readonly Parser<char, IRule> UnarySubRule =
            Parser.Map((type, param) => new UnarySubRule(type, param),
                    SubRuleType, ParametersParser.Parameter)
                .As<char, UnarySubRule, IRule>()
                .Try();

        public static readonly Parser<char, PlaceholderType> PlaceholderType =
            Parser.CIEnum<PlaceholderType>();

        public static readonly Parser<char, IRule> IsSubRule =
            Parser.Map((type, param) => new IsRule(type, param),
                    Parsers.EnumValue(Rules.SubRuleType.Is, true)
                        .TrimStart(),
                    PlaceholderType)
                .As<char, IsRule, IRule>()
                .Try();

        public static readonly Parser<char, IRule> InSubRule =
            Parser.Map((type, param) => new InRule(type, param),
                    Parsers.EnumValue(Rules.SubRuleType.In, true)
                        .TrimStart(), ParametersParser.Parameters)
                .As<char, InRule, IRule>()
                .Try();
    }
}