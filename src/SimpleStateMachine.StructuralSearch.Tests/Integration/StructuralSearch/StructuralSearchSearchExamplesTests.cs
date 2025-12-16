using System.IO;
using SimpleStateMachine.StructuralSearch.Input;
using SimpleStateMachine.StructuralSearch.Tests.Helper;
using SimpleStateMachine.StructuralSearch.Tests.Unit.Parsing.Configuration;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests.Integration.StructuralSearch;

public static class StructuralSearchSearchExamplesTests
{
    [Theory]
    [InlineData("NullUnionOperator", 2)]
    [InlineData("TernaryOperator", 3)]
    public static void StructuralSearchShouldBe(string exampleName, int matchesCount)
    {
        var config = ConfigurationMock.GetConfigurationFromFiles(exampleName);
        var parser = new StructuralSearchParser(config);
        var fileInfo = DataHelper.GetDataFileInfo(Path.Combine("ExamplesInput", $"{exampleName}.txt"));
        var input = Input.Input.File(fileInfo);
        var matches = parser.StructuralSearch(input);
        Assert.Equal(matchesCount, matches.Count);
    }

    [Theory]
    [InlineData("Methods")]
    public static void StructuralSearchFileParsingShouldBeSuccess(string exampleName)
    {
        var fileInfo = DataHelper.GetDataFileInfo(Path.Combine("ExamplesInput", $"{exampleName}.txt"));
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
        var results = parser.StructuralSearch(new FileInput(fileInfo));
        Assert.Single(results);
    }
}