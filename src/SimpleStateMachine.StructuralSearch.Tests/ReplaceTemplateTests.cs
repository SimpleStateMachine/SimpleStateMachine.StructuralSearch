using System.IO;
using System.Linq;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Templates.ReplaceTemplate;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests;

public static class ReplaceTemplateTests
{
    [Theory]
    [InlineData("ReplaceTemplate/NullUnionOperator.txt", 6)]
    [InlineData("ReplaceTemplate/AssignmentNullUnionOperator.txt", 4)]
    [InlineData("ReplaceTemplate/TernaryOperator.txt", 7)]
    public static void ReplaceTemplateParsingShouldHaveStepCount(string templatePath, int stepsCount)
    {
        var replaceTemplate = File.ReadAllText(templatePath);
        var replaceBuilder = StructuralSearch.StructuralSearch.ParseReplaceTemplate(replaceTemplate);

        Assert.NotNull(replaceTemplate);
        Assert.Equal(((ReplaceBuilder)replaceBuilder).Steps.Count(), stepsCount);
    }

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
        var replaceBuilder = StructuralSearch.StructuralSearch.ParseReplaceTemplate(replaceTemplate);

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
        
    // TODO validation parenthesis for parameters
        
    [Theory]
    [InlineData("test $var1$.Lenght")]
    [InlineData("(test) $var1$.Lenght")]
    [InlineData("test ($var1$.Lenght)")]
    [InlineData("(test $var1$.Lenght)")]
    [InlineData("test ")]
    [InlineData("($var1$.Lenght)")]
    [InlineData(" ($var1$.Lenght)")]
    [InlineData(" ( )")]  
    [InlineData("test ( )")]
    [InlineData(" (test $var1$.Lenght)")]
    [InlineData("(test) ($var1$.Lenght)")]
    [InlineData("((test) $var1$.Lenght)")]
    [InlineData("(test ($var1$.Lenght))")]
    [InlineData("((test) ($var1$.Lenght))")]
    [InlineData("()")]
    [InlineData("(test ($var1$.Lenght) test2)")]
    public static void ReplaceTemplateParsingShouldBeSuccess(string templateStr)
    {
        var replaceBuilder = StructuralSearch.StructuralSearch.ParseReplaceTemplate(templateStr);
        var replaceStr = replaceBuilder.ToString()?.ToLower();
        Assert.Equal(replaceStr, templateStr.ToLower());
    }
        
    [Theory]
    [InlineData("(test $var1$.Lenght")]
    [InlineData("test ($var1$.Lenght")]
    [InlineData("test $var1$.Lenght)")]
    [InlineData(" ( ")]  
    [InlineData("test ( ")]
    public static void ReplaceTemplateParsingShouldBeFail(string templateStr)
    {
        Assert.Throws<ParseException<char>>(() => StructuralSearch.StructuralSearch.ParseReplaceTemplate(templateStr));
    }
}