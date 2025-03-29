using System;
using System.Collections.Generic;
using System.Linq;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Operator.Logical.Type;
using SimpleStateMachine.StructuralSearch.StructuralSearch;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests;

public static class LogicalExpressionParserTests
{
    [Theory]
    [InlineData("Equals $var$")]
    [InlineData("Contains $var$")]
    [InlineData("StartsWith $var$")]
    [InlineData("EndsWith $var$")]
    public static void StringCompareOperationParsingShouldBeSuccess(string input)
    {
        LogicalExpressionParser.StringCompareOperation.Before(CommonParser.Eof).ParseOrThrow(input);
    }

    public static IEnumerable<object[]> GetParameterTypeNames() 
        => Enum.GetNames<ParameterType>().Select(x => new object[] { $"Is {x}" });

    [Theory]
    [MemberData(nameof(GetParameterTypeNames))]
    public static void IsOperationParsingShouldBeSuccess(string input)
    {
        LogicalExpressionParser.IsOperation.Before(CommonParser.Eof).ParseOrThrow(input);
    }

    [Theory]
    [InlineData("Match [a-z]")]
    public static void MatchOperationParsingShouldBeSuccess(string input)
    {
        LogicalExpressionParser.MatchOperation.Before(CommonParser.Eof).ParseOrThrow(input);
    }

    [Theory]
    [InlineData("In 123, 456, 789")]
    [InlineData("In 123,456,789")]
    public static void InOperationParsingShouldBeSuccess(string input)
    {
        LogicalExpressionParser.InOperation.Before(CommonParser.Eof).ParseOrThrow(input);
    }
    
    [Theory]
    [InlineData("Not 123 Equals 789")]
    public static void NotOperationParsingShouldBeSuccess(string input)
    {
        LogicalExpressionParser.NotOperation.Before(CommonParser.Eof).ParseOrThrow(input);
    }

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
    public static void FindRuleExprParsingShouldBeSuccess(string input)
    {
        var t = TemplatesParser.Template;
        var rule = LogicalExpressionParser.LogicalExpression.Before(CommonParser.Eof).ParseOrThrow(input);
        var _ruleStr = rule.ToString()?.ToLower();
        Assert.NotNull(rule);
        Assert.Equal(input.ToLower(), _ruleStr);
    }
        
    // [Theory]
    // [InlineData("$var$ In \"Is\", \"==\", \"!=\", \"is not\"", "$var$ In \"Is\",\"==\",\"!=\",\"is not\"")]
    // [InlineData("$var$ In (\"Is\", \"==\", \"!=\", \"is not\")", "$var$ In \"Is\",\"==\",\"!=\",\"is not\"")]
    // [InlineData("$var$ In \"Is\",\"==\",\"!=\",\"is not\"", "$var$ In \"Is\",\"==\",\"!=\",\"is not\"")]
    // [InlineData("$var$ In (\"Is\",\"==\",\"!=\",\"is not\")", "$var$ In \"Is\",\"==\",\"!=\",\"is not\"")]
    // [InlineData("$var$ In(\"Is\",\"==\",\"!=\",\"is not\")", "$var$ In \"Is\",\"==\",\"!=\",\"is not\"")]
    // [InlineData("$var$ In ( \"Is\",\"==\",\"!=\",\"is not\" ) ", "$var$ In \"Is\",\"==\",\"!=\",\"is not\"")]
    // [InlineData("$var$ In ( \"Is\",\"==\",\"!=\",\"is not\" )", "$var$ In \"Is\",\"==\",\"!=\",\"is not\"")]
    // [InlineData("$var$ In ( \"Is\",\"==\",\"!=\",\"is not\") ", "$var$ In \"Is\",\"==\",\"!=\",\"is not\"")]
    // [InlineData("$var$ In (\"Is\",\"==\",\"!=\",\"is not\" ) ", "$var$ In \"Is\",\"==\",\"!=\",\"is not\"")]
    // [InlineData("$var$ In (\"Is\",\"==\",\"!=\",\"is not\" )", "$var$ In \"Is\",\"==\",\"!=\",\"is not\"")]
    // [InlineData("$var$ In (\"Is\",\"==\",\"!=\",\"is not\") ", "$var$ In \"Is\",\"==\",\"!=\",\"is not\"")]
    // [InlineData("$var$ In( \"Is\",\"==\",\"!=\",\"is not\" ) ", "$var$ In \"Is\",\"==\",\"!=\",\"is not\"")]
    // [InlineData("$var$ In( \"Is\",\"==\",\"!=\",\"is not\" )", "$var$ In \"Is\",\"==\",\"!=\",\"is not\"")]
    // [InlineData("$var$ In( \"Is\",\"==\",\"!=\",\"is not\") ", "$var$ In \"Is\",\"==\",\"!=\",\"is not\"")]
    // [InlineData("Not ($var$ equals $var$.Lenght and $var$ StartsWith \"123\")", "Not $var$ equals $var$.Lenght and $var$ StartsWith \"123\"")]
    // [InlineData("Not ($var$ equals $var$.Lenght)", "Not $var$ equals $var$.Lenght")]
    // public static void FindRuleExprParsingShouldBeEqualsCustomResult(string ruleStr, string customResult)
    // {
    //     var rule = LogicalExpressionParser.LogicalExpression.ParseOrThrow(ruleStr);
    //     var ruleAsStr = rule.ToString()?.ToLower();
    //     Assert.NotNull(rule);
    //     Assert.Equal(ruleAsStr, customResult.ToLower());
    // }
    //
    // [Theory]
    // [InlineData("FindRule/NullUnionOperator.txt", "$sign$ In \"is\",\"==\",\"!=\",\"is not\"")]
    // [InlineData("FindRule/AssignmentNullUnionOperator.txt", "$sign$ In \"is\",\"==\",\"!=\",\"is not\"")]
    // public static void FindRuleParsingFromFileShouldBeSuccess(string filePath, params string[] customResult)
    // {
    //     var ruleStr = File.ReadAllText(filePath);
    //     var rules = ruleStr.Split(Constant.LineFeed)
    //         .Select(StructuralSearch.StructuralSearch.ParseFindRule);
    //     var rulesAsStr = rules.Select(x => x.ToString()).ToArray();
    //         
    //     Assert.True(customResult.SequenceEqual(rulesAsStr));
    // }
}