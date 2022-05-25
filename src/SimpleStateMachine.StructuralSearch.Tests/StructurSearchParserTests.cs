using System.IO;
using System.Linq;
using SimpleStateMachine.StructuralSearch.Tests.Mock;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests
{
    public class StructuralSearchParserTests
    {
        [Theory]
        // [InlineData("AssignmentNullUnionOperator")]
        [InlineData("NullUnionOperator", "ExamplesInput/NullUnionOperator.cs", 2)]
        [InlineData("TernaryOperator", "ExamplesInput/TernaryOperator.cs", 3)]
        public static void StructuralSearchShouldBeSuccess(string exampleName, string exampleFilePath, int matchesCount)
        {
            var config = ConfigurationMock.GetConfigurationFromFiles(exampleName);
            var parser = new StructuralSearchParser(config);

            var fileInfo = new FileInfo(exampleFilePath);
            var input = Input.File(fileInfo);
            IParsingContext context = new ParsingContext(input);
           
            var matches = parser.Parse(ref context);
            Assert.Equal(matches.Count(), matchesCount);
        }
        
        [Theory]
        // [InlineData("AssignmentNullUnionOperator")]
        [InlineData("NullUnionOperator", 2)]
        [InlineData("TernaryOperator", 3)]
        public static void StructuralSearchShouldBe(string exampleName, int matchesCount)
        {
            var config = ConfigurationMock.GetConfigurationFromFiles(exampleName);
            var inputFilePath = Path.Combine($"ExamplesInput/{exampleName}.cs");
            var resultFilePath = Path.Combine($"ExamplesOutput/{exampleName}.txt");
            var outputFilePath = Path.Combine($"{exampleName}.cs");
            
            var parser = new StructuralSearchParser(config);

            var inputFileInfo = new FileInfo(inputFilePath);
            var input = Input.File(inputFileInfo);
            IParsingContext context = new ParsingContext(input);
            var matches = parser.Parse(ref context);
            matches = parser.ApplyFindRule(ref context, matches);
            matches = parser.ApplyReplaceRule(ref context, matches);
            var replaceMatches = parser.GetReplaceMatches(ref context, matches);
            
            var outputFileInfo = new FileInfo(outputFilePath);
            var output = Output.File(outputFileInfo);
            output.Replace(input, replaceMatches);
            Assert.Equal(matches.Count(), matchesCount);
         
            var resultStr = File.ReadAllText(resultFilePath);
            var outputStr = File.ReadAllText(outputFilePath);
            Assert.Equal(resultStr, outputStr);
        }
    }
}