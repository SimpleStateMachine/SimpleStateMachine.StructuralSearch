using System;
using System.Collections.Generic;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Operator.Logical.Type;
using SimpleStateMachine.StructuralSearch.Parsing;
using SimpleStateMachine.StructuralSearch.Tests.Attributes;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests.Unit.Parsing.LogicalExpression;

public static class StringCompareOperationParserTests
{
    private static IEnumerable<string> StringCompareOperationCases()
    {
        foreach (var @operator in Enum.GetNames<StringCompareOperator>())
        {
            yield return $"{@operator} $var$";
            yield return $"{@operator}   $var$";
            yield return $"{@operator} \"123\"";
            yield return $"{@operator}   \"123\"";
        }
    }

    [Theory]
    [StringMemberData(nameof(StringCompareOperationCases))]
    public static void StringCompareOperationParsingShouldBeSuccess(string input)
    {
        LogicalExpressionParser.StringCompareOperation.ParseToEnd(input);
    }
}