using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Parsing;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests.Unit.Parsing.LogicalExpression;

public static class LogicalExpressionParserTests
{
    [Theory]
    [InlineData("$var$ equals $var$")]
    [InlineData("$var$ equals \"$\"")]
    [InlineData("Not $var$ equals $var$.Length")]
    [InlineData("Not $var$ equals $var$.offset.Start")]
    [InlineData("$var$ equals $var$.Length and Not $var$ StartsWith \"123\"")]
    [InlineData("Not $var$ equals $var$.Length and $var$ StartsWith \"123\"")]
    [InlineData("$var$ Contains $var$.Length")]
    [InlineData("not $var$ Contains \"123\"")]
    [InlineData("$var1$.Length Contains $var2$.Length")]
    [InlineData("$var$ Contains \"123\"")]
    [InlineData("$var$ StartsWith $var$.Length")]
    [InlineData("$var$.Length Equals $var$.Length")]
    [InlineData("$var$ StartsWith \"123\"")]
    [InlineData("$var$ EndsWith $var$.Length")]
    [InlineData("$var$ EndsWith \"123\"")]
    [InlineData("$var$ Is Int")]
    [InlineData("$var$ Is DateTime")]
    [InlineData("$var$ equals $var1$ or $var2$ equals $var1$")]
    [InlineData("$var$ match \"[a-b]+\"")]
    public static void LogicalExpressionParsingShouldBeSuccess(string input)
    {
        var operation = LogicalExpressionParser.LogicalExpression.ParseToEnd(input);
        Assert.Equal(input, operation.ToString(), true);
    }

    [Theory]
    [InlineData("$var$ Is int", "$var$ Is int")]
    [InlineData("$var$ In \"Is\", \"==\", \"!=\", \"is not\"", "$var$ In \"Is\",\"==\",\"!=\",\"is not\"")]
    [InlineData("$var$ In (\"Is\", \"==\", \"!=\", \"is not\")", "$var$ In \"Is\",\"==\",\"!=\",\"is not\"")]
    [InlineData("$var$ In \"Is\",\"==\",\"!=\",\"is not\"", "$var$ In \"Is\",\"==\",\"!=\",\"is not\"")]
    [InlineData("$var$ In \"Is\" , \"==\" , \"!=\" , \"is not\"", "$var$ In \"Is\",\"==\",\"!=\",\"is not\"")]
    [InlineData("$var$ In (\"Is\",\"==\",\"!=\",\"is not\")", "$var$ In \"Is\",\"==\",\"!=\",\"is not\"")]
    [InlineData("$var$ In(\"Is\",\"==\",\"!=\",\"is not\")", "$var$ In \"Is\",\"==\",\"!=\",\"is not\"")]
    [InlineData("$var$ In (\"Is\",\"==\",\"!=\",\"is not\") ", "$var$ In \"Is\",\"==\",\"!=\",\"is not\"")]
    [InlineData("$var$ In ( \"Is\",\"==\",\"!=\",\"is not\" ) ", "$var$ In \"Is\",\"==\",\"!=\",\"is not\"")]
    [InlineData("$var$ In ( \"Is\",\"==\",\"!=\",\"is not\" )", "$var$ In \"Is\",\"==\",\"!=\",\"is not\"")]
    [InlineData("$var$ In ( \"Is\",\"==\",\"!=\",\"is not\") ", "$var$ In \"Is\",\"==\",\"!=\",\"is not\"")]
    [InlineData("$var$ In (\"Is\",\"==\",\"!=\",\"is not\" ) ", "$var$ In \"Is\",\"==\",\"!=\",\"is not\"")]
    [InlineData("$var$ In (\"Is\",\"==\",\"!=\",\"is not\" )", "$var$ In \"Is\",\"==\",\"!=\",\"is not\"")]
    [InlineData("$var$ In( \"Is\",\"==\",\"!=\",\"is not\" ) ", "$var$ In \"Is\",\"==\",\"!=\",\"is not\"")]
    [InlineData("$var$ In( \"Is\",\"==\",\"!=\",\"is not\" )", "$var$ In \"Is\",\"==\",\"!=\",\"is not\"")]
    [InlineData("$var$ In( \"Is\",\"==\",\"!=\",\"is not\") ", "$var$ In \"Is\",\"==\",\"!=\",\"is not\"")]
    [InlineData("Not $var$ Is int", "Not $var$ Is int")]
    [InlineData("$var$ equals $var$.Length", "$var$ equals $var$.Length")]
    [InlineData("$var$ equals (\"123\")", "$var$ equals (\"123\")")]
    [InlineData("Not $var$ equals $var$.Length", "Not $var$ equals $var$.Length")]
    [InlineData("Not ($var$ equals $var$.Length)", "Not ($var$ equals $var$.Length)")]
    [InlineData("Not ($var$ equals $var$.Length and $var$ StartsWith \"123\")", "Not ($var$ equals $var$.Length and $var$ StartsWith \"123\")")]
    public static void LogicalExpressionParsingShouldBeEqualsCustomResult(string ruleStr, string customResult)
    {
        var rule = LogicalExpressionParser.LogicalExpression.ParseOrThrow(ruleStr);
        Assert.NotNull(rule);
        Assert.Equal(customResult, rule.ToString(), true);
    }
}