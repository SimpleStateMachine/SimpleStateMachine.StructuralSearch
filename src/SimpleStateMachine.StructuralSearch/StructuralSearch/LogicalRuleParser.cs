using System;
using Pidgin;
using Pidgin.Expression;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Rules;

namespace SimpleStateMachine.StructuralSearch;

public static class LogicalRuleParser
{
        internal static Parser<char, Func<ILogicalRule, ILogicalRule, ILogicalRule>> BinaryLogical(Parser<char, BinaryRuleType> op)
        => op.Select<Func<ILogicalRule, ILogicalRule, ILogicalRule>>(type => (l, r) => new BinaryLogicalRule(type, l, r));

        internal static Parser<char, Func<ILogicalRule, ILogicalRule>> UnaryLogical(Parser<char, UnaryRuleType> op)
            => op.Select<Func<ILogicalRule, ILogicalRule>>(type => param => new UnaryLogicalRule(type, param));
        
        internal static readonly Parser<char, Func<ILogicalRule, ILogicalRule, ILogicalRule>> And
            = BinaryLogical(Parsers.EnumValue(BinaryRuleType.And, true)
                .TrimStart()
                .Try());

        internal static readonly Parser<char, Func<ILogicalRule, ILogicalRule, ILogicalRule>> Or
            = BinaryLogical(Parsers.EnumValue(BinaryRuleType.Or, true)
                .TrimStart()
                .Try());

        internal static readonly Parser<char, Func<ILogicalRule, ILogicalRule, ILogicalRule>> NOR
            = BinaryLogical(Parsers.EnumValue(BinaryRuleType.NOR, true)
                .TrimStart()
                .Try());

        internal static readonly Parser<char, Func<ILogicalRule, ILogicalRule, ILogicalRule>> XOR
            = BinaryLogical(Parsers.EnumValue(BinaryRuleType.XOR, true)
                .TrimStart()
                .Try());

        internal static readonly Parser<char, Func<ILogicalRule, ILogicalRule, ILogicalRule>> NAND
            = BinaryLogical(Parsers.EnumValue(BinaryRuleType.NAND, true)
                .TrimStart()
                .Try());

        internal static readonly Parser<char, Func<ILogicalRule, ILogicalRule, ILogicalRule>> XNOR
            = BinaryLogical(Parsers.EnumValue(BinaryRuleType.XNOR, true)
                .TrimStart()
                .Try());

        internal static readonly Parser<char, Func<ILogicalRule, ILogicalRule>> Not
            = UnaryLogical(Parsers.EnumValue(UnaryRuleType.Not, true)
                .TrimStart()
                .Try());

        public static readonly Parser<char, ILogicalRule> Expr = ExpressionParser.Build<char, ILogicalRule>(
            rule => (
                Parser.OneOf(
                RuleParser.PlaceholderLogicalRule,
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

        internal static readonly Parser<char, ILogicalRule> Rule =
            Parser.Map((parameter, rule) => new PlaceholderLogicalRule(parameter, rule),
                    ParametersParser.PlaceholderParameter,
                    Expr.TrimStart())
                .As<char, PlaceholderLogicalRule, ILogicalRule>();

        internal static ILogicalRule ParseTemplate(string str)
        {
            return Rule.ParseOrThrow(str);
        }
}