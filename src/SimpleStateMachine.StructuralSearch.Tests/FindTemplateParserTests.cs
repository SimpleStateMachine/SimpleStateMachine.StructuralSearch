using System.IO;
using Pidgin;
using SimpleStateMachine.StructuralSearch.StructuralSearch;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests;

public static class FindTemplateParserTests
{
    [Theory]
    [InlineData("()")]
    [InlineData("(123)")]
    [InlineData("($var$)")]
    [InlineData("($var$)(123)")]
    [InlineData("($var$)( )")]
    [InlineData("($var$) ( )")]
    [InlineData("( $var$ )")]
    [InlineData("(123$var$)")]
    [InlineData("if ($value$ $sign$ null)")]
    public static void FindTemplateParsingShouldBeSuccess(string templateStr)
    {
        var template = FindTemplateParser.Template.Before(CommonParser.Eof).ParseOrThrow(templateStr);
    }

    [Theory]
    [InlineData("FindTemplate/NullUnionOperator.txt")]
    [InlineData("FindTemplate/AssignmentNullUnionOperator.txt")]
    [InlineData("FindTemplate/NestedParenthesised.txt")]
    [InlineData("FindTemplate/TernaryOperator.txt")]
    public static void FindTemplateFileParsingShouldBeSuccess(string templatePath)
    {
        var templateStr = File.ReadAllText(templatePath);
        var parsers = FindTemplateParser.Template.Before(CommonParser.Eof).ParseOrThrow(templateStr);
        Assert.NotEmpty(parsers);
    }
    
    // [Theory]
    // [InlineData("FindTemplate/NullUnionOperator.txt", "Source/NullUnionOperator.txt")]
    // [InlineData("FindTemplate/AssignmentNullUnionOperator.txt", "Source/AssignmentNullUnionOperator.txt")]
    // [InlineData("FindTemplate/NestedParenthesised.txt", "Source/NestedParenthesised.txt")]
    // [InlineData("FindTemplate/TernaryOperator.txt", "Source/TernaryOperator.txt")]
    // public static void SourceParsingBeFindTemplateShouldBeSuccess(string templatePath, string sourcePath)
    // {
    //     var findTemplate = File.ReadAllText(templatePath);
    //     var source = File.ReadAllText(sourcePath);
    //     var input = Input.Input.String(source);
    //     var findParser = StructuralSearch.StructuralSearch.ParseFindTemplate(findTemplate);
    //     var matches = findParser.Parse(input);
    //     Assert.Single(matches);
    //
    //     var match = matches.First();
    //
    //     Assert.NotNull(findParser);
    //     Assert.Equal(match.Match.Length, source.Length);
    // }
}