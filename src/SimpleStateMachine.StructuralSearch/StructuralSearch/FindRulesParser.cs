using System;
using Pidgin;
using Pidgin.Expression;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Rules.FindRule;
using SimpleStateMachine.StructuralSearch.Rules.Parameters;

namespace SimpleStateMachine.StructuralSearch.StructuralSearch
{
    public static class FindRuleParser
    {
        internal static Parser<char, Func<IRule, IRule, IRule>> Binary(Parser<char, BinaryRuleType> op)
            => op.Select<Func<IRule, IRule, IRule>>(type => (l, r) => new BinaryRule(type, l, r));

        internal static Parser<char, Func<IRule, IRule>> Unary(Parser<char, UnaryRuleType> op)
            => op.Select<Func<IRule, IRule>>(type => param => new UnaryRule(type, param));
        
        internal static readonly Parser<char, Func<IRule, IRule, IRule>> And
            = Binary(Parsers.Parsers.EnumValue(BinaryRuleType.And, true)
                .TrimStart()
                .Try());

        internal static readonly Parser<char, Func<IRule, IRule, IRule>> Or
            = Binary(Parsers.Parsers.EnumValue(BinaryRuleType.Or, true)
                .TrimStart()
                .Try());

        internal static readonly Parser<char, Func<IRule, IRule, IRule>> NOR
            = Binary(Parsers.Parsers.EnumValue(BinaryRuleType.NOR, true)
                .TrimStart()
                .Try());

        internal static readonly Parser<char, Func<IRule, IRule, IRule>> XOR
            = Binary(Parsers.Parsers.EnumValue(BinaryRuleType.XOR, true)
                .TrimStart()
                .Try());

        internal static readonly Parser<char, Func<IRule, IRule, IRule>> NAND
            = Binary(Parsers.Parsers.EnumValue(BinaryRuleType.NAND, true)
                .TrimStart()
                .Try());

        internal static readonly Parser<char, Func<IRule, IRule, IRule>> XNOR
            = Binary(Parsers.Parsers.EnumValue(BinaryRuleType.XNOR, true)
                .TrimStart()
                .Try());

        internal static readonly Parser<char, Func<IRule, IRule>> Not
            = Unary(Parsers.Parsers.EnumValue(UnaryRuleType.Not, true)
                .TrimStart()
                .Try());

        public static readonly Parser<char, IRule> Expr = ExpressionParser.Build<char, IRule>(
            rule => (
                Parser.OneOf(
                    SubRuleParser.UnarySubRule,
                    SubRuleParser.IsSubRule,
                    SubRuleParser.InSubRule,
                    CommonParser.Parenthesised(rule, x => x.TrimStart())
                ),
                new[]
                {
                    Operator.Prefix(Not),
                    Operator.InfixL(And),
                    Operator.InfixL(NOR),
                    Operator.InfixL(XOR),
                    Operator.InfixL(NAND),
                    Operator.InfixL(XNOR),
                    Operator.InfixL(Or),
                }
            )
        );

        internal static readonly Parser<char, FindRule> Rule =
            Parser.Map((parameter, rule) => new FindRule(parameter, rule),
                ParametersParser.PlaceholderParameter,
                    Expr.TrimStart());

        internal static FindRule ParseTemplate(string str)
        {
            return Rule.ParseOrThrow(str);
        }
    }
}