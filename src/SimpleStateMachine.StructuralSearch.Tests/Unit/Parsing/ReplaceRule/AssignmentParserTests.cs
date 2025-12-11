using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Parsing;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests.Unit.Parsing.ReplaceRule;

public static class AssignmentParserTests
{
    [Theory]
    [InlineData("$var1$ => \"132\"")]
    [InlineData("$var1$ => $var3$")]
    [InlineData("$var1$ => $var3$.Length")]
    public static void AssignmentParsingShouldBeSuccess(string assignmentStr)
    {
        var assignment = ReplaceRuleParser.Assignment.ParseToEnd(assignmentStr);
        Assert.Equal(assignmentStr, assignment.ToString(), true);
    }
}