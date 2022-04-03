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
        [InlineData("ReplaceTemplate/TernaryOperatorReplaceTemplate.txt", 11)]
        public void TemplateParsingShouldHaveStepCount(string templatePath, int stepsCount)
        {
            var replaceTemplate = File.ReadAllText(templatePath);
            var replaceBuilder = StructuralSearch.ParseReplaceTemplate(replaceTemplate, new ParsingContext());
            var result = replaceBuilder.Build();
            
            Assert.NotNull(replaceTemplate);
            Assert.Equal(replaceBuilder.Steps.Count(), stepsCount);
        }
        
        // [Theory]
        // [InlineData("Source/IfElseSource.txt", "ReplaceTemplate/IfElseReplaceTemplate.txt")]
        // public void ReplaceByTemplateShouldBeSuccess(string sourcePath, string templatePath)
        // {
        //     var replaceTemplate = File.ReadAllText(templatePath);
        //     var replaceBuilder = StructuralSearch.ParseReplaceTemplate(replaceTemplate, new ParsingContext());
        //     var result = replaceBuilder.Build();
        //     
        //     Assert.NotNull(replaceTemplate);
        //     Assert.Equal(replaceBuilder.Steps.Count(), stepsCount);
        // }
    }
}