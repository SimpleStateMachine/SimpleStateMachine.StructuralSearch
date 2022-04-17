using System.IO;
using SimpleStateMachine.StructuralSearch.Tests.Mock;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests
{
    public class StructuralSearchParserTests
    {
        [Theory]
        // [InlineData("AssignmentNullUnionOperator")]
        // [InlineData("NullUnionOperator")]
        [InlineData("TernaryOperator", "Examples/TernaryOperator.cs")]
        public static void StructuralSearchShouldBeSuccess(string exampleName, string exampleFilePath)
        {
            var config = ConfigurationMock.GetConfigurationFromFiles(exampleName);
            var parser = new StructuralSearchParser(config);

            var fileInfo = new FileInfo(exampleFilePath);
            var input = Input.File(fileInfo);
            IParsingContext context = new ParsingContext(input);
           
            parser.Parse(ref context);
        }
    }
}