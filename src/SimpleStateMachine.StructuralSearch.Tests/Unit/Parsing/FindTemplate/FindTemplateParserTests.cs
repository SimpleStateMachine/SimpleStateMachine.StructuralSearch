using System.IO;
using System.Runtime.CompilerServices;
using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Parsing;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests.Unit.Parsing.FindTemplate;

public static class FindTemplateParserTests
{
    private static string GetDataFilePath(string fileName, [CallerFilePath] string? testFilePath = null)
        => Path.Combine(Path.GetDirectoryName(testFilePath!)!, "Data", fileName);

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
        FindTemplateParser.Template.ParseToEnd(templateStr);
    }

    [Theory]
    [InlineData("AssignmentNullUnionOperator.txt")]
    [InlineData("NestedParenthesised.txt")]
    [InlineData("NullUnionOperator.txt")]
    [InlineData("TernaryOperator.txt")]
    public static void FindTemplateFileParsingShouldBeSuccess(string fileName)
    {
        var path = GetDataFilePath(fileName);
        var str = File.ReadAllText(path);
        var parsers = FindTemplateParser.Template.ParseToEnd(str);
        Assert.NotEmpty(parsers);
    }
}