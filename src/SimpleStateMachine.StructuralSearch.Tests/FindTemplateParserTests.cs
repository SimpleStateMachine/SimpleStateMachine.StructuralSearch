using System.IO;
using System.Linq;
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
            var input = Input.String(source);
            var findParser = StructuralSearch.ParseFindTemplate(findTemplate);
            IParsingContext parsingContext = new ParsingContext(input);
            var matches = findParser.Parse(ref parsingContext);
            Assert.Single(matches);
            
            var match = matches.First();
            
            Assert.NotNull(findParser);
            Assert.Equal(match.Match.Lenght, source.Length);
        }
    }
}