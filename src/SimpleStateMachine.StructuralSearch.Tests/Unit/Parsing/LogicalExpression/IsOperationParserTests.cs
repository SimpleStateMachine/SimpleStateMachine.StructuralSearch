using System;
using System.Collections.Generic;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Operator.Logical.Type;
using SimpleStateMachine.StructuralSearch.Parsing;
using SimpleStateMachine.StructuralSearch.Tests.Attributes;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests.Unit.Parsing.LogicalExpression;

public static class IsOperationParserTests
{
    private static IEnumerable<string> IsOperationCases()
    {
        foreach (var type in Enum.GetNames<ParameterType>())
        {
            yield return $"Is {type}";
            yield return $"Is    {type}";
        }
    }

    [Theory]
    [StringMemberData(nameof(IsOperationCases))]
    public static void IsOperationParsingShouldBeSuccess(string input)
    {
        LogicalExpressionParser.IsOperation.ParseToEnd(input);
    }
}