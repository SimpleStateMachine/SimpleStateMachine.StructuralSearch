using System;
using System.Collections.Generic;
using SimpleStateMachine.StructuralSearch.Helper;

namespace SimpleStateMachine.StructuralSearch.Configurations
{
    public class Configuration : IEquatable<Configuration>
    {
        public Configuration(string findTemplate, IEnumerable<string> findRules, string replaceTemplate, IEnumerable<string> replaceRules)
        {
            FindTemplate = findTemplate;
            FindRules = findRules;
            ReplaceTemplate = replaceTemplate;
            ReplaceRules = replaceRules;
        }

        public string FindTemplate { get; init; }

        public IEnumerable<string> FindRules { get; init; }

        public string ReplaceTemplate { get; init; }

        public IEnumerable<string> ReplaceRules { get; init; }

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