using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace SimpleStateMachine.StructuralSearch.Helper;

internal static class YmlHelper
{
    private static readonly IDeserializer Deserializer = new DeserializerBuilder()
        .WithNamingConvention(PascalCaseNamingConvention.Instance)
        .EnablePrivateConstructors()
        .Build();

    public static ConfigurationFile Parse(string filePath)
    {
        var textReader = File.OpenText(filePath);
        var cfg = Deserializer.Deserialize<ConfigurationFile>(textReader);
        return cfg;
    }
}