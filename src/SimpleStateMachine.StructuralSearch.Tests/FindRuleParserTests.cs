using System;
using System.IO;
using System.Linq;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Rules;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests
{
    public class FindRuleParserTests
    {
        [Theory]
        [InlineData("$var$ equals $var$")]
        [InlineData("$var$ equals \"\\$\"")]
        [InlineData("Not $var$ equals $var$.Lenght")]
        [InlineData("Not $var$ equals $var$.offset.Start")]
        [InlineData("$var$ equals $var$.Lenght and Not $var$ StartsWith \"123\"")]
        [InlineData("Not $var$ equals $var$.Lenght and $var$ StartsWith \"123\"")]
        [InlineData("$var$ equals $var$.Lenght and Not $var$ StartsWith \"\\\"Test\"")]
        [InlineData("$var$ Contains $var$.Lenght")]
        [InlineData("$var1$.Lenght Contains $var2$.Lenght")]
        [InlineData("$var$ Contains \"123\"")]
        [InlineData("$var$ StartsWith $var$.Lenght")]
        [InlineData("$var$.Lenght Equals $var$.Lenght")]
        [InlineData("$var$ StartsWith \"123\"")]
        [InlineData("$var$ EndsWith $var$.Lenght")]
        [InlineData("$var$ EndsWith \"123\"")]
        [InlineData("$var$ Match $var$.Lenght")]
        [InlineData("$var$ Is Int")]
        [InlineData("$var$ Is DateTime")]

        public void FindRuleExprParsingShouldBeSuccess(string ruleStr)
        {
            var rule = FindRuleParser.Expr.ParseOrThrow(ruleStr);
            var _ruleStr = rule.ToString()?.ToLower();
            Assert.NotNull(rule);
            Assert.Equal(_ruleStr, ruleStr.ToLower());
        }
        
        [Theory]
        [InlineData("$var$ In \"Is\", \"==\", \"!=\", \"is not\"", "$var$ In \"Is\",\"==\",\"!=\",\"is not\"")]
        [InlineData("$var$ In (\"Is\", \"==\", \"!=\", \"is not\")", "$var$ In \"Is\",\"==\",\"!=\",\"is not\"")]
        [InlineData("Not ($var$ equals $var$.Lenght and $var$ StartsWith \"123\")", "Not $var$ equals $var$.Lenght and $var$ StartsWith \"123\"")]
        [InlineData("Not ($var$ equals $var$.Lenght)", "Not $var$ equals $var$.Lenght")]
        public void FindRuleExprParsingShouldBeEqualsCustomResult(string ruleStr, string customResult)
        {
            var rule = FindRuleParser.Expr.ParseOrThrow(ruleStr);
            var _ruleStr = rule.ToString()?.ToLower();
            Assert.NotNull(rule);
            Assert.Equal(_ruleStr, customResult.ToLower());
        }
        [Theory]
        [InlineData("FindRule/NullUnionOperator.txt", "$sign$ In \"Is\",\"==\",\"!=\",\"is not\"",  "$value$ In $value1$,\"$value1$.Value\",$value2$,\"$value2$.Value\"")]
        [InlineData("FindRule/AssignmentNullUnionOperator.txt", "$sign$ In \"Is\",\"==\",\"!=\",\"is not\"")]
        public void FindRuleParsingFromFileShouldBeSuccess(string filePath, params string[] customResult)
        {
            var ruleStr = File.ReadAllText(filePath);
            var rules = ruleStr.Split(Constant.LineFeed)
                .Select(StructuralSearch.ParseFindRule);
            var rulesAsStr = rules.Select(x => x.ToString()).ToArray();
            
            Assert.True(customResult.SequenceEqual(rulesAsStr));
        }
    }
}