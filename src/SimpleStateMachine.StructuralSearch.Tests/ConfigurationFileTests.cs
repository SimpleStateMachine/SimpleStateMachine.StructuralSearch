using System.Collections.Generic;
using SimpleStateMachine.StructuralSearch.Configurations;
using SimpleStateMachine.StructuralSearch.Helper;
using SimpleStateMachine.StructuralSearch.Tests.Mock;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests
{
    public class ConfigurationFileParserTests
    {
        [Theory]
        [InlineData("ConfigurationFile/ShortConfig.yml")]
        [InlineData("ConfigurationFile/FullConfig.yml")]
        public void ConfigurationFileParsingShouldBeSuccess(string filePath)
        {
            var cfg = YmlHelper.Parse(filePath);
            var mock = Mock();
            Assert.Equal(mock, cfg);
        }

        private ConfigurationFile Mock()
        {
            var names = new[] { "AssignmentNullUnionOperator", "NullUnionOperator", "TernaryOperator"};
            
            var configurationFile = new ConfigurationFile
            {
                Configurations = new List<Configuration>()
            };
            
            foreach (var name in names)
            {
                var config = ConfigurationMock.GetConfigurationFromFiles(name);
                configurationFile.Configurations.Add(config);
            }
            
            return configurationFile;
        }
    }
}