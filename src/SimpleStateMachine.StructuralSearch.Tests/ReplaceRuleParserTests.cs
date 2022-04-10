using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests
{
    public class ReplaceRuleParserTests
    {
        [Theory]
        [InlineData("equals $var$", "$var$")]
        [InlineData("equals \"\\$\"", "\"\\$\"")]
        [InlineData("Not equals $var$.Lenght", "$var$.Lenght")]
        [InlineData("Not equals $var$.offset.Start", "$var$.offset.Start")]
        [InlineData("equals $var$.Lenght and Not StartsWith \"123\"", "$var$.offset.Start.Trim")]
        [InlineData("equals $var$.Lenght and Not StartsWith \"\\\"Test\"", "$var$.offset.Start.ToUpper")]
        public void ReplaceRuleParsingShouldBeSuccess(string findRule, string replaceRule)
        {
            var placeholder = "$var$";
            var replaceRuleStr = $"{placeholder} {findRule} => {replaceRule}";
            var rule = StructuralSearch.StructuralSearch.ParseReplaceRule(replaceRuleStr);
            var ruleStr = rule.ToString().ToLower();
            Assert.NotNull(rule);
            Assert.Equal(ruleStr, replaceRuleStr.ToLower());
        }
    }
}