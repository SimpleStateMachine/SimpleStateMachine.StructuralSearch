using System.IO;
using Pidgin;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests
{
    public class FindTemplateTests
    {
        [Theory]
        [InlineData("FindTemplate/IfElseFindTemplate.txt")]
        [InlineData("FindTemplate/IfValueIsNullFindTemplate.txt")]
        [InlineData("FindTemplate/NestedParenthesisedFindTemplate.txt")]
        [InlineData("FindTemplate/TernaryOperatorFindTemplate.txt")]
        public void TemplateParsingShouldBeSuccess(string templatePath)
        {
           var findTemplate = File.ReadAllText(templatePath);
           var template = StructuralSearch.ParseFindTemplate(findTemplate, new ParsingContext());
           
           Assert.NotNull(template);
        }
        
        [Theory]
        [InlineData("FindTemplate/IfElseFindTemplate.txt", "Source/IfElseSource.txt")]
        [InlineData("FindTemplate/IfValueIsNullFindTemplate.txt", "Source/IfValueIsNullSource.txt")]
        [InlineData("FindTemplate/NestedParenthesisedFindTemplate.txt", "Source/NestedParenthesisedSource.txt")]
        [InlineData("FindTemplate/TernaryOperatorFindTemplate.txt", "Source/TernaryOperatorSource.txt")]
        public void SourceParsingBeFindTemplateShouldBeSuccess(string templatePath, string sourcePath)
        {
            var findTemplate = File.ReadAllText(templatePath);
            var source = File.ReadAllText(sourcePath);

            var context = new ParsingContext();
            var template = StructuralSearch.ParseFindTemplate(findTemplate, context);
            var result = template.ParseOrThrow(source);
            
            Assert.NotNull(template);
            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            Assert.Equal(result.Lenght, source.Length);
        }
    }
}