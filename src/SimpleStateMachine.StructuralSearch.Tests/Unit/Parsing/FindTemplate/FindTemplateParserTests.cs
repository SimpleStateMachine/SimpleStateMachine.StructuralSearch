using SimpleStateMachine.StructuralSearch.Extensions;
using SimpleStateMachine.StructuralSearch.Parsing;
using SimpleStateMachine.StructuralSearch.Tests.Helper;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests.Unit.Parsing.FindTemplate;

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
        FindTemplateParser.Template.ParseToEnd(templateStr);
    }

    [Theory]
    [InlineData("AssignmentNullUnionOperator.txt")]
    [InlineData("NestedParenthesised.txt")]
    [InlineData("NullUnionOperator.txt")]
    [InlineData("TernaryOperator.txt")]
    public static void FindTemplateFileParsingShouldBeSuccess(string fileName)
    {
        var str = DataHelper.ReadDataFileText(fileName);
        var parsers = FindTemplateParser.Template.ParseToEnd(str);
        Assert.NotEmpty(parsers);
    }
}