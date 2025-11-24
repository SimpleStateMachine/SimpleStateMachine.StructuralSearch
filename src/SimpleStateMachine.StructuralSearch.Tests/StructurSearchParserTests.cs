using System.IO;
using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Input;
using SimpleStateMachine.StructuralSearch.Output;
using SimpleStateMachine.StructuralSearch.Tests.Mock;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests;

public static class StructuralSearchParserTests
{
    [Theory]
    [InlineData("NullUnionOperator", "ExamplesInput/NullUnionOperator.txt", 2)]
    [InlineData("TernaryOperator", "ExamplesInput/TernaryOperator.txt", 3)]
    public static void StructuralSearchShouldBeSuccess(string exampleName, string exampleFilePath, int matchesCount)
    {
        var config = ConfigurationMock.GetConfigurationFromFiles(exampleName);
        var parser = new StructuralSearchParser(config);

        var fileInfo = new FileInfo(exampleFilePath);
        var input = Input.Input.File(fileInfo);
        var matches = parser.StructuralSearch(input);
        Assert.Equal(matchesCount, matches.Count);
    }

    [Theory]
    [InlineData("NullUnionOperator", 2)]
    [InlineData("TernaryOperator", 3)]
    public static void StructuralSearchShouldBe(string exampleName, int matchesCount)
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