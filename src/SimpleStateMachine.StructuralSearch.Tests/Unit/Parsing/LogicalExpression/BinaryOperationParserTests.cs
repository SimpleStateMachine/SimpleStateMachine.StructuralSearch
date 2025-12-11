using System;
using System.Collections.Generic;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Operator.Logical.Type;
using SimpleStateMachine.StructuralSearch.Parsing;
using SimpleStateMachine.StructuralSearch.Tests.Attributes;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests.Unit.Parsing.LogicalExpression;

public static class BinaryOperationParserTests
{
    private static IEnumerable<string> BinaryOperationCases()
    {
        foreach (var @operator in Enum.GetNames<LogicalBinaryOperator>())
        {
            yield return $"{@operator} $var$ Equals $var$";
            yield return $"{@operator}    $var$ Equals $var$";
            yield return $"{@operator} $var$ Equals    $var$";
            yield return $"{@operator}    $var$ Equals    $var$";
        }
    }

    [Theory]
    [StringMemberData(nameof(BinaryOperationCases))]
    public static void BinaryOperationParsingShouldBeSuccess(string input)
    {
        LogicalExpressionParser.BinaryOperation.ParseToEnd(input);
    }
}