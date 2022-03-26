using System.Linq;
using Pidgin;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests
{
    public class ReplaceRuleParserTests
    {
        [Theory]
        [InlineData("equals $var$", "$var$")]
        [InlineData("Not equals $var$.Lenght", "$var$.Lenght")]
        [InlineData("Not equals $var$.offset.Start", "$var$.offset.Start")]
        [InlineData("equals $var$.Lenght and Not StartsWith \"123\"", "$var$.offset.Start.Trim")]
        [InlineData("equals $var$.Lenght and Not StartsWith \"\\\"Test\"", "$var$.offset.Start.ToUpper")]
        public void FindRuleParsingShouldBeSuccess(string findRule, string replaceRule)
        {
            string placeholder = "$var$";
            string replaceRuleStr = $"{placeholder} {findRule} => {replaceRule}";
            var rule = StructuralSearch.ParseReplaceRule(replaceRuleStr);
            var _ruleStr = rule.ToString()?.ToLower();
            Assert.NotNull(rule);
            Assert.Equal(_ruleStr, replaceRuleStr.ToLower());
        }
    }
}