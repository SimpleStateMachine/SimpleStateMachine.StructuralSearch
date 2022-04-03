using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleStateMachine.StructuralSearch.Configurations
{
    public class ConfigurationsFile: IEquatable<ConfigurationsFile>
    {
        public List<Configuration> Configurations { get; set; }

        public bool Equals(ConfigurationsFile? other)
        {
            return Configurations.SequenceEqual(other.Configurations);
        }

        public override bool Equals(object? obj)
        {
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ConfigurationsFile)obj);
        }

        public override int GetHashCode()
        {
            return Configurations.GetHashCode();
        }
    }
}