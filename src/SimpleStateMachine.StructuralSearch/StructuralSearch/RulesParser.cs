using System;
using Pidgin;
using Pidgin.Expression;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Rules;


namespace SimpleStateMachine.StructuralSearch
{
    internal static class FindRuleParser
    {
        internal static Parser<char, Func<IRule, IRule, IRule>> Binary(Parser<char, BinaryRuleType> op)
            => op.Select<Func<IRule, IRule, IRule>>(type => (l, r) => new BinaryRule(type, l, r));

        internal static Parser<char, Func<IRule, IRule>> Unary(Parser<char, UnaryRuleType> op)
            => op.Select<Func<IRule, IRule>>(type => param => new UnaryRule(type, param));


        internal static readonly Parser<char, Func<IRule, IRule, IRule>> And
            = Binary(Parsers.EnumValue(BinaryRuleType.And).Trim());

        internal static readonly Parser<char, Func<IRule, IRule, IRule>> Or
            = Binary(Parsers.EnumValue(BinaryRuleType.Or).Trim());

        internal static readonly Parser<char, Func<IRule, IRule, IRule>> NOR
            = Binary(Parsers.EnumValue(BinaryRuleType.NOR).Trim());

        internal static readonly Parser<char, Func<IRule, IRule, IRule>> XOR
            = Binary(Parsers.EnumValue(BinaryRuleType.XOR).Trim());

        internal static readonly Parser<char, Func<IRule, IRule, IRule>> NAND
            = Binary(Parsers.EnumValue(BinaryRuleType.NAND).Trim());

        internal static readonly Parser<char, Func<IRule, IRule, IRule>> XNOR
            = Binary(Parsers.EnumValue(BinaryRuleType.XNOR).Trim());

        internal static readonly Parser<char, Func<IRule, IRule>> Not
            = Unary(Parsers.EnumValue(UnaryRuleType.Not).Trim());

        internal static readonly Parser<char, IRule> Expr = ExpressionParser.Build<char, IRule>(
            rule => (
                Parser.OneOf(
                    SubRuleParser.UnarySubRule,
                    SubRuleParser.IsSubRule,
                    SubRuleParser.InSubRule,
                    CommonParser.Parenthesised(rule, x => x.Trim())
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

        internal static readonly Parser<char, IRule> Rule =
            Parser.Map((name, rule) => new Rule(name, rule),
                    CommonTemplateParser.Placeholder.Trim(),
                    Expr)
                .As<char, Rule, IRule>();

        internal static IRule ParseTemplate(string str)
        {
            return Rule.ParseOrThrow(str);
        }
    }
}