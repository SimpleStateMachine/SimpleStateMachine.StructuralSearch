using System.IO;
using SimpleStateMachine.StructuralSearch.Input;
using SimpleStateMachine.StructuralSearch.Tests.Mock;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests.Integration.StructuralSearch;

public static class StructuralSearchSearchExamplesTests
{
    [Theory]
    [InlineData("NullUnionOperator", "ExamplesInput/NullUnionOperator.txt", 2)]
    [InlineData("TernaryOperator", "ExamplesInput/TernaryOperator.txt", 3)]
    public static void StructuralSearchShouldBe(string exampleName, string exampleFilePath, int matchesCount)
    {
        var config = ConfigurationMock.GetConfigurationFromFiles(exampleName);
        var parser = new StructuralSearchParser(config);

        var fileInfo = new FileInfo(exampleFilePath);
        var input = Input.Input.File(fileInfo);
        var matches = parser.StructuralSearch(input);
        Assert.Equal(matchesCount, matches.Count);
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
        var results = parser.StructuralSearch(new FileInput(new FileInfo(filePath)));
        Assert.Single(results);
    }
}