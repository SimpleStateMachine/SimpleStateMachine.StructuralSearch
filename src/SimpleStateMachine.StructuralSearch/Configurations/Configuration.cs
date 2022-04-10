using System;
using System.Collections.Generic;
using System.Linq;
using SimpleStateMachine.StructuralSearch.Helper;

namespace SimpleStateMachine.StructuralSearch.Configurations
{
    public class Configuration : IEquatable<Configuration>
    {
        public string FindTemplate { get; set; }

        public List<string>? FindRules { get; set; }

        public string ReplaceTemplate { get; set; }

        public List<string>? ReplaceRules { get; set; }

        public bool Equals(Configuration? other)
        {
            var findTemplateEquals = FindTemplate == other.FindTemplate;
            var findRulesEquals = EnumerableHelper.SequenceNullableEqual(FindRules, other.FindRules);
            var replaceTemplateEquals = ReplaceTemplate == other.ReplaceTemplate;
            var replaceRulesEquals = EnumerableHelper.SequenceNullableEqual(ReplaceRules, other.ReplaceRules);
            return findTemplateEquals && findRulesEquals && replaceTemplateEquals && replaceRulesEquals;
        }

        public override bool Equals(object? obj)
        {
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Configuration)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FindTemplate, FindRules, ReplaceTemplate, ReplaceRules);
        }
    }
}