using System.IO;
using SimpleStateMachine.StructuralSearch.Tests.Mock;
using Xunit;

namespace SimpleStateMachine.StructuralSearch.Tests.Unit.Parsing.Configuration;

public static class ConfigurationFileParserTests
{
    [Theory]
    [InlineData("ShortConfig.yml")]
    [InlineData("FullConfig.yml")]
    [InlineData("if.yml")]
    public static void ConfigurationFileParsingShouldBeSuccess(string filePath)
    {
        var fileInfo = DataHelper.GetDataFileInfo(filePath);
        using var reader = new StreamReader(fileInfo.FullName);
        var cfg = ConfigurationFile.ParseYaml(reader);
    }

    private static ConfigurationFile Mock()
    {
        var names = new[] { "AssignmentNullUnionOperator", "NullUnionOperator", "TernaryOperator" };

        var configurationFile = new ConfigurationFile
        (
            []
        );

        foreach (var name in names)
        {
            var config = ConfigurationMock.GetConfigurationFromFiles(name);
            configurationFile.Configurations.Add(config);
        }

        return configurationFile;
    }
}