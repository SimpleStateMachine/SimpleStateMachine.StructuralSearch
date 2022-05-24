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
        [InlineData("NullUnionOperator", "Examples/NullUnionOperator.cs", 2)]
        [InlineData("TernaryOperator", "Examples/TernaryOperator.cs", 3)]
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
        [InlineData("NullUnionOperator", "Examples/NullUnionOperator.cs", 2)]
        [InlineData("TernaryOperator", "Examples/TernaryOperator.cs", 3)]
        public static void StructuralSearchShouldBe(string exampleName, string exampleFilePath, int matchesCount)
        {
            var config = ConfigurationMock.GetConfigurationFromFiles(exampleName);
            var parser = new StructuralSearchParser(config);

            var fileInfo = new FileInfo(exampleFilePath);
            var input = Input.File(fileInfo);
            IParsingContext context = new ParsingContext(input);
            var matches = parser.Parse(ref context);
            matches = parser.ApplyFindRule(ref context, matches);
            matches = parser.ApplyReplaceRule(ref context, matches);
            parser.Replace(ref context, matches);
            
            Assert.Equal(matches.Count(), matchesCount);
        }
    }
}