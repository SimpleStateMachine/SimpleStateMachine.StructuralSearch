using System.Collections.Generic;
using System.IO;
using System.Linq;
using SimpleStateMachine.StructuralSearch.Tests.Mock;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests
{
    public class ReplaceTemplateTests
    {
        [Theory]
        [InlineData("ReplaceTemplate/IfElse.txt", 14)]
        [InlineData("ReplaceTemplate/IfValueIsNull.txt", 6)]
        [InlineData("ReplaceTemplate/TernaryOperator.txt", 11)]
        public void TemplateParsingShouldHaveStepCount(string templatePath, int stepsCount)
        {
            var replaceTemplate = File.ReadAllText(templatePath);
            var replaceBuilder = StructuralSearch.ParseReplaceTemplate(replaceTemplate);
            var result = replaceBuilder.Build(new EmptyParsingContext());

            Assert.NotNull(replaceTemplate);
            Assert.Equal(replaceBuilder.Steps.Count(), stepsCount);
        }

        [Theory]
        [InlineData("ReplaceTemplate/IfElse.txt", "ReplaceResult/IfElse.txt", 
            new[] { "var1", "sign", "value1", "value2", "value3" }, 
            new[] { "temp", "==", "125", "12", "15" })]
        [InlineData("ReplaceTemplate/IfValueIsNull.txt", "ReplaceResult/IfValueIsNull.txt", 
            new[] { "var", "value"  }, 
            new[] { "temp", "12" })]
        [InlineData("ReplaceTemplate/TernaryOperator.txt", "ReplaceResult/TernaryOperator.txt", 
            new[] { "condition", "value1", "value2" }, 
            new[] { "temp == 125", "12", "15" })]
        public void ReplaceBuildShouldBeSuccess(string templatePath, string resultPath, string[] keys, string[] values)
        {
            var replaceTemplate = File.ReadAllText(templatePath);
            var replaceResult = File.ReadAllText(resultPath);
            var replaceBuilder = StructuralSearch.ParseReplaceTemplate(replaceTemplate);
            
            var parsingContext = new ParsingContext();
            for (int i = 0; i < keys.Length; i++)
            {
                parsingContext.AddPlaceholder(keys[i], values[i]);
            }
            
            var result = replaceBuilder.Build(parsingContext);

            Assert.NotNull(replaceTemplate);
            Assert.NotNull(replaceResult);
            Assert.Equal(replaceResult, result);
        }
    }
}