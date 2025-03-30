using System.IO;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.StructuralSearch;
using SimpleStateMachine.StructuralSearch.Tests.Attributes;
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
        var template = FindTemplateParser.Template.ParseToEnd(templateStr);
    }

    [Theory]
    [FilesData("FindTemplate")]
    public static void FindTemplateFileParsingShouldBeSuccess(string filePath)
    {
        var str = File.ReadAllText(filePath);
        var parsers = FindTemplateParser.Template.ParseToEnd(str);
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