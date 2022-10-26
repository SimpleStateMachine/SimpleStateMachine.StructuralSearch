using System.IO;
using System.Linq;
using Pidgin;
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
        var replaceBuilder = StructuralSearch.ParseReplaceTemplate(replaceTemplate);
        IParsingContext context = ParsingContext.Empty;
        var result = replaceBuilder.Build(ref context);

        Assert.NotNull(replaceTemplate);
        Assert.Equal(replaceBuilder.Steps.Count(), stepsCount);
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
        var replaceBuilder = StructuralSearch.ParseReplaceTemplate(replaceTemplate);
            
        IParsingContext parsingContext = new ParsingContext(Input.Empty);
        for (int i = 0; i < keys.Length; i++)
        {
            parsingContext.AddPlaceholder(Placeholder.CreateEmpty(parsingContext, keys[i], values[i]));
        }
            
        var result = replaceBuilder.Build(ref parsingContext);

        Assert.NotNull(replaceTemplate);
        Assert.NotNull(replaceResult);
        Assert.Equal(replaceResult, result);
    }
        
    // TODO validation parenthesis for parameters
        
    [Theory]
    [InlineData(0, "test $var1$.Lenght")]
    [InlineData(1, "(test) $var1$.Lenght")]
    [InlineData(2, "test ($var1$.Lenght)")]
    [InlineData(3, "(test $var1$.Lenght)")]
    [InlineData(4, "test ")]
    [InlineData(5, "($var1$.Lenght)")]
    [InlineData(6, " ($var1$.Lenght)")]
    [InlineData(7, " ( )")]  
    [InlineData(8, "test ( )")]
    [InlineData(9, " (test $var1$.Lenght)")]
    [InlineData(10, "(test) ($var1$.Lenght)")]
    [InlineData(11, "((test) $var1$.Lenght)")]
    [InlineData(12, "(test ($var1$.Lenght))")]
    [InlineData(13, "((test) ($var1$.Lenght))")]
    [InlineData(14, "()")]
    [InlineData(15, "(test ($var1$.Lenght) test2)")]
    public static void ReplaceTemplateParsingShouldBeSuccess(int number, string templateStr)
    {
        var replaceBuilder = StructuralSearch.ParseReplaceTemplate(templateStr);
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
        Assert.Throws<ParseException>(() => StructuralSearch.ParseReplaceTemplate(templateStr));
    }
}