using System;
using System.Collections.Generic;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Operator.Logical.Type;
using SimpleStateMachine.StructuralSearch.Parsing;
using SimpleStateMachine.StructuralSearch.Tests.Attributes;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests;

public static class LogicalExpressionParserTests
{
    public static IEnumerable<string> StringCompareOperationCases()
    {
        // var parameters = ["$var$", "\"123\"", "$var$.Len"]
        foreach (var @operator in Enum.GetNames<StringCompareOperator>())
        {
            yield return $"{@operator} $var$";
            yield return $"{@operator}   $var$";
            yield return $"{@operator} \"123\"";
            yield return $"{@operator}   \"123\"";
            yield return $"{@operator} $var$";
        }
    }

    [Theory]
    [StringMemberData(nameof(StringCompareOperationCases))]
    public static void StringCompareOperationParsingShouldBeSuccess(string input)
    {
        LogicalExpressionParser.StringCompareOperation.ParseToEnd(input);
    }

    public static IEnumerable<string> IsOperationCases()
    {
        foreach (var type in Enum.GetNames<ParameterType>())
        {
            yield return $"Is {type}";
            yield return $"Is    {type}";
        }
    }

    [Theory]
    [StringMemberData(nameof(IsOperationCases))]
    public static void IsOperationParsingShouldBeSuccess(string input)
    {
        LogicalExpressionParser.IsOperation.ParseToEnd(input);
    }

    [Theory]
    [InlineData("Match \"[a-z]\"")]
    public static void MatchOperationParsingShouldBeSuccess(string input)
    {
        LogicalExpressionParser.MatchOperation.ParseToEnd(input);
    }

    [Theory]
    [InlineData("In \"123\",\"456\",\"789\"")]
    [InlineData("In   \"123\",\"456\",\"789\"")]
    [InlineData("In \"123\", \"456\", \"789\"")]
    [InlineData("In \"123\",  \"456\",  \"789\"")]
    [InlineData("In   \"123\", \"456\", \"789\"")]
    [InlineData("In   \"123\",  \"456\",  \"789\"")]
    public static void InOperationParsingShouldBeSuccess(string input)
    {
        LogicalExpressionParser.InOperation.ParseToEnd(input);
    }

    [Theory]
    [InlineData("Not \"123\" Equals \"789\"")]
    public static void NotOperationParsingShouldBeSuccess(string input)
    {
        LogicalExpressionParser.NotOperation.ParseToEnd(input);
    }

    public static IEnumerable<string> BinaryOperationCases()
    {
        foreach (var @operator in Enum.GetNames<LogicalBinaryOperator>())
        {
            yield return $"{@operator} $var$ Equals $var$";
            yield return $"{@operator}    $var$ Equals $var$";
            yield return $"{@operator} $var$ Equals    $var$";
            yield return $"{@operator}    $var$ Equals    $var$";
        }
    }

    [Theory]
    [StringMemberData(nameof(BinaryOperationCases))]
    public static void BinaryOperationParsingShouldBeSuccess(string input)
    {
        LogicalExpressionParser.BinaryOperation.ParseToEnd(input);
    }
    
    // public static IEnumerable<string> BinaryOperationCases()
    // {
    //     foreach (var access in ParametersParserTests.PropertyAccessCases())
    //     {
    //         foreach (var VARIABLE in COLLECTION)
    //         {
    //             
    //         }
    //         yield return $"{@operator} $var$ Equals $var$";
    //         yield return $"{@operator}    $var$ Equals $var$";
    //         yield return $"{@operator} $var$ Equals    $var$";
    //         yield return $"{@operator}    $var$ Equals    $var$";
    //     }
    // }

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
        var result = operation.ToString()!;
        Assert.Equal(input.ToLower(), result.ToLower());
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
    // [InlineData("Not ($var$ equals $var$.Length and $var$ StartsWith \"123\")", "Not $var$ equals $var$.Length and $var$ StartsWith \"123\"")]
    // [InlineData("Not ($var$ equals $var$.Length)", "Not $var$ equals $var$.Length")]
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