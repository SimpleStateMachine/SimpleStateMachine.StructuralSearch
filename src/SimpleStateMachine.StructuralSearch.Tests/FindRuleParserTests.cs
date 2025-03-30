using System.IO;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.StructuralSearch;
using SimpleStateMachine.StructuralSearch.Tests.Attributes;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests;

public class FindRuleParserTests
{
    [Theory]
    [FilesData("FindRule")]
    public static void FindRuleFileParsingShouldBeSuccess(string filePath)
    {
        var ruleStr = File.ReadAllText(filePath);
        LogicalExpressionParser.LogicalExpression.ParseToEnd(ruleStr);
    }
}