using System.IO;
using SimpleStateMachine.StructuralSearch.Context;
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
        var config = ConfigurationMock.GetConfigurationFromFiles(exampleName);
        var directory = Directory.GetCurrentDirectory();
        var inputFilePath = Path.Combine(directory, $"ExamplesInput/{exampleName}.txt");
        var expectedResultFilePath = Path.Combine(directory, $"ExamplesOutput/{exampleName}.txt");
        var resultFilePath = Path.GetTempFileName();
        var parser = new StructuralSearchParser(config);

        var input = new FileInput(new FileInfo(inputFilePath));
        IParsingContext context = new ParsingContext(input, []);
        var matches = parser.StructuralSearch(input);
        var results = parser.Replace(input, matches);
        var output = new FileOutput(new FileInfo(resultFilePath));
        output.Replace(input, results);

        Assert.Equal(matches.Count, matchesCount);
        var expectedResultStr = File.ReadAllText(expectedResultFilePath);
        var resultStr = File.ReadAllText(resultFilePath);
        Assert.Equal(expectedResultStr, resultStr);
    }
}