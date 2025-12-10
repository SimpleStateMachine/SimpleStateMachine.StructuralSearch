using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Parsing;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests.Unit.Parsing.ReplaceRule;

public static class ReplaceRuleConditionParserTests
{
    [Theory]
    [InlineData("if $var1$ equals $var2$ then")]
    [InlineData("if $var1$ equals \"123\" then")]
    [InlineData("if Not $var1$ equals $var$.Length then")]
    [InlineData("if Not $var1$ equals $var$.offset.Start then")]
    [InlineData("if $var1$ equals $var$.Length and Not $var1$ StartsWith \"123\" then")]
    [InlineData("if $var1$ equals $var$.Length and Not $var1$ StartsWith \"Test\" then")]
    public static void ReplaceRuleConditionParsingShouldBeSuccess(string condition)
    {
        var logicalOperation = ReplaceRuleParser.ReplaceRuleCondition.ParseToEnd(condition);
        var result = logicalOperation.ToString()!;
        Assert.Equal(condition.ToLower(), result.ToLower());
    }
}