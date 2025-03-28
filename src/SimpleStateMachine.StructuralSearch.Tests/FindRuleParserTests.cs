using System.IO;
using System.Linq;
using Pidgin;
using SimpleStateMachine.StructuralSearch.StructuralSearch;
using Xunit;
using TemplateParser = SimpleStateMachine.StructuralSearch.CustomParsers.TemplateParser;

namespace SimpleStateMachine.StructuralSearch.Tests;

public static class FindRuleParserTests
{
    [Theory]
    [InlineData("$var$ equals $var$")]
    [InlineData("$var$ equals \"$\"")]
    [InlineData("Not $var$ equals $var$.Lenght")]
    [InlineData("Not $var$ equals $var$.offset.Start")]
    [InlineData("$var$ equals $var$.Lenght and Not $var$ StartsWith \"123\"")]
    [InlineData("Not $var$ equals $var$.Lenght and $var$ StartsWith \"123\"")]
    [InlineData("$var$ Contains $var$.Lenght")]
    [InlineData("not $var$ Contains \"123\"")]
    [InlineData("$var1$.Lenght Contains $var2$.Lenght")]
    [InlineData("$var$ Contains \"123\"")]
    [InlineData("$var$ StartsWith $var$.Lenght")]
    [InlineData("$var$.Lenght Equals $var$.Lenght")]
    [InlineData("$var$ StartsWith \"123\"")]
    [InlineData("$var$ EndsWith $var$.Lenght")]
    [InlineData("$var$ EndsWith \"123\"")]
    [InlineData("$var$ Is Int")]
    [InlineData("$var$ Is DateTime")]
    [InlineData("$var$ equals $var1$ or $var2$ equals $var1$")]
    [InlineData("$var$ match [a-b]+")]
    public static void FindRuleExprParsingShouldBeSuccess(string ruleStr)
    {
        var t = TemplatesParser.Template;
        var rule = LogicalExpressionParser.LogicalExpression.Before(CommonParser.Eof).ParseOrThrow(ruleStr);
        var _ruleStr = rule.ToString()?.ToLower();
        Assert.NotNull(rule);
        Assert.Equal(ruleStr.ToLower(), _ruleStr);
    }
        
    [Theory]
    [InlineData("$var$ In \"Is\", \"==\", \"!=\", \"is not\"", "$var$ In \"Is\",\"==\",\"!=\",\"is not\"")]
    [InlineData("$var$ In (\"Is\", \"==\", \"!=\", \"is not\")", "$var$ In \"Is\",\"==\",\"!=\",\"is not\"")]
    [InlineData("$var$ In \"Is\",\"==\",\"!=\",\"is not\"", "$var$ In \"Is\",\"==\",\"!=\",\"is not\"")]
    [InlineData("$var$ In (\"Is\",\"==\",\"!=\",\"is not\")", "$var$ In \"Is\",\"==\",\"!=\",\"is not\"")]
    [InlineData("$var$ In(\"Is\",\"==\",\"!=\",\"is not\")", "$var$ In \"Is\",\"==\",\"!=\",\"is not\"")]
    [InlineData("$var$ In ( \"Is\",\"==\",\"!=\",\"is not\" ) ", "$var$ In \"Is\",\"==\",\"!=\",\"is not\"")]
    [InlineData("$var$ In ( \"Is\",\"==\",\"!=\",\"is not\" )", "$var$ In \"Is\",\"==\",\"!=\",\"is not\"")]
    [InlineData("$var$ In ( \"Is\",\"==\",\"!=\",\"is not\") ", "$var$ In \"Is\",\"==\",\"!=\",\"is not\"")]
    [InlineData("$var$ In (\"Is\",\"==\",\"!=\",\"is not\" ) ", "$var$ In \"Is\",\"==\",\"!=\",\"is not\"")]
    [InlineData("$var$ In (\"Is\",\"==\",\"!=\",\"is not\" )", "$var$ In \"Is\",\"==\",\"!=\",\"is not\"")]
    [InlineData("$var$ In (\"Is\",\"==\",\"!=\",\"is not\") ", "$var$ In \"Is\",\"==\",\"!=\",\"is not\"")]
    [InlineData("$var$ In( \"Is\",\"==\",\"!=\",\"is not\" ) ", "$var$ In \"Is\",\"==\",\"!=\",\"is not\"")]
    [InlineData("$var$ In( \"Is\",\"==\",\"!=\",\"is not\" )", "$var$ In \"Is\",\"==\",\"!=\",\"is not\"")]
    [InlineData("$var$ In( \"Is\",\"==\",\"!=\",\"is not\") ", "$var$ In \"Is\",\"==\",\"!=\",\"is not\"")]
    [InlineData("Not ($var$ equals $var$.Lenght and $var$ StartsWith \"123\")", "Not $var$ equals $var$.Lenght and $var$ StartsWith \"123\"")]
    [InlineData("Not ($var$ equals $var$.Lenght)", "Not $var$ equals $var$.Lenght")]
    public static void FindRuleExprParsingShouldBeEqualsCustomResult(string ruleStr, string customResult)
    {
        var rule = LogicalExpressionParser.LogicalExpression.ParseOrThrow(ruleStr);
        var ruleAsStr = rule.ToString()?.ToLower();
        Assert.NotNull(rule);
        Assert.Equal(ruleAsStr, customResult.ToLower());
    }

    [Theory]
    [InlineData("FindRule/NullUnionOperator.txt", "$sign$ In \"is\",\"==\",\"!=\",\"is not\"")]
    [InlineData("FindRule/AssignmentNullUnionOperator.txt", "$sign$ In \"is\",\"==\",\"!=\",\"is not\"")]
    public static void FindRuleParsingFromFileShouldBeSuccess(string filePath, params string[] customResult)
    {
        var ruleStr = File.ReadAllText(filePath);
        var rules = ruleStr.Split(Constant.LineFeed)
            .Select(StructuralSearch.StructuralSearch.ParseFindRule);
        var rulesAsStr = rules.Select(x => x.ToString()).ToArray();
            
        Assert.True(customResult.SequenceEqual(rulesAsStr));
    }
}