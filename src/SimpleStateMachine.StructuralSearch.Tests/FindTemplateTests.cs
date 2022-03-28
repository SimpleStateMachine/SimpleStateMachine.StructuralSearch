using System.IO;
using Pidgin;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests
{
    public class FindTemplateTests
    {
        [Theory]
        [InlineData("FindTemplate/IfElseTemplate.txt")]
        [InlineData("FindTemplate/IfValueIsNullTemplate.txt")]
        [InlineData("FindTemplate/NestedParenthesisedTemplate.txt")]
        public void TemplateParsingShouldBeSuccess(string templatePath)
        {
           var findTemplate = File.ReadAllText(templatePath);
           var template = StructuralSearch.ParseFindTemplate(findTemplate, new ParsingContext());
           
           Assert.NotNull(template);
        }
        
        [Theory]
        [InlineData("FindTemplate/IfElseTemplate.txt", "Source/IfElseSource.txt")]
        [InlineData("FindTemplate/IfValueIsNullTemplate.txt", "Source/IfValueIsNullSource.txt")]
        [InlineData("FindTemplate/NestedParenthesisedTemplate.txt", "Source/NestedParenthesisedSource.txt")]
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