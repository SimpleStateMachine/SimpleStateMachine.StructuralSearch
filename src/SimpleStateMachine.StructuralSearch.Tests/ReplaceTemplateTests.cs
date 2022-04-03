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
        [InlineData("ReplaceTemplate/IfElseReplaceTemplate.txt", 14)]
        [InlineData("ReplaceTemplate/IfValueIsNullReplaceTemplate.txt", 6)]
        [InlineData("ReplaceTemplate/TernaryOperatorReplaceTemplate.txt", 11)]
        public void TemplateParsingShouldHaveStepCount(string templatePath, int stepsCount)
        {
            var replaceTemplate = File.ReadAllText(templatePath);
            var replaceBuilder = StructuralSearch.ParseReplaceTemplate(replaceTemplate);
            var result = replaceBuilder.Build(new EmptyParsingContext());

            Assert.NotNull(replaceTemplate);
            Assert.Equal(replaceBuilder.Steps.Count(), stepsCount);
        }

        [Theory]
        [InlineData("ReplaceTemplate/IfElseReplaceTemplate.txt", "ReplaceResult/IfElseReplaceResult.txt", new[] { "var1", "sign", "value1", "value2", "value3" }, new[] { "temp", "==", "125", "12", "15" })]
        [InlineData("ReplaceTemplate/IfValueIsNullReplaceTemplate.txt", "ReplaceResult/IfValueIsNullReplaceResult.txt", new[] { "var", "value"  }, new[] { "temp", "12" })]
        [InlineData("ReplaceTemplate/TernaryOperatorReplaceTemplate.txt", "ReplaceResult/TernaryOperatorReplaceResult.txt", new[] { "condition", "value1", "value2" }, new[] { "temp == 125", "12", "15" })]
        public void ReplaceByTemplateShouldBeSuccess(string templatePath, string resultPath, string[] keys, string[] values)
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