using Pidgin;
using SimpleStateMachine.StructuralSearch.StructuralSearch;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests;

public static class ReplaceRuleParserTests
{
    [Theory]
    [InlineData("if $var1$ equals $var2$ then")]
    [InlineData("if Not $var1$ equals $var$.Lenght then")]
    [InlineData("if Not $var1$ equals $var$.offset.Start then")]
    [InlineData("if $var1$ equals $var$.Lenght and Not $var1$ StartsWith \"123\" then")]
    [InlineData("if $var1$ equals $var$.Lenght and Not $var1$ StartsWith \"Test\" then")]
    public static void ReplaceConditionParsingShouldBeSuccess(string replaceRule)
    {
        var rule = ReplaceRuleParser.ReplaceCondition.Before(CommonParser.Eof).ParseOrThrow(replaceRule);
        var ruleStr = rule.ToString()?.ToLower();
        Assert.NotNull(rule);
        Assert.Equal(replaceRule.ToLower(), ruleStr);
    }
    
    [Theory]
    [InlineData("if $var1$ equals $var2$ then $var1$ => \"test $var3$\"")]
    [InlineData("if $var1$ equals \"\\$\" then $var1$ => \"\\$\",$var2$ => \"132\"")]
    [InlineData("$var1$ => \"test $var3$.Lenght\"")]
    [InlineData("$var1$ => \"$\",$var2$ => \"132\"")]
    [InlineData("if Not $var1$ equals $var$.Lenght then $var1$ => $var$.Lenght")]
    [InlineData("if Not $var1$ equals $var$.offset.Start then $var1$ => $var$.offset.Start")]
    [InlineData("if $var1$ equals $var$.Lenght and Not $var1$ StartsWith \"123\" then $var1$ => $var$.offset.Start")]
    [InlineData("if $var1$ equals $var$.Lenght and Not $var1$ StartsWith \"123\" then $var1$ => $var$.offset.Start,$var2$ => $var$.offset.Start")]
    [InlineData("if $var1$ equals $var$.Lenght and Not $var1$ StartsWith \"Test\" then $var1$ => $var$.offset.Start")]
    public static void ReplaceRuleParsingShouldBeSuccess(string replaceRule)
    {
        var rule = StructuralSearch.StructuralSearch.ParseReplaceRule(replaceRule);
        var ruleStr = rule.ToString()?.ToLower();
        Assert.NotNull(rule);
        Assert.Equal(ruleStr, replaceRule.ToLower());
    }
        
    [Theory]
    [InlineData("($var1$ equals $var2$) then $var1$ => \"test $var3$\"", "$var1$ equals $var2$ then $var1$ => \"test $var3$\"")]
    public static void ReplaceRuleShouldBeEqualsString(string replaceRule, string customResult)
    {
        var rule = StructuralSearch.StructuralSearch.ParseReplaceRule(replaceRule);
        var ruleStr = rule.ToString()?.ToLower();
        Assert.NotNull(rule);
        Assert.Equal(ruleStr, customResult.ToLower());
    }
        
    [Theory]
    [InlineData("$var1$ equals $var2$ then $var1$ => (\"test $var3$\"")]
    [InlineData("($var1$ equals $var2$ then $var1$ => \"test $var3$\"")]
    [InlineData("$var1$ equals $var2$ then ($var1$) => \"test $var3$\"")]
    public static void ReplaceRuleParsingShouldBeFail(string replaceRuleStr)
    {
        Assert.Throws<ParseException<char>>(() => StructuralSearch.StructuralSearch.ParseReplaceRule(replaceRuleStr));
    }
}