using Pidgin;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests;

public static class ReplaceRuleParserTests
{
    [Theory]
    [InlineData("$var1$ equals $var2$ then $var1$ => \"test $var3$\"")]
    [InlineData("$var1$ equals \"\\$\" then $var1$ => \"\\$\",$var2$ => \"132\"")]
    [InlineData("_ then $var1$ => \"test $var3$.Lenght\"")]
    [InlineData("_ then $var1$ => \"\\$\",$var2$ => \"132\"")]
    [InlineData("Not $var1$ equals $var$.Lenght then $var1$ => $var$.Lenght")]
    [InlineData("Not $var1$ equals $var$.offset.Start then $var1$ => $var$.offset.Start")]
    [InlineData("$var1$ equals $var$.Lenght and Not $var1$ StartsWith \"123\" then $var1$ => $var$.offset.Start.Trim")]
    [InlineData("$var1$ equals $var$.Lenght and Not $var1$ StartsWith \"123\" then $var1$ => $var$.offset.Start.Trim,$var2$ => $var$.offset.Start.Trim")]
    [InlineData("$var1$ equals $var$.Lenght and Not $var1$ StartsWith \"\\\"Test\" then $var1$ => $var$.offset.Start.ToUpper")]
    public static void ReplaceRuleParsingShouldBeSuccess(string replaceRule)
    {
        var rule = StructuralSearch.ParseReplaceRule(replaceRule);
        var ruleStr = rule.ToString().ToLower();
        Assert.NotNull(rule);
        Assert.Equal(ruleStr, replaceRule.ToLower());
    }
        
    [Theory]
    [InlineData("($var1$ equals $var2$) then $var1$ => \"test $var3$\"", "$var1$ equals $var2$ then $var1$ => \"test $var3$\"")]
    public static void ReplaceRuleShouldBeEqualsString(string replaceRule, string customResult)
    {
        var rule = StructuralSearch.ParseReplaceRule(replaceRule);
        var ruleStr = rule.ToString().ToLower();
        Assert.NotNull(rule);
        Assert.Equal(ruleStr, customResult.ToLower());
    }
        
    [Theory]
    [InlineData("$var1$ equals $var2$ then $var1$ => (\"test $var3$\"")]
    [InlineData("($var1$ equals $var2$ then $var1$ => \"test $var3$\"")]
    [InlineData("$var1$ equals $var2$ then ($var1$) => \"test $var3$\"")]
    public static void ReplaceRuleParsingShouldBeFail(string replaceRuleStr)
    {
        Assert.Throws<ParseException>(() => StructuralSearch.ParseReplaceRule(replaceRuleStr));
    }
}