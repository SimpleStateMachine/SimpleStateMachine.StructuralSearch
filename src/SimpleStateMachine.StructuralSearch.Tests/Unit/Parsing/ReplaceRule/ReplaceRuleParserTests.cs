using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Parsing;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests.Unit.Parsing.ReplaceRule;

public static class ReplaceRuleParserTests
{
    [Theory]
    [InlineData("if $var1$ equals $var2$ then $var1$ => \"test $var3$\"")]
    [InlineData("if $var1$ equals \"$\" then $var1$ => \"$\",$var2$ => \"132\"")]
    [InlineData("$var1$ => \"test $var3$.Length\"")]
    [InlineData("$var1$ => \"$\",$var2$ => \"132\"")]
    [InlineData("if Not $var1$ equals $var$.Length then $var1$ => $var$.Length")]
    [InlineData("if Not $var1$ equals $var$.offset.Start then $var1$ => $var$.offset.Start")]
    [InlineData("if $var1$ equals $var$.Length and Not $var1$ StartsWith \"123\" then $var1$ => $var$.offset.Start")]
    [InlineData("if $var1$ equals $var$.Length and Not $var1$ StartsWith \"123\" then $var1$ => $var$.offset.Start,$var2$ => $var$.offset.Start")]
    [InlineData("if $var1$ equals $var$.Length and Not $var1$ StartsWith \"Test\" then $var1$ => $var$.offset.Start")]
    public static void ReplaceRuleParsingShouldBeSuccess(string replaceRuleStr)
    {
        var rule = ReplaceRuleParser.ReplaceRule.ParseToEnd(replaceRuleStr);
        var result = rule.ToString().ToLower();
        Assert.Equal(replaceRuleStr.ToLower(), result);
    }

    [Theory]
    [InlineData("if $sign$ In (\"!=\", \"is not\") then $value2$ => $value1$, $value1$ => $value2$", "if $sign$ In \"!=\",\"is not\" then $value2$ => $value1$,$value1$ => $value2$")]
    public static void ReplaceRuleParsingSupportOptional(string replaceRuleStr, string resultStr)
    {
        var rule = ReplaceRuleParser.ReplaceRule.ParseToEnd(replaceRuleStr);
        Assert.Equal(rule.ToString().ToLower(), resultStr.ToLower());
    }
}