using Pidgin;

namespace SimpleStateMachine.StructuralSearch;

internal static class ParsingConfiguration
{
    public static readonly Parser<char, string> Comment = EmptyParser.AlwaysNotCorrectString;
    // public static Action<Parser<>> OnDebug;
}