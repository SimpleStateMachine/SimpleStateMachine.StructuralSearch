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
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return FindTemplate == other.FindTemplate
               && NullableSequenceEqual(FindRules, other.FindRules)
               && ReplaceTemplate == other.ReplaceTemplate
               && NullableSequenceEqual(ReplaceRules, other.ReplaceRules);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Configuration)obj);
    }

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