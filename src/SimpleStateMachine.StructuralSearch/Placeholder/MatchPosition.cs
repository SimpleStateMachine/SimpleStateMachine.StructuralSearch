namespace SimpleStateMachine.StructuralSearch;

public readonly record struct Match<T>(T Value, int Lenght, ColumnPosition Column, LinePosition Line,
    OffsetPosition Offset);

internal static class Match
{
    public static readonly Match<string> EmptyMatchString = new(
        string.Empty, 
        0, 
        ColumnPosition.Empty, 
        LinePosition.Empty,
        OffsetPosition.Empty);
}