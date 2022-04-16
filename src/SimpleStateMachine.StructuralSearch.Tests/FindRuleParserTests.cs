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
        [InlineData("equals $var$")]
        [InlineData("equals \"\\$\"")]
        [InlineData("Not equals $var$.Lenght")]
        [InlineData("Not equals $var$.offset.Start")]
        [InlineData("equals $var$.Lenght and Not StartsWith \"123\"")]
        [InlineData("equals $var$.Lenght and Not StartsWith \"\\\"Test\"")]
        [InlineData("Contains $var$.Lenght")]
        [InlineData("Contains \"123\"")]
        [InlineData("StartsWith $var$.Lenght")]
        [InlineData("StartsWith \"123\"")]
        [InlineData("EndsWith $var$.Lenght")]
        [InlineData("EndsWith \"123\"")]
        [InlineData("Match $var$.Lenght")]
        [InlineData("Is Int")]
        [InlineData("Is DateTime")]

        public void FindRuleExprParsingShouldBeSuccess(string ruleStr)
        {
            var rule = FindRuleParser.Expr.ParseOrThrow(ruleStr);
            var _ruleStr = rule.ToString()?.ToLower();
            Assert.NotNull(rule);
            Assert.Equal(_ruleStr, ruleStr.ToLower());
        }
        
        [Theory]
        [InlineData("In \"Is\", \"==\", \"!=\", \"is not\"", "In \"Is\",\"==\",\"!=\",\"is not\"")]
        [InlineData("In (\"Is\", \"==\", \"!=\", \"is not\")", "In \"Is\",\"==\",\"!=\",\"is not\"")]
        public void FindRuleExprParsingShouldBeEqualsCustomResult(string ruleStr, string customResult)
        {
            var rule = FindRuleParser.Expr.ParseOrThrow(ruleStr);
            var _ruleStr = rule.ToString()?.ToLower();
            Assert.NotNull(rule);
            Assert.Equal(_ruleStr, customResult.ToLower());
        }
        
        [Theory]
        [InlineData("FindRule/NullUnionOperator.txt", "$sign$ In \"Is\",\"==\",\"!=\",\"is not\"")]
        [InlineData("FindRule/AssignmentNullUnionOperator.txt", "$sign$ In \"Is\",\"==\",\"!=\",\"is not\"")]
        public void FindRuleParsingFromFileShouldBeSuccess(string filePath, string customResult)
        {
            var ruleStr = File.ReadAllText(filePath);
            var rule = StructuralSearch.ParseFindRule(ruleStr);
            var _ruleStr = rule.ToString()?.ToLower();
            Assert.NotNull(rule);
            Assert.Equal(_ruleStr, customResult.ToLower());
        }
    }
}