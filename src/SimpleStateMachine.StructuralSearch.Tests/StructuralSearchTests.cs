using System.Collections.Generic;
using System.IO;
using System.Linq;
using SimpleStateMachine.StructuralSearch.Input;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests;

public class StructuralSearchTests
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
        var results = Parsing.StructuralSearch.ParseFindTemplate(template).ParseString(inputText).ToList();
        Assert.Single(results);
        var parseResult = results.Single();

        Assert.Equal(expectedResult.Count, parseResult.Placeholders.Count);

        foreach (var (key, value) in expectedResult)
            Assert.Equal(parseResult.Placeholders[key].Value, value);
    }


    [Theory]
    [InlineData("ExamplesInput/Methods.txt")]
    public static void StructuralSearchFileParsingShouldBeSuccess(string filePath)
    {
        var configuration = new Configuration
        {
            FindTemplate = "$Modificator$ $ReturnType$ $MethodName$($params$)",
            FindRules =
            [
                "$Modificator$ in (\"public\", \"private\", \"internal\")",
                "$ReturnType$ is var",
                "$MethodName$ is var"
            ]
        };

        var parser = new StructuralSearchParser(configuration);
        var results = parser.StructuralSearch(new FileInput(new FileInfo(filePath))).ToList();
        Assert.Single(results);
    }
}