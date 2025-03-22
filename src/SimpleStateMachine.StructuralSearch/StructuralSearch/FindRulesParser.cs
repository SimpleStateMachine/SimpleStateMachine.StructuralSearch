using System;
using Pidgin;
using Pidgin.Expression;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Rules.FindRules;
using SimpleStateMachine.StructuralSearch.Rules.FindRules.Types;

namespace SimpleStateMachine.StructuralSearch.StructuralSearch;

internal static class FindRuleParser
{
    internal static Parser<char, Func<IFindRule, IFindRule, IFindRule>> Binary(Parser<char, BinaryRuleType> op)
        => op.Select<Func<IFindRule, IFindRule, IFindRule>>(type => (l, r) => new BinaryRule(type, l, r));

    internal static Parser<char, Func<IFindRule, IFindRule>> Unary(Parser<char, UnaryRuleType> op)
        => op.Select<Func<IFindRule, IFindRule>>(type => param => new UnaryRule(type, param));
        
    internal static readonly Parser<char, Func<IFindRule, IFindRule, IFindRule>> And
        = Binary(Parsers.Parsers.EnumValue(BinaryRuleType.And, true)
                .Trim()
                .Try())
            .Labelled($"{nameof(FindRuleParser)}.{nameof(And)}");

    internal static readonly Parser<char, Func<IFindRule, IFindRule, IFindRule>> Or
        = Binary(Parsers.Parsers.EnumValue(BinaryRuleType.Or, true)
                .Trim()
                .Try())
            .Labelled($"{nameof(FindRuleParser)}.{nameof(Or)}");

    internal static readonly Parser<char, Func<IFindRule, IFindRule, IFindRule>> NOR
        = Binary(Parsers.Parsers.EnumValue(BinaryRuleType.NOR, true)
                .Trim()
                .Try())
            .Labelled($"{nameof(FindRuleParser)}.{nameof(NOR)}");

    internal static readonly Parser<char, Func<IFindRule, IFindRule, IFindRule>> XOR
        = Binary(Parsers.Parsers.EnumValue(BinaryRuleType.XOR, true)
                .Trim()
                .Try())
            .Labelled($"{nameof(FindRuleParser)}.{nameof(XOR)}");

    internal static readonly Parser<char, Func<IFindRule, IFindRule, IFindRule>> NAND
        = Binary(Parsers.Parsers.EnumValue(BinaryRuleType.NAND, true)
                .Trim()
                .Try())
            .Labelled($"{nameof(FindRuleParser)}.{nameof(NAND)}");

    internal static readonly Parser<char, Func<IFindRule, IFindRule, IFindRule>> XNOR
        = Binary(Parsers.Parsers.EnumValue(BinaryRuleType.XNOR, true)
                .Trim()
                .Try())
            .Labelled($"{nameof(FindRuleParser)}.{nameof(XNOR)}");

    internal static readonly Parser<char, Func<IFindRule, IFindRule>> Not
        = Unary(Parsers.Parsers.EnumValue(UnaryRuleType.Not, true)
                .Trim()
                .Try())
            .Labelled($"{nameof(FindRuleParser)}.{nameof(Not)}");

    public static readonly Parser<char, IFindRule> Expr = ExpressionParser.Build<char, IFindRule>(
        rule => (
            Parser.OneOf(
                SubRuleParser.OneOfSubRule,
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
            ? Rule.Empty
            : Expr.Before(CommonParser.EOF).ParseOrThrow(str);
}