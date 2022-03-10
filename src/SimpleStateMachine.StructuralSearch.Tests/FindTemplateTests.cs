using System.IO;
using Pidgin;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests
{
    public class FindTemplateTests
    {
        [Theory]
        [InlineData("Templates/IfElseTemplate.txt")]
        [InlineData("Templates/IfValueIsNullTemplate.txt")]
        [InlineData("Templates/NestedParenthesisedTemplate.txt")]
        public void TemplateParsingShouldBeSuccess(string templatePath)
        {
           var templateStr = File.ReadAllText(templatePath);
           var template = StructuralSearch.ParseTemplate(templateStr);
           
           Assert.NotNull(template);
        }
        
        [Theory]
        [InlineData("Templates/IfElseTemplate.txt", "Sources/IfElseSource.txt")]
        [InlineData("Templates/IfValueIsNullTemplate.txt", "Sources/IfValueIsNullSource.txt")]
        [InlineData("Templates/NestedParenthesisedTemplate.txt", "Sources/NestedParenthesisedSource.txt")]
        public void SourceParsingBeTemplateShouldBeSuccess(string templatePath, string sourcePath)
        {
            var templateStr = File.ReadAllText(templatePath);
            var sourceStr = File.ReadAllText(sourcePath);

            var template = StructuralSearch.ParseTemplate(templateStr);
            var result = template.ParseOrThrow(sourceStr);
            
            Assert.NotNull(template);
            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            Assert.Equal(result.Lenght, sourceStr.Length);
        }
    }
}