using System.IO;
using Pidgin;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests
{
    public class FindTemplateTests
    {
        [Theory]
        [InlineData("FindTemplate/NullUnionOperator.txt")]
        [InlineData("FindTemplate/AssignmentNullUnionOperator.txt")]
        [InlineData("FindTemplate/NestedParenthesised.txt")]
        [InlineData("FindTemplate/TernaryOperator.txt")]
        public void TemplateParsingShouldBeSuccess(string templatePath)
        {
           var findTemplate = File.ReadAllText(templatePath);
           var template = StructuralSearch.ParseFindTemplate(findTemplate);
           
           Assert.NotNull(template);
        }
        
        [Theory]
        [InlineData("FindTemplate/NullUnionOperator.txt", "Source/NullUnionOperator.txt")]
        [InlineData("FindTemplate/AssignmentNullUnionOperator.txt", "Source/AssignmentNullUnionOperator.txt")]
        [InlineData("FindTemplate/NestedParenthesised.txt", "Source/NestedParenthesised.txt")]
        [InlineData("FindTemplate/TernaryOperator.txt", "Source/TernaryOperator.txt")]
        public void SourceParsingBeFindTemplateShouldBeSuccess(string templatePath, string sourcePath)
        {
            var findTemplate = File.ReadAllText(templatePath);
            var source = File.ReadAllText(sourcePath);
            var findParser = StructuralSearch.ParseFindTemplate(findTemplate);
            var parsingContext = new ParsingContext();
            var result = findParser.Parse(parsingContext, source);
            
            Assert.NotNull(findParser);
            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            Assert.Equal(result.Lenght, source.Length);
        }
    }
}