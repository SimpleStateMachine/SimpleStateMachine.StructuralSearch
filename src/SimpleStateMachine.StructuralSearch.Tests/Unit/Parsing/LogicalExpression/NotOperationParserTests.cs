using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Parsing;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests.Unit.Parsing.LogicalExpression;

public static class NotOperationParserTests
{
    [Theory]
    [InlineData("Not \"123\" Equals \"789\"")]
    public static void NotOperationParsingShouldBeSuccess(string input)
    {
        LogicalExpressionParser.NotOperation.ParseToEnd(input);
    }
}