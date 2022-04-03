using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Configurations;
using Xunit;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace SimpleStateMachine.StructuralSearch.Tests
{
    public class ConfigurationFileTests
    {
        [Theory]
        [InlineData("ConfigurationFile/ShortConfig.yml")]
        [InlineData("ConfigurationFile/FullConfig.yml")]
        public void ConfigurationFileParsingShouldBeSuccess(string filePath)
        {
            var yml = File.ReadAllText(filePath);
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(PascalCaseNamingConvention.Instance) 
                .Build();
            
            var cfg = deserializer.Deserialize<ConfigurationsFile>(yml);
        }

        // private ConfigurationsFile Mock()
        // {
        //     var ifElseFindTemplate = "FindTemplate/IfElse.txt";
        //     var ifValueIsNullFindTemplate = "FindTemplate/IfValueIsNull.txt";
        //     var ternaryOperatorFindTemplate = "FindTemplate/TernaryOperator.txt";
        //     
        //     var configurationFile = new ConfigurationsFile();
        //     
        // }
    }
}