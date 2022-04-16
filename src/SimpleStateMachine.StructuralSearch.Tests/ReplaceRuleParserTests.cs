using System.Linq;
using Pidgin;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests
{
    public class ReplaceRuleParserTests
    {
        [Theory]
        [InlineData("$var$", "equals $var$", "$var$")]
        [InlineData("$var$", "equals \"\\$\"", "\"\\$\"")]
        [InlineData("$var$", "Not equals $var$.Lenght", "$var$.Lenght")]
        [InlineData("$var$", "Not equals $var$.offset.Start", "$var$.offset.Start")]
        [InlineData("$var$", "equals $var$.Lenght and Not StartsWith \"123\"", "$var$.offset.Start.Trim")]
        [InlineData("$var$", "equals $var$.Lenght and Not StartsWith \"\\\"Test\"", "$var$.offset.Start.ToUpper")]
        public void ReplaceRulePartsParsingShouldBeSuccess(string placeholder, string findRule, string replaceRule)
        {
            var replaceRuleStr = $"{placeholder} {findRule} => {replaceRule}";
            var rule = StructuralSearch.ParseReplaceRule(replaceRuleStr);
            var ruleStr = rule.ToString().ToLower();
            Assert.NotNull(rule);
            Assert.Equal(ruleStr, replaceRuleStr.ToLower());
        }
        
        [Theory]
        [InlineData("$var1$ equals $var$ => $var2$")]
        [InlineData("$var1$ equals \"\\$\" => \"\\$\"")]
        [InlineData("$var1$ Not equals $var$.Lenght => $var$.Lenght")]
        [InlineData("$var1$ Not equals $var$.offset.Start => $var$.offset.Start")]
        [InlineData("$var1$ equals $var$.Lenght and Not StartsWith \"123\" => $var$.offset.Start.Trim")]
        [InlineData("$var1$ equals $var$.Lenght and Not StartsWith \"\\\"Test\" => $var$.offset.Start.ToUpper")]
        public void ReplaceRuleParsingShouldBeSuccess(string replaceRule)
        {
            var rule = StructuralSearch.ParseReplaceRule(replaceRule);
            var ruleStr = rule.ToString().ToLower();
            Assert.NotNull(rule);
            Assert.Equal(ruleStr, replaceRule.ToLower());
        }
    }
}