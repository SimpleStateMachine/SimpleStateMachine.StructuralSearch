using System;
using System.Collections.Generic;
using SimpleStateMachine.StructuralSearch.Helper;

namespace SimpleStateMachine.StructuralSearch.Configurations
{
    public class Configuration : IEquatable<Configuration>
    {
        public string? FindTemplate { get; init; }
        public List<string>? FindRules { get; init; }
        public string? ReplaceTemplate { get; init; }
        public List<string>? ReplaceRules { get; init; }

        public bool Equals(Configuration? other)
        {
            var findTemplateEquals = FindTemplate == other?.FindTemplate;
            var findRulesEquals = EnumerableHelper.SequenceNullableEqual(FindRules, other?.FindRules);
            var replaceTemplateEquals = ReplaceTemplate == other?.ReplaceTemplate;
            var replaceRulesEquals = EnumerableHelper.SequenceNullableEqual(ReplaceRules, other?.ReplaceRules);
            return findTemplateEquals && findRulesEquals && replaceTemplateEquals && replaceRulesEquals;
        }

        public override bool Equals(object? obj)
        {
            return obj?.GetType() == GetType() && Equals((Configuration)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FindTemplate, FindRules, ReplaceTemplate, ReplaceRules);
        }
    }
}