using System;
using System.Collections.Generic;

namespace SimpleStateMachine.StructuralSearch.Helper;

internal static class EscapeHelper
{
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