using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace SimpleStateMachine.StructuralSearch;

public class ConfigurationFile(List<Configuration> configurations) : IEquatable<ConfigurationFile>
{
    private static readonly IDeserializer Deserializer = new DeserializerBuilder()
        .WithNamingConvention(PascalCaseNamingConvention.Instance)
        .EnablePrivateConstructors()
        .Build();

    public static ConfigurationFile ParseYaml(string input)
    {
        var cfg = Deserializer.Deserialize<ConfigurationFile>(input);
        return cfg;
    }

    public static ConfigurationFile ParseYaml(TextReader input)
    {
        var cfg = Deserializer.Deserialize<ConfigurationFile>(input);
        return cfg;
    }

    // Use for deserialization
    private ConfigurationFile() : this([])
    {
    }

    public List<Configuration> Configurations { get; init; } = configurations;

    public bool Equals(ConfigurationFile? other)
        => other?.Configurations != null && Configurations.SequenceEqual(other.Configurations);

    public override bool Equals(object? obj)
        => obj?.GetType() == GetType() && Equals((ConfigurationFile)obj);

    public override int GetHashCode()
        => Configurations.GetHashCode();
}