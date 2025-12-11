using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Parsing;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests.Unit.Parsing.LogicalExpression;

public static class InOperationParserTests
{
    [Theory]
    [InlineData("In \"123\",\"456\",\"789\"")]
    [InlineData("In   \"123\",\"456\",\"789\"")]
    [InlineData("In \"123\", \"456\", \"789\"")]
    [InlineData("In \"123\",  \"456\",  \"789\"")]
    [InlineData("In   \"123\", \"456\", \"789\"")]
    [InlineData("In   \"123\",  \"456\",  \"789\"")]
    public static void InOperationParsingShouldBeSuccess(string input)
    {
        LogicalExpressionParser.InOperation.ParseToEnd(input);
    }
}