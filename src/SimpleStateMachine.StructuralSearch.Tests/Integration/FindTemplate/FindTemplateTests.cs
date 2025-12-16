using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests.Integration.FindTemplate;

public static class FindTemplateTests
{
    public static IEnumerable<object[]> TestCases
        => new List<object[]>
        {
            new object[]
            {
                // Input text
                "void MyMethodName(int value1, double value2)",
                // Template
                "void $methodName$($params$)",
                // Result placeholders
                new Dictionary<string, string>
                {
                    { "methodName", "MyMethodName" },
                    { "params", "int value1, double value2" }
                }
            }
        };

    [Theory]
    [MemberData(nameof(TestCases))]
    public static void StructuralSearchShouldBeSuccess(string inputText, string template, Dictionary<string, string> expectedResult)
    {
        var results = Parsing.StructuralSearch.ParseFindTemplate(template).ParseString(inputText);
        Assert.Single(results);
        var parseResult = results.Single();

        Assert.Equal(expectedResult.Count, parseResult.Placeholders.Count);

        foreach (var (key, value) in expectedResult)
            Assert.Equal(parseResult.Placeholders[key].Value, value);
    }

    [Theory]
    [InlineData("NullUnionOperator")]
    [InlineData("AssignmentNullUnionOperator")]
    [InlineData("NestedParenthesised")]
    [InlineData("TernaryOperator")]
    public static void SourceParsingBeFindTemplateShouldBeSuccess(string exampleName)
    {
        var findTemplate = DataHelper.ReadDataFileText(Path.Combine("FindTemplate", $"{exampleName}.txt"));
        var source = DataHelper.ReadDataFileText(Path.Combine("Source", $"{exampleName}.txt"));
        var input = Input.Input.String(source);
        var findParser = Parsing.StructuralSearch.ParseFindTemplate(findTemplate);
        var matches = findParser.Parse(input);
        Assert.Single(matches);
        var match = matches.First();
        Assert.NotNull(findParser);
        Assert.Equal(match.Match.Length, source.Length);
    }
}