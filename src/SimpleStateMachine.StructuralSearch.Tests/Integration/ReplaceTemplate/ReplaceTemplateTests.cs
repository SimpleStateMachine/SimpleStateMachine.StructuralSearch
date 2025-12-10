using System.IO;
using SimpleStateMachine.StructuralSearch.Context;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests.Integration.ReplaceTemplate;

public static class ReplaceTemplateTests
{
    [Theory]
    [InlineData("ReplaceTemplate/NullUnionOperator.txt", "ReplaceResult/NullUnionOperator.txt",
        new[] { "var", "sign", "value1", "value2" },
        new[] { "temp", "is", "var1", "var2" })]
    [InlineData("ReplaceTemplate/AssignmentNullUnionOperator.txt", "ReplaceResult/AssignmentNullUnionOperator.txt",
        new[] { "var", "value" },
        new[] { "temp", "12" })]
    [InlineData("ReplaceTemplate/TernaryOperator.txt", "ReplaceResult/TernaryOperator.txt",
        new[] { "condition", "value1", "value2" },
        new[] { "temp == 125", "12", "15" })]
    public static void ReplaceBuildShouldBeSuccess(string templatePath, string resultPath, string[] keys, string[] values)
    {
        var replaceTemplate = File.ReadAllText(templatePath);
        var replaceResult = File.ReadAllText(resultPath);
        var replaceBuilder = Parsing.StructuralSearch.ParseReplaceTemplate(replaceTemplate);

        IParsingContext parsingContext = new ParsingContext(Input.Input.Empty, []);
        for (var i = 0; i < keys.Length; i++) parsingContext[keys[i]] = Placeholder.Placeholder.CreateEmpty(keys[i], values[i]);

        var result = replaceBuilder.Build(ref parsingContext);

        Assert.NotNull(replaceTemplate);
        Assert.NotNull(replaceResult);
        Assert.Equal(replaceResult, result);
    }
}