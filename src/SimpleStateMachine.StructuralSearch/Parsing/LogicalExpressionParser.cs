using System;
using System.Collections.Generic;
using System.Linq;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Operator.Logical;
using SimpleStateMachine.StructuralSearch.Operator.Logical.Type;
using SimpleStateMachine.StructuralSearch.Parameters;

namespace SimpleStateMachine.StructuralSearch.Parsing;

internal static class LogicalExpressionParser
{
    // string_compare_operation = ('Equals' | 'Contains' | 'StartsWith' | 'EndsWith') string_expr
    internal static readonly Parser<char, Func<IParameter, ILogicalOperation>> StringCompareOperation = 
        Parser.CIEnum<StringCompareOperator>()
            .TrimEnd() // Skip whitespaces
            .Then(ParametersParser.StringExpression, (@operator, right) => (@operator, right))
            .Select<Func<IParameter, ILogicalOperation>>(x => 
                left => new StringCompareOperation(left, x.@operator, x.right));

    // is_operation ='Is' ('Var' | 'Int' | 'Double' | 'DateTime' | 'Guid' | 'Bool')
    internal static readonly Parser<char, Func<IParameter, ILogicalOperation>> IsOperation = 
        CommonParser.Is
            .TrimEnd() // Skip whitespaces
            .Then(Parser.CIEnum<ParameterType>())
            .Select<Func<IParameter, ILogicalOperation>>(type => parameter => new IsOperation(parameter, type));

    // match_operation ='Match' '"' <regex> '"'
    internal static readonly Parser<char, Func<IParameter, ILogicalOperation>> MatchOperation = 
        CommonParser.Match
            .TrimEnd() // Skip whitespaces
            .Then(Grammar.StringLiteral)
            .Select<Func<IParameter, ILogicalOperation>>(regex => parameter => new MatchOperation(parameter, regex));

    // string_expr { ',' string_expr }
    private static readonly Parser<char, IEnumerable<IParameter>> InOperationParameters =
        ParametersParser.StringExpression.SeparatedAtLeastOnce(CommonParser.Comma.TrimEnd());

    // in_operation ='In' [ '(' ] string_expr { ',' string_expr } [ ')' ]
    internal static readonly Parser<char, Func<IParameter, ILogicalOperation>> InOperation = 
        CommonParser.In
            .TrimEnd()// Skip whitespaces
            // .Then(InOperationParameters)
            .Then(Parser.OneOf(InOperationParameters.BetweenParentheses((_, parameters, _) => parameters), InOperationParameters))
            .Select<Func<IParameter, ILogicalOperation>>(arguments => 
                parameter => new InOperation(parameter, arguments.ToList()));

    // string_logic_operation = string_expr (string_compare_operation | is_operation | match_operation| in_operation )
    private static readonly Parser<char, ILogicalOperation> StringLogicOperation = 
        ParametersParser.StringExpression
            .TrimEnd() // skip whitespaces
            .Then(Parser.OneOf(StringCompareOperation.Try(), IsOperation.Try(), MatchOperation.Try(), InOperation), 
            (parameter, buildOperationFunc) => buildOperationFunc(parameter));

    // binary_operation = logic_expr ('And' | 'Or' | 'NAND' | 'NOR' | 'XOR' | 'XNOR') logic_expr
    internal static readonly Parser<char, Func<ILogicalOperation, ILogicalOperation>> BinaryOperation = 
        Parser.CIEnum<LogicalBinaryOperator>()
            .TrimEnd() // Skip whitespaces
            .Then(Parser.Rec(() => LogicalExpression ?? throw new ArgumentNullException(nameof(LogicalExpression))), (@operator, right) => (@operator, right))
            .Select<Func<ILogicalOperation, BinaryOperation>>(x => left => new BinaryOperation(left, x.@operator, x.right))
            .AtLeastOnce().Select<Func<ILogicalOperation, ILogicalOperation>>(funcList =>
                placeholder => funcList.Aggregate(placeholder, (parameter, func) => func(parameter)));

    // not_operation = 'Not' logic_expr
    internal static readonly Parser<char, NotOperation> NotOperation = 
        CommonParser.Not
            .TrimEnd() // Skip whitespaces
            .Then(Parser.Rec(() => LogicalExpression ?? throw new ArgumentNullException(nameof(LogicalExpression))))
            .Select(operation => new NotOperation(operation));

    // logic_expr_term = 'Not' logic_term | string_logic_operation | '(' logic_expr ')'
    private static readonly Parser<char, ILogicalOperation> LogicalExpressionTerm =
        Parser.OneOf
        (
            NotOperation.Cast<ILogicalOperation>(),
            StringLogicOperation,
            Parser.Rec(() => LogicalExpression ?? throw new ArgumentNullException(nameof(LogicalExpression)))
                .After(CommonParser.LeftParenthesis)
                .Before(CommonParser.RightParenthesis)
        );

    // logic_expr = logic_term { binary_operation logic_term }
    internal static readonly Parser<char, ILogicalOperation> LogicalExpression =
        LogicalExpressionTerm
            .TrimEnd() // Skip whitespaces 
            .Then(BinaryOperation.Optional(),
            (operation, optionalBinaryOperation) =>
                optionalBinaryOperation.HasValue ? optionalBinaryOperation.Value(operation) : operation);
}