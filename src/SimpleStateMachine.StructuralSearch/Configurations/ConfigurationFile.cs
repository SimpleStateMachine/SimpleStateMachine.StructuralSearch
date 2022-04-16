using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleStateMachine.StructuralSearch.Configurations
{
    public class ConfigurationFile: IEquatable<ConfigurationFile>
    {
        public List<Configuration> Configurations { get; set; }

        public bool Equals(ConfigurationFile? other)
        {
            return Configurations.SequenceEqual(other.Configurations);
        }

        public override bool Equals(object? obj)
        {
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ConfigurationFile)obj);
        }

        public override int GetHashCode()
        {
            return Configurations.GetHashCode();
        }
    }
}