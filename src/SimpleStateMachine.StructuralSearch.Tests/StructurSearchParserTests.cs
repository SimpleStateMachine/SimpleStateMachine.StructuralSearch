using SimpleStateMachine.StructuralSearch.Tests.Mock;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests
{
    public class StructuralSearchParserTests
    {
        [Theory]
        [InlineData("AssignmentNullUnionOperator")]
        [InlineData("NullUnionOperator")]
        [InlineData("TernaryOperator")]
        public static void StructuralSearchShouldBeSuccess(string exampleName)
        {
            var config = ConfigurationMock.GetConfigurationFromFiles(exampleName);
            var parser = new StructuralSearchParser(config);
            
        }
    }
}