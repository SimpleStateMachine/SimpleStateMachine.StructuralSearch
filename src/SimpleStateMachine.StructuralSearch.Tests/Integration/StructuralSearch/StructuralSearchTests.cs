using System.IO;
using SimpleStateMachine.StructuralSearch.Input;
using SimpleStateMachine.StructuralSearch.Output;
using SimpleStateMachine.StructuralSearch.Tests.Mock;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests.Integration.StructuralSearch;

public static class StructuralSearchTests
{
    [Theory]
    [InlineData("NullUnionOperator", 2)]
    [InlineData("TernaryOperator", 3)]
    public static void StructuralSearchAndReplaceShouldBe(string exampleName, int matchesCount)
    {
        var inputFilePath = DataHelper.GetDataFileInfo(Path.Combine("ExamplesInput", $"{exampleName}.txt"));
        var expectedResultFilePath = DataHelper.GetDataFileInfo(Path.Combine("ExamplesOutput", $"{exampleName}.txt"));
        var config = ConfigurationMock.GetConfigurationFromFiles(exampleName);

        var resultFilePath = Path.GetTempFileName();
        var parser = new StructuralSearchParser(config);

        var input = new FileInput(inputFilePath);
        var matches = parser.StructuralSearch(input);
        var results = parser.Replace(input, matches);
        var output = new FileOutput(new FileInfo(resultFilePath));
        output.Replace(input, results);

        Assert.Equal(matches.Count, matchesCount);
        var resultStr = File.ReadAllText(inputFilePath.FullName);
        Assert.Equal(resultStr, resultStr);
    }
}