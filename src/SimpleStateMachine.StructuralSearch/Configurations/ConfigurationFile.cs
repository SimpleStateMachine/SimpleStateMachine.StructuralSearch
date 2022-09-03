using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleStateMachine.StructuralSearch.Configurations
{
    public class ConfigurationFile: IEquatable<ConfigurationFile>
    {
        public List<Configuration> Configurations { get; init; }

        public bool Equals(ConfigurationFile? other)
        {
            return Configurations.SequenceEqual(other.Configurations);
        }

        public override bool Equals(object? obj)
        {
            return obj?.GetType() == GetType() && Equals((ConfigurationFile)obj);
        }

        public override int GetHashCode()
        {
            return Configurations.GetHashCode();
        }
    }
}