using System.IO;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Parsing;
using SimpleStateMachine.StructuralSearch.Tests.Attributes;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests;

public class FindRuleParserTests
{
    [Theory]
    [InlineData("$sign$ In (\"is\", \"==\", \"!=\", \"is not\")")]
    public static void FindRuleParsingShouldBeSuccess(string ruleStr)
    {
        LogicalExpressionParser.LogicalExpression.ParseToEnd(ruleStr);
    }
    
    [Theory]
    [FilesData("FindRule")]
    public static void FindRuleFileParsingShouldBeSuccess(string filePath)
    {
        var ruleStr = File.ReadAllText(filePath);
        LogicalExpressionParser.LogicalExpression.ParseToEnd(ruleStr);
    }
}