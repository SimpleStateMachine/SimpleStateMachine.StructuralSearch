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
            
            var cfg = deserializer.Deserialize<ConfigurationFile>(yml);
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
                var fileName = $"{name}.txt";
                var findTemplate = FileOrNull("FindTemplate", fileName);
                var fileRule = FileOrNull("FindRule", fileName) ;
                var replaceTemplate = FileOrNull("ReplaceTemplate", fileName);
                var replaceRule = FileOrNull("ReplaceRule", fileName);

                var fileRules = fileRule is null ? null : new List<string>{ fileRule };
                var replaceRules = replaceRule is null ? null : new List<string>{ replaceRule };
                var config = new Configuration
                {
                    FindTemplate = findTemplate,
                    FindRules = fileRules,
                    ReplaceTemplate = replaceTemplate,
                    ReplaceRules = replaceRules
                };

                configurationFile.Configurations.Add(config);
            }
            
            string? FileOrNull(string folder, string name)
            {
                var path = Path.Combine(folder, name);
                if (!File.Exists(path))
                    return null;

                var file = File.ReadAllText(path);
                return file.Replace("\r\n", "\n");
            }

            return configurationFile;
        }
    }
}