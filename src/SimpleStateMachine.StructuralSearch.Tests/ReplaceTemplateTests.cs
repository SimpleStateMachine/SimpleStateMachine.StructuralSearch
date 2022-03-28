using System.IO;
using System.Linq;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests
{
    public class ReplaceTemplateTests
    {
        [Theory]
        [InlineData("ReplaceTemplate/IfElseReplaceTemplate.txt", 14)]
        [InlineData("ReplaceTemplate/IfValueIsNullReplaceTemplate.txt", 6)]
        public void TemplateParsingShouldBeSuccess(string templatePath, int stepsCount)
        {
            var replaceTemplate = File.ReadAllText(templatePath);
            var replaceBuilder = StructuralSearch.ParseReplaceTemplate(replaceTemplate, new ParsingContext());
            var result = replaceBuilder.Build();
            
            Assert.NotNull(replaceTemplate);
            Assert.Equal(replaceBuilder.Steps.Count(), stepsCount);
        }
    }
}