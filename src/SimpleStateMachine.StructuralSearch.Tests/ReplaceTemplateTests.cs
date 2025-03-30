using System.IO;
using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Parsing;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests;

public static class ReplaceTemplateTests
{
    [Theory]
    [InlineData("ReplaceTemplate/NullUnionOperator.txt", "ReplaceResult/NullUnionOperator.txt", 
        new[] { "var", "sign", "value1", "value2"}, 
        new[] { "temp", "is", "var1", "var2"})]
    [InlineData("ReplaceTemplate/AssignmentNullUnionOperator.txt", "ReplaceResult/AssignmentNullUnionOperator.txt", 
        new[] { "var", "value"  }, 
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
        for (int i = 0; i < keys.Length; i++)
        {
            parsingContext[keys[i]] = Placeholder.Placeholder.CreateEmpty(keys[i], values[i]);
        }

        var result = replaceBuilder.Build(ref parsingContext);

        Assert.NotNull(replaceTemplate);
        Assert.NotNull(replaceResult);
        Assert.Equal(replaceResult, result);
    }

    [Theory]
    [InlineData("test")]
    [InlineData("()")]
    [InlineData("$var1$")]
    [InlineData("test () test")]  
    [InlineData("test $var1$ test")]
    [InlineData("($var1$)")]
    [InlineData("($var1$ test)")]
    [InlineData("test (test)")]
    [InlineData("test ($var1$)")]
    [InlineData("test ($var1$) test")]

    [InlineData("test $var1$.Length")]
    [InlineData("(test) $var1$.Length")]
    [InlineData("test ($var1$.Length)")]
    [InlineData("(test $var1$.Length)")]
    [InlineData("($var1$.Length)")]
    [InlineData(" ($var1$.Length)")]
    [InlineData(" (test $var1$.Length)")]
    [InlineData("(test) ($var1$.Length)")]
    [InlineData("((test) $var1$.Length)")]
    [InlineData("(test ($var1$.Length))")]
    [InlineData("((test) ($var1$.Length))")]
    [InlineData("(test ($var1$.Length) test2)")]
    public static void ReplaceTemplateParsingShouldBeSuccess(string templateStr)
    {
        var replaceBuilder = ReplaceTemplateParser.ReplaceTemplate.ParseToEnd(templateStr);
        var replaceStr = replaceBuilder.ToString()?.ToLower();
        Assert.Equal(replaceStr, templateStr.ToLower());
    }
}