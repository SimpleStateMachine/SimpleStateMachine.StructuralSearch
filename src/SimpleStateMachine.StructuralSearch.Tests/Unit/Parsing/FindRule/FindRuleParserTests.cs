using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests.Unit.Parsing.FindRule;

public static class FindRuleParserTests
{
    [Theory]
    [InlineData("$sign$ In (\"is\", \"==\", \"!=\", \"is not\")")]
    public static void FindRuleParsingShouldBeSuccess(string ruleStr)
    {
        SimpleStateMachine.StructuralSearch.Parsing.StructuralSearch.ParseFindRule(ruleStr);
    }
}