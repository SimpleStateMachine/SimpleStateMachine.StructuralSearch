namespace SimpleStateMachine.StructuralSearch.StructuralSearch;

internal static class FindRuleParser
{
    // private static readonly Parser<char, Func<IFindRule, IFindRule, IFindRule>> And = BinaryOperation(LogicalBinaryOperator.And);
    // private static readonly Parser<char, Func<IFindRule, IFindRule, IFindRule>> Or = BinaryOperation(LogicalBinaryOperator.Or);
    // private static readonly Parser<char, Func<IFindRule, IFindRule, IFindRule>> NOR = BinaryOperation(LogicalBinaryOperator.NOR);
    // private static readonly Parser<char, Func<IFindRule, IFindRule, IFindRule>> XOR = BinaryOperation(LogicalBinaryOperator.XOR);
    // private static readonly Parser<char, Func<IFindRule, IFindRule, IFindRule>> NAND = BinaryOperation(LogicalBinaryOperator.NAND);
    // private static readonly Parser<char, Func<IFindRule, IFindRule, IFindRule>> XNOR = BinaryOperation(LogicalBinaryOperator.XNOR);
    // private static readonly Parser<char, Func<IFindRule, IFindRule>> Not = UnaryOperation(LogicalUnaryOperator.Not);
    //
    // internal static readonly Parser<char, IFindRule> Expr = ExpressionParser.Build<char, IFindRule>
    // (
    //     rule =>
    //     (
    //         Parser.OneOf
    //         (
    //             FindSubRuleParser.OneOfSubRule,
    //             CommonParser.Parenthesised(rule, x => Parser.Char(x).Trim())
    //         ),
    //         [
    //             Operator.Prefix(Not),
    //             Operator.InfixL(And),
    //             Operator.InfixL(NOR),
    //             Operator.InfixL(XOR),
    //             Operator.InfixL(NAND),
    //             Operator.InfixL(XNOR),
    //             Operator.InfixL(Or)
    //         ]
    //     )
    // );

    // private static readonly Parser<char, ILogicalOperation>
    //
    // internal static IFindRule ParseTemplate(string? str)
    //     => string.IsNullOrEmpty(str)
    //         ? EmptyFindRule.Instance
    //         : Expr.Before(CommonParser.Eof).ParseOrThrow(str);

    // private static Parser<char, Func<IFindRule, IFindRule>> UnaryOperation(LogicalUnaryOperator @operator)
    //     => Parsers.EnumValue(@operator).Trim().Try()
    //         .Select(Func<IFindRule, IFindRule> (type) => param => new UnaryRule(type, param));
    //
    // private static Parser<char, Func<IFindRule, IFindRule, IFindRule>> BinaryOperation(LogicalBinaryOperator ruleType)
    //     => Parsers.EnumValue(ruleType).Trim().Try()
    //         .Select<Func<IFindRule, IFindRule, IFindRule>>(type => (left, right) => new BinaryRule(type, left, right));
}