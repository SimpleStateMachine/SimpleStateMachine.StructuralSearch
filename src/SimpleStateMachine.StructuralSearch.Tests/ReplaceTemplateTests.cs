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
        [InlineData("ReplaceTemplate/NullUnionOperator.txt", 10)]
        [InlineData("ReplaceTemplate/AssignmentNullUnionOperator.txt", 6)]
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
        [InlineData("ReplaceTemplate/NullUnionOperator.txt", "ReplaceResult/NullUnionOperator.txt", 
            new[] { "var", "sign", "value1", "value2"}, 
            new[] { "temp", "is", "var1", "var2"})]
        [InlineData("ReplaceTemplate/AssignmentNullUnionOperator.txt", "ReplaceResult/AssignmentNullUnionOperator.txt", 
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
            
            var parsingContext = new ParsingContext(Input.Empty);
            for (int i = 0; i < keys.Length; i++)
            {
                parsingContext.AddPlaceholder(Placeholder.CreateEmpty(parsingContext, keys[i], values[i]));
            }
            
            var result = replaceBuilder.Build(parsingContext);

            Assert.NotNull(replaceTemplate);
            Assert.NotNull(replaceResult);
            Assert.Equal(replaceResult, result);
        }
    }
}