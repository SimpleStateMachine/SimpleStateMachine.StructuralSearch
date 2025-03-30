using System.IO;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Parsing;
using SimpleStateMachine.StructuralSearch.Tests.Attributes;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests;

public static class ReplaceRuleParserTests
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

    [Theory]
    [InlineData("$var1$ => \"132\"")]
    [InlineData("$var1$ => $var3$")]
    [InlineData("$var1$ => $var3$.Length")]
    public static void AssignmentParsingShouldBeSuccess(string assignmentStr)
    {
        var assignment = ReplaceRuleParser.Assignment.ParseToEnd(assignmentStr);
        var result = assignment.ToString().ToLower();
        Assert.Equal(assignmentStr.ToLower(), result.ToLower());
    }

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
    [FilesData("ReplaceTemplate")]
    public static void FindTemplateFileParsingShouldBeSuccess(string filePath)
    {
        var templateStr = File.ReadAllText(filePath);
        var parsers = FindTemplateParser.Template.ParseToEnd(templateStr);
        Assert.NotEmpty(parsers);
    }
    
    [Theory]
    [FilesData("ReplaceRule")]
    public static void ReplaceRuleFileParsingShouldBeSuccess(string filePath)
    {
        var ruleStr = File.ReadAllText(filePath);
        ReplaceRuleParser.ReplaceRule.ParseToEnd(ruleStr);
    }
}