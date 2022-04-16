using System.IO;
using SimpleStateMachine.StructuralSearch.Configurations;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace SimpleStateMachine.StructuralSearch.Helper
{
    public static class YmlHelper
    {
        public static ConfigurationFile Parse(string filePath)
        {
            var yml = File.ReadAllText(filePath);
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(PascalCaseNamingConvention.Instance)
                .Build();
            
            var cfg = deserializer.Deserialize<ConfigurationFile>(yml);
            return cfg;
        }
    }
}