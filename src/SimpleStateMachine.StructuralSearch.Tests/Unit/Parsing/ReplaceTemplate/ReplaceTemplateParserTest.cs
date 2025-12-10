using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Parsing;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests.Unit.Parsing.ReplaceTemplate;

public static class ReplaceTemplateParserTest
{
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
    [InlineData("$var$ ??= $value$;")]
    [InlineData("$var$ = $value1$ ?? $value2$;")]
    [InlineData("return $condition$? $value1$ : $value2$;")]
    public static void ReplaceTemplateParsingShouldBeSuccess(string templateStr)
    {
        var replaceBuilder = ReplaceTemplateParser.ReplaceTemplate.ParseToEnd(templateStr);
        var replaceStr = replaceBuilder.ToString()?.ToLower();
        Assert.Equal(replaceStr, templateStr.ToLower());
    }
}