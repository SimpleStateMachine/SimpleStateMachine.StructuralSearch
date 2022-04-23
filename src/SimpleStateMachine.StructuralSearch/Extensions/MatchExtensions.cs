namespace SimpleStateMachine.StructuralSearch.Extensions;

public static class MatchExtensions
{
    public static bool IsEmpty(this Match<string> match)
    {
        return string.IsNullOrEmpty(match.Value);
    }
}