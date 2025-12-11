using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Parsing;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests.Unit.Parsing.LogicalExpression;

public static class MatchOperationParserTests
{
    [Theory]
    [InlineData("Match \"[a-z]\"")]
    public static void MatchOperationParsingShouldBeSuccess(string input)
    {
        LogicalExpressionParser.MatchOperation.ParseToEnd(input);
    }
}