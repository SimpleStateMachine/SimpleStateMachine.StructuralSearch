using System;
using Pidgin;
using Pidgin.Expression;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Rules;

namespace SimpleStateMachine.StructuralSearch
{
    public static class FindRuleParser
    {
        internal static Parser<char, Func<IRule, IRule, IRule>> Binary(Parser<char, BinaryRuleType> op)
            => op.Select<Func<IRule, IRule, IRule>>(type => (l, r) => new BinaryRule(type, l, r));

        internal static Parser<char, Func<IRule, IRule>> Unary(Parser<char, UnaryRuleType> op)
            => op.Select<Func<IRule, IRule>>(type => param => new UnaryRule(type, param));
        
        internal static readonly Parser<char, Func<IRule, IRule, IRule>> And
            = Binary(Parsers.EnumValue(BinaryRuleType.And, true)
                .Trim()
                .Try())
                .Labelled($"{nameof(FindRuleParser)}.{nameof(And)}");

        internal static readonly Parser<char, Func<IRule, IRule, IRule>> Or
            = Binary(Parsers.EnumValue(BinaryRuleType.Or, true)
                .Trim()
                .Try())
                .Labelled($"{nameof(FindRuleParser)}.{nameof(Or)}");

        internal static readonly Parser<char, Func<IRule, IRule, IRule>> NOR
            = Binary(Parsers.EnumValue(BinaryRuleType.NOR, true)
                .Trim()
                .Try())
                .Labelled($"{nameof(FindRuleParser)}.{nameof(NOR)}");

        internal static readonly Parser<char, Func<IRule, IRule, IRule>> XOR
            = Binary(Parsers.EnumValue(BinaryRuleType.XOR, true)
                .Trim()
                .Try())
                .Labelled($"{nameof(FindRuleParser)}.{nameof(XOR)}");

        internal static readonly Parser<char, Func<IRule, IRule, IRule>> NAND
            = Binary(Parsers.EnumValue(BinaryRuleType.NAND, true)
                .Trim()
                .Try())
                .Labelled($"{nameof(FindRuleParser)}.{nameof(NAND)}");

        internal static readonly Parser<char, Func<IRule, IRule, IRule>> XNOR
            = Binary(Parsers.EnumValue(BinaryRuleType.XNOR, true)
                .Trim()
                .Try())
                .Labelled($"{nameof(FindRuleParser)}.{nameof(XNOR)}");

        internal static readonly Parser<char, Func<IRule, IRule>> Not
            = Unary(Parsers.EnumValue(UnaryRuleType.Not, true)
                .Trim()
                .Try())
                .Labelled($"{nameof(FindRuleParser)}.{nameof(Not)}");

        public static readonly Parser<char, IRule> Expr = ExpressionParser.Build<char, IRule>(
            rule => (
                Parser.OneOf(
                    SubRuleParser.OneOfSubRule,
                    CommonParser.Parenthesised(rule, x => Parser.Char(x).Trim())
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

        internal static IRule ParseTemplate(string? str) 
            => string.IsNullOrEmpty(str)
                ? Rule.Empty
                : Expr.Before(CommonParser.EOF).ParseOrThrow(str);
    }
}