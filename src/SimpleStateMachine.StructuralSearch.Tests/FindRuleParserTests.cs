﻿using System.Linq;
using Pidgin;
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
        public void FindRuleParsingShouldBeSuccess(string ruleStr)
        {
            var rule = FindRuleParser.Expr.ParseOrThrow(ruleStr);
            var _ruleStr = rule.ToString()?.ToLower();
            Assert.NotNull(rule);
            Assert.Equal(_ruleStr, ruleStr.ToLower());
        }
    }
}