using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleStateMachine.StructuralSearch;

public class Configuration : IEquatable<Configuration>
{
    public string? FindTemplate { get; init; }
    public List<string>? FindRules { get; init; }
    public string? ReplaceTemplate { get; init; }
    public List<string>? ReplaceRules { get; init; }

    public bool Equals(Configuration? other)
    {
        var findTemplateEquals = FindTemplate == other?.FindTemplate;
        var findRulesEquals = NullableSequenceEqual(FindRules, other?.FindRules);
        var replaceTemplateEquals = ReplaceTemplate == other?.ReplaceTemplate;
        var replaceRulesEquals = NullableSequenceEqual(ReplaceRules, other?.ReplaceRules);
        return findTemplateEquals && findRulesEquals && replaceTemplateEquals && replaceRulesEquals;
    }

    public override bool Equals(object? obj) 
        => obj?.GetType() == GetType() && Equals((Configuration)obj);

    public override int GetHashCode() 
        => HashCode.Combine(FindTemplate, FindRules, ReplaceTemplate, ReplaceRules);

    private static bool NullableSequenceEqual<TSource>(IEnumerable<TSource>? first, IEnumerable<TSource>? second)
    {
        if (first is null && second is null)
            return true;
            
        if (first is null || second is null)
            return false;

        return first.SequenceEqual(second);
    }
}