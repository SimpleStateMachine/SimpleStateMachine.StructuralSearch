using System;
using Pidgin;
using Pidgin.Expression;
using SimpleStateMachine.StructuralSearch.CustomParsers;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Rules.FindRules;
using SimpleStateMachine.StructuralSearch.Rules.FindRules.Types;

namespace SimpleStateMachine.StructuralSearch.StructuralSearch;

internal static class FindRuleParser
{
    private static readonly Parser<char, Func<IFindRule, IFindRule, IFindRule>> And = BinaryOperation(BinaryRuleType.And);
    private static readonly Parser<char, Func<IFindRule, IFindRule, IFindRule>> Or = BinaryOperation(BinaryRuleType.Or);
    private static readonly Parser<char, Func<IFindRule, IFindRule, IFindRule>> NOR = BinaryOperation(BinaryRuleType.NOR);
    private static readonly Parser<char, Func<IFindRule, IFindRule, IFindRule>> XOR = BinaryOperation(BinaryRuleType.XOR);
    private static readonly Parser<char, Func<IFindRule, IFindRule, IFindRule>> NAND = BinaryOperation(BinaryRuleType.NAND);
    private static readonly Parser<char, Func<IFindRule, IFindRule, IFindRule>> XNOR = BinaryOperation(BinaryRuleType.XNOR);
    private static readonly Parser<char, Func<IFindRule, IFindRule>> Not = UnaryOperation(UnaryRuleType.Not);

    internal static readonly Parser<char, IFindRule> Expr = ExpressionParser.Build<char, IFindRule>
    (
        rule =>
        (
            Parser.OneOf
            (
                FindSubRuleParser.OneOfSubRule,
                CommonParser.Parenthesised(rule, x => Parser.Char(x).Trim())
            ),
            [
                Operator.Prefix(Not),
                Operator.InfixL(And),
                Operator.InfixL(NOR),
                Operator.InfixL(XOR),
                Operator.InfixL(NAND),
                Operator.InfixL(XNOR),
                Operator.InfixL(Or)
            ]
        )
    );

    internal static IFindRule ParseTemplate(string? str)
        => string.IsNullOrEmpty(str)
            ? EmptyFindRule.Instance
            : Expr.Before(CommonParser.Eof).ParseOrThrow(str);

    private static Parser<char, Func<IFindRule, IFindRule>> UnaryOperation(UnaryRuleType ruleType)
        => Parsers.EnumValue(ruleType).Trim().Try()
            .Select(Func<IFindRule, IFindRule> (type) => param => new UnaryRule(type, param));

    private static Parser<char, Func<IFindRule, IFindRule, IFindRule>> BinaryOperation(BinaryRuleType ruleType)
        => Parsers.EnumValue(ruleType).Trim().Try()
            .Select<Func<IFindRule, IFindRule, IFindRule>>(type => (left, right) => new BinaryRule(type, left, right));
}