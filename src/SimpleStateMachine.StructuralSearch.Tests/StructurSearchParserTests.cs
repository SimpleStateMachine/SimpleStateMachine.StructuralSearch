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
        // [InlineData("NullUnionOperator")]
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
            // foreach (var match in matches)
            // {
            //     input.Replace(match.Match);
            // }
            
        }
    }
}