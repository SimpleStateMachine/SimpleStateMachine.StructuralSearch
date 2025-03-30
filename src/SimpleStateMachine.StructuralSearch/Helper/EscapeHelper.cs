using System;
using System.Collections.Generic;

namespace SimpleStateMachine.StructuralSearch.Helper;

internal static class EscapeHelper
{
    // public static string Escape(string str, Func<char, char> replaceRule) 
    //     => new(str.Select(replaceRule).ToArray());
    //
    // public static string EscapeChars(string str, Func<char, char> replaceRule, params char[] filter) 
    //     => new(str.Select(c => filter.Contains(c) ? replaceRule(c) : c).ToArray());
    //
    // public static string EscapeExclude(string str, Func<char, char> replaceRule, params char[] excluded) 
    //     => new(str.Select(c => excluded.Contains(c) ? c : replaceRule(c)).ToArray());
    //
    // public static string Escape(string str, Func<char, string> replaceRule) 
    //     => string.Join(string.Empty, str.Select(replaceRule));
    //
    // public static string EscapeExclude(string str, Func<char, string> replaceRule, params char[] excluded) 
    //     => string.Join(string.Empty, str.Select(c => excluded.Contains(c) ? c.ToString() : replaceRule(c)));

    internal static string Escape(ReadOnlySpan<char> input, IReadOnlySet<char> charsToEscape)
    {
        // Precalculate string size
        var extraLength = 0;
        foreach (var c in input)
        {
            if (charsToEscape.Contains(c))
                extraLength++;
        }

        if (extraLength == 0)
            return new string(input);

        Span<char> buffer = stackalloc char[input.Length + extraLength];
        var bufferIndex = 0;

        foreach (var c in input)
        {
            if (charsToEscape.Contains(c))
                buffer[bufferIndex++] = Constant.BackSlash;

            buffer[bufferIndex++] = c;
        }

        return new string(buffer);
    }
}