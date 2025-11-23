using System;
using System.Collections.Generic;
using System.IO;
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
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Configurations.Equals(other.Configurations);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((ConfigurationFile)obj);
    }

    public override int GetHashCode()
    {
        return Configurations.GetHashCode();
    }
}