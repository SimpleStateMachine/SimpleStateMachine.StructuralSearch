using System.Collections.Generic;
using SimpleStateMachine.StructuralSearch.Helper;
using SimpleStateMachine.StructuralSearch.Tests.Mock;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests;

public static class ConfigurationFileParserTests
{
    [Theory]
    [InlineData("ConfigurationFile/ShortConfig.yml")]
    [InlineData("ConfigurationFile/FullConfig.yml")]
    public static void ConfigurationFileParsingShouldBeSuccess(string filePath)
    {
        var cfg = YmlHelper.Parse(filePath);
        var mock = Mock();
        Assert.Equal(mock, cfg);
    }

    private static ConfigurationFile Mock()
    {
        var names = new[] { "AssignmentNullUnionOperator", "NullUnionOperator", "TernaryOperator"};

        var configurationFile = new ConfigurationFile
        (
            configurations: new List<Configuration>()
        );
            
        foreach (var name in names)
        {
            var config = ConfigurationMock.GetConfigurationFromFiles(name);
            configurationFile.Configurations.Add(config);
        }
            
        return configurationFile;
    }
}