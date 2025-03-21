using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleStateMachine.StructuralSearch.Configurations;

public class ConfigurationFile: IEquatable<ConfigurationFile>
{
    // Use for deserialization
    private ConfigurationFile()
    {
        Configurations = new List<Configuration>();
    }
        
    public ConfigurationFile(List<Configuration> configurations)
    {
        Configurations = configurations;
    }

    public List<Configuration> Configurations { get; init; }

    public bool Equals(ConfigurationFile? other) 
        => other?.Configurations != null && Configurations.SequenceEqual(other.Configurations);

    public override bool Equals(object? obj) 
        => obj?.GetType() == GetType() && Equals((ConfigurationFile)obj);

    public override int GetHashCode() 
        => Configurations.GetHashCode();
}