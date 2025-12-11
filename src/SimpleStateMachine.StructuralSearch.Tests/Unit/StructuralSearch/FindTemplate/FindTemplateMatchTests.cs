using System.Linq;
using SimpleStateMachine.StructuralSearch.Tests.Helper;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests.Unit.StructuralSearch.FindTemplate;

public static class FindTemplateMatchTests
{
    [Theory]
    [InlineData("FindTemplate/NullUnionOperator.txt", "Source/NullUnionOperator.txt")]
    [InlineData("FindTemplate/AssignmentNullUnionOperator.txt", "Source/AssignmentNullUnionOperator.txt")]
    [InlineData("FindTemplate/NestedParenthesised.txt", "Source/NestedParenthesised.txt")]
    [InlineData("FindTemplate/TernaryOperator.txt", "Source/TernaryOperator.txt")]
    public static void SourceParsingBeFindTemplateShouldBeSuccess(string templatePath, string sourcePath)
    {
        var findTemplate = DataHelper.ReadDataFileText(templatePath);
        var source = DataHelper.ReadDataFileText(sourcePath);
        var input = Input.Input.String(source);
        var findParser = SimpleStateMachine.StructuralSearch.Parsing.StructuralSearch.ParseFindTemplate(findTemplate);
        var matches = findParser.Parse(input);
        Assert.Single(matches);

        var match = matches.First();

        Assert.NotNull(findParser);
        Assert.Equal(match.Match.Length, source.Length);
    }
}